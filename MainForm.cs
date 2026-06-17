using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DelayedStartupTool
{
    public partial class MainForm : Form
    {
        private const string CONFIG_FILE = "config.json";
        private const string VBS_SCRIPT_NAME = "DelayedStartup.vbs";
        private List<StartupItem> startupItems = new List<StartupItem>();
        private string startupFolderPath;

        public MainForm()
        {
            InitializeComponent();
            InitializeStartupFolder();
            LoadConfig();
        }

        private void InitializeStartupFolder()
        {
            startupFolderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Startup)
            );
        }

        private void LoadConfig()
        {
            if (File.Exists(CONFIG_FILE))
            {
                try
                {
                    string json = File.ReadAllText(CONFIG_FILE);
                    startupItems = JsonConvert.DeserializeObject<List<StartupItem>>(json) ?? new List<StartupItem>();
                    RefreshListView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载配置文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveConfig()
        {
            try
            {
                string json = JsonConvert.SerializeObject(startupItems, Formatting.Indented);
                File.WriteAllText(CONFIG_FILE, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存配置文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshListView()
        {
            listViewItems.Items.Clear();
            foreach (var item in startupItems)
            {
                string displayText = item.FilePath;
                if (!string.IsNullOrWhiteSpace(item.Arguments))
                {
                    displayText += " [参数: " + item.Arguments + "]";
                }
                var listItem = new ListViewItem(new[] { displayText, item.DelaySeconds.ToString() });
                listItem.Tag = item;
                listViewItems.Items.Add(listItem);
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "可执行文件 (*.exe)|*.exe|所有文件 (*.*)|*.*";
                ofd.Multiselect = true;
                ofd.Title = "选择要添加的启动程序";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in ofd.FileNames)
                    {
                        if (!startupItems.Any(x => x.FilePath.Equals(filePath, StringComparison.OrdinalIgnoreCase)))
                        {
                            startupItems.Add(new StartupItem { FilePath = filePath, DelaySeconds = 3, Arguments = "" });
                        }
                    }
                    RefreshListView();
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedIndices.Count > 0)
            {
                int index = listViewItems.SelectedIndices[0];
                if (index > 0)
                {
                    var item = startupItems[index];
                    startupItems.RemoveAt(index);
                    startupItems.Insert(index - 1, item);
                    RefreshListView();
                    listViewItems.Items[index - 1].Selected = true;
                    listViewItems.Items[index - 1].Focused = true;
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedIndices.Count > 0)
            {
                int index = listViewItems.SelectedIndices[0];
                if (index < startupItems.Count - 1)
                {
                    var item = startupItems[index];
                    startupItems.RemoveAt(index);
                    startupItems.Insert(index + 1, item);
                    RefreshListView();
                    listViewItems.Items[index + 1].Selected = true;
                    listViewItems.Items[index + 1].Focused = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listViewItems.SelectedIndices.Count > 0)
            {
                int index = listViewItems.SelectedIndices[0];
                startupItems.RemoveAt(index);
                RefreshListView();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要清空所有启动项吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                startupItems.Clear();
                RefreshListView();
            }
        }

        private void btnSaveAndStartup_Click(object sender, EventArgs e)
        {
            SaveConfig();
            GenerateVBScript();
            CopyToStartupFolder();
            MessageBox.Show("已保存配置并设置为开机启动！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GenerateVBScript()
        {
            try
            {
                var vbsContent = new System.Text.StringBuilder();
                vbsContent.AppendLine("' 延迟启动工具 - Delayed Startup Tool 2.0");
                vbsContent.AppendLine("' 使用 VBScript 实现后台静默启动，无黑框窗口");
                vbsContent.AppendLine();
                vbsContent.AppendLine("Set objShell = CreateObject(\"WScript.Shell\")");
                vbsContent.AppendLine();

                foreach (var item in startupItems)
                {
                    string fileName = Path.GetFileName(item.FilePath);
                    string directory = Path.GetDirectoryName(item.FilePath);

                    vbsContent.AppendLine("' 准备启动: " + fileName);
                    vbsContent.AppendLine("WScript.Sleep " + (item.DelaySeconds * 1000).ToString());
                    vbsContent.AppendLine("objShell.CurrentDirectory = \"" + directory + "\"");

                    if (!string.IsNullOrWhiteSpace(item.Arguments))
                    {
                        vbsContent.AppendLine("objShell.Run \"\"\"" + item.FilePath + "\"\" " + item.Arguments + "\", 0, False");
                    }
                    else
                    {
                        vbsContent.AppendLine("objShell.Run \"\"\"" + item.FilePath + "\"\"\", 0, False");
                    }
                    vbsContent.AppendLine();
                }

                File.WriteAllText(VBS_SCRIPT_NAME, vbsContent.ToString(), System.Text.Encoding.Default);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成启动脚本失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyToStartupFolder()
        {
            try
            {
                string destPath = Path.Combine(startupFolderPath, VBS_SCRIPT_NAME);
                File.Copy(VBS_SCRIPT_NAME, destPath, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("复制到启动文件夹失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveStartup_Click(object sender, EventArgs e)
        {
            try
            {
                string vbsPath = Path.Combine(startupFolderPath, VBS_SCRIPT_NAME);
                if (File.Exists(vbsPath))
                {
                    File.Delete(vbsPath);
                    MessageBox.Show("已删除开机启动脚本！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("启动文件夹中未找到启动脚本。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除启动脚本失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTestNow_Click(object sender, EventArgs e)
        {
            if (!File.Exists(VBS_SCRIPT_NAME))
            {
                MessageBox.Show("请先保存配置并生成启动脚本！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start("wscript.exe", "\"" + Path.GetFullPath(VBS_SCRIPT_NAME) + "\"");
                MessageBox.Show("已启动测试，程序将在后台静默启动（无黑框）。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动测试失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenStartupFolder_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", startupFolderPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开启动文件夹失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewItems_DoubleClick(object sender, EventArgs e)
        {
            if (listViewItems.SelectedIndices.Count > 0)
            {
                int index = listViewItems.SelectedIndices[0];
                var item = startupItems[index];

                using (var form = new StartupItemEditForm(item.DelaySeconds, item.Arguments))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        item.DelaySeconds = form.DelaySeconds;
                        item.Arguments = form.Arguments;
                        RefreshListView();
                    }
                }
            }
        }
    }

    public class StartupItem
    {
        public string FilePath { get; set; }
        public int DelaySeconds { get; set; }
        public string Arguments { get; set; }
    }
}
