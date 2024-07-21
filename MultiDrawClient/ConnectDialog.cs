using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiDrawClient
{
    //Modeless Dialog
    public partial class ConnectDialog : Form
    {
        public ConnectDialog()
        {
            InitializeComponent();

            //if connect button is clicked
            buttonConnect.Click += ButtonConnect_Click;
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            //Dialog result is ok
            DialogResult = DialogResult.OK;
        }

        public string textAddress
        {
            //give back the Address text
            get
            {
                return textBoxAddress.Text;
            }
        }


        public string textPort
        {
            //give back the port text
            get
            {
                return textBoxPort.Text;
            }
        }
    }
}
