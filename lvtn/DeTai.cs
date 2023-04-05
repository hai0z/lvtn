using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DeTai
{
    public partial class Form1 : Form
    {
        public MainClass mc;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mc = new MainClass();
            radioButton_Cmean.Checked = true;
        }

        private void button_xuly_Click(object sender, EventArgs e)
        {
            TienXuly txlForm = new TienXuly(this);
            txlForm.getMc(mc);
            txlForm.Show();
        }
        //hien thi cac thong bao len man hinh chinh
        public void setMessage(String m)
        {
            textBox1.Text = m;
        }

        public void showTienXuLy()
        {
            String s = "Kết quả tiền xử lý văn bản";
            s += "\r\n--------------------------";
            s += "\r\nCác từ có tần xuất nhiều nhất";
            s += "\r\nTS   |   Tọa độ   |   Từ";
            for (int i = 0; i < mc.MAX_ARRAY; i++)
            {
                s += "\r\n   " + mc.MKQ[i].t + "      " + mc.layToaDo(mc.MKQ[i].w) + "      " + mc.MKQ[i].w;
            }
        }



        String getType(int t)
        {
            switch (t)
            {
                case 0:
                    return "Kinh doanh";
                case 1:
                    return "Pháp luật";
                case 2:
                    return "Thể thao";
                case 3:
                    return "Văn hóa";
            }
            return "";
        }

        private void button_phanloai_Click(object sender, EventArgs e)
        {
            Boolean type = radioButton_Cmean.Checked;
            if (type)
            {
                //cMean
                mc.FCM();
                textBox1.Text += "\r\n-----------C-mean mờ-----------\r\n";
                //viet noi dung ra
                for (int i = 0; i < mc.listInputFile.Length; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        textBox1.Text += mc.arrKq[j, i] + "   ";
                    }
                    textBox1.Text += "\r\n";
                }
                //ket luan
                int c = 0;
                float max = 0;
                for (int i = 0; i < mc.listInputFile.Length; i++)
                {
                    max = 0;
                    c = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        if (mc.arrKq[j, i] > max)
                        {
                            c = j;
                            max = mc.arrKq[j, i];
                        }
                    }
                    textBox1.Text += "\r\n" + "vb: " + i + " loại " + getType(c);
                }
            }
            else
            {
                //svm
                mc.SVM();
                textBox1.Text += "\r\n-----------SVM-----------\r\n";
                //viet noi dung ra
                for (int i = 0; i < mc.listInputFile.Length; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        textBox1.Text += mc.arrKq[i, j] + "   ";
                    }
                    textBox1.Text += "\r\n";
                }

                //ket luan
                int c = 0;
                float min = 10000;
                for (int i = 0; i < mc.listInputFile.Length; i++)
                {
                    min = 10000;
                    c = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        if (mc.arrKq[i, j] < min)
                        {
                            c = j;
                            min = mc.arrKq[i, j];
                        }
                    }
                    textBox1.Text += "\r\n" + "vb: " + i + " loại " + getType(c);
                }
            }
        }

        private void button_chude_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Kinh doanh\r\nPháp luật\r\nThể thao\r\nVăn hóa");

        }
    }
}
