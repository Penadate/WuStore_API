using AutoMapper;
using BLL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Category
{
    public class CategoryModel : IMapFrom<DAL.Models.Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameName { get; set; }

        public void MapFrom(Profile profile)
        {
            profile.CreateMap<DAL.Models.Category, CategoryModel>()
                .ForMember(dest => dest.GameName, src => src.MapFrom(otp => otp.Game!.Name));
        }
    }
}
