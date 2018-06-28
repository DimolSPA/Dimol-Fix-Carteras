using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class ClasificacionInsumo
    {
        public string Exento { get; set; }
        public int TipoStock { get; set; }
        public string Arancel { get; set; }
        public decimal PorcentajeArancel { get; set; }
        public string ImputableCliente { get; set; }
       
    }
}
