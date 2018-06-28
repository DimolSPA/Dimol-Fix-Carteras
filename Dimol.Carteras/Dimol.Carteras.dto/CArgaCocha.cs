using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class CargaCocha
    {
        public int Ctcid { get; set; }
        public int CodigoCarga { get; set; }
        public string OrigenCobranza { get; set; }
        public string SubTipoDeuda1 { get; set; }
        public string SubTipoDeuda2 { get; set; }
        public int IdDeuda { get; set; }
        public string IdDeudaX { get; set; }
        public int IdDeudaAux { get; set; }
        public string AntiguedadDeuda { get; set; }
        public string Cobrador { get; set; }
        public int CodCCO { get; set; }
        public string CodVen { get; set; }
        public string Comentario { get; set; }
        public int CtdPaxNeg  { get; set; }
        public int  Cuit { get; set; }
        public int DiasAntiguedad { get; set; }
        public string DV { get; set; }
        public string EjecCtasComer { get; set; }
        public string Empresa { get; set; }
        public DateTime FecUltLlamada { get; set; }
        public DateTime FechaDoc { get; set; }
        public DateTime FecEmi { get; set; }
        public DateTime FecHoy { get; set; }
        public DateTime FecUltSolRetPago  { get; set; }
        public DateTime FecVen { get; set; }
        public string Frec { get; set; }
        public string GlosaFactura { get; set; }
        public string Holding { get; set; }
        public int Inacti { get; set; }
        public string IndDescFee { get; set; }
        public string IndTarjetaTesorero { get; set; }
        public string Merc { get; set; }
        public string Moneda { get; set; }
        public string Negocio { get; set; }
        public string Nemote { get; set; }
        public string NomAnalista { get; set; }
        public string NombreDeudor { get; set; }
        public string NombrePax1 { get; set; }
        public string NomCCO { get; set; }
        public string NomVen { get; set; }
        public string NumFact { get; set; }
        public int NumRut { get; set; }
        public int OpTelCob { get; set; }
        public int PlazoPago { get; set; }
        public decimal PromPago { get; set; }
        public string RazonSocial { get; set; }
        public decimal SaldoCLP { get; set; }
        public decimal SaldoUSD { get; set; }
        public string StatusDev { get; set; }
        public decimal TasaCambio { get; set; }
        public string TipCli { get; set; }
        public string TipoCobranza { get; set; }
        public string TipoFactura { get; set; }
        public DateTime UltLlamada { get; set; }
        public string UsuMensaje { get; set; }
        public string NacInt { get; set; }
        public string Proveedor { get; set; }
        public string TipoMov { get; set; }
        public string Inbduc { get; set; }
        public long Limcre { get; set; }
        public int LimCreUSD { get; set; }
        public int CodFacturador { get; set; }
        public bool Error { set; get; }
        public bool Existe { set; get; }
    }
}
