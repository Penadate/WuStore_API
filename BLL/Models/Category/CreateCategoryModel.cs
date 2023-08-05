using BLL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Category
{
    public class CreateCategoryModel : IMapTo<DAL.Models.Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
    }
}
