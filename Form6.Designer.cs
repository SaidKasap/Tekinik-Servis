
namespace TServis1
{
    partial class Form6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form6));
            this.ad = new System.Windows.Forms.TextBox();
            this.tel = new System.Windows.Forms.TextBox();
            this.adres = new System.Windows.Forms.TextBox();
            this.srmlu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.sorumlu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ad
            // 
            this.ad.Location = new System.Drawing.Point(79, 17);
            this.ad.Name = "ad";
            this.ad.Size = new System.Drawing.Size(389, 20);
            this.ad.TabIndex = 0;
            // 
            // tel
            // 
            this.tel.Location = new System.Drawing.Point(79, 43);
            this.tel.Name = "tel";
            this.tel.Size = new System.Drawing.Size(389, 20);
            this.tel.TabIndex = 1;
            // 
            // adres
            // 
            this.adres.Location = new System.Drawing.Point(79, 69);
            this.adres.Name = "adres";
            this.adres.Size = new System.Drawing.Size(389, 20);
            this.adres.TabIndex = 2;
            // 
            // srmlu
            // 
            this.srmlu.Location = new System.Drawing.Point(79, 95);
            this.srmlu.Name = "srmlu";
            this.srmlu.Size = new System.Drawing.Size(389, 20);
            this.srmlu.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FİRMA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "TELEFON";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ADRES";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(176, 159);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Kaydet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // sorumlu
            // 
            this.sorumlu.AutoSize = true;
            this.sorumlu.Location = new System.Drawing.Point(12, 102);
            this.sorumlu.Name = "sorumlu";
            this.sorumlu.Size = new System.Drawing.Size(61, 13);
            this.sorumlu.TabIndex = 8;
            this.sorumlu.Text = "SORUMLU";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 217);
            this.Controls.Add(this.sorumlu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.srmlu);
            this.Controls.Add(this.adres);
            this.Controls.Add(this.tel);
            this.Controls.Add(this.ad);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form6";
            this.Text = "FİRMA EKLE";
            this.Load += new System.EventHandler(this.Form6_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ad;
        private System.Windows.Forms.TextBox tel;
        private System.Windows.Forms.TextBox adres;
        private System.Windows.Forms.TextBox srmlu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label sorumlu;
    }
}