using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Program1
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        /// <summary> Обработка события. Загрузка формы </summary>
            private void Authorization_Load(object sender, EventArgs e)
            {
                Location = new Point
                    (Screen.PrimaryScreen.Bounds.Size.Width / 2 - Size.Width / 2, Screen.PrimaryScreen.Bounds.Size.Height / 2 - Size.Height / 2);
            }

        public static string CompanyID { get; private set; }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Авторизация
        /// </summary>
        private void Button_Enter_Click(object sender, EventArgs e)
        {
            try
            {
                bool T = false;

                using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                {
                    Connect.Open();

                    using (SqlCommand Command = new SqlCommand("[Guide. Diseases].[dbo].[Authentication]", Connect))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.Add("@Login", SqlDbType.VarChar, 32); Command.Parameters["@Login"].Value = TB_Login.Text;
                        Command.Parameters.Add("@Password", SqlDbType.NVarChar, 32); Command.Parameters["@Password"].Value = TB_Password.Text;

                        SqlDataReader Reader = Command.ExecuteReader(); T = Reader.HasRows;
                        while (Reader.Read())CompanyID = (string)Reader.GetValue(0);
                    }

                    Connect.Close();
                }

                if (T)
                {
                   Hide(); 
                    if (CompanyID[1] == 'U') new Main().ShowDialog(); 
                    else new MainAdmin().ShowDialog();
                    Show();
                }
                else throw new Exceptions.Errors.Authentication();
            }
            catch (Exceptions.Errors.Authentication Ex)
            {
                MessageBox.Show($"{Ex.Message}", Ex.Data["Kod"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Регистрация
        /// </summary>
        private void Button_Register_Click(object sender, EventArgs e)
        {
            Hide(); new Registration().ShowDialog(); Show();
        }

        /// <summary>
        /// Быстрая авторизация </br> 
        /// !!! Удалить при производственной эксплуатации !!!
        /// </summary>
        private void panel1_Click(object sender, EventArgs e)
        {
            TB_Login.Text = "Lighfjf4r6"; TB_Password.Text = "mg34";
        }
        private void panel2_Click(object sender, EventArgs e)
        {
TB_Login.Text = "Jr2qav58!"; TB_Password.Text = "hgr%2g";
        }
    }
}