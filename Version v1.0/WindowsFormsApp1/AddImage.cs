using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide_Diseases
{
    class AddImage
    {
        public AddImage()
        {
            // Сохранение изображения
            string filename = @"C:\Users\User\Desktop\Проект\Фото\max_g480_c12_r2x3_pd20 (17).jpeg"; byte[] imageData;
            using (FileStream fs = new FileStream(filename, FileMode.Open)) { imageData = new byte[fs.Length]; fs.Read(imageData, 0, imageData.Length); }

            using (SqlConnection SQL_Connection = new SqlConnection(Main.ConnectString))
            {
                SQL_Connection.Open(); SqlCommand SQL_Command = SQL_Connection.CreateCommand();
                string Request = $"UPDATE [Guide. Diseases].[dbo].[Images] SET [Image] = @ImageData WHERE [ID] = 'I018';"; // SQL-запрос
                SQL_Command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000); SQL_Command.Parameters["@ImageData"].Value = imageData;
                SQL_Command.CommandText = Request; SQL_Command.ExecuteNonQuery(); SQL_Connection.Close();
            }
        }
    }
}
