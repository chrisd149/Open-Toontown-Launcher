using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System;

namespace OpenTTLauncher
{
    partial class OpenTTLauncher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenTTLauncher));
            this.LinkLabel = new System.Windows.Forms.LinkLabel();
            this.LoginButton = new System.Windows.Forms.Button();
            this.UserTextBox = new System.Windows.Forms.TextBox();
            this.PwsdTextBox = new System.Windows.Forms.TextBox();
            this.AuthLabel = new System.Windows.Forms.Label();
            this.PopCounterLabel = new System.Windows.Forms.Label();
            this.PwsdLabel = new System.Windows.Forms.Label();
            this.UserLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.DescLabel = new System.Windows.Forms.Label();
            this.PopLabel = new System.Windows.Forms.Label();
            this.InstructionsButton = new System.Windows.Forms.Button();
            this.SaveAcctButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.QuickLoginButton = new System.Windows.Forms.Button();
            this.QuickLoginComboBox = new System.Windows.Forms.ComboBox();
            this.QuickLoginLabel = new System.Windows.Forms.Label();
            this.RemoveAcctButton = new System.Windows.Forms.Button();
            this.GameDirButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(Path.Combine(Environment.CurrentDirectory, "ImpressBT.ttf"));
            //Font font = new Font(pfc.Families[0], 12, FontStyle.Regular);
            this.SuspendLayout();
            // 
            // LinkLabel
            // 
            this.LinkLabel.AutoSize = true;
            this.LinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.LinkLabel.Font = new System.Drawing.Font(pfc.Families[0], 12F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabel.LinkColor = System.Drawing.Color.Navy;
            this.LinkLabel.Location = new System.Drawing.Point(419, 471);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(159, 25);
            this.LinkLabel.TabIndex = 0;
            this.LinkLabel.TabStop = true;
            this.LinkLabel.Text = "Github repository";
            this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // LoginButton
            // 
            this.LoginButton.BackColor = System.Drawing.Color.Transparent;
            this.LoginButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LoginButton.BackgroundImage")));
            this.LoginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.LoginButton.ForeColor = System.Drawing.Color.Transparent;
            this.LoginButton.Location = new System.Drawing.Point(857, 416);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(120, 85);
            this.LoginButton.TabIndex = 2;
            this.LoginButton.UseVisualStyleBackColor = false;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // UserTextBox
            // 
            this.UserTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.UserTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserTextBox.Location = new System.Drawing.Point(187, 169);
            this.UserTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserTextBox.Name = "UserTextBox";
            this.UserTextBox.Size = new System.Drawing.Size(611, 30);
            this.UserTextBox.TabIndex = 4;
            this.UserTextBox.TextChanged += new System.EventHandler(this.UsernameTextBox_TextChanged);
            // 
            // PwsdTextBox
            // 
            this.PwsdTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.PwsdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PwsdTextBox.Location = new System.Drawing.Point(187, 231);
            this.PwsdTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PwsdTextBox.Name = "PwsdTextBox";
            this.PwsdTextBox.PasswordChar = '*';
            this.PwsdTextBox.Size = new System.Drawing.Size(611, 30);
            this.PwsdTextBox.TabIndex = 5;
            // 
            // AuthLabel
            // 
            this.AuthLabel.AutoSize = true;
            this.AuthLabel.Location = new System.Drawing.Point(309, 284);
            this.AuthLabel.Name = "AuthLabel";
            this.AuthLabel.Size = new System.Drawing.Size(0, 17);
            this.AuthLabel.TabIndex = 9;
            this.AuthLabel.Visible = false;
            this.AuthLabel.Click += new System.EventHandler(this.AuthLabel_Click_2);
            // 
            // PopCounterLabel
            // 
            this.PopCounterLabel.AutoSize = true;
            this.PopCounterLabel.BackColor = System.Drawing.Color.Transparent;
            this.PopCounterLabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.PopCounterLabel.Font = new System.Drawing.Font(pfc.Families[0], 16.2F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PopCounterLabel.ForeColor = System.Drawing.Color.Black;
            this.PopCounterLabel.Location = new System.Drawing.Point(252, 465);
            this.PopCounterLabel.Name = "PopCounterLabel";
            this.PopCounterLabel.Size = new System.Drawing.Size(138, 35);
            this.PopCounterLabel.TabIndex = 11;
            this.PopCounterLabel.Text = "population";
            this.PopCounterLabel.Click += new System.EventHandler(this.PopCounterLabel_Click);
            // 
            // PwsdLabel
            // 
            this.PwsdLabel.AutoSize = true;
            this.PwsdLabel.BackColor = System.Drawing.Color.Transparent;
            this.PwsdLabel.Font = new System.Drawing.Font(pfc.Families[0], 13.8F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PwsdLabel.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.PwsdLabel.Location = new System.Drawing.Point(19, 230);
            this.PwsdLabel.Name = "PwsdLabel";
            this.PwsdLabel.Size = new System.Drawing.Size(119, 30);
            this.PwsdLabel.TabIndex = 13;
            this.PwsdLabel.Text = "Password:";
            this.PwsdLabel.Click += new System.EventHandler(this.PwsdLabel_Click_3);
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.BackColor = System.Drawing.Color.Transparent;
            this.UserLabel.Font = new System.Drawing.Font(pfc.Families[0], 13.8F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserLabel.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.UserLabel.Location = new System.Drawing.Point(17, 166);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(123, 30);
            this.UserLabel.TabIndex = 14;
            this.UserLabel.Text = "Username:";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font(pfc.Families[0], 25.8F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(243, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(486, 54);
            this.TitleLabel.TabIndex = 15;
            this.TitleLabel.Text = "Open Toontown Launcher";
            this.TitleLabel.Click += new System.EventHandler(this.TitleLabel_Click);
            // 
            // DescLabel
            // 
            this.DescLabel.AutoSize = true;
            this.DescLabel.BackColor = System.Drawing.Color.Transparent;
            this.DescLabel.Font = new System.Drawing.Font(pfc.Families[0], 12F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DescLabel.Location = new System.Drawing.Point(307, 64);
            this.DescLabel.Name = "DescLabel";
            this.DescLabel.Size = new System.Drawing.Size(388, 25);
            this.DescLabel.TabIndex = 16;
            this.DescLabel.Text = "An open source unoffical Toontown launcher";
            // 
            // PopLabel
            // 
            this.PopLabel.AutoSize = true;
            this.PopLabel.BackColor = System.Drawing.Color.Transparent;
            this.PopLabel.Font = new System.Drawing.Font(pfc.Families[0], 16.2F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PopLabel.Location = new System.Drawing.Point(3, 464);
            this.PopLabel.Name = "PopLabel";
            this.PopLabel.Size = new System.Drawing.Size(244, 35);
            this.PopLabel.TabIndex = 17;
            this.PopLabel.Text = "Current Population:";
            this.PopLabel.Click += new System.EventHandler(this.PopLabel_Click);
            // 
            // InstructionsButton
            // 
            this.InstructionsButton.BackColor = System.Drawing.Color.White;
            this.InstructionsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.InstructionsButton.Font = new System.Drawing.Font(pfc.Families[0], 12F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionsButton.ForeColor = System.Drawing.Color.Black;
            this.InstructionsButton.Location = new System.Drawing.Point(429, 340);
            this.InstructionsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InstructionsButton.Name = "InstructionsButton";
            this.InstructionsButton.Size = new System.Drawing.Size(129, 38);
            this.InstructionsButton.TabIndex = 18;
            this.InstructionsButton.Text = "Instructions";
            this.InstructionsButton.UseVisualStyleBackColor = false;
            this.InstructionsButton.Click += new System.EventHandler(this.InstructionsButton_Click);
            // 
            // SaveAcctButton
            // 
            this.SaveAcctButton.BackColor = System.Drawing.Color.White;
            this.SaveAcctButton.Font = new System.Drawing.Font(pfc.Families[0], 10F);
            this.SaveAcctButton.Location = new System.Drawing.Point(187, 284);
            this.SaveAcctButton.Margin = new System.Windows.Forms.Padding(4);
            this.SaveAcctButton.Name = "SaveAcctButton";
            this.SaveAcctButton.Size = new System.Drawing.Size(139, 37);
            this.SaveAcctButton.TabIndex = 19;
            this.SaveAcctButton.Text = "Save Account";
            this.SaveAcctButton.UseVisualStyleBackColor = false;
            this.SaveAcctButton.Click += new System.EventHandler(this.SaveAcctButton_Click);
            // 
            // QuickLoginButton
            // 
            this.QuickLoginButton.BackColor = System.Drawing.Color.White;
            this.QuickLoginButton.Font = new System.Drawing.Font(pfc.Families[0], 10F);
            this.QuickLoginButton.Location = new System.Drawing.Point(501, 284);
            this.QuickLoginButton.Margin = new System.Windows.Forms.Padding(4);
            this.QuickLoginButton.Name = "QuickLoginButton";
            this.QuickLoginButton.Size = new System.Drawing.Size(128, 37);
            this.QuickLoginButton.TabIndex = 20;
            this.QuickLoginButton.Text = "QuickLogin";
            this.QuickLoginButton.UseVisualStyleBackColor = false;
            this.QuickLoginButton.Click += new System.EventHandler(this.QuickLoginButton_Click);
            // 
            // QuickLoginComboBox
            // 
            this.QuickLoginComboBox.BackColor = System.Drawing.Color.White;
            this.QuickLoginComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QuickLoginComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.QuickLoginComboBox.FormattingEnabled = true;
            this.QuickLoginComboBox.Location = new System.Drawing.Point(637, 300);
            this.QuickLoginComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.QuickLoginComboBox.Name = "QuickLoginComboBox";
            this.QuickLoginComboBox.Size = new System.Drawing.Size(160, 24);
            this.QuickLoginComboBox.TabIndex = 21;
            this.QuickLoginComboBox.SelectedIndexChanged += new System.EventHandler(this.QuickLoginComboBox_SelectedIndexChanged);
            // 
            // QuickLoginLabel
            // 
            this.QuickLoginLabel.AutoSize = true;
            this.QuickLoginLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.QuickLoginLabel.Font = new System.Drawing.Font(pfc.Families[0], 9.75F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuickLoginLabel.Location = new System.Drawing.Point(645, 274);
            this.QuickLoginLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.QuickLoginLabel.Name = "QuickLoginLabel";
            this.QuickLoginLabel.Size = new System.Drawing.Size(142, 21);
            this.QuickLoginLabel.TabIndex = 22;
            this.QuickLoginLabel.Text = "Select An Account";
            this.QuickLoginLabel.Click += new System.EventHandler(this.QuickLoginLabel_Click);
            // 
            // RemoveAcctButton
            // 
            this.RemoveAcctButton.BackColor = System.Drawing.Color.White;
            this.RemoveAcctButton.Font = new System.Drawing.Font(pfc.Families[0], 10F);
            this.RemoveAcctButton.Location = new System.Drawing.Point(335, 284);
            this.RemoveAcctButton.Margin = new System.Windows.Forms.Padding(4);
            this.RemoveAcctButton.Name = "RemoveAcctButton";
            this.RemoveAcctButton.Size = new System.Drawing.Size(157, 37);
            this.RemoveAcctButton.TabIndex = 23;
            this.RemoveAcctButton.Text = "Remove Account";
            this.RemoveAcctButton.UseVisualStyleBackColor = false;
            this.RemoveAcctButton.Click += new System.EventHandler(this.RemoveAcctButton_Click);
            // 
            // GameDirButton
            // 
            this.GameDirButton.BackColor = System.Drawing.Color.White;
            this.GameDirButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.GameDirButton.Font = new System.Drawing.Font(pfc.Families[0], 10F);
            this.GameDirButton.ForeColor = System.Drawing.Color.Black;
            this.GameDirButton.Location = new System.Drawing.Point(9, 406);
            this.GameDirButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GameDirButton.Name = "GameDirButton";
            this.GameDirButton.Size = new System.Drawing.Size(186, 38);
            this.GameDirButton.TabIndex = 25;
            this.GameDirButton.Text = "Set Game Directory";
            this.GameDirButton.UseVisualStyleBackColor = false;
            this.GameDirButton.Click += new System.EventHandler(this.GameDirButton_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.BackColor = System.Drawing.Color.Transparent;
            this.VersionLabel.Font = new System.Drawing.Font(pfc.Families[0], 7.8F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.VersionLabel.Location = new System.Drawing.Point(943, -1);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.VersionLabel.Size = new System.Drawing.Size(45, 17);
            this.VersionLabel.TabIndex = 26;
            this.VersionLabel.Text = "v1.0.0";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.VersionLabel.Click += new System.EventHandler(this.VersionLabel_Click);
            // 
            // OpenTTLauncher
            // 
            this.AcceptButton = this.LoginButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(989, 510);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.GameDirButton);
            this.Controls.Add(this.RemoveAcctButton);
            this.Controls.Add(this.QuickLoginLabel);
            this.Controls.Add(this.QuickLoginComboBox);
            this.Controls.Add(this.QuickLoginButton);
            this.Controls.Add(this.SaveAcctButton);
            this.Controls.Add(this.InstructionsButton);
            this.Controls.Add(this.PopLabel);
            this.Controls.Add(this.DescLabel);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.PwsdLabel);
            this.Controls.Add(this.PopCounterLabel);
            this.Controls.Add(this.AuthLabel);
            this.Controls.Add(this.PwsdTextBox);
            this.Controls.Add(this.UserTextBox);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.LinkLabel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenTTLauncher";
            this.Text = "Open Toontown Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel LinkLabel;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox UserTextBox;
        private System.Windows.Forms.TextBox PwsdTextBox;
        private System.Windows.Forms.Label AuthLabel;
        private System.Windows.Forms.Label PopCounterLabel;
        private System.Windows.Forms.Label PwsdLabel;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label DescLabel;
        private System.Windows.Forms.Label PopLabel;
        private System.Windows.Forms.Button InstructionsButton;
        private System.Windows.Forms.Button SaveAcctButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button QuickLoginButton;
        private System.Windows.Forms.ComboBox QuickLoginComboBox;
        private System.Windows.Forms.Label QuickLoginLabel;
        private System.Windows.Forms.Button RemoveAcctButton;
        private System.Windows.Forms.Button GameDirButton;
        private System.Windows.Forms.Label VersionLabel;
    }
}

