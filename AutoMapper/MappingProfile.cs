using AutoMapper;
using Vega.Models;
using Vega.ViewModels;

namespace Vega.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){

            CreateMap<Make,MakeViewModel>();
            CreateMap<MakeViewModel,Make>();

            CreateMap<Feature,FeatureViewModel>();
            CreateMap<FeatureViewModel,Feature>();

            CreateMap<Model,ModelViewModel>();
            CreateMap<ModelViewModel,Model>();

        }
    }
}