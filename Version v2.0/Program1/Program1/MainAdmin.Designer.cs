namespace Program1
{
    partial class MainAdmin
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
            this.TableInterface = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // TableInterface
            // 
            this.TableInterface.AutoScroll = true;
            this.TableInterface.ColumnCount = 1;
            this.TableInterface.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.TableInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableInterface.Location = new System.Drawing.Point(0, 0);
            this.TableInterface.Margin = new System.Windows.Forms.Padding(5, 5, 5, 60);
            this.TableInterface.Name = "TableInterface";
            this.TableInterface.RowCount = 1;
            this.TableInterface.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TableInterface.Size = new System.Drawing.Size(692, 619);
            this.TableInterface.TabIndex = 4;
            // 
            // MainAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(692, 619);
            this.Controls.Add(this.TableInterface);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainAdmin";
            this.Load += new System.EventHandler(this.MainAdmin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableInterface;
    }
}