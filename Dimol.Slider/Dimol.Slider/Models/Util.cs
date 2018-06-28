using CYPH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dimol.Slider.Models
{
    public class Util
    {
        public string Encripta(string password)
        {
            string encrip = "";

            Ucode objUcode = new Ucode();

            encrip = objUcode.Encripta(password);

            return encrip;

        }

        public string Desencripta(string psw_encriptada)
        {
            string result = "";

            Ucode objUcode = new Ucode();

            result = objUcode.Desencripta(psw_encriptada);

            return result;

        }
    }
}