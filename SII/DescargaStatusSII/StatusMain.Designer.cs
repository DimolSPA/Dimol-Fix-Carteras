namespace DescargaStatusSII
{
    partial class StatusMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.RUT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DV = new System.Windows.Forms.TextBox();
            this.imgcapt = new System.Windows.Forms.PictureBox();
            this.Refrescar = new System.Windows.Forms.Button();
            this.txt_captcha = new System.Windows.Forms.TextBox();
            this.txt_code = new System.Windows.Forms.TextBox();
            this.Consultar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imgcapt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrese Rut del Contribuyente :";
            // 
            // RUT
            // 
            this.RUT.Location = new System.Drawing.Point(244, 63);
            this.RUT.Name = "RUT";
            this.RUT.Size = new System.Drawing.Size(100, 20);
            this.RUT.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(350, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "-";
            // 
            // DV
            // 
            this.DV.Location = new System.Drawing.Point(366, 63);
            this.DV.Name = "DV";
            this.DV.Size = new System.Drawing.Size(24, 20);
            this.DV.TabIndex = 3;
            // 
            // imgcapt
            // 
            this.imgcapt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imgcapt.Location = new System.Drawing.Point(86, 102);
            this.imgcapt.Name = "imgcapt";
            this.imgcapt.Size = new System.Drawing.Size(150, 60);
            this.imgcapt.TabIndex = 4;
            this.imgcapt.TabStop = false;
            // 
            // Refrescar
            // 
            this.Refrescar.Location = new System.Drawing.Point(269, 122);
            this.Refrescar.Name = "Refrescar";
            this.Refrescar.Size = new System.Drawing.Size(75, 23);
            this.Refrescar.TabIndex = 5;
            this.Refrescar.Text = "Refrescar";
            this.Refrescar.UseVisualStyleBackColor = true;
            this.Refrescar.Click += new System.EventHandler(this.Refrescar_Click);
            // 
            // txt_captcha
            // 
            this.txt_captcha.Location = new System.Drawing.Point(55, 195);
            this.txt_captcha.Name = "txt_captcha";
            this.txt_captcha.ReadOnly = true;
            this.txt_captcha.Size = new System.Drawing.Size(459, 20);
            this.txt_captcha.TabIndex = 6;
            // 
            // txt_code
            // 
            this.txt_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_code.Location = new System.Drawing.Point(189, 244);
            this.txt_code.MaxLength = 4;
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(171, 26);
            this.txt_code.TabIndex = 7;
            this.txt_code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_code.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_code_KeyPress);
            this.txt_code.Focus();
            //this.txt_code.KeyUp += TextBoxKeyUp; //here we attach the event
            // 
            // Consultar
            // 
            this.Consultar.Location = new System.Drawing.Point(233, 292);
            this.Consultar.Name = "Consultar";
            this.Consultar.Size = new System.Drawing.Size(75, 23);
            this.Consultar.TabIndex = 8;
            this.Consultar.Text = "Consultar";
            this.Consultar.UseVisualStyleBackColor = true;
            this.Consultar.Click += new System.EventHandler(this.Consultar_Click);
            // 
            // StatusMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 362);
            this.Controls.Add(this.Consultar);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.txt_captcha);
            this.Controls.Add(this.Refrescar);
            this.Controls.Add(this.imgcapt);
            this.Controls.Add(this.DV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RUT);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "StatusMain";
            this.Text = "Descargar Datos RUT SII";
            ((System.ComponentModel.ISupportInitialize)(this.imgcapt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RUT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DV;
        private System.Windows.Forms.PictureBox imgcapt;
        private System.Windows.Forms.Button Refrescar;
        private System.Windows.Forms.TextBox txt_captcha;
        private System.Windows.Forms.TextBox txt_code;
        private System.Windows.Forms.Button Consultar;
    }
}

