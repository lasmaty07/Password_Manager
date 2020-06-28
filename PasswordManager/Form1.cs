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

        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form2 inputBox = new Form2();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Contraseña";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            inputBox.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
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
                if (appsDataGridView.SelectedCells.Count > 0)
                {
                    var password = "";
                    if (ShowInputDialog(ref password) == System.Windows.Forms.DialogResult.OK)
                    { 
                        int selectedrowindex = appsDataGridView.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = appsDataGridView.Rows[selectedrowindex];
                        var strEncryptred = Convert.ToString(selectedRow.Cells["Password"].Value);
                        var strDecrypted = PasswordManager.Crypto.Decrypt(strEncryptred, password);
                        MessageBox.Show(strDecrypted); 
                    }
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
                    var strEncryptred = PasswordManager.Crypto.Encrypt(strDecrypted, password);
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
