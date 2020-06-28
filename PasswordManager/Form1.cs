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
                this.appsDataGridView.Columns["Id"].Visible = false;
                //this.appsDataGridView.Columns["Password"].Visible = false;
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
                var password = "p@SSword";
                //var strEncryptred = PasswordManager.AplicativoController.Encrypt(str, password);
                if (appsDataGridView.SelectedCells.Count > 0)
                {
                    int selectedrowindex = appsDataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = appsDataGridView.Rows[selectedrowindex];
                    var strEncryptred = Convert.ToString(selectedRow.Cells["Password"].Value);
                    var strDecrypted = PasswordManager.AplicativoController.Decrypt(strEncryptred, password);
                    MessageBox.Show(strDecrypted);
                }
                else
                {
                    MessageBox.Show("No se seleccionó ningun registro");
                }
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
                var password = "p@SSword";
                //var strEncryptred = PasswordManager.AplicativoController.Encrypt(str, password);
                if (appsDataGridView.SelectedCells.Count > 0)
                {
                    int selectedrowindex = appsDataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = appsDataGridView.Rows[selectedrowindex];
                    var strDecrypted = Convert.ToString(selectedRow.Cells["Password"].Value);
                    var strEncryptred = PasswordManager.AplicativoController.Encrypt(strDecrypted, password);
                    MessageBox.Show(strEncryptred);
                }
                else
                {
                    MessageBox.Show("No se seleccionó ningun registro");
                }
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
                if (appsDataGridView.SelectedCells.Count > 0)
                {

                }
                else
                {
                    MessageBox.Show("No se seleccionó ningun registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }
    }
}
