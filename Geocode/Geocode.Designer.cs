namespace Geocode
{
    partial class Geocode
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
            this.mtxtArquivo = new MetroFramework.Controls.MetroTextBox();
            this.mbntSelecionar = new MetroFramework.Controls.MetroButton();
            this.mbntCancelar = new MetroFramework.Controls.MetroButton();
            this.mbntFechar = new MetroFramework.Controls.MetroButton();
            this.mbntGeo = new MetroFramework.Controls.MetroButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mcmbFonte = new MetroFramework.Controls.MetroComboBox();
            this.mlblFonte = new MetroFramework.Controls.MetroLabel();
            this.mbtnExcel = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // mtxtArquivo
            // 
            this.mtxtArquivo.Enabled = false;
            this.mtxtArquivo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtArquivo.Lines = new string[] {
        "Selecione um arquivo xlsx..."};
            this.mtxtArquivo.Location = new System.Drawing.Point(123, 115);
            this.mtxtArquivo.MaximumSize = new System.Drawing.Size(400, 30);
            this.mtxtArquivo.MaxLength = 32767;
            this.mtxtArquivo.MinimumSize = new System.Drawing.Size(350, 30);
            this.mtxtArquivo.Name = "mtxtArquivo";
            this.mtxtArquivo.PasswordChar = '\0';
            this.mtxtArquivo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtArquivo.SelectedText = "";
            this.mtxtArquivo.Size = new System.Drawing.Size(366, 30);
            this.mtxtArquivo.TabIndex = 15;
            this.mtxtArquivo.Text = "Selecione um arquivo xlsx...";
            this.mtxtArquivo.UseSelectable = true;
            // 
            // mbntSelecionar
            // 
            this.mbntSelecionar.Location = new System.Drawing.Point(23, 115);
            this.mbntSelecionar.Name = "mbntSelecionar";
            this.mbntSelecionar.Size = new System.Drawing.Size(94, 30);
            this.mbntSelecionar.TabIndex = 11;
            this.mbntSelecionar.Text = "Selecione";
            this.mbntSelecionar.UseSelectable = true;
            this.mbntSelecionar.Click += new System.EventHandler(this.mbntSelecionar_Click);
            // 
            // mbntCancelar
            // 
            this.mbntCancelar.Location = new System.Drawing.Point(137, 286);
            this.mbntCancelar.Name = "mbntCancelar";
            this.mbntCancelar.Size = new System.Drawing.Size(91, 35);
            this.mbntCancelar.TabIndex = 13;
            this.mbntCancelar.Text = "Cancelar";
            this.mbntCancelar.UseSelectable = true;
            this.mbntCancelar.Visible = false;
            // 
            // mbntFechar
            // 
            this.mbntFechar.Location = new System.Drawing.Point(23, 286);
            this.mbntFechar.Name = "mbntFechar";
            this.mbntFechar.Size = new System.Drawing.Size(94, 35);
            this.mbntFechar.TabIndex = 14;
            this.mbntFechar.Text = "Fechar";
            this.mbntFechar.UseSelectable = true;
            this.mbntFechar.Click += new System.EventHandler(this.mbntFechar_Click);
            // 
            // mbntGeo
            // 
            this.mbntGeo.Location = new System.Drawing.Point(739, 286);
            this.mbntGeo.Name = "mbntGeo";
            this.mbntGeo.Size = new System.Drawing.Size(122, 35);
            this.mbntGeo.TabIndex = 12;
            this.mbntGeo.Text = "Geocode";
            this.mbntGeo.UseSelectable = true;
            this.mbntGeo.Click += new System.EventHandler(this.mbntGeo_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // mcmbFonte
            // 
            this.mcmbFonte.FormattingEnabled = true;
            this.mcmbFonte.ItemHeight = 23;
            this.mcmbFonte.Items.AddRange(new object[] {
            "Selecione a fonte",
            "Google",
            "Nominatim",
            "Bing",
            "Yahoo"});
            this.mcmbFonte.Location = new System.Drawing.Point(602, 120);
            this.mcmbFonte.Name = "mcmbFonte";
            this.mcmbFonte.Size = new System.Drawing.Size(219, 29);
            this.mcmbFonte.TabIndex = 16;
            this.mcmbFonte.UseSelectable = true;
            // 
            // mlblFonte
            // 
            this.mlblFonte.AutoSize = true;
            this.mlblFonte.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblFonte.Location = new System.Drawing.Point(524, 120);
            this.mlblFonte.Name = "mlblFonte";
            this.mlblFonte.Size = new System.Drawing.Size(58, 25);
            this.mlblFonte.TabIndex = 17;
            this.mlblFonte.Text = "Fonte:";
            // 
            // mbtnExcel
            // 
            this.mbtnExcel.Location = new System.Drawing.Point(602, 286);
            this.mbtnExcel.Name = "mbtnExcel";
            this.mbtnExcel.Size = new System.Drawing.Size(116, 35);
            this.mbtnExcel.TabIndex = 18;
            this.mbtnExcel.Text = "Exportar Excel";
            this.mbtnExcel.UseSelectable = true;
            this.mbtnExcel.Click += new System.EventHandler(this.mbtnExcel_Click);
            // 
            // Geocode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 344);
            this.Controls.Add(this.mbtnExcel);
            this.Controls.Add(this.mlblFonte);
            this.Controls.Add(this.mcmbFonte);
            this.Controls.Add(this.mtxtArquivo);
            this.Controls.Add(this.mbntSelecionar);
            this.Controls.Add(this.mbntCancelar);
            this.Controls.Add(this.mbntFechar);
            this.Controls.Add(this.mbntGeo);
            this.Name = "Geocode";
            this.Text = "Geocode";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox mtxtArquivo;
        private MetroFramework.Controls.MetroButton mbntSelecionar;
        private MetroFramework.Controls.MetroButton mbntCancelar;
        private MetroFramework.Controls.MetroButton mbntFechar;
        private MetroFramework.Controls.MetroButton mbntGeo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MetroFramework.Controls.MetroComboBox mcmbFonte;
        private MetroFramework.Controls.MetroLabel mlblFonte;
        private MetroFramework.Controls.MetroButton mbtnExcel;
    }
}