using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AESm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SecureTextBox secureTextBox1 = new SecureTextBox();
        
        private byte[] GetPasswordBytes()
        {
            secureTextBox1.Text = "(!t12@^2n V21~ T!2u7O721Vg 6!4n9~)";
            // The real password characters is stored in System.SecureString
            // Below code demonstrates on converting System.SecureString into Byte[]
            // Credit: http://social.msdn.microsoft.com/Forums/vstudio/en-US/f6710354-32e3-4486-b866-e102bb495f86/converting-a-securestring-object-to-byte-array-in-net

            byte[] ba = null;

            if (secureTextBox1.Text.Length == 0)
                ba = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            else
            {
                // Convert System.SecureString to Pointer
                IntPtr unmanagedBytes = Marshal.SecureStringToGlobalAllocAnsi(secureTextBox1.SecureText);
                try
                {
                    // You have to mark your application to allow unsafe code
                    // Enable it at Project's Properties > Build
                    unsafe
                    {
                        byte* byteArray = (byte*)unmanagedBytes.ToPointer();

                        // Find the end of the string
                        byte* pEnd = byteArray;
                        while (*pEnd++ != 0) { }
                        // Length is effectively the difference here (note we're 1 past end) 
                        int length = (int)((pEnd - byteArray) - 1);

                        ba = new byte[length];

                        for (int i = 0; i < length; ++i)
                        {
                            // Work with data in byte array as necessary, via pointers, here
                            byte dataAtIndex = *(byteArray + i);
                            ba[i] = dataAtIndex;
                        }
                    }
                }
                finally
                {
                    // This will completely remove the data from memory
                    Marshal.ZeroFreeGlobalAllocAnsi(unmanagedBytes);
                }
            }

            return System.Security.Cryptography.SHA256.Create().ComputeHash(ba);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = AES.Encrypt(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = AES.Decrypt(textBox1.Text);
        }
    }
}
