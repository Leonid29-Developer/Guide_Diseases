using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Program1
{
    public partial class MakeOrder : Form
    {
        public MakeOrder() => InitializeComponent();

        DataTable DATA_Type = new DataTable(), DATA_Insecticides = new DataTable();

        List<Insecticid> SelectData = new List<Insecticid>();

        private void MakeOrder_Load(object sender, EventArgs e)
        {
            using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
            {
                Connect.Open();

                using (SqlCommand Command = new SqlCommand("[Guide. Diseases].[dbo].[Get TypeInsecticides]", Connect))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader Reader = Command.ExecuteReader()) DATA_Type.Load(Reader);
                }

                Connect.Close();
            }

            Creating_AnAddPanel();
        }

        /// <summary> Список элементов управления для быстрого обращения к ним </summary>
        /// Type_ComboBox       > Выбор типа инсектицидов           > 0
        /// TestingTypes_Label  > Описание типа инсектицидов        > 1
        /// Insecticid_ComboBox > Выбор инсектицида                 > 2
        /// Count_TextBox       > Количество выбранного инсектицида > 3
        private List<Control> List_Contorls = new List<Control>();

        /// <summary> Вывод панелей списка </summary>
        private void Out_Panels()
        {
            AutoScroll = false;
            TableInterface.Controls.Clear(); List_Contorls.Clear();
            for (int I1 = 0; I1 < SelectData.Count(); I1++) Out_Panel(SelectData[I1], I1);
            Creating_AnAddPanel();
            AutoScroll = true;
        }

        /// <summary> Вывод панели </summary>
        private void Out_Panel(Insecticid Insd, int Index)
        {
            try
            {
                Panel AddPanel = new Panel
                {
                    Size = new Size(TableInterface.Width - 12, 100),
                    BorderStyle = BorderStyle.FixedSingle,
                };

                // Вывод данных
                {
                    Label Testing_Label = new Label
                    {
                        AutoSize = false,
                        Font = new Font("Times New Roman", 14),
                        Text = $"Инсектицид {SelectData[Index].Name}\n{SelectData[Index].Count} Шт",
                        Size = new Size(AddPanel.Width - 100, AddPanel.Height),
                        Location = new Point(0, 0),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    AddPanel.Controls.Add(Testing_Label);

                    Panel DopPanel = new Panel
                    {
                        Size = new Size(100, AddPanel.Height),
                        Location = new Point(AddPanel.Width - 100, -1),
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    {
                        // Кнопка удаления
                        {
                            PictureBox Delete_Button = new PictureBox()
                            {
                                Tag = $"{Index}",
                                BackgroundImage = Properties.Resources.Trashcan,
                                Size = new Size(44, 44),
                                Location = new Point(28, 20),
                                BorderStyle = BorderStyle.FixedSingle,
                                BackgroundImageLayout = ImageLayout.Stretch
                            };
                            {
                                Delete_Button.Click += Button_Delete_Click;
                            }
                            DopPanel.Controls.Add(Delete_Button);

                            Label Save_Label = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 8),
                                Text = $"Удалить",
                                Size = new Size(60, 20),
                                Location = new Point(20, 65),
                                TextAlign = ContentAlignment.MiddleCenter
                            };
                            DopPanel.Controls.Add(Save_Label);
                        }
                    }
                    AddPanel.Controls.Add(DopPanel);
                }
                TableInterface.Controls.Add(AddPanel);
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary> Создание панели добавления </summary>
        private void Creating_AnAddPanel()
        {
            try
            {
                Panel AddPanel = new Panel
                {
                    Size = new Size(TableInterface.Width - 12, 200),
                    BorderStyle = BorderStyle.FixedSingle,
                };
                {
                    // Выбор типа инсектицидов
                    {
                        Label Type_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Выберите тип инсектицидов",
                            Size = new Size(220, 22),
                            Location = new Point(50, 15),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(Type_Label);

                        ComboBox Type_ComboBox = new ComboBox()
                        {
                            Name = $"CB_TestingTypes",
                            Font = new Font("Times New Roman", 12),
                            Size = Type_Label.Size,
                            Location = new Point(Type_Label.Location.X, 25 + Type_Label.Height)
                        };
                        {
                            List_Contorls.Add(Type_ComboBox);
                            for (int I1 = 0; I1 < DATA_Type.Rows.Count; I1++)
                                Type_ComboBox.Items.Add(DATA_Type.Rows[I1][1]);
                            Type_ComboBox.SelectedIndex = 0;

                            Type_ComboBox.SelectedIndexChanged += new EventHandler(ComboBox_Type_SelectedIndexChanged);
                        }
                        AddPanel.Controls.Add(Type_ComboBox);
                    }

                    // Описание типа инсектицидов
                    {
                        Label TypeDescription_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Описание:\n{DATA_Type.Rows[0][2]}",
                            Size = new Size(220, 20),
                            Location = new Point(20, 100),
                            TextAlign = ContentAlignment.BottomLeft
                        };
                        AddPanel.Controls.Add(TypeDescription_Label);

                        Label TypeDescription_Text = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 11),
                            Text = $"{DATA_Type.Rows[0][2]}",
                            Size = new Size(220, 80),
                            Location = new Point(20, 120),
                            TextAlign = ContentAlignment.TopLeft
                        };

                        TypeDescription_Text.AutoEllipsis = true;

                        List_Contorls.Add(TypeDescription_Text);
                        AddPanel.Controls.Add(TypeDescription_Text);
                    }

                    // Выбор инсектицида
                    {
                        Label TestedElemnets_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Выберите инсектицид",
                            Size = new Size(340, 22),
                            Location = new Point(300, 15),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(TestedElemnets_Label);

                        ComboBox Insecticid_ComboBox = new ComboBox()
                        {
                            Name = $"CB_TestedElemnets",
                            Font = new Font("Times New Roman", 12),
                            Size = TestedElemnets_Label.Size,
                            Location = new Point(TestedElemnets_Label.Location.X, 25 + TestedElemnets_Label.Height)
                        };
                        {
                            List_Contorls.Add(Insecticid_ComboBox);
                            Insecticid_ComboBox.Enabled = false;
                        }
                        AddPanel.Controls.Add(Insecticid_ComboBox);
                    }

                    // Количество выбранного инсектицида
                    {
                        Label NameTestedElemnet_Label = new Label
                        {
                            AutoSize = false,
                            Font = new Font("Times New Roman", 12),
                            Text = $"Ведение количество выбранного инсектицида",
                            Size = new Size(260, 22),
                            Location = new Point(340, 122),
                            TextAlign = ContentAlignment.BottomCenter
                        };
                        AddPanel.Controls.Add(NameTestedElemnet_Label);

                        TextBox NameTestedElemnet_TextBox = new TextBox
                        {
                            Name = $"TB_NameTestedElemnet",
                            Font = new Font("Times New Roman", 12),
                            Size = NameTestedElemnet_Label.Size,
                            Location = new Point(NameTestedElemnet_Label.Location.X, 128 + NameTestedElemnet_Label.Height)
                        };
                        {
                            List_Contorls.Add(NameTestedElemnet_TextBox);
                        }
                        AddPanel.Controls.Add(NameTestedElemnet_TextBox);
                    }

                    Panel DopPanel = new Panel
                    {
                        Size = new Size(100, AddPanel.Height),
                        Location = new Point(AddPanel.Width - 100, -1),
                        BorderStyle = BorderStyle.FixedSingle,
                    };
                    {
                        // Кнопка сохранения
                        {
                            PictureBox Save_Button = new PictureBox()
                            {
                                BackgroundImage = Properties.Resources.Save,
                                Size = new Size(44, 44),
                                Location = new Point(28, 80),
                                BorderStyle = BorderStyle.FixedSingle,
                                BackgroundImageLayout = ImageLayout.Stretch
                            };
                            {
                                Save_Button.Click += Button_Save_Click;
                            }
                            DopPanel.Controls.Add(Save_Button);

                            Label Save_Label = new Label
                            {
                                AutoSize = false,
                                Font = new Font("Times New Roman", 8),
                                Text = $"Сохранить",
                                Size = new Size(60, 20),
                                Location = new Point(20, 125),
                                TextAlign = ContentAlignment.MiddleCenter
                            };

                            DopPanel.Controls.Add(Save_Label);
                        }
                    }
                    AddPanel.Controls.Add(DopPanel);
                }
                TableInterface.Controls.Add(AddPanel);
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Обработка события «ComboBox» </br> 
        ///  Изменение выбора типа инсектицидов
        /// </summary>
        private void ComboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox SelectedComboBox = (ComboBox)List_Contorls[0];
                int Index = SelectedComboBox.SelectedIndex;
DATA_Insecticides = new DataTable();

                using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                {
                    Connect.Open();

                    using (SqlCommand Command = new SqlCommand("[Guide. Diseases].[dbo].[Get Insecticides All<Type]", Connect))
                    {
                        Command.CommandType = CommandType.StoredProcedure;

                        Command.Parameters.Add("@Type", SqlDbType.NVarChar, 30); Command.Parameters["@Type"].Value = DATA_Type.Rows[Index][0];

                        using (SqlDataReader Reader = Command.ExecuteReader()) DATA_Insecticides.Load(Reader);
                    }

                    Connect.Close();
                }

                Label Description = (Label)List_Contorls[1];
                Description.Text = DATA_Type.Rows[Index][2].ToString();

                ComboBox TwoComboBox = (ComboBox)List_Contorls[2];
                TwoComboBox.Items.Clear(); TwoComboBox.Enabled = true;

                if (DATA_Insecticides.Rows.Count > 0)
                {
                    for (int I1 = 0; I1 < DATA_Insecticides.Rows.Count; I1++)
                    TwoComboBox.Items.Add(DATA_Insecticides.Rows[I1][1]);
                    TwoComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Сохранение параметров и добавление инсектицида в список
        /// </summary>
        private void Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Result =
                    MessageBox.Show("Уверены, что хотите сохранить параметры и добавить инсектицид в список?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (Result == DialogResult.Yes)
                {
                    ComboBox TwoComboBox = (ComboBox)List_Contorls[2];
                    TextBox TB_Count = (TextBox)List_Contorls[3];

                    if (TB_Count.Text == "") throw new Exceptions.Errors.FieldsNoFiled();

                    SelectData.Add(new Insecticid
                        (DATA_Insecticides.Rows[TwoComboBox.SelectedIndex][0].ToString(), DATA_Insecticides.Rows[TwoComboBox.SelectedIndex][1].ToString(), Convert.ToInt32(TB_Count.Text)));
                }

                Out_Panels();
            }
            catch (Exceptions.Errors.FieldsNoFiled Ex)
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
        /// Удаление инсектицида из списка
        /// </summary>
        private void Button_Delete_Click(object sender, EventArgs e)
        {
            DialogResult Result =
                MessageBox.Show("Уверены, что хотите удалить инсектицид из списка?", "Сообщение", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (Result == DialogResult.Yes)
            {
                PictureBox Picture = (PictureBox)sender;
                int Index = Convert.ToInt32(Picture.Tag);
                SelectData.RemoveAt(Index);
            }

            Out_Panels();
        }

        /// <summary>
        /// Обработка события «Button» </br> 
        /// Создание заявки и сохранение в БД
        /// </summary>
        private void Label_Create_Click(object sender, EventArgs e)
        {
            try
            {
                string StringKod = "";

                for (int I1 = 0; I1 < SelectData.Count; I1++)
                {
                    if (I1 != 0) StringKod += ":";
                    StringKod += $"{SelectData[I1].ID}#{SelectData[I1].Count}";
                }

                if (StringKod.Length > 0)
                    using (SqlConnection Connect = new SqlConnection("Data Source='';Integrated Security=True"))
                    {
                        Connect.Open();

                        using (SqlCommand Command = new SqlCommand("[Guide. Diseases].[dbo].[Add Order]", Connect))
                        {
                            Command.CommandType = CommandType.StoredProcedure;

                            Command.Parameters.Add("@CompanyID", SqlDbType.VarChar, 32); Command.Parameters["@CompanyID"].Value = Authorization.CompanyID;
                            Command.Parameters.Add("@StringKodOrder", SqlDbType.NVarChar, 32); Command.Parameters["@StringKodOrder"].Value = StringKod;

                            Command.ExecuteNonQuery();
                        }

                        Connect.Close();
                        throw new Exceptions.Information.Successfully();
                    }
            }
            catch (Exceptions.Information.Successfully Ex)
            {
                MessageBox.Show(Ex.Message, Ex.Data["Say"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show($"{Ex.Message}", "Неизвестная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}