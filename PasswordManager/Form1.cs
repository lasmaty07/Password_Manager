using PasswordManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void btn_buscar_Click(object sender, EventArgs e)
        {
            try 
            {
                string name = this.textBox1.Text;
                string user = this.textBox2.Text;
                string env = this.textBox3.Text;
                List<Aplicativo> apps = PasswordManager.AplicativoController.GetAplicativo(name,user,env);
                this.appsDataGridView.DataSource = apps;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_copiar_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Now working yet");
            }
            catch (Exception ex)
        {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
        {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}
