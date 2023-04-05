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

namespace DeTai
{
    public partial class TienXuly : Form
    {
        MainClass mc;
        Form1 parent;

        public TienXuly(Form1 p)
        {
            parent = p;
            InitializeComponent();
        }

        public void getMc(MainClass m)
        {
            mc = m;
        }

        private string RemoveUnwantedWords(string input, string[] wordsToRemove)
        {
            foreach (string word in wordsToRemove)
            {
                int pos = input.IndexOf(word);
                while (pos >= 0)
                {
                    input = input.Substring(0, pos) + input.Substring(pos + word.Length);
                    pos = input.IndexOf(word);
                }
            }
            return input;
        }

        public void tienXuLy(String path)
        {

            String sdata = System.IO.File.ReadAllText(@path);

            if (sdata.Length <= 0)
            {
                MessageBox.Show("Lỗi đọc file: " + path);
                return;
            }
            int pos;
            String sTemp;
            String world;  //chua cac tu don cat dc
                           //mang chua cac tu de xu ly

            sdata = sdata.Trim();
            sdata = sdata.Replace(',', '.');
            sdata = sdata.Replace('"', '.');
            sdata = sdata.Replace(':', '.');
            sdata = sdata.Replace('(', '.');
            sdata = sdata.Replace(')', '.');
            while (sdata.Contains("..")) sdata = sdata.Replace("..", ".");
            sdata = sdata.ToLower();
            mc.resetMKQ();

            //sArr mang chua cac phan moi tach dc
            String[] sArr = sdata.Split('.');
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
                    int c = 0;
                    List<string> arrTemp = new List<string>();

                    for (int j = 0; j < mc.dicAray.Length; j++)
                    {
                        if (mc.dicAray[j] != null && mc.dicAray[j].StartsWith(world))
                        {
                            arrTemp.Add(mc.dicAray[j]);
                            c++;
                        }
                    }

                    if (c > 0)
                    {
                        arrTemp.Sort((x, y) => y.Length.CompareTo(x.Length));

                        foreach (string item in arrTemp)
                        {
                            if (sTemp.IndexOf(item) == 0)
                            {
                                mc.insertMKQ(item);
                                sTemp = sTemp.Substring(item.Length);
                                break;
                            }
                        }

                        if (sTemp.StartsWith(world))
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
                Console.WriteLine("remove done" + i);

            }
            mc.sortMKQ();



        }


        private void TextBoxAddText(String text)
        {
            if (textBox_data.InvokeRequired)
            {
                textBox_data.BeginInvoke(
                    new MethodInvoker(
                        delegate () { TextBoxAddText(text); }));
            }
            else
            {
                textBox_data.Text += text;
                mc.note = textBox_data.Text;
                //textBox_data.
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
        private async void ThreadProcAsync()
        {
            for (int i = 0; i < mc.listInputFile.Length; i++)
            {
                TextBoxAddText("\r\n\r\nĐang xử lý file:" + mc.listInputFile[i]);
                tienXuLy(mc.listInputFile[i]);
                mc.updateMKQALL(i);
                TextBoxAddText("\r\nHệ số | Lượng giá | Từ");
                for (int j = 0; j < mc.MAX_ARRAY; j++)
                {
                    TextBoxAddText("\r\n" + mc.MKQ[j].t + " " + mc.arrMain[i, j] + " " + mc.MKQ[j].w);
                }
            }

            TextBoxAddText("\r\nTiền xử lý kết thúc!");
            Application.UseWaitCursor = false;
            buttonCheck(true);
        }

        // Gọi hàm ThreadProcAsync() bằng cách sử dụng Task.Run()
        private void RunThreadProcAsync()
        {
            Task.Run(() => ThreadProcAsync());
        }
        /* private void ThreadProc()
         {

             for (int i = 0; i < mc.listInputFile.Length; i++)
             {
                 TextBoxAddText("\r\n\r\nĐang xử lý file:" + mc.listInputFile[i]);
                 //textBox_data.Text += "/r/ Đang xử lý file:" + i;
                 tienXuLy(mc.listInputFile[i]);
                 mc.updateMKQALL(i);
                 TextBoxAddText("\r\nHệ số | Lượng giá | Từ");
                 //tu va trong so
                 for (int j = 0; j < mc.MAX_ARRAY; j++)
                 {
                     TextBoxAddText("\r\n" + mc.MKQ[j].t + " " + mc.arrMain[i, j] + " " + mc.MKQ[j].w);
                 }
             }

             TextBoxAddText("\r\nTiền xử lý kết thúc!");
             Application.UseWaitCursor = false;
             buttonCheck(true);
             //}
         }*/
        //xu ly van ban dau vao
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox_data.Text.Length <= 0) return;
            //public Node[] MKQ;  
            Application.UseWaitCursor = true;
            TextBoxAddText("\r\n---------------------\r\nBắt đầu xử lý file!");
            mc.initMKQALL(mc.listInputFile.Length);
            RunThreadProcAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //String path = textBox1.Text.Trim();

            String path = System.IO.Directory.GetCurrentDirectory();
            path += "\\" + textBox1.Text.Trim();
            button_exit.Enabled = false;
            //Console.WriteLine("cwd is '{0}'", cwd);

            if (path.Length <= 0) return;
            if (Directory.Exists(path) != true)
            {
                MessageBox.Show("Đường dẫn không đúng!");
                return;
            }
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] rgFiles = di.GetFiles("*.txt");
            textBox_data.Text = "Các văn bản đầu vào:\r\n";

            String name = "";
            foreach (FileInfo fi in rgFiles)
            {
                textBox_data.Text += "\r\n" + fi.Name;
                name += path + "\\" + fi.Name + "|";
                //Console.Write(fi.Name );              
            }
            name = name.Substring(0, name.Length - 1);
            mc.listInputFile = name.Split('|');
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
