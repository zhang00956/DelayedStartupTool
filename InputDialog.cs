using System;
using System.Drawing;
using System.Windows.Forms;

namespace DelayedStartupTool
{
    public class InputDialog : Form
    {
        private Label lblPrompt;
        private TextBox txtInput;
        private Button btnOK;
        private Button btnCancel;

        public string InputValue { get; private set; }

        public InputDialog(string prompt, string title, string defaultValue)
        {
            InitializeComponent(prompt, title, defaultValue);
        }

        private void InitializeComponent(string prompt, string title, string defaultValue)
        {
            this.Text = title;
            this.Size = new Size(400, 150);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblPrompt = new Label
            {
                Text = prompt,
                Location = new Point(15, 15),
                Size = new Size(360, 30),
                Font = new Font("微软雅黑", 9F)
            };

            txtInput = new TextBox
            {
                Text = defaultValue,
                Location = new Point(15, 50),
                Size = new Size(360, 25),
                Font = new Font("微软雅黑", 9F)
            };

            btnOK = new Button
            {
                Text = "确定",
                DialogResult = DialogResult.OK,
                Location = new Point(210, 80),
                Size = new Size(80, 28),
                Font = new Font("微软雅黑", 9F)
            };
            btnOK.Click += (s, e) => { InputValue = txtInput.Text; };

            btnCancel = new Button
            {
                Text = "取消",
                DialogResult = DialogResult.Cancel,
                Location = new Point(295, 80),
                Size = new Size(80, 28),
                Font = new Font("微软雅黑", 9F)
            };

            this.Controls.Add(lblPrompt);
            this.Controls.Add(txtInput);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            txtInput.Select();
            txtInput.SelectAll();
        }
    }
}
