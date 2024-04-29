using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Program1
{
    public partial class Registration : Form
    {
        public Registration() => InitializeComponent();

        /// <summary> Обработка события. Загрузка формы </summary>
        private void Registration_Load(object sender, EventArgs e)
        {
            Location = new Point
                            (Screen.PrimaryScreen.Bounds.Size.Width / 2 - Size.Width / 2, Screen.PrimaryScreen.Bounds.Size.Height / 2 - Size.Height / 2);
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Регистрация
        /// </summary>
        private void Button_Register_Click(object sender, EventArgs e)
        {
            try
            {
                bool T = true; string Error = "";

                // Проверка доступности Логина
                {
                    using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                    {
                        Connect.Open();

                        using (SqlCommand Command = new SqlCommand("[Guide. Diseases].[dbo].[CheckingAvailabilityLogin]", Connect))
                        {
                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.Add("@Login", SqlDbType.NVarChar, 32); Command.Parameters["@Login"].Value = TB_Login.Text;

                            SqlDataReader Reader = Command.ExecuteReader(); if (Reader.HasRows)
                            {
                                T = false;
                                Error += "\nЛогин не доступен";
                            }
                        }

                        Connect.Close();
                    }
                }

                // Пароли должны совпадать
                if (TB_Password.Text != TB_PasswordAgain.Text)
                {
                    T = false;
                    Error += "\nПароли должны совпадать";
                }

                if (T)
                {
                    using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                    {
                        Connect.Open();

                        using (SqlCommand Command = new SqlCommand("[System_For_AnalyzingProject].[dbo].[Registration]", Connect))
                        {
                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.Add("@Login", SqlDbType.VarChar, 32); Command.Parameters["@Login"].Value = TB_Login.Text;
                            Command.Parameters.Add("@Password", SqlDbType.NVarChar, 32); Command.Parameters["@Password"].Value = TB_Password.Text;
                            Command.Parameters.Add("@CompanyName", SqlDbType.NVarChar, 200); Command.Parameters["@CompanyName"].Value = TB_CompanyName.Text;
                            Command.Parameters.Add("@CompanyPhone", SqlDbType.VarChar, 18); Command.Parameters["@CompanyPhone"].Value = TB_CompanyPhone.Text;
                            Command.Parameters.Add("@Surname", SqlDbType.NVarChar, 30); Command.Parameters["@Surname"].Value = TB_Surname.Text;
                            Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30); Command.Parameters["@FirstName"].Value = TB_Name.Text;
                            Command.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 30); Command.Parameters["@MiddleName"].Value = TB_MiddleName.Text;

                            Command.ExecuteNonQuery();
                        }

                        Connect.Close();
                    }

                    throw new Exceptions.Information.Registration();
                }
                else throw new Exceptions.Errors.Registration(Error);
            }
            catch (Exceptions.Information.Registration Ex)
            {
                MessageBox.Show(Ex.Message, Ex.Data["Say"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exceptions.Errors.Registration Ex)
            {
                MessageBox.Show($"{Ex.Message}", Ex.Data["Kod"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}