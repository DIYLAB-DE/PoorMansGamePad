
namespace PoorMansGamePad {
    partial class FormMain {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.ButtonSelect = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.ButtonA = new System.Windows.Forms.Button();
            this.ButtonB = new System.Windows.Forms.Button();
            this.LabelUSB = new System.Windows.Forms.Label();
            this.ButtonRight = new System.Windows.Forms.Button();
            this.ButtonLeft = new System.Windows.Forms.Button();
            this.ButtonUp = new System.Windows.Forms.Button();
            this.ButtonDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ButtonSelect.FlatAppearance.BorderSize = 0;
            this.ButtonSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonSelect.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.ButtonSelect.ForeColor = System.Drawing.Color.White;
            this.ButtonSelect.Location = new System.Drawing.Point(11, 10);
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(207, 81);
            this.ButtonSelect.TabIndex = 7;
            this.ButtonSelect.Text = "SELECT/RESET (S)";
            this.ButtonSelect.UseVisualStyleBackColor = false;
            this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonStart
            // 
            this.ButtonStart.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ButtonStart.FlatAppearance.BorderSize = 0;
            this.ButtonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonStart.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonStart.ForeColor = System.Drawing.Color.White;
            this.ButtonStart.Location = new System.Drawing.Point(11, 97);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(207, 81);
            this.ButtonStart.TabIndex = 8;
            this.ButtonStart.Text = "START/PAUSE (P)";
            this.ButtonStart.UseVisualStyleBackColor = false;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonA
            // 
            this.ButtonA.BackColor = System.Drawing.Color.SandyBrown;
            this.ButtonA.FlatAppearance.BorderSize = 0;
            this.ButtonA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonA.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonA.ForeColor = System.Drawing.Color.White;
            this.ButtonA.Location = new System.Drawing.Point(11, 184);
            this.ButtonA.Name = "ButtonA";
            this.ButtonA.Size = new System.Drawing.Size(207, 81);
            this.ButtonA.TabIndex = 9;
            this.ButtonA.Text = "A";
            this.ButtonA.UseVisualStyleBackColor = false;
            this.ButtonA.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonB
            // 
            this.ButtonB.BackColor = System.Drawing.Color.SandyBrown;
            this.ButtonB.FlatAppearance.BorderSize = 0;
            this.ButtonB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonB.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonB.ForeColor = System.Drawing.Color.White;
            this.ButtonB.Location = new System.Drawing.Point(11, 271);
            this.ButtonB.Name = "ButtonB";
            this.ButtonB.Size = new System.Drawing.Size(207, 81);
            this.ButtonB.TabIndex = 10;
            this.ButtonB.Text = "B";
            this.ButtonB.UseVisualStyleBackColor = false;
            this.ButtonB.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // LabelUSB
            // 
            this.LabelUSB.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelUSB.ForeColor = System.Drawing.Color.RoyalBlue;
            this.LabelUSB.Image = global::PoorMansGamePad.Properties.Resources.usb_disconnected_64;
            this.LabelUSB.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LabelUSB.Location = new System.Drawing.Point(592, 263);
            this.LabelUSB.Name = "LabelUSB";
            this.LabelUSB.Size = new System.Drawing.Size(80, 89);
            this.LabelUSB.TabIndex = 11;
            this.LabelUSB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // ButtonRight
            // 
            this.ButtonRight.BackColor = System.Drawing.Color.White;
            this.ButtonRight.BackgroundImage = global::PoorMansGamePad.Properties.Resources.keyboard_key_right;
            this.ButtonRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonRight.FlatAppearance.BorderSize = 0;
            this.ButtonRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonRight.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonRight.ForeColor = System.Drawing.Color.White;
            this.ButtonRight.Location = new System.Drawing.Point(518, 116);
            this.ButtonRight.Name = "ButtonRight";
            this.ButtonRight.Size = new System.Drawing.Size(128, 128);
            this.ButtonRight.TabIndex = 3;
            this.ButtonRight.UseVisualStyleBackColor = false;
            this.ButtonRight.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonLeft
            // 
            this.ButtonLeft.BackColor = System.Drawing.Color.White;
            this.ButtonLeft.BackgroundImage = global::PoorMansGamePad.Properties.Resources.keyboard_key_left;
            this.ButtonLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonLeft.FlatAppearance.BorderSize = 0;
            this.ButtonLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonLeft.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLeft.ForeColor = System.Drawing.Color.White;
            this.ButtonLeft.Location = new System.Drawing.Point(250, 116);
            this.ButtonLeft.Name = "ButtonLeft";
            this.ButtonLeft.Size = new System.Drawing.Size(128, 128);
            this.ButtonLeft.TabIndex = 2;
            this.ButtonLeft.UseVisualStyleBackColor = false;
            this.ButtonLeft.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonUp
            // 
            this.ButtonUp.BackColor = System.Drawing.Color.White;
            this.ButtonUp.BackgroundImage = global::PoorMansGamePad.Properties.Resources.keyboard_key_up;
            this.ButtonUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonUp.FlatAppearance.BorderSize = 0;
            this.ButtonUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonUp.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonUp.ForeColor = System.Drawing.Color.White;
            this.ButtonUp.Location = new System.Drawing.Point(384, 50);
            this.ButtonUp.Name = "ButtonUp";
            this.ButtonUp.Size = new System.Drawing.Size(128, 128);
            this.ButtonUp.TabIndex = 1;
            this.ButtonUp.UseVisualStyleBackColor = false;
            this.ButtonUp.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonDown
            // 
            this.ButtonDown.BackColor = System.Drawing.Color.White;
            this.ButtonDown.BackgroundImage = global::PoorMansGamePad.Properties.Resources.keyboard_key_down;
            this.ButtonDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonDown.FlatAppearance.BorderSize = 0;
            this.ButtonDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDown.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDown.ForeColor = System.Drawing.Color.White;
            this.ButtonDown.Location = new System.Drawing.Point(384, 184);
            this.ButtonDown.Name = "ButtonDown";
            this.ButtonDown.Size = new System.Drawing.Size(128, 128);
            this.ButtonDown.TabIndex = 0;
            this.ButtonDown.UseVisualStyleBackColor = false;
            this.ButtonDown.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.LabelUSB);
            this.Controls.Add(this.ButtonB);
            this.Controls.Add(this.ButtonA);
            this.Controls.Add(this.ButtonStart);
            this.Controls.Add(this.ButtonSelect);
            this.Controls.Add(this.ButtonRight);
            this.Controls.Add(this.ButtonLeft);
            this.Controls.Add(this.ButtonUp);
            this.Controls.Add(this.ButtonDown);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 400);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "FormMain";
            this.Text = "PoorMan´s GamePad";
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonDown;
        private System.Windows.Forms.Button ButtonUp;
        private System.Windows.Forms.Button ButtonLeft;
        private System.Windows.Forms.Button ButtonRight;
        private System.Windows.Forms.Button ButtonSelect;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.Button ButtonA;
        private System.Windows.Forms.Button ButtonB;
        private System.Windows.Forms.Label LabelUSB;
    }
}

