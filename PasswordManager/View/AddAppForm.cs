using PasswordManager.Model;
using System;
using System.Windows.Forms;

namespace PasswordManager.View
{
    public partial class AddAppForm : Form
    {
        public AddAppForm()
        {
            InitializeComponent();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (validateFields()) {
                try
                {
                    var strDecrypted = Convert.ToString(this.input_pass.Text.Trim());
                    var strEncryptred = PasswordManager.Crypto.Encrypt(strDecrypted, Crypto.admin_pass);

                    Aplicativo aplicativo = new Aplicativo();
                    aplicativo.Name = this.input_app.Text.Trim();
                    aplicativo.User = this.input_user.Text.Trim();
                    aplicativo.Env = this.input_env.Text.Trim();
                    aplicativo.Password = strEncryptred;
                    var result = AplicativoController.AddAplicativo(aplicativo);
                    if (result) {
                        MessageBox.Show("Registro guardado exitosamente");
                        this.Close();
                    }
                    else {
                        MessageBox.Show("Ya existe un registro con Para ese aplcativo, con el mismo usuario y ambiente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                MessageBox.Show("Debe completar todos los campos.");
            }
        }

        private bool validateFields() {
            if (this.input_app.Text.Trim() == "") {
                return false;
            }
            if (this.input_user.Text.Trim() == "")
            {
                return false;
            }
            if (this.input_env.Text.Trim() == "")
            {
                return false;
            }
            if (this.input_pass.Text.Trim() == "")
            {
                return false;
            }
            return true;
        }
    }
}
