using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lvtn
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.MdiParent = this;
            if (panel2.Controls.Count >= 0)
            {
                panel2.Controls.Clear();
                panel2.Controls.Add(f1);
                f1.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DictionaryForm dictionaryForm = new DictionaryForm();
            dictionaryForm.MdiParent = this;
            if (panel2.Controls.Count >= 0)
            {
                panel2.Controls.Clear();
                panel2.Controls.Add(dictionaryForm);
                dictionaryForm.Show();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var option = MessageBox.Show("Bạn có muốn thoát không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (option == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                Application.Exit();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Home home = new Home();
            home.MdiParent = this;
            panel2.Controls.Add(home);
            home.Show();
        }

        private void button3_Click(object sender, EventArgs e)

        {
            Home home = new Home();
            home.MdiParent = this;
            if (panel2.Controls.Count >= 0)
            {
                panel2.Controls.Clear();
                panel2.Controls.Add(home);
                home.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*FCM fCM = new FCM();
            fCM.MdiParent = this;
            if (panel2.Controls.Count >= 0)
            {
                panel2.Controls.Clear();
                panel2.Controls.Add(fCM);
                fCM.Show();
            }*/
        }
    }
}
