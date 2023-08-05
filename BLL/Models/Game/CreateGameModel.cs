using BLL.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Game
{
    public class CreateGameModel : IMapTo<DAL.Models.Game>
    {
        public string Name { get; set; }
        public string Genre { get; set; }
    }
}
