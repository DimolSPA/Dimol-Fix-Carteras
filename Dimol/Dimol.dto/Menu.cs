using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.dto
{
    public class Menu
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string Tooltip { get; set; }

        public List<Menu> Hijo { set; get; }
    }
}
