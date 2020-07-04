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
            try
            {
                var strDecrypted = Convert.ToString(this.input_pass.Text);
                var strEncryptred = PasswordManager.Crypto.Encrypt(strDecrypted, Crypto.admin_pass);

                Aplicativo aplicativo = new Aplicativo();
                aplicativo.Name = this.input_app.Text;
                aplicativo.User = this.input_user.Text;
                aplicativo.Env = this.input_env.Text;
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
    }
}
