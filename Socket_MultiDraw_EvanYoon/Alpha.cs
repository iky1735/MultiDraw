using System;
using System.Windows.Forms;

namespace Socket_MultiDraw_EvanYoon
{
    public delegate void delVoidByte(byte i);

    /// <summary>
    /// used to allow the user the choose the alpha value of the line
    /// </summary>
    public partial class Alpha : Form
    {
        //delegate used to send back the alpha value that the user has chosen
        public delVoidByte _alpha = null;
        public Alpha()
        {
            InitializeComponent();
        }

        /// <summary>
        /// occurs everytime the value of the trackbar changes. Changes the label that shows the trackbar value
        /// to the current value. Send back the alpha value back to the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            lbl_current.Text = $"{trackBar2.Value / 255.0:P}";  //change text of the label to current value
            _alpha.Invoke((byte)trackBar2.Value);           //send back the information to the main form
        }

        /// <summary>
        /// stops the dialog from closing, and hides it from the user instead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alpha_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;    //stop the form from closing
            Hide(); //hides it from the user
        }
    }
}
