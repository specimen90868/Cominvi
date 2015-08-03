using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Core
{
    public class EntityList<EntityClass>
    {
        public List<EntityClass> datos { get; set; }
    }
}
