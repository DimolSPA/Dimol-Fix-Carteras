using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dao;
using Dimol.dto;

namespace Dimol.bcp
{
    public class Utilidades
    {
        #region " Propiedades "

        public int Empresa { get; set; }
        public int Sucursal { get; set; }
        public int Usuario { get; set; }
        public string IpRed { get; set; }
        public string IpMaquina { get; set; }
        public Funciones func = new Funciones();
        public dao.Utilidades objUtil;

        #endregion

        public bool Revisiones()
        {
            objUtil = new dao.Utilidades(Empresa, Sucursal, Usuario, IpRed, IpMaquina);
            return objUtil.Revisiones();
        }

        private bool RevCompromisos()
        {
            return objUtil.RevCompromisos();
        }

        private bool Crea_SupCamp()
        {
            return objUtil.CreaSupCamp();
        }

        private bool Revisa_Aplicaciones_Estados()
        {
            return objUtil.RevisaAplicacionesEstados();
        }

        private bool Revisa_Fechas_Judiciales_Rol()
        {
            return true;// objUtil.RevisaFechasJudicialesRol();
        }

        public  int InsertarHistorialCartera(int codemp, int suc, int pclid, int ctcid, int ccbid, int estid, string monto, string saldo, int idUsuario, string comentario)
        {
            objUtil = new dao.Utilidades(Empresa, Sucursal, Usuario, IpRed, IpMaquina);
            return objUtil.InsertarHistorialCartera(codemp,  suc,  pclid,  ctcid,  ccbid,  estid,  monto,  saldo,  idUsuario, comentario);
        }
    }
}
