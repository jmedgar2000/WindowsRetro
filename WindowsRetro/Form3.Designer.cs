namespace WindowsRetro
{
    partial class Form3: Windows31Form
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
            this.SuspendLayout();
            // 
            // retroButton1
            // 
            this.retroButton1.Location = new System.Drawing.Point(26, 50);
            this.retroButton1.Name = "retroButton1";
            this.retroButton1.Size = new System.Drawing.Size(120, 30);
            this.retroButton1.Symbol = WindowsRetro.SystemSymbol.Up;
            this.retroButton1.TabIndex = 2;
            this.retroButton1.Text = "retroButton1";
            this.retroButton1.Type = WindowsRetro.ButtonType.Normal;
            // 
            // retroButton2
            // 
            this.retroButton2.Location = new System.Drawing.Point(26, 86);
            this.retroButton2.Name = "retroButton2";
            this.retroButton2.Size = new System.Drawing.Size(120, 30);
            this.retroButton2.Symbol = WindowsRetro.SystemSymbol.Up;
            this.retroButton2.TabIndex = 3;
            this.retroButton2.Text = "retroButton2";
            this.retroButton2.Type = WindowsRetro.ButtonType.Normal;
            // 
            // retroTextBox1
            // 
            this.retroTextBox1.BorderColor = System.Drawing.Color.Black;
            this.retroTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retroTextBox1.Location = new System.Drawing.Point(152, 86);
            this.retroTextBox1.Multiline = true;
            this.retroTextBox1.Name = "retroTextBox1";
            this.retroTextBox1.Size = new System.Drawing.Size(171, 30);
            this.retroTextBox1.TabIndex = 4;
            // 
            // Form3
            // 
            this.ClientSize = new System.Drawing.Size(356, 311);
            this.Controls.Add(this.retroTextBox1);
            this.Controls.Add(this.retroButton2);
            this.Controls.Add(this.retroButton1);
            this.Name = "Form3";
            this.Text = "text-ccc";
            this.Controls.SetChildIndex(this.retroButton1, 0);
            this.Controls.SetChildIndex(this.retroButton2, 0);
            this.Controls.SetChildIndex(this.retroTextBox1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RetroButton retroButton1;
        private RetroButton retroButton2;
        private RetroTextBox retroTextBox1;
    }
}
