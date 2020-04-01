namespace Application
{
    partial class VoidMethodsForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(241, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "3. PARALLEL NOT AWAITED CALL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.EXECUTION_IN_PARALLEL_DUE_TO_NOT_AWAITED_CALL);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(447, 206);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(232, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "6. ASYNC CALL using async keyword";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ASYNCHRONOUS_EXECUTION_USING_AWAIT_KEYWORD);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(447, 255);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(232, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "7. ASYNC CALL without switching of context";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.ASYNCHRONOUS_EXECUTION_USING_AWAIT_KEYWORD_WITHOUT_SWITCHING_OF_SynchronisationContext_AFTER_EXECUTION_OF_LIBRARY_METHOD);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(43, 116);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(241, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "1. BLOCKING UI CALL with DEADLOCK";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.BLOCKING_OF_UI_THREAD_WITH_DEADLOCK_DUE_TO_ConfigureAwaitTrue_IN_THE_LIBRARY_METHOD);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(43, 159);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(241, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "2. BLOCKING UI CALL";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.BLOCKING_OF_UI_THREAD_BUT_WITHOUT_DEADLOCK_BECAUSE_OF_ConfigureAwaitFalse_IN_THE_LIBRARY_METHOD);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(447, 116);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(232, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "4. ASYNC CALL using GetAwaiter";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.ASYNCHRONOUS_EXECUTION_USING_GetAwaiterMethod);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(447, 159);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(232, 23);
            this.button8.TabIndex = 8;
            this.button8.Text = "5. ASYNC CALL using ContinueWith";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.ASYNCHRONOUS_EXECUTION_USING_ContinueWithMethod);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "PROBLEMATIC METHODS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(501, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "ASYNC METHODS:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(198, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(354, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Methods used for operation call without getting result";
            // 
            // MethodsInvokingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 324);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "VoidMethodsForm";
            this.Text = "VoidMethodsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}