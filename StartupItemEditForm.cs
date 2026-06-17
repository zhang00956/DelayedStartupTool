using System;
using System.Windows.Forms;

namespace DelayedStartupTool
{
    public partial class StartupItemEditForm : Form
    {
        public int DelaySeconds { get; private set; }
        public string Arguments { get; private set; }

        public StartupItemEditForm(int currentDelay, string currentArgs)
        {
            InitializeComponent();
            numericUpDownDelay.Value = currentDelay;
            textBoxArgs.Text = currentArgs ?? "";
            DelaySeconds = currentDelay;
            Arguments = currentArgs ?? "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DelaySeconds = (int)numericUpDownDelay.Value;
            Arguments = textBoxArgs.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.labelPrompt = new System.Windows.Forms.Label();
            this.numericUpDownDelay = new System.Windows.Forms.NumericUpDown();
            this.labelArgs = new System.Windows.Forms.Label();
            this.textBoxArgs = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.labelTip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).BeginInit();
            this.SuspendLayout();
            //
            // labelPrompt
            //
            this.labelPrompt.AutoSize = true;
            this.labelPrompt.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelPrompt.Location = new System.Drawing.Point(20, 25);
            this.labelPrompt.Name = "labelPrompt";
            this.labelPrompt.Size = new System.Drawing.Size(104, 17);
            this.labelPrompt.TabIndex = 0;
            this.labelPrompt.Text = "延迟启动时间(秒):";
            //
            // numericUpDownDelay
            //
            this.numericUpDownDelay.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.numericUpDownDelay.Location = new System.Drawing.Point(140, 23);
            this.numericUpDownDelay.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.numericUpDownDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDelay.Name = "numericUpDownDelay";
            this.numericUpDownDelay.Size = new System.Drawing.Size(180, 23);
            this.numericUpDownDelay.TabIndex = 1;
            this.numericUpDownDelay.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            //
            // labelArgs
            //
            this.labelArgs.AutoSize = true;
            this.labelArgs.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelArgs.Location = new System.Drawing.Point(20, 65);
            this.labelArgs.Name = "labelArgs";
            this.labelArgs.Size = new System.Drawing.Size(104, 17);
            this.labelArgs.TabIndex = 2;
            this.labelArgs.Text = "启动参数(可选):";
            //
            // textBoxArgs
            //
            this.textBoxArgs.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBoxArgs.Location = new System.Drawing.Point(140, 62);
            this.textBoxArgs.Name = "textBoxArgs";
            this.textBoxArgs.Size = new System.Drawing.Size(300, 23);
            this.textBoxArgs.TabIndex = 3;
            //
            // labelTip
            //
            this.labelTip.AutoSize = true;
            this.labelTip.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.labelTip.ForeColor = System.Drawing.Color.Gray;
            this.labelTip.Location = new System.Drawing.Point(140, 88);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(280, 16);
            this.labelTip.TabIndex = 4;
            this.labelTip.Text = "例如: -startup (启动到托盘), -minimized (最小化)";
            //
            // btnOK
            //
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnOK.Location = new System.Drawing.Point(120, 125);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 32);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btnCancel.Location = new System.Drawing.Point(240, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 32);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // StartupItemEditForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 181);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelTip);
            this.Controls.Add(this.textBoxArgs);
            this.Controls.Add(this.labelArgs);
            this.Controls.Add(this.numericUpDownDelay);
            this.Controls.Add(this.labelPrompt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupItemEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑启动项";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelPrompt;
        private System.Windows.Forms.NumericUpDown numericUpDownDelay;
        private System.Windows.Forms.Label labelArgs;
        private System.Windows.Forms.TextBox textBoxArgs;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
