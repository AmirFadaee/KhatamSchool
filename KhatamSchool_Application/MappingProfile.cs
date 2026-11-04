using AutoMapper;
using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.ViewModels;
using KhatamSchool_Application.ViewModels.Home;
using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddNewsCommand, News>()
                .ForMember(x => x.CoverImage, opt => opt.Ignore())
                .ForMember(x => x.Slug, opt => opt.Ignore())
                .ForMember(x => x.ViewCount, opt => opt.Ignore());
            CreateMap<News, HomeNewsViewModel>();
            CreateMap<News, HomeNewsViewModel>().ReverseMap();

            CreateMap<AddGradeCommand, Grade>();
            CreateMap<Grade, AddGradeCommand>().ReverseMap();

            CreateMap<AddGalleryItemCommand, GalleryItem>();
            CreateMap<GalleryItem, AddGalleryItemCommand>().ReverseMap();

            CreateMap<AddGalleryAlbumCommand, GalleryAlbum>();
            CreateMap<GalleryAlbum, AddGalleryAlbumCommand>().ReverseMap();

            CreateMap<AddCounselingTicketCommand, CounselingTicket>();
            CreateMap<CounselingTicket, AddCounselingTicketCommand>().ReverseMap();

            CreateMap<AddExecutiveStaffCommand, ExecutiveStaff>();
            CreateMap<ExecutiveStaff, AddExecutiveStaffCommand>().ReverseMap();

            CreateMap<AddTeacherCommand, Teacher>()
                .ForMember(x => x.ProfileImagePath, opt => opt.Ignore());
            CreateMap<Teacher, TeacherListItemViewModel>();

            CreateMap<AddAchievementCategoryCommand, AchievementCategory>();
            CreateMap<AchievementCategory, AddAchievementCategoryCommand>().ReverseMap();

            CreateMap<AddAchievementItemCommand, AchievementItem>()
                .ForMember(x => x.ImagePath, opt => opt.Ignore());
            CreateMap<AchievementItem, AddAchievementItemCommand>().ReverseMap();
        }
    }
}
