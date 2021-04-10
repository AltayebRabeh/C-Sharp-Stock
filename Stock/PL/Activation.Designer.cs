namespace Stock.PL
{
    partial class Activation
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
            this.txt_serial = new System.Windows.Forms.TextBox();
            this.btn_active = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_exit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_serial
            // 
            this.txt_serial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_serial.Font = new System.Drawing.Font("Arial", 17.75F);
            this.txt_serial.Location = new System.Drawing.Point(12, 95);
            this.txt_serial.MaxLength = 16;
            this.txt_serial.Name = "txt_serial";
            this.txt_serial.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_serial.Size = new System.Drawing.Size(292, 35);
            this.txt_serial.TabIndex = 1;
            this.txt_serial.TextChanged += new System.EventHandler(this.txt_serial_TextChanged);
            // 
            // btn_active
            // 
            this.btn_active.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(103)))), ((int)(((byte)(99)))));
            this.btn_active.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_active.Enabled = false;
            this.btn_active.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(111)))));
            this.btn_active.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(111)))));
            this.btn_active.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_active.Font = new System.Drawing.Font("Arial", 15.75F);
            this.btn_active.ForeColor = System.Drawing.SystemColors.Window;
            this.btn_active.Image = global::Stock.Properties.Resources.Ok_30px;
            this.btn_active.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_active.Location = new System.Drawing.Point(304, 95);
            this.btn_active.Name = "btn_active";
            this.btn_active.Size = new System.Drawing.Size(89, 35);
            this.btn_active.TabIndex = 12;
            this.btn_active.Text = "تفعيل";
            this.btn_active.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_active.UseVisualStyleBackColor = false;
            this.btn_active.Click += new System.EventHandler(this.btn_active_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 32);
            this.label1.TabIndex = 13;
            this.label1.Text = "الرجاء كتابة كود التفعيل";
            // 
            // lbl_exit
            // 
            this.lbl_exit.AutoSize = true;
            this.lbl_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_exit.Location = new System.Drawing.Point(391, 0);
            this.lbl_exit.Name = "lbl_exit";
            this.lbl_exit.Size = new System.Drawing.Size(20, 24);
            this.lbl_exit.TabIndex = 14;
            this.lbl_exit.Text = "x";
            this.lbl_exit.Click += new System.EventHandler(this.lbl_exit_Click);
            // 
            // Activation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 161);
            this.Controls.Add(this.lbl_exit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_active);
            this.Controls.Add(this.txt_serial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Activation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txt_serial;
        private System.Windows.Forms.Button btn_active;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_exit;
    }
}