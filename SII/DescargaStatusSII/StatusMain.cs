using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SII.bcp;

namespace DescargaStatusSII
{
    public partial class StatusMain : Form
    {
        private SII.dto.Captcha objCaptcha = new SII.dto.Captcha();
        private List<SII.dto.Status> Lst = new List<SII.dto.Status>();
        private int Indice = 0;

        public StatusMain()
        {
            InitializeComponent();
            //SII.bcp.Status.ConsultarRutPagina();
            //objCaptcha = SII.bcp.Status.ObtenerCaptcha();
            //imgcapt.Load(System.Configuration.ConfigurationManager.AppSettings["UrlViewCaptcha"] + "&txtCaptcha=" + objCaptcha.txtCaptcha);
            //txt_captcha.Text = objCaptcha.txtCaptcha;
            //SII.bcp.Status.DescargarCaptchaSSL( objCaptcha);
            Lst = SII.bcp.Status.ListarRutporEstado("L");
            RUT.Text = Lst[Indice].Rut.ToString();
            DV.Text = Lst[Indice].DigitoVerificador;
            RefrescarCaptcha();

            txt_code.Focus();


            //bool salida = SII.bcp.Status.CargarDatos();
        }

        private void Refrescar_Click(object sender, EventArgs e)
        {
            RefrescarCaptcha();
            txt_code.Focus();
        }

        private void Consultar_Click(object sender, EventArgs e)
        {
            ConsultarCaptcha();
            RefrescarCaptcha();
            txt_code.Focus();
        }

        private void RefrescarCaptcha()
        {
            objCaptcha = SII.bcp.Status.ObtenerCaptcha();
            imgcapt.Load(System.Configuration.ConfigurationManager.AppSettings["UrlViewCaptcha"] + "&txtCaptcha=" + objCaptcha.txtCaptcha);
            txt_captcha.Text = objCaptcha.txtCaptcha;
            txt_code.Text = "";
            string codigo = SII.bcp.Status.BuscarCaptcha(objCaptcha.txtCaptcha);
            if (!string.IsNullOrEmpty(codigo))
            {
                txt_code.Text = codigo;
                ConsultarCaptcha();
                RefrescarCaptcha();
            }
            txt_code.Focus();
        }

        private void ConsultarCaptcha()
        {
            objCaptcha.Codigo = txt_code.Text;
            int resultado = SII.bcp.Status.ConsultarRut(objCaptcha, Lst[Indice]);
            if (resultado > 0)
            {
                Indice++;
                RUT.Text = Lst[Indice].Rut.ToString();
                DV.Text = Lst[Indice].DigitoVerificador;
            }
            txt_code.Focus();
        }

        private void txt_code_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == (char)Keys.Return)
            {
                ConsultarCaptcha();
                RefrescarCaptcha();
            }
            txt_code.Focus();
        }

        //private void TextBoxKeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        //Do something
                
        //        e.Handled = true;
        //    }
        //}

    }
}
