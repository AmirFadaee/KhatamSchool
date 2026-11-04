using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces
{
    public interface INewsRepository : IGenericRepositoryAsync<News>
    {
        public Task<List<News>> GetHomeNewsAsync(Expression<Func<News,bool>> predicate,CancellationToken cancellationToken);
    }
}
