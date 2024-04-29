namespace Program1
{
    partial class MakeOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_Create = new System.Windows.Forms.Label();
            this.TableInterface = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // Label_Create
            // 
            this.Label_Create.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Label_Create.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_Create.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_Create.Location = new System.Drawing.Point(247, 534);
            this.Label_Create.Name = "Label_Create";
            this.Label_Create.Size = new System.Drawing.Size(307, 50);
            this.Label_Create.TabIndex = 4;
            this.Label_Create.Text = "Создать документ по списку кейсов";
            this.Label_Create.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_Create.Click += new System.EventHandler(this.Label_Create_Click);
            // 
            // TableInterface
            // 
            this.TableInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TableInterface.AutoScroll = true;
            this.TableInterface.ColumnCount = 1;
            this.TableInterface.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableInterface.Location = new System.Drawing.Point(14, 11);
            this.TableInterface.Margin = new System.Windows.Forms.Padding(5, 5, 5, 60);
            this.TableInterface.Name = "TableInterface";
            this.TableInterface.RowCount = 1;
            this.TableInterface.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableInterface.Size = new System.Drawing.Size(772, 512);
            this.TableInterface.TabIndex = 3;
            // 
            // MakeOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 595);
            this.Controls.Add(this.Label_Create);
            this.Controls.Add(this.TableInterface);
            this.Name = "MakeOrder";
            this.Load += new System.EventHandler(this.MakeOrder_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label_Create;
        private System.Windows.Forms.TableLayoutPanel TableInterface;
    }
}