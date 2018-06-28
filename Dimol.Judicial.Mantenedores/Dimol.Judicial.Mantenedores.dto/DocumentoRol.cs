using System;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class DocumentoRol
    {
        public int Ccbid { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }

        //Datos Previsionales
        public string Resolucion { get; set; }
        public DateTime FechaResolucion { get; set; }
        //Monto
        //Saldo
    }

    public class DocumentoEstampe
    {
        public int Codemp { get; set; }
        public int Pclid { get; set; }
        public int Ctcid { get; set; }
        public int Rolid { get; set; }
        public int Ddeid { get; set; }
        public string FechaJudicial { get; set; }
        public string Nombre { get; set; }
        public string NombreInsumo { get; set; }
        public string Usuario { get; set; }
    }
}
