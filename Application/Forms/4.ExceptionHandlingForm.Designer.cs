namespace Application
{
    partial class ExceptionHandlingForm
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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(182, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(75, 73);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(304, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "EXCEPTION HANDLING USING \"ContinueWith\"";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.continueWith_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(75, 141);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(304, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "EXCEPTION HANDLING USING \"async\" keyword";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.asyncAwait_Click);
            // 
            // ExceptionHandlingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 238);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Name = "ExceptionHandlingForm";
            this.Text = "ExceptionHandlingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}