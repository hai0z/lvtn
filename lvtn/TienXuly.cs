using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lvtn
{
    public partial class TienXuly : Form
    {
        MainClass mc;
        FCM parent;

        public TienXuly(FCM p)
        {
            parent = p;
            InitializeComponent();
            textBox_data.ReadOnly = true;
        }

        public void getMc(MainClass m)
        {
            mc = m;
        }

        private string RemoveUnwantedWords(string input, string[] wordsToRemove)
        {
            // Join the words to remove into a regex pattern
            string pattern = string.Join("|", wordsToRemove.Select(w => Regex.Escape(w)));

            // Remove unwanted words using regex replace
            return Regex.Replace(input, pattern, string.Empty);
        }

        public void tienXuLy(string path)
        {

            string sdata = System.IO.File.ReadAllText(@path);

            if (sdata.Length <= 0)
            {
                MessageBox.Show("Lỗi đọc file: " + path);
                return;
            }
            int pos;
            string sTemp;
            string world;

            char[] separators = { ',', '"', ':', '(', ')', '.' };
            string[] parts = sdata.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            sdata = string.Join(".", parts).ToLower();
            mc.resetMKQ();

            //sArr mang chua cac phan moi tach dc
            string[] sArr = sdata.Split('.');
            //xu ly tung doan tach dc


            for (int i = 0; i < sArr.Length; i++)
            {
                sArr[i] = RemoveUnwantedWords(sArr[i], mc.stopWordArr);
                sArr[i] = RemoveUnwantedWords(sArr[i], mc.noMeaningArr);
                //cat cac tu ra
                //thay 2 khoan trang bang 1 khoan trang
                while (sArr[i].Contains("  ")) sArr[i] = Regex.Replace(sArr[i], @"\s+", " ");


                sTemp = sArr[i].Trim();
                world = "";

                pos = sTemp.IndexOf(" ");
                if (pos >= 0)
                {
                    world = sTemp.Substring(0, pos);
                }

                while (sTemp.Length > 0 && pos > 0)
                {
                    List<string> arrTemp = mc.dicAray.AsParallel()
                                           .Where(x => x != null && x.StartsWith(world))
                                           .ToList();
                    int c = arrTemp.Count;
                    if (c > 0)
                    {
                        arrTemp.Sort((x, y) => y.Length.CompareTo(x.Length));

                        bool inserted = false;
                        Parallel.ForEach(arrTemp, (item) =>
                        {
                            if (sTemp.StartsWith(item))
                            {
                                lock (mc)
                                {
                                    if (!inserted)
                                    {
                                        mc.insertMKQ(item);
                                        sTemp = sTemp.Substring(item.Length);
                                        inserted = true;
                                    }
                                }
                            }
                        });

                        if (!inserted && sTemp.StartsWith(world))
                        {
                            mc.insertMKQ(world);
                            sTemp = sTemp.Substring(world.Length);
                        }
                    }
                    else
                    {
                        mc.insertMKQ(world);
                        sTemp = sTemp.Substring(world.Length);
                    }

                    sTemp = sTemp.Trim();
                    pos = sTemp.IndexOf(" ");
                    if (pos >= 0) world = sTemp.Substring(0, pos);
                }
                double percentage = (double)i / (sArr.Length - 1) * 100;
                Console.WriteLine(percentage + " ");
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = (int)percentage;
                }));
                label2.Invoke(new Action(() =>
                {
                    label2.Text = (int)percentage + "%";
                }));

            }
            mc.sortMKQ();

        }


        private StringBuilder sb = new StringBuilder();

        private void TextBoxAddText(string text)
        {
            if (textBox_data.InvokeRequired)
            {
                textBox_data.BeginInvoke(new MethodInvoker(delegate () { TextBoxAddText(text); }));
            }
            else
            {
                sb.Append(text);
                textBox_data.Text = sb.ToString();
                mc.note = sb.ToString();
                textBox_data.SelectionStart = textBox_data.Text.Length;
                textBox_data.ScrollToCaret();
            }
        }
        private void buttonCheck(Boolean b)
        {
            if (button_exit.InvokeRequired)
            {
                button_exit.BeginInvoke(
                    new MethodInvoker(
                        delegate () { buttonCheck(b); }));
            }
            else
            {
                button_exit.Enabled = b;
            }
        }
        private void ThreadProc()
        {
            for (int i = 0; i < mc.listInputFile.Length; i++)
            {
                label2.Invoke(new Action(() =>
                {
                    label3.Text = "Xử lí văn bản " + (i + 1);

                }));
                TextBoxAddText("\r\n\r\nĐang xử lý file:" + mc.listInputFile[i]);
                tienXuLy(mc.listInputFile[i]);
                mc.updateMKQALL(i);
                TextBoxAddText("\r\nHệ số \t| Lượng giá \t| Từ");
                for (int j = 0; j < mc.MAX_ARRAY; j++)
                {
                    TextBoxAddText("\r\n" + mc.MKQ[j].t + "\t  " + mc.arrMain[i, j] + "\t\t" + mc.MKQ[j].w);
                }

            }

            TextBoxAddText("\r\nTiền xử lý kết thúc!");
            Application.UseWaitCursor = false;
            buttonCheck(true);

        }

        // Gọi hàm ThreadProcAsync() bằng cách sử dụng Task.Run()
        private void RunThreadProcAsync()
        {
            Task.Run(() => ThreadProc());
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox_data.Text.Length <= 0) return;
            button_exit.Enabled = false;
            button1.Enabled = false;
            //public Node[] MKQ;  
            Application.UseWaitCursor = true;
            TextBoxAddText("\r\n---------------------\r\nBắt đầu xử lý file!");
            mc.initMKQALL(mc.listInputFile.Length);
            RunThreadProcAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string path = textBox1.Text.Trim();

            /*string path = System.IO.Directory.GetCurrentDirectory();
            path += "\\" + textBox1.Text.Trim();
            button_exit.Enabled = false;

            if (path.Length <= 0) return;
            if (Directory.Exists(path) != true)
            {
                MessageBox.Show("Đường dẫn không đúng!");
                return;
            }
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] rgFiles = di.GetFiles("*.txt");
            textBox_data.Text = "Các văn bản đầu vào:\r\n";

            string name = "";
            foreach (FileInfo fi in rgFiles)
            {
                textBox_data.Text += "\r\n" + fi.Name;
                name += path + "\\" + fi.Name + "|";
                //Console.Write(fi.Name );              
            }
            name = name.Substring(0, name.Length - 1);
            mc.listInputFile = name.Split('|');*/
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog1.SelectedPath))
            {
                // Lấy đường dẫn được chọn
                string path = folderBrowserDialog1.SelectedPath;

                // Thực hiện các thao tác lưu đường dẫn vào đây
                // Ví dụ: hiển thị đường dẫn trong textbox
                textBox1.Text = path;
                DirectoryInfo directory = new DirectoryInfo(path);
                FileInfo[] files = directory.GetFiles();
                List<string> allFile = new List<string>();
                foreach (FileInfo file in files)
                {
                    allFile.Add(file.FullName);
                    TextBoxAddText(file.Name + "\r");
                }
                mc.listInputFile = allFile.ToArray();
            }
        }



        private void button_exit_Click(object sender, EventArgs e)
        {
            parent.setMessage(mc.note);
            this.Close();
        }

        private void TienXuly_Load(object sender, EventArgs e)
        {

        }
    }
}
