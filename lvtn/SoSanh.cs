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
    public partial class SoSanh : Form
    {
        public SoSanh()
        {
            InitializeComponent();
            rtSoSanh.Text = Program.txtSoSanh;
        }
    }
}
