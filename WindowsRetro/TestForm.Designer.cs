namespace WindowsRetro
{
    partial class TestForm
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
            this.retroButton1 = new WindowsRetro.RetroButton();
            this.retroButton2 = new WindowsRetro.RetroButton();
            this.retroTextBox1 = new WindowsRetro.RetroTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // retroButton1
            // 
            this.retroButton1.Location = new System.Drawing.Point(12, 29);
            this.retroButton1.Name = "retroButton1";
            this.retroButton1.Size = new System.Drawing.Size(120, 30);
            this.retroButton1.Symbol = WindowsRetro.SystemSymbol.Up;
            this.retroButton1.TabIndex = 1;
            this.retroButton1.Text = "retroButton1";
            this.retroButton1.Type = WindowsRetro.ButtonType.Normal;
            // 
            // retroButton2
            // 
            this.retroButton2.Location = new System.Drawing.Point(168, 29);
            this.retroButton2.Name = "retroButton2";
            this.retroButton2.Size = new System.Drawing.Size(120, 30);
            this.retroButton2.Symbol = WindowsRetro.SystemSymbol.Up;
            this.retroButton2.TabIndex = 2;
            this.retroButton2.Text = "retroButton2";
            this.retroButton2.Type = WindowsRetro.ButtonType.Normal;
            // 
            // retroTextBox1
            // 
            this.retroTextBox1.BorderColor = System.Drawing.Color.Black;
            this.retroTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retroTextBox1.Location = new System.Drawing.Point(12, 65);
            this.retroTextBox1.Name = "retroTextBox1";
            this.retroTextBox1.Size = new System.Drawing.Size(276, 20);
            this.retroTextBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(45, 113);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 56);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 272);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.retroTextBox1);
            this.Controls.Add(this.retroButton2);
            this.Controls.Add(this.retroButton1);
            this.Name = "TestForm";
            this.Text = "Exit Windows";

            this.Load += new System.EventHandler(this.TestForm_Load);
            this.Controls.SetChildIndex(this.retroButton1, 0);
            this.Controls.SetChildIndex(this.retroButton2, 0);
            this.Controls.SetChildIndex(this.retroTextBox1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RetroButton retroButton1;
        private RetroButton retroButton2;
        private RetroTextBox retroTextBox1;
        private System.Windows.Forms.Button button1;
    }
}