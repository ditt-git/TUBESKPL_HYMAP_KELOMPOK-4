namespace Admin
{
    partial class Login
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
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            Username = new TextBox();
            Passw = new TextBox();
            submit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(214, 59);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(214, 150);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 1;
            label2.Text = "Password";
            // 
            // Username
            // 
            Username.Location = new Point(217, 102);
            Username.Margin = new Padding(3, 2, 3, 2);
            Username.Name = "Username";
            Username.Size = new Size(110, 23);
            Username.TabIndex = 2;
            // 
            // Passw
            // 
            Passw.Location = new Point(214, 200);
            Passw.Margin = new Padding(3, 2, 3, 2);
            Passw.Name = "Passw";
            Passw.Size = new Size(110, 23);
            Passw.TabIndex = 3;
            // 
            // submit
            // 
            submit.Location = new Point(306, 284);
            submit.Margin = new Padding(3, 2, 3, 2);
            submit.Name = "submit";
            submit.Size = new Size(82, 22);
            submit.TabIndex = 4;
            submit.Text = "go";
            submit.UseVisualStyleBackColor = true;
            submit.Click += submit_Click;
            // 
            // Login
            // 
            AcceptButton = submit;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(submit);
            Controls.Add(Passw);
            Controls.Add(Username);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Login";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox Username;
        private TextBox Passw;
        private Button submit;
    }
}
