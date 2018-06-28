using Dimol.Slider.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Slider.bcp
{
    public class Tasks
    {
        public static List<Tarea> ListarTareas(string fecha, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Tasks.ListarTareas(fecha, where, sidx, sord, inicio, limite);
        }

        public static int ListarTareasCount(string fecha, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Tasks.ListarTareasCount(fecha, where, sidx, sord, inicio, limite);
        }

        public static int CompletarTarea(int IdTarea, int Completa)
        {
            return dao.Tasks.CompletarTarea(IdTarea, Completa);
        }

        public static int ValidarTareaCumplida(int idTarea)
        {
            return dao.Tasks.ValidarTareaCumplida(idTarea);
        }

        public static int DesactivarTarea(string nombre)
        {
            return dao.Tasks.DesactivarTarea(nombre);
        }

        public static int GuardarTarea(string nombre, string obs, string fechaTarea, string dias)
        {
            return dao.Tasks.GuardarTarea(nombre, obs, fechaTarea, dias);
        }

        public static int VerificarTareasSemanales()
        {
            return dao.Tasks.VerificarTareasSemanales();
        }        

        public static int ActualizarTarea(int id, string nombre, string obs, string fechaTarea, string dias)
        {
            return dao.Tasks.ActualizarTarea(id, nombre, obs, fechaTarea, dias);
        }

        public static bool EnviarEmail()
        {
            Email emailSender = new Email();
            dto.Email email = new dto.Email();
            dao.Util func = new dao.Util();
            bool error = false;
            string emailDestino = func.ConfigParamStr(5);

            email = new dto.Email();
            email.From = func.ConfigParamStr(4);
            email.Sender = func.ConfigParamStr(4);

            foreach (string mail in emailDestino.Split(','))
            {
                email.To.Add(mail);
            }

            //email.Bcc.Add(objSession.Email);
            email.Subject = func.ConfigParamStr(6);
            email.Body = dao.Tasks.GenerarEmailBody();
            email.Html = true;
            //System.Net.Mail.Attachment objAtt = new System.Net.Mail.Attachment(obj.PathArchivo);
            //email.Attachments.Add(objAtt);
            error = emailSender.EnviarEmail(email) ? false : true;

            return error;
        }

    }
}
