using System.Collections.Generic;

namespace Dimol.Carteras.bcp
{
    public class Cliente
    {
        public List<Dimol.dto.Autocomplete> ListarRutCliente(string numero)
        {
            return dao.Cliente.ListarRutCliente(numero);
        }

        public List<Dimol.dto.Autocomplete> ListarNombreCliente(string nombre)
        {
            return dao.Cliente.ListarNombreCliente(nombre);
        }

        public List<Dimol.dto.Autocomplete> ListarRutNombreCliente(string nombre)
        {
            return dao.Cliente.ListarRutNombreCliente(nombre);
        }

        public List<Dimol.dto.Autocomplete> ListarRutNombreClienteFiltradoEsPrevisional(string nombre, bool esPrevisional = false)
        {
            return dao.Cliente.ListarRutNombreClienteFiltradoEsPrevisional(nombre, esPrevisional);
        }

        public List<Dimol.dto.Autocomplete> BuscarRutNombreClienteTipoCliente(string nombre, int codemp)
        {
            return dao.Cliente.BuscarRutNombreClienteTipoCliente(nombre, codemp);
        }

        public static bool VerificarEsClientePrevisional(int codemp, int IdCliente, string RutCliente)
        {
            return dao.Cliente.VerificarEsClientePrevisional(codemp, IdCliente, RutCliente);
        }
    }
}
