using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class DatosCarga
    {
        public string RutCliente { get; set; }
        public string Rut { get; set; }
        public string Dv { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Comuna { get; set; }
        public int ccbid  { get; set; }
        public int ctcid { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Telefono3 { get; set; }
        public string Telefono4 { get; set; }
        public string Telefono5 { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Celular3 { get; set; }
        public string Celular4 { get; set; }
        public string Celular5 { get; set; }
        public string Fax { get; set; }
        public string Mail1 { get; set; }
        public string Mail2 { get; set; }
        public string Mail3 { get; set; }
        public string TipoDocumento { get; set; }
        public string Numero { get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string MotivoCobranza { get; set; }
        public string CodigoCarga { get; set; }
        public string Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal MontoAsignado { get; set; }
        public decimal Capital { get; set; }
        public decimal Saldo { get; set; }
        public decimal GastoJudicial { get; set; }
        public decimal GastoPreJudicial { get; set; }
        public string Banco { get; set; }
        public string RutGirador { get; set; }
        public string NombreGirador { get; set; }
        public string Negocio { get; set; }
        public string NumeroAgrupar { get; set; }
        public string RutAsegurado { get; set; }
        public string NombreAsegurado { get; set; }
        public string Originales { get; set; }
        public string Antecedentes { get; set; }
        public string Comentario { get; set; }
        public string Filler { get; set; }

        public bool Error { set; get; }
        public bool Existe { set; get; }

        public string NombreCompleto()
        {
            return this.Nombre.Trim() + " " + this.Paterno.Trim() + " " + this.Materno.Trim();
        }

        public string RutTercero { get; set; }
        public string NombreTercero { get; set; }

        public string IdCuenta { get; set; }
        public string DescripcionCuenta { get; set; }
        // Carga Previsional
        public string NumeroResolucion { get; set; }
        public DateTime FechaResolucion { get; set; }
        public string RutRepresentante1 { get; set; }
        public string NombreRepresentante1 { get; set; }
        public string RutRepresentante2 { get; set; }
        public string NombreRepresentante2 { get; set; }
        public string RutRepresentante3 { get; set; }
        public string NombreRepresentante3 { get; set; }
    }

    public class CargaJudicial: DatosCarga
    {
        public string RutTribunal { get; set; }
        public string TipoCausa { get; set; }
        public string RutAbogado { get; set; }
        public string RutProcurador { get; set; }
        public DateTime FechaAvenimiento { get; set; }
        public decimal MontoAvenimiento { get; set; }
        public int NumeroCuotasAvenimiento { get; set; }
        public decimal MontoCuotasAvenimiento { get; set; }
        public DateTime FechaPrimeraCuotaAvenimiento { get; set; }
        public decimal InteresAvenimiento { get; set; }
        public DateTime FechaDemanda { get; set; }
        public decimal MontoDemanda { get; set; }
        public int NumeroCuotasDemanda { get; set; }
        public decimal MontoCuotasDemanda { get; set; }
        public decimal MontoUltimaCuotaDemanda { get; set; }
        public DateTime FechaPrimeraCuotaDemanda { get; set; }
        public DateTime FechaUltimaCuotaDemanda { get; set; }
        public decimal InteresDemanda { get; set; }
        public int IdTribunal { get; set; }
        public int IdTipoCausa { get; set; }
        public int IdAbogado { get; set; }
        public int IdProcurador { get; set; }
    }

    public class DatosCargaPago
    {
        public string RutCliente { get; set; }
        public string Rut { get; set; }
        public string Dv { get; set; }
        public string NumeroCpbt { get; set; }
        public decimal Monto { get; set; }
        public decimal Interes { get; set; }
        public decimal Honorario { get; set; }
        public string Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        public int Ctcid { get; set; }
        public int Ccbid { get; set; }
    }
}
