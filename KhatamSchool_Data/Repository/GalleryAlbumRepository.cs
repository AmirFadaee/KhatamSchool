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
    public class GalleryAlbumRepository : GenericRepositoryAsync<GalleryAlbum>, IGalleryAlbumRepository
    {
        private readonly DbSet<GalleryAlbum> _dbSet;
        public GalleryAlbumRepository(KhatamSchool_DbContext dbContext) : base(dbContext) => _dbSet = dbContext.Set<GalleryAlbum>();
    }
}
