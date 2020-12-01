
using System;

namespace APIConsumer
{
    partial class AddProductForm
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
            this.IdTB = new System.Windows.Forms.TextBox();
            this.PriceTB = new System.Windows.Forms.TextBox();
            this.NameTB = new System.Windows.Forms.TextBox();
            this.CatCB = new System.Windows.Forms.ComboBox();
            this.IdLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.CategoryLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.ProductDetailsGroup = new System.Windows.Forms.GroupBox();
            this.ProductDetailsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // IdTB
            // 
            this.IdTB.Location = new System.Drawing.Point(122, 22);
            this.IdTB.Name = "IdTB";
            this.IdTB.Size = new System.Drawing.Size(100, 23);
            this.IdTB.TabIndex = 0;
            // 
            // PriceTB
            // 
            this.PriceTB.Location = new System.Drawing.Point(122, 187);
            this.PriceTB.Name = "PriceTB";
            this.PriceTB.Size = new System.Drawing.Size(100, 23);
            this.PriceTB.TabIndex = 1;
            // 
            // NameTB
            // 
            this.NameTB.Location = new System.Drawing.Point(122, 78);
            this.NameTB.Name = "NameTB";
            this.NameTB.Size = new System.Drawing.Size(100, 23);
            this.NameTB.TabIndex = 2;
            // 
            // CatCB
            // 
            this.CatCB.FormattingEnabled = true;
            this.CatCB.Location = new System.Drawing.Point(122, 134);
            this.CatCB.Name = "CatCB";
            this.CatCB.Size = new System.Drawing.Size(100, 23);
            this.CatCB.TabIndex = 3;
            this.CatCB.DataSource = Enum.GetValues(typeof(ProductCategory));
            // 
            // IdLabel
            // 
            this.IdLabel.AutoSize = true;
            this.IdLabel.Location = new System.Drawing.Point(32, 25);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(17, 15);
            this.IdLabel.TabIndex = 4;
            this.IdLabel.Text = "Id";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(32, 81);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(39, 15);
            this.NameLabel.TabIndex = 5;
            this.NameLabel.Text = "Name";
            // 
            // CategoryLabel
            // 
            this.CategoryLabel.AutoSize = true;
            this.CategoryLabel.Location = new System.Drawing.Point(32, 137);
            this.CategoryLabel.Name = "CategoryLabel";
            this.CategoryLabel.Size = new System.Drawing.Size(55, 15);
            this.CategoryLabel.TabIndex = 6;
            this.CategoryLabel.Text = "Category";
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(32, 190);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(33, 15);
            this.PriceLabel.TabIndex = 7;
            this.PriceLabel.Text = "Price";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(138, 299);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(71, 40);
            this.OKButton.TabIndex = 8;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ProductDetailsGroup
            // 
            this.ProductDetailsGroup.Controls.Add(this.IdLabel);
            this.ProductDetailsGroup.Controls.Add(this.IdTB);
            this.ProductDetailsGroup.Controls.Add(this.PriceLabel);
            this.ProductDetailsGroup.Controls.Add(this.PriceTB);
            this.ProductDetailsGroup.Controls.Add(this.CategoryLabel);
            this.ProductDetailsGroup.Controls.Add(this.NameTB);
            this.ProductDetailsGroup.Controls.Add(this.NameLabel);
            this.ProductDetailsGroup.Controls.Add(this.CatCB);
            this.ProductDetailsGroup.Location = new System.Drawing.Point(40, 51);
            this.ProductDetailsGroup.Name = "ProductDetailsGroup";
            this.ProductDetailsGroup.Size = new System.Drawing.Size(271, 228);
            this.ProductDetailsGroup.TabIndex = 9;
            this.ProductDetailsGroup.TabStop = false;
            this.ProductDetailsGroup.Text = "Product Details";
            // 
            // AddProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 368);
            this.Controls.Add(this.ProductDetailsGroup);
            this.Controls.Add(this.OKButton);
            this.Name = "AddProductForm";
            this.Text = "AddProductForm";
            this.ProductDetailsGroup.ResumeLayout(false);
            this.ProductDetailsGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label IdLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label CategoryLabel;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.TextBox IdTB;
        private System.Windows.Forms.TextBox NameTB;
        private System.Windows.Forms.ComboBox CatCB;
        private System.Windows.Forms.TextBox PriceTB;
        private System.Windows.Forms.GroupBox ProductDetailsGroup;
        private System.Windows.Forms.Button OKButton;
    }
}