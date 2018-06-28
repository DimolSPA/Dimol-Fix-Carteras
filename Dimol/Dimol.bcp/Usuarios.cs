using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dao;
using System.Data;
using System.Configuration;

namespace Dimol.bcp
{
    public class Usuarios:dto.Usuarios
    {
        private dao.Usuarios objUsuario = new dao.Usuarios(1); // 1 por idioma español

        public string Encripta(string password)
        {
            return objUsuario.Encripta(password);
        }

        public string Desencripta(string psw_encriptada)
        {
            return objUsuario.Desencripta(psw_encriptada) ;
        }

        public string ValidaUsuario()
        {
            return objUsuario.ValidaUsuario(this.Usuario,this.Password);
        }

        private bool Comprobar(string usuario)
        {
            return objUsuario.Comprobar(usuario);
        }

        private string Comprobar(string usuario, string password)
        {
            return objUsuario.Comprobar(usuario,password);
        }

        public DataSet Trae_DatUsuario(string usuario)
        {
            return objUsuario.Trae_DatUsuario(usuario);
        }

        public List<string> TraeClienteAsociadoUsuario(int codemp, int usrid)
        {
            return objUsuario.TraeClienteAsociadoUsuario(codemp, usrid);
        }


    }
}
