using System;
using System.Windows.Forms;

namespace Socket_MultiDraw_EvanYoon
{
    public delegate void delVoidString(string s);

    /// <summary>
    /// allows the user to enter an ip address which will later be used to connect to the server
    /// </summary>
    public partial class Connect : Form
    {
        //delegate used to send the address the user has entered into the textbox
        public delVoidString _connect = null;
        public Connect()
        {
            InitializeComponent();

            //default address to connect to
            txt_ip.Text = "bits.net.nait.ca";
        }

        /// <summary>
        /// user has decided upon the server address. Send the address back to the main form, and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ok_Click(object sender, EventArgs e)
        {
            _connect.Invoke(txt_ip.Text);
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// allows the user to submit the information by pressing the enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_ip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btn_ok.PerformClick();
        }
    }
}
