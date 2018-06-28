using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Open.bcp
{
    public class ConsultaPJ
    {
        public static List<dto.ConsultaPJ> ConsultarPorRut(string rut)
        {
            return dao.ConsultaPJ.ConsultarPorRut(rut);
        }

        public static List<dto.UserPJ> TraeUserPJ(int userid, string button)
        {
            return dao.ConsultaPJ.TraeUserPJ(userid, button);
        }

        public static List<dto.UserPJ> TraeRutaLogoEmpresaPJ(int userid, string button)
        {
            return dao.ConsultaPJ.TraeRutaLogoEmpresaPJ(userid, button);
        }

        public static string ValidaUsuario(string usuario, string password, string red, string local)
        {
            return dao.ConsultaPJ.ValidaUsuario(usuario, password, red, local);
        }

        public static int ValidaEmpresaRutaPJ(string Pclid)
        {
            return dao.ConsultaPJ.ValidaEmpresaRutaPJ(Pclid);
        }

        public static string TraeLoginPJ(string usuario, string red, string local)
        {
            return dao.ConsultaPJ.TraeLoginPJ(usuario, red, local);
        }
        public static string TraeRutaLogo(string usuario)
        {
            return dao.ConsultaPJ.TraeRutaLogo(usuario);
        }
        public static string TraePrf(string usuario, string red, string local)
        {
            return dao.ConsultaPJ.TraePrf(usuario, red, local);
        }
        public static void LogOffUsrByIp(string ip)
        {
            dao.ConsultaPJ.LogOffUsrByIp(ip);
        }
        public static int ActPass(string usrid, string passAct, string newPass)
        {
            return dao.ConsultaPJ.ActPass(usrid, passAct, newPass);
        }

        public static int GuardarUserPJ(int iduser, string nombre, string username, string pass, int activa, int pclid, string adm)
        {
            return dao.ConsultaPJ.GuardarUserPJ(iduser, nombre, username, pass, activa, pclid, adm);
        }

        public static int InsertarRutaLogoPJ(int iduser, string pclid, string ruta)
        {
            return dao.ConsultaPJ.InsertarRutaLogoPJ(iduser, pclid, ruta);
        }
    }
}
