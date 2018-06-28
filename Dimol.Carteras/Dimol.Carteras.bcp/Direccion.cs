using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class Direccion
    {
        public List<Combobox> ListarPais()
        {
            return dao.Direccion.ListarPais();
        }

        public List<Combobox> ListarRegion(int pais)
        {
            return dao.Direccion.ListarRegion(pais);
        }

        public List<Combobox> ListarCiudad(int region)
        {
            return dao.Direccion.ListarCiudad(region);
        }

        public List<Combobox> ListarComuna(int ciudad)
        {
            return dao.Direccion.ListarComuna(ciudad);
        }

        public List<Combobox> ListarEstado(int idioma)
        {
            return dao.Direccion.ListarEstado(idioma);
        }

        public static int BuscarComuna(string nombre)
        {
            return dao.Direccion.BuscarComuna(nombre);
        }
    }
}
