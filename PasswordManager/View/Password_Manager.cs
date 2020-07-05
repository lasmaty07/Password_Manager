using PasswordManager.Model;
using PasswordManager.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordManager
{
    public partial class Password_Manager : Form
    {
        public Password_Manager()
        {
            
            InitializeComponent();
        }

        private void textBox_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_buscar_Click(this, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private void btn_buscar_Click(object sender, EventArgs e)
        {
            try 
            {
                string name = this.textBox1.Text;
                string user = this.textBox2.Text;
                string env = this.textBox3.Text;
                List<Aplicativo> apps = PasswordManager.AplicativoController.GetAplicativo(name,user,env);               
                this.appsDataGridView.DataSource = apps.OrderBy(x => x.Name).ToList();
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
                    if (Crypto.admin_pass != "")
                    { 
                        int selectedrowindex = appsDataGridView.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = appsDataGridView.Rows[selectedrowindex];
                        var strEncryptred = Convert.ToString(selectedRow.Cells["Password"].Value);
                        var strDecrypted = PasswordManager.Crypto.Decrypt(strEncryptred, Crypto.admin_pass);
                        //MessageBox.Show(strDecrypted);
                        ShowTextDialog(strDecrypted);
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
                if (appsDataGridView.SelectedCells.Count > 0)
                {
                    string new_pass = "";
                    if (Crypto.admin_pass != "" && ShowInputDialog(ref new_pass) == DialogResult.OK && new_pass!= "")
                    {
                        Aplicativo aplicativo = new Aplicativo();
                        int selectedrowindex = appsDataGridView.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = appsDataGridView.Rows[selectedrowindex];
                        var strDecrypted = Convert.ToString(new_pass);
                        var strEncryptred = PasswordManager.Crypto.Encrypt(strDecrypted, Crypto.admin_pass);
                        aplicativo.Id = (int)selectedRow.Cells["Id"].Value;
                        aplicativo.Name = Convert.ToString(selectedRow.Cells["Name"].Value);
                        aplicativo.User = Convert.ToString(selectedRow.Cells["User"].Value);
                        aplicativo.Env = Convert.ToString(selectedRow.Cells["Env"].Value);
                        aplicativo.Password = strEncryptred;
                        PasswordManager.AplicativoController.UpdateAplicativo(aplicativo);
                        this.btn_buscar_Click(sender, e);
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

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (appsDataGridView.SelectedCells.Count > 0)
                {
                    Aplicativo aplicativo = new Aplicativo();
                    int selectedrowindex = appsDataGridView.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = appsDataGridView.Rows[selectedrowindex];
                    aplicativo.Id = (int)selectedRow.Cells["Id"].Value;
                    if (MessageBox.Show("Se va a eliminar : " + Convert.ToString(selectedRow.Cells["Name"].Value), "Eliminar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PasswordManager.AplicativoController.DeleteAplicativo(aplicativo);
                        this.btn_buscar_Click(sender, e);
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
        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new AddAppForm())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form2 inputBox = new Form2();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.MaximizeBox = false;
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
            cancelButton.Text = "&Cerrar";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;
            inputBox.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }


        private static DialogResult ShowTextDialog(string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form2 textDialog = new Form2();

            textDialog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            textDialog.ClientSize = size;
            textDialog.MaximizeBox = false;
            textDialog.Text = "Contraseña";

            System.Windows.Forms.Label label = new Label();
            label.Size = new System.Drawing.Size(size.Width - 10, 23);
            label.Location = new System.Drawing.Point(5, 5);
            label.Text = input;
            textDialog.Controls.Add(label);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "copiarButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&Copiar";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            okButton.Click += (sender, e) => { copyToClipboard(sender, e, input); };
            textDialog.Controls.Add(okButton);
            

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cerrar";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            textDialog.Controls.Add(cancelButton);

            textDialog.AcceptButton = okButton;
            textDialog.CancelButton = cancelButton;
            textDialog.StartPosition = FormStartPosition.CenterParent;

            DialogResult result = textDialog.ShowDialog();
            input = label.Text;
            return result;
        }

        private static void copyToClipboard(object sender, EventArgs e, string text)
        {
            object obj = text;
            try
            {
                Clipboard.SetDataObject(obj, true, 2, 1);
            }
            catch
            {
                Clipboard.SetText(text);
            }
        }
    }
}
