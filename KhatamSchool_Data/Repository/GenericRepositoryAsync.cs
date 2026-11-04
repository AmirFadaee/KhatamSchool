using BuildingManagment_Domain.Models;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace KhatamSchool_Data.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly KhatamSchool_DbContext _dbContext;
        public DbSet<T> Entities { get; set; }


        public GenericRepositoryAsync(KhatamSchool_DbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = dbContext.Set<T>();
        }

        public virtual async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = new())
        {
            return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
        }

        public virtual IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public virtual async Task<IAsyncEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return (IAsyncEnumerable<T>)await _dbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = new())
        {
            var dto = entity as AuditableBaseEntity;

            if (dto != null)
            {
                if (dto.Created == null)
                    dto.Created = DateTime.Now;

                if (dto.CreatedBy == null)
                    dto.CreatedBy = "سیستمی";
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        public async Task<T> AddBasicAsync(T entity, CancellationToken cancellationToken = new())
        {
            var dto = entity as BaseEntity;

            if (dto != null)
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = new())
        {
            var dto = entity as AuditableBaseEntity;
            if (dto != null)
            {
                dto.LastModified = DateTime.Now;
                if (dto.LastModifiedBy == null)
                    dto.LastModifiedBy = "سیستمی";
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            if (dto == null)
            {
                var baseEntity = entity as BaseEntity;
                if (baseEntity != null)
                {
                    _dbContext.Entry(entity).State = EntityState.Modified;
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteAsync(T entity, string deletedBy = "سیستمی", CancellationToken cancellationToken = new())
        {
            var dto = entity as AuditableBaseEntity;
            if (dto != null)
            {
                dto.Deleted = DateTime.Now;
                if (dto.DeletedBy == null)
                    dto.DeletedBy = deletedBy;
                dto.IsDeleted = true;

                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SoftDeleteAsync(IEnumerable<T> entities, string deletedBy = "سیستمی", CancellationToken cancellationToken = new())
        {
            foreach (var entity in entities)
            {
                var dto = entity as AuditableBaseEntity;
                if (dto != null)
                {
                    dto.Deleted = DateTime.Now;
                    if (dto.DeletedBy == null)
                        dto.DeletedBy = deletedBy;
                    dto.IsDeleted = true;

                    _dbContext.Entry(entity).State = EntityState.Modified;
                }
            }
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = new())
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = new())
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync(cancellationToken);
        }

        public DataTable ToDataTable(List<T> items)
        {

            try
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// نشان دادن لیست جدول ها بر اساس اسم یک ستون
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public virtual List<string> ShowTablesWhichHaveSpecificColumn(string columnName)
        {
            var connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            using var sqlConnection = new SqlConnection(connectionString);
            using var sqlCommand = new SqlCommand("", sqlConnection);

            sqlConnection.Open();

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "GetTableNamesWhichHasSpecificColumnName";

            var sqlParameterForColumnName = sqlCommand.Parameters.Add("@columnName", SqlDbType.NVarChar);
            sqlParameterForColumnName.Value = columnName;


            var dataTable = new DataTable();
            dataTable.Load(sqlCommand.ExecuteReader());

            var result = (from DataRow row in dataTable.Rows select Convert.ToString(row["TableName"])).ToList();

            return result;

        }


        /// <summary>
        /// نشان دادن اطلاعات ستون ها بر اساس اسم جدول
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<ColumnInformation> ShowColumnInformationBaseOnTableName(string tableName)
        {
            var connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

            using var sqlConnection = new SqlConnection(connectionString);
            using var sqlCommand = new SqlCommand("", sqlConnection);

            sqlConnection.Open();

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "GetColumnInformationBaseOnTableName";

            var sqlParameterForTableName = sqlCommand.Parameters.Add("@tableName", SqlDbType.NVarChar);
            sqlParameterForTableName.Value = tableName;


            var dataTable = new DataTable();
            dataTable.Load(sqlCommand.ExecuteReader());

            var result = (from DataRow row in dataTable.Rows
                          select new ColumnInformation
                          {
                              Name = Convert.ToString(row["ColumnName"]),
                              DataType = Convert.ToString(row["DataType"])
                          }).ToList();

            return result;
        }

        /// <summary>
        /// بروزرسانی لیست از موجودیت
        /// </summary>
        /// <returns>عدد الصفوف التي تم تغييرها ممدی ☺</returns>
        public async Task<int> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = new())
        {
            foreach (var entity in entities)
            {
                Entities.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// افزودن لیست موجودیت
        /// </summary>
        /// <returns>عدد الصفوف التي تم جمع ها ممدی ☺</returns>
        public async Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = new())
        {
            foreach (var entity in entities)
            {
                var dto = entity as AuditableBaseEntity;

                if (dto != null)
                {
                    dto.Created = DateTime.Now;
                    if (dto.CreatedBy == null)
                        dto.CreatedBy = "سیستمی";
                    await _dbContext.Set<T>().AddAsync(entity);
                }
            }
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Add(T entity)
        {
            var dto = entity as AuditableBaseEntity;

            if (dto != null)
            {
                dto.Created = DateTime.Now;
                if (dto.CreatedBy == null)
                    dto.CreatedBy = "سیستمی";
                _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();
            }
        }


    }
}
