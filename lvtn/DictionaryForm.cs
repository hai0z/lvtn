using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lvtn
{
    public partial class DictionaryForm : Form
    {
        Form1 form1;
        string[] dictionary;
        public DictionaryForm()
        {
            InitializeComponent();
            form1 = new Form1();
            cbxTudien.SelectedIndex = 0;

        }

        private void DictionaryForm_Load(object sender, EventArgs e)
        {
            dictionary = form1.LoadDictionary("all");
            lbDic.Text = ("Từ điển gốc: " + dictionary.Length + " từ");

        }
        private void AddItemsToListBox(string[] itemsToAdd)
        {
            // Create a new instance of BackgroundWorker
            BackgroundWorker worker = new BackgroundWorker();

            // Attach event handlers to the DoWork and RunWorkerCompleted events
            worker.DoWork += new DoWorkEventHandler(AddItemsToListBox_Worker);
            /* worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(AddItemsToListBox_Completed);*/

            // Start the background worker, passing in the items to add as the argument
            worker.RunWorkerAsync(itemsToAdd);
        }

        private void AddItemsToListBox_Worker(object sender, DoWorkEventArgs e)
        {
            // Retrieve the items to add from the argument
            string[] itemsToAdd = e.Argument as string[];

            // Loop through the items and add them to the ListBox
            foreach (string item in itemsToAdd)
            {
                // Check if the background worker has been cancelled
                if (((BackgroundWorker)sender).CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                // Add the item to the ListBox on the UI thread
                listBox1.Invoke(new MethodInvoker(() => listBox1.Items.Add(item)));
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLoadDic_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            AddItemsToListBox(dictionary);
            lbDic.Text = (cbxTudien.Text + ": " + dictionary.Length + " từ");
        }

        private void cbxTudien_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxTudien.SelectedIndex)
            {
                case 0:
                    dictionary = form1.LoadDictionary("all");
                    break;
                case 1:
                    dictionary = form1.LoadDictionary("thethao");
                    break;
                case 2:
                    dictionary = form1.LoadDictionary("kinhdoanh");
                    break;
                case 3:
                    dictionary = form1.LoadDictionary("giaoduc");
                    break;
                case 4:
                    dictionary = form1.LoadDictionary("suckhoe");
                    break;
                case 5:
                    dictionary = form1.LoadDictionary("phapluat");
                    break;
                case 6:
                    dictionary = form1.LoadDictionary("vanhoa");
                    break;

                default:
                    dictionary = form1.LoadDictionary("all");
                    break;
            }
        }
    }
}
