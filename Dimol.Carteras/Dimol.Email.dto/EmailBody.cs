using System;
using System.Collections.Generic;
using System.Linq;

namespace Dimol.Email.dto
{
    public class EmailBody
    {
        public int Ctcid { get; set; }
        public int Pclid { get; set; }
        public string NombreCliente { get; set; }
        public string NombreEmpresa { get; set; }
        public string NombreDeudor { get; set; }
        public int TipoCartera { get; set; }
        public string Rut { get; set; }
        public int Contactos { get; set; }
        public string  MensajePersonalizado { get; set; }

        public List<Documento> ListaDocumentos = new List<Documento>();
        public List<DatosDeudor> ListaDatosDeudor = new List<DatosDeudor>();
        public Gestor DatosGestor = new Gestor();
        public string PathReporte { get; set; }

        public string GenerarEmailBody(DatosDeudor deudor)
        {
            string tabla = "";
            string body = "<html><head></head><body>";

            tabla += "<table border=1>tr><td>TIPO</td><td>NUMERO</td><td>MONEDA</td><td>FEC. VENC.</td><td>DIAS VENC.</td><td>MONTO</td><td>SALDO</td></tr>";
            foreach (Documento doc in ListaDocumentos)
            {
                tabla += "<tr>";
                tabla += " <td>" + doc.Tipo + "</td>";
                tabla += " <td>" + doc.Numero + "</td>";
                tabla += " <td>" + doc.Moneda + "</td>";
                tabla += " <td>" + doc.FechaVencimiento.ToShortDateString() + "</td>";
                tabla += " <td>" + (DateTime.Today - doc.FechaVencimiento).TotalDays.ToString() + "</td>";
                tabla += " <td>" + doc.Monto.ToString() + "</td>";
                tabla += " <td>" + doc.Saldo.ToString() + "</td>";
                tabla += " </tr>";
            }
            tabla += "<tr><td></td><td></td><td></td><td></td><td></td><td>TOTAL</td><td>" + ListaDocumentos.Sum(x => x.Saldo).ToString() + "</td></tr></table>";

            if (string.IsNullOrEmpty(MensajePersonalizado))
            {
                if (TipoCartera == 2)
                {
                    body += "<p><strong>Estimado(a) Cliente";
                    body += ": " + deudor.Nombre + "<br>" + "  RUT: " + deudor.Rut + "-" + deudor.Rut.Substring(ListaDatosDeudor[0].Rut.Length - 1, 1) + "</strong></p>";
                    body += "<p><strong>" + NombreCliente + "</strong> informa la siguiente deuda:</p>";
                    body += "<p>" + tabla + "</p>";

                    body += "<p><strong> Estos valores no reflejan gastos asociados, los cuales seran informados al momento de contactarse.</strong></p>";
                    body += "<p>Con la finalidad de regularizar su situación y de evitar molestias y gastos, rogamos a usted tomar contacto con nuestro ejecutivo el Señor(a):";
                }
                else
                {
                    body += "<p><strong>Estimado(a) Señor(a)";
                    body += " " + deudor.Nombre + ":</strong></p>";
                    body += "<p>Con el fin de apoyar su gestión, adjunto le hacemos llegar detalle de facturas emitidas hasta la fecha (vencidas y por vencer) de nuestro cliente ";
                    body += "<strong>" + NombreCliente + "</strong></p>";
                    body += "<p> ";
                }
            }
            else
            {
                if (TipoCartera == 2)
                {
                    body += "<p><strong>Estimado(a) Cliente";
                    body += ": " + deudor.Nombre + "<br>" + "  RUT: " + deudor.Rut + "-" + deudor.Rut.Substring(ListaDatosDeudor[0].Rut.Length - 1, 1) + "</strong></p>";
                    body += "<p><strong>" + NombreCliente + "</strong> informa la siguiente deuda:</p>";
                    body += "<p>" + MensajePersonalizado + "</p>";


                    body += "<p>Con la finalidad de regularizar su situación y de evitar molestias y gastos, rogamos a usted tomar contacto con nuestro ejecutivo el Señor(a):";
                }
                else
                {
                    body += "<p><strong>Señores: " + deudor.NombreFantasia + "</strong></p>";
                    body += "<p><strong>Estimado(a) Señor(a) " + deudor.Nombre + ":</strong></p>";
                    body += "<p>" + MensajePersonalizado + "</p>";
                    if (Pclid == 22)
                    {
                        body += "<p>Con el fin de apoyar su gestión, adjunto le hacemos llegar detalle de facturas emitidas hasta la fecha (vencidas y por vencer) de nuestro cliente ";
                        body += "<strong>" + NombreCliente + "</strong></p>";
                    }
                    else
                    {
                        // tabla+= "<p>Con el fin de apoyar su gestión, adjunto le hacemos llegar detalle de facturas emitidas hasta la fecha de nuestro cliente "
                        // tabla+= "<strong>" + NombreCliente + "</strong></p>"
                    }
                    body += "<p> ";

                }

            }
            if (!string.IsNullOrEmpty(DatosGestor.Nombre))
            {
                body += "<p>Cualquier duda o diferencia respecto al detalle enviado, le solicitamos contactarse con su ejecutivo asignado Señor(a) ";
                body += DatosGestor.Nombre;
                if (Pclid != 22)
                {
                    body += ", al teléfono de Santiago (+562) " + DatosGestor.Telefono;
                }
                if (Pclid == 22)
                {
                    body += ", quien lo contactará a la brevedad para cordinar fechas de pago.</p>";
                }
                else
                {
                    body += "<p> En caso de encontrarse canceladas a la fecha de esta comunicación, le agradeceremos informar a través de esta misma vía.</p>";
                }
                body += "<p> Saludos cordiales,</p>";
                body += "<p><strong>" + NombreEmpresa + "</strong><br>";
                body += DatosGestor.Nombre;
                body += "<br> Ejecutivo Cobranza";
                body += "<br>Telefono (02) " + DatosGestor.Telefono;
                body += ", E-Mail " + DatosGestor.Email;
                body += "</p>";
            }
            else
            {

            }
            if (TipoCartera == 2)
            {
                body += "<p>Por lo anterior, acerquese a la brevedad a nuestras oficinas, para dar solución a su morosidad, evitando inicio de una cobranza judicial.</p><p><strong><em> En caso de tener regularizada su situación, favor dejar sin efecto la presente.</em></strong></p>";
            }
            body += "</body>";
            body += "[" + DateTime.Now.ToString() + "] End of message.";

            return body;
        }

    }

    public class Documento
    {
        public string Tipo { get; set; }
        public int Numero { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int DiasVencido { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
    }

    public class Gestor
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    
}
