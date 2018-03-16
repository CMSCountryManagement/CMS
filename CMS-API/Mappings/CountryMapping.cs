using AutoMapper;
using CMS_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Db = CMS_Entity.Db;

namespace CMS_API.Mappings
{
    public class CountryMapping
    {
        public static void Configure()
        {
            //Mapper.CreateMap<Db.Country, Country>();
            Mapper.CreateMap<Country, Db.Country>()
                .ForMember(
                    gd => gd.Id,
                    opt => opt.MapFrom(
                            dg => Guid.NewGuid()
                        )
                )
                .ForMember(
                    gd => gd.Latlng,
                    opt => opt.MapFrom(
                            dg => string.Join(",", dg.latlng)
                        )
                )
                .ForMember(
                    gd => gd.TopLevelDomain,
                    opt => opt.MapFrom(
                            dg => string.Join(",", dg.topLevelDomain)
                        )
                )
                .ForMember(
                    gd => gd.CallingCodes,
                    opt => opt.MapFrom(
                            dg => string.Join(",", dg.callingCodes)
                        )
                )
                .ForMember(
                    gd => gd.AltSpellings,
                    opt => opt.MapFrom(
                            dg => string.Join(",", dg.altSpellings)
                        )
                )
                .ForMember(
                    gd => gd.Timezones,
                    opt => opt.MapFrom(
                            dg => string.Join(",", dg.timezones)
                        )
                )
                .ForMember(
                    gd => gd.Borders,
                    opt => opt.MapFrom(
                            dg => string.Join(",", dg.borders)
                        )
                );
        }
    }
}
