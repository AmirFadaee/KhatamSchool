using KhatamSchool_Application.Interfaces;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace KhatamSchool_Data.Repository
{
    public class CounselingTicketRepository : GenericRepositoryAsync<CounselingTicket>, ICounselingTicketRepository
    {
        private readonly DbSet<CounselingTicket> _dbSet;
        public CounselingTicketRepository(KhatamSchool_DbContext dbContext) : base(dbContext) => _dbSet = dbContext.Set<CounselingTicket>();

    }
}
