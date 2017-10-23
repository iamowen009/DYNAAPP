namespace DYNAMatching
{
    partial class ScanMatchingList
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
            this.dgView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pack_Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.ckNissanNMT = new System.Windows.Forms.CheckBox();
            this.ckNissanNPT = new System.Windows.Forms.CheckBox();
            this.ckToyota = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.lbUniqueID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgView
            // 
            this.dgView.AllowUserToAddRows = false;
            this.dgView.AllowUserToDeleteRows = false;
            this.dgView.AllowUserToOrderColumns = true;
            this.dgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Column1,
            this.PartNo,
            this.Pack_Qty});
            this.dgView.Location = new System.Drawing.Point(43, 94);
            this.dgView.Name = "dgView";
            this.dgView.ReadOnly = true;
            this.dgView.Size = new System.Drawing.Size(819, 442);
            this.dgView.TabIndex = 4;
            // 
            // No
            // 
            this.No.DataPropertyName = "No";
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 50;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Customer";
            this.Column1.HeaderText = "Customer";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // PartNo
            // 
            this.PartNo.DataPropertyName = "Part_No";
            this.PartNo.HeaderText = "PartNo";
            this.PartNo.Name = "PartNo";
            this.PartNo.ReadOnly = true;
            this.PartNo.Width = 200;
            // 
            // Pack_Qty
            // 
            this.Pack_Qty.DataPropertyName = "Pack_Qty";
            this.Pack_Qty.HeaderText = "SNP/Quantity";
            this.Pack_Qty.Name = "Pack_Qty";
            this.Pack_Qty.ReadOnly = true;
            this.Pack_Qty.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(376, 674);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 24);
            this.label3.TabIndex = 18;
            this.label3.Text = "Back to Main Menu";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DYNAMatching.Properties.Resources._2017_10_19_221600;
            this.pictureBox2.Location = new System.Drawing.Point(399, 557);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(120, 109);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btSearch
            // 
            this.btSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSearch.Location = new System.Drawing.Point(651, 25);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(216, 49);
            this.btSearch.TabIndex = 19;
            this.btSearch.TabStop = false;
            this.btSearch.Text = "Search (ค้นหา)";
            this.btSearch.UseVisualStyleBackColor = false;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // ckNissanNMT
            // 
            this.ckNissanNMT.AutoSize = true;
            this.ckNissanNMT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckNissanNMT.Location = new System.Drawing.Point(135, 21);
            this.ckNissanNMT.Name = "ckNissanNMT";
            this.ckNissanNMT.Size = new System.Drawing.Size(133, 28);
            this.ckNissanNMT.TabIndex = 20;
            this.ckNissanNMT.TabStop = false;
            this.ckNissanNMT.Text = "Nissan NMT";
            this.ckNissanNMT.UseVisualStyleBackColor = true;
            // 
            // ckNissanNPT
            // 
            this.ckNissanNPT.AutoSize = true;
            this.ckNissanNPT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckNissanNPT.Location = new System.Drawing.Point(289, 21);
            this.ckNissanNPT.Name = "ckNissanNPT";
            this.ckNissanNPT.Size = new System.Drawing.Size(129, 28);
            this.ckNissanNPT.TabIndex = 21;
            this.ckNissanNPT.TabStop = false;
            this.ckNissanNPT.Text = "Nissan NPT";
            this.ckNissanNPT.UseVisualStyleBackColor = true;
            // 
            // ckToyota
            // 
            this.ckToyota.AutoSize = true;
            this.ckToyota.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckToyota.Location = new System.Drawing.Point(448, 21);
            this.ckToyota.Name = "ckToyota";
            this.ckToyota.Size = new System.Drawing.Size(86, 28);
            this.ckToyota.TabIndex = 22;
            this.ckToyota.TabStop = false;
            this.ckToyota.Text = "Toyota";
            this.ckToyota.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 24);
            this.label4.TabIndex = 23;
            this.label4.Text = "Filter :";
            // 
            // txtCustomer
            // 
            this.txtCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomer.Location = new System.Drawing.Point(43, 69);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(195, 19);
            this.txtCustomer.TabIndex = 24;
            this.txtCustomer.TextChanged += new System.EventHandler(this.txtCustomer_TextChanged);
            this.txtCustomer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomer_KeyPress);
            // 
            // lbUniqueID
            // 
            this.lbUniqueID.AutoSize = true;
            this.lbUniqueID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUniqueID.Location = new System.Drawing.Point(44, 539);
            this.lbUniqueID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lbUniqueID.Name = "lbUniqueID";
            this.lbUniqueID.Size = new System.Drawing.Size(43, 16);
            this.lbUniqueID.TabIndex = 25;
            this.lbUniqueID.Text = "Filter :";
            // 
            // ScanMatchingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 733);
            this.Controls.Add(this.lbUniqueID);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ckToyota);
            this.Controls.Add(this.ckNissanNPT);
            this.Controls.Add(this.ckNissanNMT);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.dgView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ScanMatchingList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scan Matching List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ScanMatchingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.CheckBox ckNissanNMT;
        private System.Windows.Forms.CheckBox ckNissanNPT;
        private System.Windows.Forms.CheckBox ckToyota;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pack_Qty;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label lbUniqueID;
    }
}