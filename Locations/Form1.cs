using System;
using System.Windows.Forms;

namespace Locations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = clsEncDec.Decrypt(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = string.Empty;
            var a = string.Empty;
            var b = string.Empty;
            label1.Text = "Location: " + HRMConfig.LocationCount(textBox3.Text, textBox4.Text) + Environment.NewLine +
                          "Role: " + HRMConfig.RoleCount(textBox3.Text);

            foreach (var item in HRMConfig.GetCouldBeConnectedServerName(textBox3.Text, textBox4.Text))
                a += item + Environment.NewLine;

            foreach (var item in HRMConfig.GetIsAdminServerName())
                b += item + Environment.NewLine;
            label2.Text = "Number of server could be connected: " +
                          HRMConfig.LocationCount(textBox3.Text, textBox4.Text) +
                          Environment.NewLine +
                          "Server that could be connected to: " + Environment.NewLine + a +
                          Environment.NewLine +
                          "Server that is admin: " + Environment.NewLine + b;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = clsEncDec.Encrypt(textBox2.Text);
        }
    }
}