namespace Fase2LFA
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btFase2 = new System.Windows.Forms.Button();
            this.dgNodoFLN = new System.Windows.Forms.DataGridView();
            this.SímbFLN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.First = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nullable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgNodoFollow = new System.Windows.Forms.DataGridView();
            this.SimbFollow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Follow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgEstados = new System.Windows.Forms.DataGridView();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgNodoFLN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNodoFollow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEstados)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(610, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(283, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Análisis sintáctico de gramática";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(509, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(467, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Lenguajes Formales y Autómatas";
            // 
            // btFase2
            // 
            this.btFase2.Location = new System.Drawing.Point(1297, 43);
            this.btFase2.Name = "btFase2";
            this.btFase2.Size = new System.Drawing.Size(141, 29);
            this.btFase2.TabIndex = 4;
            this.btFase2.Text = "Proceso Fase 2";
            this.btFase2.UseVisualStyleBackColor = true;
            this.btFase2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btFase2_MouseClick);
            // 
            // dgNodoFLN
            // 
            this.dgNodoFLN.AllowUserToAddRows = false;
            this.dgNodoFLN.AllowUserToDeleteRows = false;
            this.dgNodoFLN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgNodoFLN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNodoFLN.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SímbFLN,
            this.First,
            this.Last,
            this.Nullable});
            this.dgNodoFLN.Location = new System.Drawing.Point(12, 129);
            this.dgNodoFLN.Name = "dgNodoFLN";
            this.dgNodoFLN.ReadOnly = true;
            this.dgNodoFLN.RowHeadersWidth = 51;
            this.dgNodoFLN.RowTemplate.Height = 24;
            this.dgNodoFLN.Size = new System.Drawing.Size(446, 150);
            this.dgNodoFLN.TabIndex = 5;
            // 
            // SímbFLN
            // 
            this.SímbFLN.HeaderText = "Símbolo";
            this.SímbFLN.MinimumWidth = 6;
            this.SímbFLN.Name = "SímbFLN";
            this.SímbFLN.ReadOnly = true;
            this.SímbFLN.Width = 87;
            // 
            // First
            // 
            this.First.HeaderText = "First";
            this.First.MinimumWidth = 6;
            this.First.Name = "First";
            this.First.ReadOnly = true;
            this.First.Width = 64;
            // 
            // Last
            // 
            this.Last.HeaderText = "Last";
            this.Last.MinimumWidth = 6;
            this.Last.Name = "Last";
            this.Last.ReadOnly = true;
            this.Last.Width = 64;
            // 
            // Nullable
            // 
            this.Nullable.HeaderText = "Nullable";
            this.Nullable.MinimumWidth = 6;
            this.Nullable.Name = "Nullable";
            this.Nullable.ReadOnly = true;
            this.Nullable.Width = 88;
            // 
            // dgNodoFollow
            // 
            this.dgNodoFollow.AllowUserToAddRows = false;
            this.dgNodoFollow.AllowUserToDeleteRows = false;
            this.dgNodoFollow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgNodoFollow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNodoFollow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SimbFollow,
            this.Follow});
            this.dgNodoFollow.Location = new System.Drawing.Point(94, 308);
            this.dgNodoFollow.Name = "dgNodoFollow";
            this.dgNodoFollow.ReadOnly = true;
            this.dgNodoFollow.RowHeadersWidth = 51;
            this.dgNodoFollow.RowTemplate.Height = 24;
            this.dgNodoFollow.Size = new System.Drawing.Size(277, 150);
            this.dgNodoFollow.TabIndex = 6;
            // 
            // SimbFollow
            // 
            this.SimbFollow.HeaderText = "Símbolo";
            this.SimbFollow.MinimumWidth = 6;
            this.SimbFollow.Name = "SimbFollow";
            this.SimbFollow.ReadOnly = true;
            this.SimbFollow.Width = 87;
            // 
            // Follow
            // 
            this.Follow.HeaderText = "Follow";
            this.Follow.MinimumWidth = 6;
            this.Follow.Name = "Follow";
            this.Follow.ReadOnly = true;
            this.Follow.Width = 76;
            // 
            // dgEstados
            // 
            this.dgEstados.AllowUserToAddRows = false;
            this.dgEstados.AllowUserToDeleteRows = false;
            this.dgEstados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEstados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Estado});
            this.dgEstados.Location = new System.Drawing.Point(515, 129);
            this.dgEstados.Name = "dgEstados";
            this.dgEstados.ReadOnly = true;
            this.dgEstados.RowHeadersWidth = 51;
            this.dgEstados.RowTemplate.Height = 24;
            this.dgEstados.Size = new System.Drawing.Size(934, 335);
            this.dgEstados.TabIndex = 7;
            // 
            // Estado
            // 
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 6;
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 125;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 502);
            this.Controls.Add(this.dgEstados);
            this.Controls.Add(this.dgNodoFollow);
            this.Controls.Add(this.dgNodoFLN);
            this.Controls.Add(this.btFase2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.dgNodoFLN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNodoFollow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEstados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btFase2;
        private System.Windows.Forms.DataGridView dgNodoFLN;
        private System.Windows.Forms.DataGridViewTextBoxColumn SímbFLN;
        private System.Windows.Forms.DataGridViewTextBoxColumn First;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nullable;
        private System.Windows.Forms.DataGridView dgNodoFollow;
        private System.Windows.Forms.DataGridViewTextBoxColumn SimbFollow;
        private System.Windows.Forms.DataGridViewTextBoxColumn Follow;
        private System.Windows.Forms.DataGridView dgEstados;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
    }
}