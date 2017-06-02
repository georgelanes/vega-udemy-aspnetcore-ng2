using System;
using System.Collections.Generic;
using System.Linq;
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

            CreateMap<Vehicle,VehicleViewModel>()
                .ForMember(vm=>vm.Contact, opt=>opt.MapFrom(v=> new 
                    ContactViewModel{
                        Name = v.ContactName, Email = v.ContactName, Phone = v.ContactPhone
                    }))
                .ForMember(vm=>vm.Features, opt=>opt.MapFrom(f=> f.Features.Select(vf=> vf.FeatureId)));
                
            CreateMap<VehicleViewModel,Vehicle>()
                .ForMember(v=>v.Id, opt=>opt.Ignore())
                .ForMember(v=>v.ContactName, opt=>opt.MapFrom(vr=>vr.Contact.Name))
                .ForMember(v=>v.ContactEmail, opt=>opt.MapFrom(vr=>vr.Contact.Email))
                .ForMember(v=>v.ContactPhone, opt=>opt.MapFrom(vr=>vr.Contact.Phone))
                .ForMember(v=>v.Features, opt=>opt.Ignore())
                .AfterMap((vm,v) => {
                    //remove unselected features
                    var removedFeatures = v.Features.Where(f=> !vm.Features.Contains(f.FeatureId));
                    foreach (var f in removedFeatures)
                    {
                        v.Features.Remove(f);
                    }

                    //add new features
                    var addedFeatures = vm.Features.Where(id => !v.Features.Any(f=>f.FeatureId == id)).Select(id=>new VehicleFeature{FeatureId = id});
                    foreach (var f in addedFeatures)
                    {
                        v.Features.Add(f);
                    }

                });

        }
    }
}