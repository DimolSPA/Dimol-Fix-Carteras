using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Dimol.dto
{
    public class UserSession
    {
        public int Idioma { get; set; }
        public string User { get; set; }
        public string Nombre { get; set; }
        public string Rut { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoSucursal { get; set; }
        public int UserId { get; set; }
        public int PrfId { get; set; }
        public int Permisos { get; set; }
        public int EmplId { get; set; }
        public int PclId { get; set; }
        public string IpRed { get; set; }
        public string IpPc { get; set; }
        public string NombreEmpresa { set; get; }
        public string Menu { set; get; }
        public string Cargo { get; set; }
        public int CodPais { set; get; }
        public int Ctcid { set; get; }
        public string UrlFoto { set; get; }
        public int ClienteAsociado { get; set; }
        public string  EstadosClienteAsociado { get; set; }
        public string RutClienteAsociado { get; set; }
        public string NombreClienteAsociado { get; set; }
        public int Gestor { set;get;}
        public string Domain { get; set; }
        public string Email { get; set; }

        public UserSession()
        {
            this.Idioma = Int16.Parse( ConfigurationManager.AppSettings["Idioma"]);
            this.Cargo = "Cargo";
            this.CodPais = Int16.Parse(ConfigurationManager.AppSettings["Pais"]);
        }

        public UserSession(string user, string nombre, int codemp, int sucid, int userId, int prfId,int permisos, int emplId, 
            int pclid, string ipRed, string ipPC, string nombreEmpresa, string menu, string cargo)
        {
            this.Idioma = Int16.Parse(ConfigurationManager.AppSettings["Idioma"]);
            this.User = user;
            this.Nombre = nombre;
            this.CodigoEmpresa = codemp;
            this.CodigoSucursal = sucid;
            this.UserId = userId;
            this.PrfId = prfId;
            this.Permisos = permisos;
            this.EmplId = emplId;
            this.PclId = pclid;
            this.IpRed = ipRed;
            this.IpPc = ipPC;
            this.NombreEmpresa = nombreEmpresa;
            this.Menu = menu;
            this.Cargo = cargo;
        }
    }
}
