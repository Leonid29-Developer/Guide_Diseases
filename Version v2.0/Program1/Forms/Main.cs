using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Program1
{
    public partial class Main : Form
    {
        public Main() => InitializeComponent();

        // Строка подключения
        public static string ConnectString = "Data Source='';Integrated Security=True";

        private List<SummaryDiseases> Diseases = new List<SummaryDiseases>(); private int ShowDiseaseID = -1;

        private bool AgriculturesActive = false, PreventionMethodsActive = false, RecognitionMethodsActive = false, StruggleMethodsActive = false;

        private void Main_Load(object sender, EventArgs e)
        {
            // Загрузка данных
            using (SqlConnection SQL_Connection = new SqlConnection(ConnectString))
            {
                SQL_Connection.Open();
                string Request = $"EXEC [Guide. Diseases].[dbo].[Diseases ALL]"; // SQL-запрос
                SqlCommand SQL_Command = new SqlCommand(Request, SQL_Connection); SqlDataReader Reader = SQL_Command.ExecuteReader();
                while (Reader.Read())
                {
                    List<Descriptions> NewDescriptions = new List<Descriptions>(); List<Agricultures> NewAgricultures = new List<Agricultures>();
                    List<PreventionMethods> NewPreventionMethods = new List<PreventionMethods>(); List<StruggleMethods> NewStruggleMethods = new List<StruggleMethods>();
                    List<RecognitionMethods> NewRecognitionMethods = new List<RecognitionMethods>();

                    using (SqlConnection SQL_Connection2 = new SqlConnection(ConnectString))
                    {
                        SQL_Connection2.Open();
                        Request = $"EXEC [Guide. Diseases].[dbo].[Summary of Diseases. ALL] @Disease"; // SQL-запрос
                        SqlCommand SQL_Command2 = new SqlCommand(Request, SQL_Connection2);
                        SQL_Command2.Parameters.Add("@Disease", SqlDbType.VarChar, 3); SQL_Command2.Parameters["@Disease"].Value = (string)Reader.GetValue(0);
                        SqlDataReader Reader2 = SQL_Command2.ExecuteReader(); while (Reader2.Read())
                        {
                            if (Reader2.GetValue(0) != DBNull.Value) NewDescriptions.Add(new Descriptions((string)Reader2.GetValue(0)));
                            if (Reader2.GetValue(1) != DBNull.Value) NewAgricultures.Add(new Agricultures((string)Reader2.GetValue(1)));
                            if (Reader2.GetValue(2) != DBNull.Value) NewPreventionMethods.Add(new PreventionMethods((string)Reader2.GetValue(2)));
                            if (Reader2.GetValue(3) != DBNull.Value) NewRecognitionMethods.Add(new RecognitionMethods((string)Reader2.GetValue(3)));
                            if (Reader2.GetValue(4) != DBNull.Value) NewStruggleMethods.Add(new StruggleMethods((string)Reader2.GetValue(4)));
                        }
                        SQL_Connection2.Close();
                    }

                    Diseases.Add
                        (
                            new SummaryDiseases
                            (
                               (string)Reader.GetValue(1), (byte[])Reader.GetValue(2),
                               NewDescriptions, NewAgricultures, NewPreventionMethods, NewRecognitionMethods, NewStruggleMethods
                            )
                        );
                }
                SQL_Connection.Close();
            }
            Table_Load(); 
            Button_MakeOrder.BringToFront();  
        }

        /// <summary> Генерация каталога болезней агрокультур </summary>
        private void Table_Load()
        {
            Table_List.Controls.Clear();

            for (int NumDisease = 0; NumDisease < Diseases.Count; NumDisease++)
            {
                Panel Main_Panel = new Panel
                {
                    Name = $"Diseases_{NumDisease}",
                    Size = new Size(Table_List.Width - 23, Table_List.Width - 3),
                    BorderStyle = BorderStyle.FixedSingle
                };
                {
                    // Изображение
                    PictureBox Picture = new PictureBox
                    {
                        Name = $"Diseases_{NumDisease}",
                        BorderStyle = BorderStyle.Fixed3D,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Size = new Size(Main_Panel.Width - 20, (int)((float)(Main_Panel.Width - 20) * 0.8)),
                        Top = 10,
                        Left = 10
                    };
                    using (MemoryStream MS = new MemoryStream(Diseases[NumDisease].Image, 0, Diseases[NumDisease].Image.Length))
                    { MS.Write(Diseases[NumDisease].Image, 0, Diseases[NumDisease].Image.Length); Picture.BackgroundImage = Image.FromStream(MS, true, true); }

                    // Наименование
                    Label Name = new Label
                    {
                        Name = $"Diseases_{NumDisease}",
                        Text = Diseases[NumDisease].Disease,
                        AutoSize = false,
                        Size = new Size(Picture.Width, 60),
                        Font = new Font("Times New Roman", 18),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Top = Picture.Height + 15,
                        Left = 10
                    };

                    // Добавление на форму
                    Picture.Click += UserChanged; Main_Panel.Controls.Add(Picture); Name.Click += UserChanged; Main_Panel.Controls.Add(Name);
                    Main_Panel.Click += UserChanged; Table_List.Controls.Add(Main_Panel);
                }
            }

            Table_List.AutoScroll = false; Table_List.HorizontalScroll.Enabled = false; Table_List.HorizontalScroll.Visible = false; Table_List.AutoScroll = true;
        }

        private void UserChanged(object sender, EventArgs e)
        {
            SplitContainer.Panel2.Controls.Clear();
            var Element_Panel = new Panel(); var Element_PictureBox = new PictureBox(); var Element_Label = new Label();

            switch (sender.GetType().ToString())
            {
                case "System.Windows.Forms.Panel":
                    {
                        Element_Panel = (Panel)sender; string[] Log = Element_Panel.Name.Split('_');
                        if (Log.Length > 1) ShowDiseaseID = Convert.ToInt16(Log[1]);
                    }
                    break;

                case "System.Windows.Forms.PictureBox":
                    {
                        Element_PictureBox = (PictureBox)sender; string[] Log = Element_PictureBox.Name.Split('_');
                        if (Log.Length > 1) ShowDiseaseID = Convert.ToInt16(Log[1]);
                    }
                    break;

                case "System.Windows.Forms.Label":
                    {
                        Element_Label = (Label)sender; string[] Log = Element_Label.Name.Split('_');
                        if (Log.Length > 1) ShowDiseaseID = Convert.ToInt16(Log[1]);
                    }
                    break;
            }

            //Создание элементов
            if (ShowDiseaseID >= 0)
            {
                // Изображение
                PictureBox Picture = new PictureBox
                {
                    BorderStyle = BorderStyle.Fixed3D,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Size = new Size((int)((float)SplitContainer.Panel2.Width / 2), (int)((float)SplitContainer.Panel2.Width * 2 / 5)),
                    Top = 15,
                    Left = 15
                };
                using (MemoryStream MS = new MemoryStream(Diseases[ShowDiseaseID].Image, 0, Diseases[ShowDiseaseID].Image.Length))
                { MS.Write(Diseases[ShowDiseaseID].Image, 0, Diseases[ShowDiseaseID].Image.Length); Picture.BackgroundImage = Image.FromStream(MS, true, true); }

                // Наименование
                Label Name = new Label
                {
                    Text = Diseases[ShowDiseaseID].Disease,
                    AutoSize = false,
                    Size = new Size(Picture.Width, 50),
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Times New Roman", 16, FontStyle.Underline),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Top = Picture.Height + 25,
                    Left = 15
                };

                // Описание
                string Description_Text = ""; int Size_Height = SplitContainer.Panel2.Size.Height - Picture.Size.Height - Name.Size.Height - 50;
                foreach (Descriptions Description in Diseases[ShowDiseaseID].Description) Description_Text += $"{Description.Text}\n";
                Label Description_Label = new Label
                {
                    Text = Description_Text,
                    AutoSize = false,
                    AutoEllipsis = true,
                    Size = new Size(Picture.Width, Size_Height),
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Times New Roman", 12),
                    TextAlign = ContentAlignment.TopLeft,
                    Top = Name.Location.Y + Name.Height + 10,
                    Left = 15
                };

                // Какие культуры поражает
                int Start1PositionY = 0;
                if (AgriculturesActive == true)
                {
                    Panel Heading_Agricultures = new Panel
                    {
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, 54 + 47 * (int)Math.Ceiling((double)Diseases[ShowDiseaseID].Agriculture.Count / 2)),
                        BorderStyle = BorderStyle.FixedSingle,
                        Top = 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    {
                        Label Arrow = new Label
                        {
                            Text = "Какие культуры поражает\n▲",
                            AutoSize = false,
                            Size = new Size(Heading_Agricultures.Width - 4, 44),
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Times New Roman", 12),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Top = 1,
                            Left = 1
                        };
                        Arrow.Click += Agricultures_ShowOrHide; Start1PositionY = Heading_Agricultures.Top + Heading_Agricultures.Size.Height;
                        Heading_Agricultures.Controls.Add(Arrow); SplitContainer.Panel2.Controls.Add(Heading_Agricultures);

                        TableLayoutPanel Tab = new TableLayoutPanel
                        {
                            Size = new Size(Heading_Agricultures.Width - 4, Heading_Agricultures.Height - 49),
                            ColumnCount = 2,
                            CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble,
                            BorderStyle = BorderStyle.FixedSingle,
                            Top = 46,
                            Left = 1
                        };
                        {
                            foreach (Agricultures Agriculture in Diseases[ShowDiseaseID].Agriculture)
                            {
                                Label AgricultureSelect = new Label
                                {
                                    Margin = new Padding(0, 0, 0, 0),
                                    BorderStyle = BorderStyle.FixedSingle,
                                    Text = Agriculture.Text,
                                    AutoSize = false,
                                    Size = new Size((int)(float)(Tab.Width / 2) - 5, 44),
                                    Font = new Font("Times New Roman", 12),
                                    ForeColor = Color.DarkOrchid,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                Tab.Controls.Add(AgricultureSelect);
                            }
                            Heading_Agricultures.Controls.Add(Tab);
                        }
                    }

                    SplitContainer.Panel2.Controls.Add(Heading_Agricultures);
                }
                else
                {
                    Label Heading_Agricultures = new Label
                    {
                        Text = "Какие культуры поражает\n▼",
                        AutoSize = false,
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, 44),
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Times New Roman", 12),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Top = 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    Heading_Agricultures.Click += Agricultures_ShowOrHide; SplitContainer.Panel2.Controls.Add(Heading_Agricultures);
                    Start1PositionY = Heading_Agricultures.Top + Heading_Agricultures.Size.Height;
                }

                // Профилактика
                int Start2PositionY = 0;
                if (PreventionMethodsActive == true)
                {
                    Panel Heading_PreventionMethods = new Panel
                    {
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, SplitContainer.Panel2.Height - Start1PositionY - 148),
                        BorderStyle = BorderStyle.FixedSingle,
                        Top = Start1PositionY + 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    {
                        Label Arrow = new Label
                        {
                            Text = "Профилактика\n▲",
                            AutoSize = false,
                            Size = new Size(Heading_PreventionMethods.Width - 4, 44),
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Times New Roman", 12),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Top = 1,
                            Left = 1
                        };
                        Arrow.Click += PreventionMethods_ShowOrHide; Start2PositionY = Heading_PreventionMethods.Top + Heading_PreventionMethods.Size.Height;
                        Heading_PreventionMethods.Controls.Add(Arrow); SplitContainer.Panel2.Controls.Add(Heading_PreventionMethods);

                        string PreventionMethods_Text = ""; int SizeHeight = SplitContainer.Panel2.Size.Height - Picture.Size.Height - Name.Size.Height - 50;
                        foreach (PreventionMethods PreventionMethod in Diseases[ShowDiseaseID].PreventionMethod) PreventionMethods_Text += $"{PreventionMethod.Text}\n";

                        Label PreventionMethods_Label = new Label
                        {
                            Text = PreventionMethods_Text,
                            AutoSize = false,
                            AutoEllipsis = true,
                            Size = new Size(Heading_PreventionMethods.Width - 4, Heading_PreventionMethods.Height - 49),
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Times New Roman", 12),
                            TextAlign = ContentAlignment.TopLeft,
                            Top = 46,
                            Left = 1
                        };
                        Heading_PreventionMethods.Controls.Add(PreventionMethods_Label);
                    }
                }
                else
                {
                    Label Heading_PreventionMethods = new Label
                    {
                        Text = "Профилактика\n▼",
                        AutoSize = false,
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, 44),
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Times New Roman", 12),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Top = Start1PositionY + 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    Heading_PreventionMethods.Click += PreventionMethods_ShowOrHide; SplitContainer.Panel2.Controls.Add(Heading_PreventionMethods);
                    Start2PositionY = Heading_PreventionMethods.Top + Heading_PreventionMethods.Size.Height;
                }

                // Как распознать
                int Start3PositionY = 0;
                if (RecognitionMethodsActive == true)
                {
                    Panel Heading_RecognitionMethods = new Panel
                    {
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, SplitContainer.Panel2.Height - Start2PositionY - 89),
                        BorderStyle = BorderStyle.FixedSingle,
                        Top = Start2PositionY + 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    {
                        Label Arrow = new Label
                        {
                            Text = "Как распознать\n▲",
                            AutoSize = false,
                            Size = new Size(Heading_RecognitionMethods.Width - 4, 44),
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Times New Roman", 12),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Top = 1,
                            Left = 1
                        };
                        Arrow.Click += RecognitionMethods_ShowOrHide; Start3PositionY = Heading_RecognitionMethods.Top + Heading_RecognitionMethods.Size.Height;
                        Heading_RecognitionMethods.Controls.Add(Arrow); SplitContainer.Panel2.Controls.Add(Heading_RecognitionMethods);

                        string RecognitionMethods_Text = ""; int SizeHeight = SplitContainer.Panel2.Size.Height - Picture.Size.Height - Name.Size.Height - 50;
                        foreach (RecognitionMethods RecognitionMethod in Diseases[ShowDiseaseID].RecognitionMethod) RecognitionMethods_Text += $"{RecognitionMethod.Text}\n";

                        Label RecognitionMethods_Label = new Label
                        {
                            Text = RecognitionMethods_Text,
                            AutoSize = false,
                            AutoEllipsis = true,
                            Size = new Size(Heading_RecognitionMethods.Width - 4, Heading_RecognitionMethods.Height - 49),
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Times New Roman", 12),
                            TextAlign = ContentAlignment.TopLeft,
                            Top = 46,
                            Left = 1
                        };
                        Heading_RecognitionMethods.Controls.Add(RecognitionMethods_Label);
                    }
                }
                else
                {
                    Label Heading_RecognitionMethods = new Label
                    {
                        Text = "Как распознать\n▼",
                        AutoSize = false,
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, 44),
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Times New Roman", 12),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Top = Start2PositionY + 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    Heading_RecognitionMethods.Click += RecognitionMethods_ShowOrHide; SplitContainer.Panel2.Controls.Add(Heading_RecognitionMethods);
                    Start3PositionY = Heading_RecognitionMethods.Top + Heading_RecognitionMethods.Size.Height;
                }

                // Меры борьбы
                if (StruggleMethodsActive == true)
                {
                    Panel Heading_StruggleMethods = new Panel
                    {
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, SplitContainer.Panel2.Height - Start3PositionY - 30),
                        BorderStyle = BorderStyle.FixedSingle,
                        Top = Start3PositionY + 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    {
                        Label Arrow = new Label
                        {
                            Text = "Меры борьбы\n▲",
                            AutoSize = false,
                            Size = new Size(Heading_StruggleMethods.Width - 4, 44),
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Times New Roman", 12),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Top = 1,
                            Left = 1
                        };
                        Arrow.Click += StruggleMethods_ShowOrHide;
                        Heading_StruggleMethods.Controls.Add(Arrow); SplitContainer.Panel2.Controls.Add(Heading_StruggleMethods);

                        string StruggleMethods_Text = ""; int SizeHeight = SplitContainer.Panel2.Size.Height - Picture.Size.Height - Name.Size.Height - 50;
                        foreach (StruggleMethods StruggleMethod in Diseases[ShowDiseaseID].StruggleMethod) StruggleMethods_Text += $"{StruggleMethod.Text}\n";

                        Label StruggleMethods_Label = new Label
                        {
                            Text = StruggleMethods_Text,
                            AutoSize = false,
                            AutoEllipsis = true,
                            Size = new Size(Heading_StruggleMethods.Width - 4, Heading_StruggleMethods.Height - 49),
                            BorderStyle = BorderStyle.FixedSingle,
                            Font = new Font("Times New Roman", 12),
                            TextAlign = ContentAlignment.TopLeft,
                            Top = 46,
                            Left = 1
                        };
                        Heading_StruggleMethods.Controls.Add(StruggleMethods_Label);
                    }
                }
                else
                {
                    Label Heading_StruggleMethod = new Label
                    {
                        Text = "Меры борьбы\n▼",
                        AutoSize = false,
                        Size = new Size(SplitContainer.Panel2.Width / 2 - 45, 44),
                        BorderStyle = BorderStyle.FixedSingle,
                        Font = new Font("Times New Roman", 12),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Top = Start3PositionY + 15,
                        Left = SplitContainer.Panel2.Width / 2 + 30
                    };
                    Heading_StruggleMethod.Click += StruggleMethods_ShowOrHide; SplitContainer.Panel2.Controls.Add(Heading_StruggleMethod);
                }

                SplitContainer.Panel2.Controls.Add(Picture); SplitContainer.Panel2.Controls.Add(Name); SplitContainer.Panel2.Controls.Add(Description_Label);
            }
        }

        private void Active_False(int Num)
        {
            if (Num != 1) AgriculturesActive = false; if (Num != 2) PreventionMethodsActive = false;
            if (Num != 3) RecognitionMethodsActive = false; if (Num != 4) StruggleMethodsActive = false;
        }

        private void Agricultures_ShowOrHide(object sender, EventArgs e)
        { Active_False(1); if (AgriculturesActive == true) AgriculturesActive = false; else AgriculturesActive = true; UserChanged(sender, e); }

        private void PreventionMethods_ShowOrHide(object sender, EventArgs e)
        { Active_False(2); if (PreventionMethodsActive == true) PreventionMethodsActive = false; else PreventionMethodsActive = true; UserChanged(sender, e); }

        private void RecognitionMethods_ShowOrHide(object sender, EventArgs e)
        { Active_False(3); if (RecognitionMethodsActive == true) RecognitionMethodsActive = false; else RecognitionMethodsActive = true; UserChanged(sender, e); }

        private void StruggleMethods_ShowOrHide(object sender, EventArgs e)
        { Active_False(4); if (StruggleMethodsActive == true) StruggleMethodsActive = false; else StruggleMethodsActive = true; UserChanged(sender, e); }

    private void Button_MakeOrder_Click(object sender, EventArgs e)
        {
            Hide(); new MakeOrder().ShowDialog(); Close();
        }
    }
}