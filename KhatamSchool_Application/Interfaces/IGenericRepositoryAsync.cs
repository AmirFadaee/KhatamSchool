using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = new());
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = new());
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = new());
        void Add(T entity);
        Task<int> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = new());
        Task<T> AddBasicAsync(T entity, CancellationToken cancellationToken = new());
        Task UpdateAsync(T entity, CancellationToken cancellationToken = new());
        Task<int> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = new());
        Task DeleteAsync(T entity, CancellationToken cancellationToken = new());
        Task SoftDeleteAsync(T entity, string deletedBy = "سیستمی", CancellationToken cancellationToken = new());
        Task<int> SoftDeleteAsync(IEnumerable<T> entities, string deletedBy = "سیستمی", CancellationToken cancellationToken = new());
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        /// <summary>
        /// نشان دادن لیست جدول ها بر اساس اسم یک ستون
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        List<string> ShowTablesWhichHaveSpecificColumn(string columnName);
        /// <summary>
        /// نشان دادن اطلاعات ستون ها بر اساس اسم جدول
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //List<ColumnInformation> ShowColumnInformationBaseOnTableName(string tableName);

    }
}