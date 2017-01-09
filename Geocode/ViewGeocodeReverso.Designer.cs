namespace Geocode
{
    partial class ViewGeocodeReverso
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mcmbDB = new MetroFramework.Controls.MetroComboBox();
            this.mbntFechar = new MetroFramework.Controls.MetroButton();
            this.mbntGeocode = new MetroFramework.Controls.MetroButton();
            this.htmlPanel1 = new MetroFramework.Drawing.Html.HtmlPanel();
            this.mpbGeocode = new MetroFramework.Controls.MetroProgressBar();
            this.mcmbFonte = new MetroFramework.Controls.MetroComboBox();
            this.grdResult = new MetroFramework.Controls.MetroGrid();
            this.mbntExportar = new MetroFramework.Controls.MetroButton();
            this.mbntCancelar = new MetroFramework.Controls.MetroButton();
            this.bgWork = new System.ComponentModel.BackgroundWorker();
            this.mbtnBD = new MetroFramework.Controls.MetroButton();
            this.htmlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).BeginInit();
            this.SuspendLayout();
            // 
            // mcmbDB
            // 
            this.mcmbDB.FormattingEnabled = true;
            this.mcmbDB.ItemHeight = 23;
            this.mcmbDB.Items.AddRange(new object[] {
            "Selecione uma tabela"});
            this.mcmbDB.Location = new System.Drawing.Point(28, 12);
            this.mcmbDB.Name = "mcmbDB";
            this.mcmbDB.Size = new System.Drawing.Size(219, 29);
            this.mcmbDB.TabIndex = 0;
            this.mcmbDB.UseSelectable = true;
            this.mcmbDB.SelectedIndexChanged += new System.EventHandler(this.mcmbDB_SelectedIndexChanged);
            // 
            // mbntFechar
            // 
            this.mbntFechar.Location = new System.Drawing.Point(51, 318);
            this.mbntFechar.Name = "mbntFechar";
            this.mbntFechar.Size = new System.Drawing.Size(75, 38);
            this.mbntFechar.TabIndex = 1;
            this.mbntFechar.Text = "Fechar";
            this.mbntFechar.UseSelectable = true;
            this.mbntFechar.Click += new System.EventHandler(this.mbntFechar_Click);
            // 
            // mbntGeocode
            // 
            this.mbntGeocode.Location = new System.Drawing.Point(589, 12);
            this.mbntGeocode.Name = "mbntGeocode";
            this.mbntGeocode.Size = new System.Drawing.Size(118, 29);
            this.mbntGeocode.TabIndex = 2;
            this.mbntGeocode.Text = "Geocode";
            this.mbntGeocode.UseSelectable = true;
            this.mbntGeocode.Click += new System.EventHandler(this.mbntGeocode_Click);
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.AutoScrollMinSize = new System.Drawing.Size(710, 0);
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.Controls.Add(this.mpbGeocode);
            this.htmlPanel1.Controls.Add(this.mcmbFonte);
            this.htmlPanel1.Controls.Add(this.mbntGeocode);
            this.htmlPanel1.Controls.Add(this.mcmbDB);
            this.htmlPanel1.Location = new System.Drawing.Point(23, 63);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(710, 70);
            this.htmlPanel1.TabIndex = 3;
            // 
            // mpbGeocode
            // 
            this.mpbGeocode.Location = new System.Drawing.Point(28, 44);
            this.mpbGeocode.Name = "mpbGeocode";
            this.mpbGeocode.Size = new System.Drawing.Size(679, 23);
            this.mpbGeocode.TabIndex = 4;
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
            this.mcmbFonte.Location = new System.Drawing.Point(312, 12);
            this.mcmbFonte.Name = "mcmbFonte";
            this.mcmbFonte.Size = new System.Drawing.Size(219, 29);
            this.mcmbFonte.TabIndex = 3;
            this.mcmbFonte.UseSelectable = true;
            this.mcmbFonte.SelectedIndexChanged += new System.EventHandler(this.mcmbFonte_SelectedIndexChanged);
            // 
            // grdResult
            // 
            this.grdResult.AllowUserToResizeRows = false;
            this.grdResult.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdResult.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grdResult.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdResult.EnableHeadersVisualStyles = false;
            this.grdResult.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grdResult.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grdResult.Location = new System.Drawing.Point(51, 139);
            this.grdResult.Name = "grdResult";
            this.grdResult.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdResult.Size = new System.Drawing.Size(682, 172);
            this.grdResult.TabIndex = 4;
            this.grdResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdResult_CellContentClick);
            // 
            // mbntExportar
            // 
            this.mbntExportar.Location = new System.Drawing.Point(609, 319);
            this.mbntExportar.Name = "mbntExportar";
            this.mbntExportar.Size = new System.Drawing.Size(121, 37);
            this.mbntExportar.TabIndex = 5;
            this.mbntExportar.Text = "Exportar";
            this.mbntExportar.UseSelectable = true;
            this.mbntExportar.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // mbntCancelar
            // 
            this.mbntCancelar.Location = new System.Drawing.Point(146, 319);
            this.mbntCancelar.Name = "mbntCancelar";
            this.mbntCancelar.Size = new System.Drawing.Size(75, 37);
            this.mbntCancelar.TabIndex = 7;
            this.mbntCancelar.Text = "Cancelar";
            this.mbntCancelar.UseSelectable = true;
            this.mbntCancelar.Click += new System.EventHandler(this.mbntCancelar_Click);
            // 
            // bgWork
            // 
            this.bgWork.WorkerReportsProgress = true;
            this.bgWork.WorkerSupportsCancellation = true;
            this.bgWork.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWork_DoWork);
            this.bgWork.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWork_ProgressChanged);
            this.bgWork.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWork_RunWorkerCompleted);
            // 
            // mbtnBD
            // 
            this.mbtnBD.Location = new System.Drawing.Point(497, 319);
            this.mbtnBD.Name = "mbtnBD";
            this.mbtnBD.Size = new System.Drawing.Size(106, 37);
            this.mbtnBD.TabIndex = 8;
            this.mbtnBD.Text = "Gravar no Banco";
            this.mbtnBD.UseSelectable = true;
            this.mbtnBD.Click += new System.EventHandler(this.mbtnBD_Click);
            // 
            // ViewGeocodeReverso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 406);
            this.Controls.Add(this.mbtnBD);
            this.Controls.Add(this.mbntCancelar);
            this.Controls.Add(this.mbntExportar);
            this.Controls.Add(this.grdResult);
            this.Controls.Add(this.htmlPanel1);
            this.Controls.Add(this.mbntFechar);
            this.Name = "ViewGeocodeReverso";
            this.Text = "Geocode BD";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.ViewGeocodeReverso_Load);
            this.htmlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox mcmbDB;
        private MetroFramework.Controls.MetroButton mbntFechar;
        private MetroFramework.Controls.MetroButton mbntGeocode;
        private MetroFramework.Drawing.Html.HtmlPanel htmlPanel1;
        private MetroFramework.Controls.MetroGrid grdResult;
        private MetroFramework.Controls.MetroButton mbntExportar;
        private MetroFramework.Controls.MetroButton mbntCancelar;
        private MetroFramework.Controls.MetroComboBox mcmbFonte;
        private MetroFramework.Controls.MetroProgressBar mpbGeocode;
        private System.ComponentModel.BackgroundWorker bgWork;
        private MetroFramework.Controls.MetroButton mbtnBD;
    }
}