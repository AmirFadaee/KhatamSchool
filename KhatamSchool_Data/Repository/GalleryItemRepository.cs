using KhatamSchool_Application.Interfaces;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository
{
    public class GalleryItemRepository : GenericRepositoryAsync<GalleryItem> ,IGalleryItemRepository
    {
        private readonly DbSet<GalleryItem> _dbSet;
        public GalleryItemRepository(KhatamSchool_DbContext dbContext) : base(dbContext) => _dbSet = dbContext.Set<GalleryItem>();  
    }
}
