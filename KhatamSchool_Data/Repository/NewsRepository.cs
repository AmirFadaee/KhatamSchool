using KhatamSchool_Application.Interfaces;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository
{
    public class NewsRepository : GenericRepositoryAsync<News>, INewsRepository
    {
        private readonly DbSet<News> _newsSet;
        public NewsRepository(KhatamSchool_DbContext dbContext) : base(dbContext) => _newsSet = dbContext.Set<News>();

        public async Task<List<News>> GetHomeNewsAsync(Expression<Func<News, bool>> predicate, CancellationToken cancellationToken)
        {
           return await _newsSet.Where(predicate).OrderByDescending(x => x.IsFeatured).ThenByDescending(x => x.PublishedAt).Take(10).ToListAsync(cancellationToken);
        }
    }
}
