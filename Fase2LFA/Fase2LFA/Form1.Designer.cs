namespace Fase2LFA
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbCorrecto = new System.Windows.Forms.Label();
            this.lbIncorrecto = new System.Windows.Forms.Label();
            this.lbProceso = new System.Windows.Forms.Label();
            this.lbArboles = new System.Windows.Forms.Label();
            this.btCrear = new System.Windows.Forms.Button();
            this.btCargar = new System.Windows.Forms.Button();
            this.tbDirección = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(212, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(467, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lenguajes Formales y Autómatas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(330, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Análisis léxico de gramática";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dirección de Archivo Ingresado";
            // 
            // lbCorrecto
            // 
            this.lbCorrecto.AutoSize = true;
            this.lbCorrecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCorrecto.ForeColor = System.Drawing.Color.Lime;
            this.lbCorrecto.Location = new System.Drawing.Point(319, 245);
            this.lbCorrecto.Name = "lbCorrecto";
            this.lbCorrecto.Size = new System.Drawing.Size(261, 32);
            this.lbCorrecto.TabIndex = 3;
            this.lbCorrecto.Text = "Gramática Correcta";
            this.lbCorrecto.Visible = false;
            // 
            // lbIncorrecto
            // 
            this.lbIncorrecto.AutoSize = true;
            this.lbIncorrecto.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIncorrecto.ForeColor = System.Drawing.Color.Red;
            this.lbIncorrecto.Location = new System.Drawing.Point(304, 245);
            this.lbIncorrecto.Name = "lbIncorrecto";
            this.lbIncorrecto.Size = new System.Drawing.Size(278, 32);
            this.lbIncorrecto.TabIndex = 4;
            this.lbIncorrecto.Text = "Gramática Incorrecta";
            this.lbIncorrecto.Visible = false;
            // 
            // lbProceso
            // 
            this.lbProceso.AutoSize = true;
            this.lbProceso.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProceso.ForeColor = System.Drawing.Color.Silver;
            this.lbProceso.Location = new System.Drawing.Point(314, 245);
            this.lbProceso.Name = "lbProceso";
            this.lbProceso.Size = new System.Drawing.Size(262, 32);
            this.lbProceso.TabIndex = 5;
            this.lbProceso.Text = "Análisis en proceso";
            this.lbProceso.Visible = false;
            // 
            // lbArboles
            // 
            this.lbArboles.AutoSize = true;
            this.lbArboles.Location = new System.Drawing.Point(204, 126);
            this.lbArboles.Name = "lbArboles";
            this.lbArboles.Size = new System.Drawing.Size(529, 17);
            this.lbArboles.TabIndex = 6;
            this.lbArboles.Text = "Los árboles de expresión han sido creados, puede cargar un archivo de gramática";
            this.lbArboles.Visible = false;
            // 
            // btCrear
            // 
            this.btCrear.Location = new System.Drawing.Point(53, 118);
            this.btCrear.Name = "btCrear";
            this.btCrear.Size = new System.Drawing.Size(145, 33);
            this.btCrear.TabIndex = 7;
            this.btCrear.Text = "Crear árboles";
            this.btCrear.UseVisualStyleBackColor = true;
            // 
            // btCargar
            // 
            this.btCargar.Location = new System.Drawing.Point(726, 183);
            this.btCargar.Name = "btCargar";
            this.btCargar.Size = new System.Drawing.Size(145, 33);
            this.btCargar.TabIndex = 8;
            this.btCargar.Text = "Realizar análisis";
            this.btCargar.UseVisualStyleBackColor = true;
            this.btCargar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btCargar_MouseClick);
            // 
            // tbDirección
            // 
            this.tbDirección.Enabled = false;
            this.tbDirección.Location = new System.Drawing.Point(53, 188);
            this.tbDirección.Name = "tbDirección";
            this.tbDirección.Size = new System.Drawing.Size(645, 22);
            this.tbDirección.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 450);
            this.Controls.Add(this.tbDirección);
            this.Controls.Add(this.btCargar);
            this.Controls.Add(this.btCrear);
            this.Controls.Add(this.lbArboles);
            this.Controls.Add(this.lbProceso);
            this.Controls.Add(this.lbIncorrecto);
            this.Controls.Add(this.lbCorrecto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "ProyectoLFA2020";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbCorrecto;
        private System.Windows.Forms.Label lbIncorrecto;
        private System.Windows.Forms.Label lbProceso;
        private System.Windows.Forms.Label lbArboles;
        private System.Windows.Forms.Button btCrear;
        private System.Windows.Forms.Button btCargar;
        private System.Windows.Forms.TextBox tbDirección;
    }
}

