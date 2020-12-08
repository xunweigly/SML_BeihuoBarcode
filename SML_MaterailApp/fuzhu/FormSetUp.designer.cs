namespace fuzhu
{
    partial class FormSetUp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtkuan = new System.Windows.Forms.TextBox();
            this.txtgao = new System.Windows.Forms.TextBox();
            this.txtshang = new System.Windows.Forms.TextBox();
            this.txtxia = new System.Windows.Forms.TextBox();
            this.txtzuo = new System.Windows.Forms.TextBox();
            this.txtyou = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "宽度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "高度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "上：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "左：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "下：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(171, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "右：";
            // 
            // txtkuan
            // 
            this.txtkuan.Location = new System.Drawing.Point(105, 31);
            this.txtkuan.Name = "txtkuan";
            this.txtkuan.Size = new System.Drawing.Size(67, 21);
            this.txtkuan.TabIndex = 6;
            // 
            // txtgao
            // 
            this.txtgao.Location = new System.Drawing.Point(105, 66);
            this.txtgao.Name = "txtgao";
            this.txtgao.Size = new System.Drawing.Size(67, 21);
            this.txtgao.TabIndex = 7;
            // 
            // txtshang
            // 
            this.txtshang.Location = new System.Drawing.Point(81, 135);
            this.txtshang.Name = "txtshang";
            this.txtshang.Size = new System.Drawing.Size(67, 21);
            this.txtshang.TabIndex = 8;
            // 
            // txtxia
            // 
            this.txtxia.Location = new System.Drawing.Point(83, 177);
            this.txtxia.Name = "txtxia";
            this.txtxia.Size = new System.Drawing.Size(67, 21);
            this.txtxia.TabIndex = 9;
            // 
            // txtzuo
            // 
            this.txtzuo.Location = new System.Drawing.Point(206, 135);
            this.txtzuo.Name = "txtzuo";
            this.txtzuo.Size = new System.Drawing.Size(67, 21);
            this.txtzuo.TabIndex = 10;
            // 
            // txtyou
            // 
            this.txtyou.Location = new System.Drawing.Point(206, 177);
            this.txtyou.Name = "txtyou";
            this.txtyou.Size = new System.Drawing.Size(67, 21);
            this.txtyou.TabIndex = 11;
            this.txtyou.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "纸张大小(0.1mm)：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(46, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "页边距(0.1mm):";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 237);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSetUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 290);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtyou);
            this.Controls.Add(this.txtzuo);
            this.Controls.Add(this.txtxia);
            this.Controls.Add(this.txtshang);
            this.Controls.Add(this.txtgao);
            this.Controls.Add(this.txtkuan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormSetUp";
            this.Text = "打印纸张设置";
            this.Load += new System.EventHandler(this.FormSetUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtkuan;
        private System.Windows.Forms.TextBox txtgao;
        private System.Windows.Forms.TextBox txtshang;
        private System.Windows.Forms.TextBox txtxia;
        private System.Windows.Forms.TextBox txtzuo;
        private System.Windows.Forms.TextBox txtyou;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}