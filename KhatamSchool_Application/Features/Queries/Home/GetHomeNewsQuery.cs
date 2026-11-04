using AutoMapper;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels.Home;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Queries.Home
{
    public class GetHomeNewsQuery : IRequest<List<HomeNewsViewModel>>
    {

    }
    public class GetHomeNewsQueryHandler : IRequestHandler<GetHomeNewsQuery, List<HomeNewsViewModel>>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IMapper _mapper;
        public GetHomeNewsQueryHandler(INewsRepository newsRepository, IMapper mapper)
        {
            _mapper = mapper;
            _newsRepository = newsRepository;   
        }
        public async Task<List<HomeNewsViewModel>> Handle(GetHomeNewsQuery request, CancellationToken cancellationToken)
        {
            var news=await _newsRepository.GetHomeNewsAsync(x=>x.IsPublished, cancellationToken);
            return _mapper.Map<List<HomeNewsViewModel>>(news);  
        }
    }
}
