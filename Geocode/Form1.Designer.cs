namespace Geocode
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.mbntGeocode = new MetroFramework.Controls.MetroButton();
            this.mbntReverso = new MetroFramework.Controls.MetroButton();
            this.mbntFechar = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // mbntGeocode
            // 
            this.mbntGeocode.Location = new System.Drawing.Point(109, 100);
            this.mbntGeocode.Name = "mbntGeocode";
            this.mbntGeocode.Size = new System.Drawing.Size(101, 53);
            this.mbntGeocode.TabIndex = 0;
            this.mbntGeocode.Text = "Geocode";
            this.mbntGeocode.UseSelectable = true;
            this.mbntGeocode.Click += new System.EventHandler(this.mbntGeocode_Click);
            // 
            // mbntReverso
            // 
            this.mbntReverso.Location = new System.Drawing.Point(255, 100);
            this.mbntReverso.Name = "mbntReverso";
            this.mbntReverso.Size = new System.Drawing.Size(100, 53);
            this.mbntReverso.TabIndex = 1;
            this.mbntReverso.Text = "Geocode Reverso";
            this.mbntReverso.UseSelectable = true;
            this.mbntReverso.Click += new System.EventHandler(this.mbntReverso_Click);
            // 
            // mbntFechar
            // 
            this.mbntFechar.Location = new System.Drawing.Point(23, 285);
            this.mbntFechar.Name = "mbntFechar";
            this.mbntFechar.Size = new System.Drawing.Size(94, 26);
            this.mbntFechar.TabIndex = 3;
            this.mbntFechar.Text = "Fechar";
            this.mbntFechar.UseSelectable = true;
            this.mbntFechar.Click += new System.EventHandler(this.mbntFechar_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(109, 172);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(246, 52);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "Geocode Reverso BD";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 334);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.mbntFechar);
            this.Controls.Add(this.mbntReverso);
            this.Controls.Add(this.mbntGeocode);
            this.Name = "Form1";
            this.Text = "Geocode";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MetroFramework.Controls.MetroButton mbntGeocode;
        private MetroFramework.Controls.MetroButton mbntReverso;
        private MetroFramework.Controls.MetroButton mbntFechar;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}

