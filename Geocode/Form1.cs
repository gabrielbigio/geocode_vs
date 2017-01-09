using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
namespace Geocode
{
    public partial class Form1 : MetroForm
    {
        Reverso reverso;
        Geocode geocode;
        ViewGeocodeReverso vwrev;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void mbntSelecionar_Click(object sender, EventArgs e)
        {

        }

        private void mbntReverso_Click(object sender, EventArgs e)
        {
            reverso = new Reverso();
            reverso.Show();
            this.Hide();
        }

        private void mbntGeocode_Click(object sender, EventArgs e)
        {
            geocode = new Geocode();
            geocode.Show();
            this.Hide();
        }

        private void mbntFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            vwrev = new ViewGeocodeReverso();
            vwrev.Show();
            this.Hide();
        }

        
    }
}
