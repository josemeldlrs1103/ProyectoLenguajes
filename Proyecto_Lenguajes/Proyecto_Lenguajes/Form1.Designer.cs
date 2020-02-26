namespace Proyecto_Lenguajes
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
            this.btCargar = new System.Windows.Forms.Button();
            this.btAnalizar = new System.Windows.Forms.Button();
            this.lbEstado = new System.Windows.Forms.Label();
            this.txDirección = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btCargar
            // 
            this.btCargar.Location = new System.Drawing.Point(646, 134);
            this.btCargar.Name = "btCargar";
            this.btCargar.Size = new System.Drawing.Size(131, 31);
            this.btCargar.TabIndex = 0;
            this.btCargar.Text = "Cargar archivo";
            this.btCargar.UseVisualStyleBackColor = true;
            // 
            // btAnalizar
            // 
            this.btAnalizar.Enabled = false;
            this.btAnalizar.Location = new System.Drawing.Point(646, 187);
            this.btAnalizar.Name = "btAnalizar";
            this.btAnalizar.Size = new System.Drawing.Size(131, 49);
            this.btAnalizar.TabIndex = 1;
            this.btAnalizar.Text = "Ejecutar Análisis";
            this.btAnalizar.UseVisualStyleBackColor = true;
            // 
            // lbEstado
            // 
            this.lbEstado.AutoSize = true;
            this.lbEstado.Enabled = false;
            this.lbEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEstado.Location = new System.Drawing.Point(300, 190);
            this.lbEstado.Name = "lbEstado";
            this.lbEstado.Size = new System.Drawing.Size(93, 32);
            this.lbEstado.TabIndex = 2;
            this.lbEstado.Text = "label1";
            this.lbEstado.Visible = false;
            // 
            // txDirección
            // 
            this.txDirección.Enabled = false;
            this.txDirección.Location = new System.Drawing.Point(92, 138);
            this.txDirección.Name = "txDirección";
            this.txDirección.Size = new System.Drawing.Size(519, 22);
            this.txDirección.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(538, 38);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lenguajes Formales Y Autómatas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(258, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Análisis Léxico de Gramática";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Dirección Archivo Gramática";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 260);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txDirección);
            this.Controls.Add(this.lbEstado);
            this.Controls.Add(this.btAnalizar);
            this.Controls.Add(this.btCargar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCargar;
        private System.Windows.Forms.Button btAnalizar;
        private System.Windows.Forms.Label lbEstado;
        private System.Windows.Forms.TextBox txDirección;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

