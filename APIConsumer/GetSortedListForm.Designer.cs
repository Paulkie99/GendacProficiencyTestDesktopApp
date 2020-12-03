
namespace APIConsumer
{
    partial class GetSortedListForm
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
            this.OKButton = new System.Windows.Forms.Button();
            this.PageTB = new System.Windows.Forms.TextBox();
            this.PageSizeTB = new System.Windows.Forms.TextBox();
            this.OrderByCB = new System.Windows.Forms.ComboBox();
            this.AscendingCheckBox = new System.Windows.Forms.CheckBox();
            this.FilterTB = new System.Windows.Forms.TextBox();
            this.PageLabel = new System.Windows.Forms.Label();
            this.PageSizeLabel = new System.Windows.Forms.Label();
            this.OrderByLabel = new System.Windows.Forms.Label();
            this.FilterLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(145, 183);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(84, 51);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_ClickAsync);
            // 
            // PageTB
            // 
            this.PageTB.Location = new System.Drawing.Point(136, 19);
            this.PageTB.Name = "PageTB";
            this.PageTB.Size = new System.Drawing.Size(100, 23);
            this.PageTB.TabIndex = 0;
            // 
            // PageSizeTB
            // 
            this.PageSizeTB.Location = new System.Drawing.Point(136, 56);
            this.PageSizeTB.Name = "PageSizeTB";
            this.PageSizeTB.Size = new System.Drawing.Size(100, 23);
            this.PageSizeTB.TabIndex = 1;
            // 
            // OrderByCB
            // 
            this.OrderByCB.FormattingEnabled = true;
            this.OrderByCB.Items.AddRange(new object[] {
            "Id",
            "Name",
            "Category",
            "Price"});
            this.OrderByCB.Location = new System.Drawing.Point(136, 90);
            this.OrderByCB.Name = "OrderByCB";
            this.OrderByCB.Size = new System.Drawing.Size(100, 23);
            this.OrderByCB.TabIndex = 2;
            // 
            // AscendingCheckBox
            // 
            this.AscendingCheckBox.AutoSize = true;
            this.AscendingCheckBox.Checked = true;
            this.AscendingCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AscendingCheckBox.Location = new System.Drawing.Point(136, 120);
            this.AscendingCheckBox.Name = "AscendingCheckBox";
            this.AscendingCheckBox.Size = new System.Drawing.Size(82, 19);
            this.AscendingCheckBox.TabIndex = 3;
            this.AscendingCheckBox.Text = "Ascending";
            this.AscendingCheckBox.UseVisualStyleBackColor = true;
            // 
            // FilterTB
            // 
            this.FilterTB.Location = new System.Drawing.Point(136, 145);
            this.FilterTB.Name = "FilterTB";
            this.FilterTB.Size = new System.Drawing.Size(100, 23);
            this.FilterTB.TabIndex = 4;
            // 
            // PageLabel
            // 
            this.PageLabel.AutoSize = true;
            this.PageLabel.Location = new System.Drawing.Point(61, 26);
            this.PageLabel.Name = "PageLabel";
            this.PageLabel.Size = new System.Drawing.Size(33, 15);
            this.PageLabel.TabIndex = 6;
            this.PageLabel.Text = "Page";
            // 
            // PageSizeLabel
            // 
            this.PageSizeLabel.AutoSize = true;
            this.PageSizeLabel.Location = new System.Drawing.Point(61, 63);
            this.PageSizeLabel.Name = "PageSizeLabel";
            this.PageSizeLabel.Size = new System.Drawing.Size(56, 15);
            this.PageSizeLabel.TabIndex = 7;
            this.PageSizeLabel.Text = "Page Size";
            // 
            // OrderByLabel
            // 
            this.OrderByLabel.AutoSize = true;
            this.OrderByLabel.Location = new System.Drawing.Point(61, 97);
            this.OrderByLabel.Name = "OrderByLabel";
            this.OrderByLabel.Size = new System.Drawing.Size(53, 15);
            this.OrderByLabel.TabIndex = 8;
            this.OrderByLabel.Text = "Order By";
            // 
            // FilterLabel
            // 
            this.FilterLabel.AutoSize = true;
            this.FilterLabel.Location = new System.Drawing.Point(61, 152);
            this.FilterLabel.Name = "FilterLabel";
            this.FilterLabel.Size = new System.Drawing.Size(33, 15);
            this.FilterLabel.TabIndex = 9;
            this.FilterLabel.Text = "Filter";
            // 
            // GetSortedListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 246);
            this.Controls.Add(this.FilterLabel);
            this.Controls.Add(this.OrderByLabel);
            this.Controls.Add(this.PageSizeLabel);
            this.Controls.Add(this.PageLabel);
            this.Controls.Add(this.FilterTB);
            this.Controls.Add(this.AscendingCheckBox);
            this.Controls.Add(this.OrderByCB);
            this.Controls.Add(this.PageSizeTB);
            this.Controls.Add(this.PageTB);
            this.Controls.Add(this.OKButton);
            this.MinimumSize = new System.Drawing.Size(370, 280);
            this.Name = "GetSortedListForm";
            this.Text = "Get Sorted List";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox PageTB;
        private System.Windows.Forms.TextBox PageSizeTB;
        private System.Windows.Forms.ComboBox OrderByCB;
        private System.Windows.Forms.CheckBox AscendingCheckBox;
        private System.Windows.Forms.TextBox FilterTB;
        private System.Windows.Forms.Label FilterLabel;
        private System.Windows.Forms.Label PageLabel;
        private System.Windows.Forms.Label PageSizeLabel;
        private System.Windows.Forms.Label OrderByLabel;
    }
}