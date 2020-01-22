using AutoMapper;
using ProductAPİ.Data.Entites;
using ProductAPİ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPİ.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            this.CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
