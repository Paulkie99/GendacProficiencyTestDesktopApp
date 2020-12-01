using System;
using System.Net.Http.Headers;

namespace APIConsumer
{
    partial class APIConsumerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ProductListLabel = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.GetButton = new System.Windows.Forms.Button();
            this.GetSorted = new System.Windows.Forms.Button();
            this.ProductGrid = new System.Windows.Forms.DataGridView();
            this.AddButton = new System.Windows.Forms.Button();
            this.GetIdButton = new System.Windows.Forms.Button();
            this.CountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductListLabel
            // 
            this.ProductListLabel.AutoSize = true;
            this.ProductListLabel.Location = new System.Drawing.Point(207, 23);
            this.ProductListLabel.Name = "ProductListLabel";
            this.ProductListLabel.Size = new System.Drawing.Size(73, 15);
            this.ProductListLabel.TabIndex = 0;
            this.ProductListLabel.Text = "Product List:";
            // 
            // DeleteButton
            // 
            this.DeleteButton.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.DeleteButton.Location = new System.Drawing.Point(570, 129);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(92, 38);
            this.DeleteButton.TabIndex = 9;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditButton
            // 
            this.EditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.EditButton.Location = new System.Drawing.Point(570, 85);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(92, 38);
            this.EditButton.TabIndex = 4;
            this.EditButton.Text = "Edit...";
            this.EditButton.UseVisualStyleBackColor = false;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // GetButton
            // 
            this.GetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GetButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.GetButton.Location = new System.Drawing.Point(559, 173);
            this.GetButton.Name = "GetButton";
            this.GetButton.Size = new System.Drawing.Size(112, 49);
            this.GetButton.TabIndex = 5;
            this.GetButton.Text = "Get Unordered List";
            this.GetButton.UseVisualStyleBackColor = false;
            this.GetButton.Click += new System.EventHandler(this.GetButton_Click);
            // 
            // GetSorted
            // 
            this.GetSorted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GetSorted.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.GetSorted.Location = new System.Drawing.Point(559, 228);
            this.GetSorted.Name = "GetSorted";
            this.GetSorted.Size = new System.Drawing.Size(112, 73);
            this.GetSorted.TabIndex = 6;
            this.GetSorted.Text = "Get Paged, Sorted, Filtered List";
            this.GetSorted.UseVisualStyleBackColor = false;
            this.GetSorted.Click += new System.EventHandler(this.GetSorted_Click);
            // 
            // ProductGrid
            // 
            this.ProductGrid.AllowUserToAddRows = false;
            this.ProductGrid.AllowUserToDeleteRows = false;
            this.ProductGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.ProductGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductGrid.DefaultCellStyle = dataGridViewCellStyle4;
            this.ProductGrid.GridColor = System.Drawing.SystemColors.ControlText;
            this.ProductGrid.Location = new System.Drawing.Point(44, 41);
            this.ProductGrid.MaximumSize = new System.Drawing.Size(450, 0);
            this.ProductGrid.MinimumSize = new System.Drawing.Size(450, 315);
            this.ProductGrid.MultiSelect = false;
            this.ProductGrid.Name = "ProductGrid";
            this.ProductGrid.ReadOnly = true;
            this.ProductGrid.RowTemplate.Height = 25;
            this.ProductGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductGrid.Size = new System.Drawing.Size(450, 315);
            this.ProductGrid.TabIndex = 7;
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.AddButton.Location = new System.Drawing.Point(570, 41);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(92, 39);
            this.AddButton.TabIndex = 10;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = false;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // GetIdButton
            // 
            this.GetIdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GetIdButton.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.GetIdButton.Location = new System.Drawing.Point(559, 307);
            this.GetIdButton.Name = "GetIdButton";
            this.GetIdButton.Size = new System.Drawing.Size(112, 49);
            this.GetIdButton.TabIndex = 11;
            this.GetIdButton.Text = "Get by Id";
            this.GetIdButton.UseVisualStyleBackColor = false;
            this.GetIdButton.Click += new System.EventHandler(this.GetIdButton_Click);
            // 
            // CountLabel
            // 
            this.CountLabel.AutoSize = true;
            this.CountLabel.Location = new System.Drawing.Point(283, 23);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(0, 15);
            this.CountLabel.TabIndex = 12;
            // 
            // APIConsumerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(717, 375);
            this.Controls.Add(this.CountLabel);
            this.Controls.Add(this.GetIdButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ProductGrid);
            this.Controls.Add(this.GetSorted);
            this.Controls.Add(this.GetButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.ProductListLabel);
            this.ForeColor = System.Drawing.Color.White;
            this.MinimumSize = new System.Drawing.Size(730, 400);
            this.Name = "APIConsumerForm";
            this.Text = "Gendac Software Engineering API Consumer";
            ((System.ComponentModel.ISupportInitialize)(this.ProductGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ProductListLabel;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button GetButton;
        private System.Windows.Forms.Button GetSorted;
        public System.Windows.Forms.DataGridView ProductGrid;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button GetIdButton;
        public System.Windows.Forms.Label CountLabel;
    }
}

