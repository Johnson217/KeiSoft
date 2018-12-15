using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeiSoft
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flag = false;
            int select = 0;
            if (this.radioButton1.Checked)
            {
                flag = true;
                select = 1;
            }

            if (this.radioButton2.Checked)
            {
                flag = true;
                select = 2;
            }

            if (flag = false)
            {
                MessageBox.Show("请选择群组");
            }
            else
            {
                this.Hide();
                var f1= new Psychological(select);
                f1.Show();
            }

        }
    }
}
