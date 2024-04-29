using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace Program1
{
    public partial class MainAdmin : Form
    {
        public MainAdmin() => InitializeComponent();

        private void MainAdmin_Load(object sender, EventArgs e)
        => Out_Panels();

        /// <summary> Вывод панелей списка </summary>
        private void Out_Panels()
        {
            //try
            //{
            using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
            {
                Connect.Open();

                using (SqlCommand Command = new SqlCommand("[Guide. Diseases].[dbo].[Get ALL CompanyOrders]", Connect))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader Reader = Command.ExecuteReader(); while (Reader.Read())
                    {
                        string[] Temp = Reader.GetValue(2).ToString().Split(':');
                        string[][] StringData = new string[Temp.Length][];
                        for (int I1 = 0; I1 < Temp.Length; I1++) StringData[I1] = Temp[I1].Split('#');
                        int SizeZ = 2; if (StringData.Length > 2) SizeZ += StringData.Length - 2;

                        Panel AddPanel = new Panel
                        {
                            Size = new Size(TableInterface.Width - 12, 54 + 50 * SizeZ),
                            BorderStyle = BorderStyle.FixedSingle,
                        };
                        // Вывод данных
                        {
                            Label Testing_Label = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 14),
                                Text = $"Компания {Reader.GetValue(1)} подала заявку на следующие товары:",
                                Size = new Size(AddPanel.Width - 120, 44),
                                Location = new Point(0, 0),
                                TextAlign = ContentAlignment.MiddleLeft
                            };
                            AddPanel.Controls.Add(Testing_Label);

                            TableLayoutPanel TableOrders = new TableLayoutPanel()
                            {
                                Size = new Size(TableInterface.Width - 288, 50* StringData.Length+1),
                                BorderStyle = BorderStyle.FixedSingle,
                                Margin = new Padding(1,1,1,1),
                                Top = 46,
                                Left = 20
                            };
                            {
                                for (int I1 = 0; I1 < StringData.Length; I1++)
                                {
                                    Panel AddPanel_TableOrders = new Panel
                                    {
                                        Size = new Size(TableOrders.Width - 8, 44),
                                        BorderStyle = BorderStyle.FixedSingle,
                                        Left = 20
                                    };
                                    {
                                        Label Name_Label = new Label
                                        {
                                            AutoSize = false,
                                            Font = new Font("Times New Roman", 14),
                                            Size = new Size(AddPanel_TableOrders.Width - 130, 44),
                                            Location = new Point(0, 0),
                                            TextAlign = ContentAlignment.MiddleLeft,
                                            Left = 10,
                                            Top = -1
                                        };
                                        using (SqlConnection Connect2 = new SqlConnection("Data Source='';Integrated Security=True"))
                                            {
                                                Connect2.Open();

                                                using (SqlCommand Command2 = new SqlCommand("[Guide. Diseases].[dbo].[Get Insecticides Name<ID]", Connect2))
                                                {
                                                    Command2.CommandType = CommandType.StoredProcedure;

                                                    Command2.Parameters.Add("@ID", SqlDbType.VarChar, 10); Command2.Parameters["@ID"].Value = StringData[I1][0];

                                                    SqlDataReader Reader2 = Command2.ExecuteReader(); while (Reader2.Read())
                                                    Name_Label.Text = $"Тип: {Reader2.GetValue(0)}\nТовар: {Reader2.GetValue(1)}";
                                                }

                                                Connect2.Close();
                                            }
                                        
                                        Label Count_Label = new Label
                                        {
                                            AutoSize = false,
                                            Font = new Font("Times New Roman", 14),
                                            Text = $"{StringData[I1][1]} Шт",
                                            Size = new Size(100, 44),
                                            Location = new Point(0, 0),
                                            TextAlign = ContentAlignment.MiddleRight,
                                            Left = Name_Label.Width+20,
                                            Top = -1
                                        };

                                        AddPanel_TableOrders.Controls.Add(Name_Label);
                                        AddPanel_TableOrders.Controls.Add(Count_Label);
                                    }
                                    TableOrders.Controls.Add(AddPanel_TableOrders);
                                }
                            }
                            AddPanel.Controls.Add(TableOrders);
                            
                            // Кнопка создания документа
                                            {
                                                PictureBox ButtonClick = new PictureBox()
                                                {
                                                    Tag = Reader.GetValue(0),
                                                    BackgroundImage = Properties.Resources.StackOfPaper,
                                                    Size = new Size(60, 60),
                                                    Location = new Point(TableInterface.Width - 170, 50),
                                                    BorderStyle = BorderStyle.FixedSingle,
                                                    BackgroundImageLayout = ImageLayout.Stretch
                                                };
                                                {
                                                   // Delete_Button.Click += Button_Delete_Click;
                                                }
                                            AddPanel.Controls.Add(ButtonClick);

                                            Label LabelButton = new Label
                                                {
                                                    AutoSize = false,
                                                    Font = new Font("Times New Roman", 10),
                                                    Text = $"Создать документ",
                                                    Size = new Size(120, 25),
                                                    Location = new Point(TableInterface.Width - 200, 116),
                                                    TextAlign = ContentAlignment.MiddleCenter,
                                                BorderStyle = BorderStyle.FixedSingle
                                            };
                                            AddPanel.Controls.Add(LabelButton);
                                        }
                        }

                        TableInterface.Controls.Add(AddPanel);
                    }
                }

                Connect.Close();
            }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}