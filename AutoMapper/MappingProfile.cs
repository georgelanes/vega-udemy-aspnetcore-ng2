using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Vega.Core.Models;
using Vega.Controllers.Resources;

namespace Vega.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            
            CreateMap(typeof(QueryResult<>),typeof(QueryResultResource<>));

            CreateMap<Make,MakeResource>();
            CreateMap<MakeResource,Make>();
            
            CreateMap<Make,KeyValuePairResource>();
            CreateMap<KeyValuePairResource,Make>();

            CreateMap<Feature,KeyValuePairResource>();
            CreateMap<KeyValuePairResource,Feature>();

            CreateMap<Model,KeyValuePairResource>();
            CreateMap<KeyValuePairResource,Model>();
            
            //GET
            CreateMap<Vehicle,VehicleResource>()
                .ForMember(vm=>vm.Make, opt=>opt.MapFrom(v=>v.Model.Make))
                .ForMember(vm=>vm.Contact, opt=>opt.MapFrom(v=> new 
                    ContactResource{
                        Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone
                    }))
                .ForMember(vm=>vm.Features, opt=>opt.MapFrom(f=> f.Features.Select(vf=> new KeyValuePairResource{ 
                    Id = vf.Feature.Id,
                    Name = vf.Feature.name
                 } )));
            //GET
            CreateMap<Vehicle,SaveVehicleResource>()
                .ForMember(vm=>vm.Contact, opt=>opt.MapFrom(v=> new 
                    ContactResource{
                        Name = v.ContactName, Email = v.ContactName, Phone = v.ContactPhone
                    }))
                .ForMember(vm=>vm.Features, opt=>opt.MapFrom(f=> f.Features.Select(vf=> vf.FeatureId)));
            
            //PUT/POST
            CreateMap<SaveVehicleResource,Vehicle>()
                .ForMember(v=>v.Id, opt=>opt.Ignore())
                .ForMember(v=>v.ContactName, opt=>opt.MapFrom(vr=>vr.Contact.Name))
                .ForMember(v=>v.ContactEmail, opt=>opt.MapFrom(vr=>vr.Contact.Email))
                .ForMember(v=>v.ContactPhone, opt=>opt.MapFrom(vr=>vr.Contact.Phone))
                .ForMember(v=>v.Features, opt=>opt.Ignore())
                .AfterMap((vr,v) => {
                    //remove unselected features
                    var removedFeatures = v.Features.Where(f=> !vr.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in removedFeatures)
                    {
                        v.Features.Remove(f);
                    }

                    

                    //add new features
                    var addedFeatures = vr.Features.Where(id => !v.Features.Any(f=>f.FeatureId == id)).Select(id=>new VehicleFeature{FeatureId = id}).ToList();
                    foreach (var f in addedFeatures)
                    {
                        v.Features.Add(f);
                    }

                });

                CreateMap<VehicleQueryResource, VehicleQuery>();

        }
    }
}