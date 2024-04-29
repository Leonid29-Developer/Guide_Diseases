namespace Program1
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.Table_List = new System.Windows.Forms.TableLayoutPanel();
            this.Button_MakeOrder = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.Table_List);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.Button_MakeOrder);
            this.SplitContainer.Panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.SplitContainer.Size = new System.Drawing.Size(1099, 657);
            this.SplitContainer.SplitterDistance = 245;
            this.SplitContainer.TabIndex = 0;
            // 
            // Table_List
            // 
            this.Table_List.ColumnCount = 1;
            this.Table_List.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Table_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Table_List.Location = new System.Drawing.Point(0, 0);
            this.Table_List.Name = "Table_List";
            this.Table_List.RowCount = 1;
            this.Table_List.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.Table_List.Size = new System.Drawing.Size(245, 657);
            this.Table_List.TabIndex = 0;
            // 
            // Button_MakeOrder
            // 
            this.Button_MakeOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Button_MakeOrder.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Button_MakeOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Button_MakeOrder.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Button_MakeOrder.Location = new System.Drawing.Point(17, 9);
            this.Button_MakeOrder.Name = "Button_MakeOrder";
            this.Button_MakeOrder.Size = new System.Drawing.Size(180, 40);
            this.Button_MakeOrder.TabIndex = 0;
            this.Button_MakeOrder.Text = "Заказать Инсектициды";
            this.Button_MakeOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Button_MakeOrder.Click += new System.EventHandler(this.Button_MakeOrder_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 657);
            this.Controls.Add(this.SplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.TableLayoutPanel Table_List;
        private System.Windows.Forms.Label Button_MakeOrder;
    }
}

