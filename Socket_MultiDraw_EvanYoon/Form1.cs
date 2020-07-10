//******************************************************************************************************************************************************
//Program:      Socket_MultiDraw_EvanYoon
//Author:       Jongwan (Evan) Yoon
//Description:  Attempts to connect to the server of the specified ip address. If connection was successful, display on the label that the client has
//              connected, and allows the user to draw on the shown canvas. The user is able to draw by holding down left click, right click, or the
//              middle click. The thickness of the line is able to be changed via scrolling. Scrolling up will thicken the line, while scrolling down
//              will make it thinner. Colour can be specified by left clicking on the 'colour' label, which will open up a colour dialog for the user
//              to choose from. If the user right clicks the 'colour' label, another dialog with a track bar will show up. The track bar on the dialog
//              allows the user to choose the opacity of the line. While connected to the server, the user can draw on the canvas, as well as receive
//              the lines that others are also drawing. 
//Date:         April 24, 2020
//Class:        CMPE2800
//Instructor:   Shane Kelemen
//******************************************************************************************************************************************************
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using mdtypes;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace Socket_MultiDraw_EvanYoon
{
    public partial class Form1 : Form
    {
        //used for drawing on the form
        BufferedGraphicsContext _bgc = null;
        BufferedGraphics _bg = null;
        Graphics _gr = null;

        //used for connecting/retrieving data, as well as processing them
        readonly BinaryFormatter _format;    //used to serialize and deserialize the received data
        Socket _socket;             //socket that is connected to the server
        Thread _thread;             //thread that will process the received data

        //used to retrieve address
        Connect _connection;//dialog used to connect to the server
        string _addrs;      //address of the server that the user has chosen in the connection dialog

        //used to retrieve alpha value
        Alpha _alpha;   //dialog to choose an alpha value of the line
        byte _alph;     //alpha value that the user has chosen in the alpha dialog

        //used for creating line segments
        ushort _thickness;  //thickness of the line to be drawn
        bool _down;         //used to check if the mouse click is held down
        PointF _start;      //gets the starting position of the line to be drawn
        PointF _end;        //gets the end position of the line to be drawn
        Color _colour;      //colour of the line to be drawn

        //used for displaying data
        int _totalRx;       //total frames received
        int _totalFrag;     //total fragments that occured
        long _totalBytes;    //total bytes received  from the server
        double _avgDestack; //average value of destack

        public Form1()
        {
            InitializeComponent();

            //initialize all variables
            _format = new BinaryFormatter();
            _socket = null;

            _alph = 255;
            _thickness = 1;
            _colour = Color.Red;

            _avgDestack = 0;

            _connection = null;
            _alpha = null;

            _down = false;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            //creates an event handler for mouse wheel moving
            MouseWheel += Form1_MouseWheel;

            //creates an initial drawing window for the user
            _gr = CreateGraphics();
            _bgc = new BufferedGraphicsContext();
            _bg = _bgc.Allocate(_gr, ClientRectangle);

            //loads it in white
            _bg.Graphics.Clear(Color.White);
            _bg.Render();

            //instruction for the user
            MessageBox.Show("Left click on the 'Colour' label to change the colour" +
                "\nRight click on the 'Colour' label to change the opacity" +
                "\nClick on the 'Disconnected' label to connect to a server", "Instructions");
        }

        /// <summary>
        /// used to determine the thickness of the line to be drawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            //wheel moves up, thickness increases
            if (e.Delta > 0)
            {
                _thickness++;
                if (_thickness > 80)
                    _thickness = 80;
            }

            //wheel moves down, thickness decreases
            if (e.Delta < 0)
            {
                _thickness--;
                if (_thickness < 1)
                    _thickness = 1;
            }

            //display the thickness on the label
            Status_Thickness.Text = $"Thickness : {_thickness}";
        }
        
        /// <summary>
        /// if the user decides to resize the window, save the previously drawn lines to a temporary bufferedgraphics,
        /// then create a new canvas the size of the resized window, and redraw the previous lines onto it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            //save all previously drawn lines
            BufferedGraphics temp = _bg;

            //create a new canvas, the size of the form
            _bg = _bgc.Allocate(_gr, ClientRectangle);
            _bg.Graphics.Clear(Color.White);

            //redraw the previously drawn lines onto the new canvas
            temp.Render(_bg.Graphics);
            _bg.Render();
        }

        /// <summary>
        /// opens a connection dialog for the user to enter an ip address, which will be used to connect to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_Connection_Click(object sender, EventArgs e)
        {
            if (_connection == null)
            {
                _connection = new Connect()
                {
                    //retrieve back the address from the user input
                    _connect = new delVoidString(GetAddress)
                };

                //opens up a dialog, and checks if the user closed it down using the 'OK' button
                if (_connection.ShowDialog() == DialogResult.OK)
                    if (_socket == null)
                    {
                        //create a new socket
                        _socket = new Socket(
                            AddressFamily.InterNetwork,
                            SocketType.Stream,
                            ProtocolType.Tcp
                            );

                        //attempt to connect to the address
                        try
                        {
                            _socket.BeginConnect(_addrs, 1666, new AsyncCallback(Callback), _socket);
                        }
                        //if connection failed, allow the user to try again
                        catch (SocketException ex)
                        {
                            Console.WriteLine(ex.Message);
                            _socket = null;
                            _connection = null;
                        }
                    }

                //user closed the window by not pressing the 'OK' button. Allow the user to re-open the dialog
                if (_connection.DialogResult == DialogResult.Cancel)
                    _connection = null;
            }
        }

        /// <summary>
        /// asynchronus callback method used to attempt connecting to the specified server
        /// </summary>
        /// <param name="ar"></param>
        private void Callback(IAsyncResult ar)
        {
            Socket got = (Socket)ar.AsyncState;

            //attempt connecting to the server
            try
            {
                got.EndConnect(ar);
                Invoke(new MethodInvoker(delegate () { Status_Connection.Text = "Connected"; }));
                Invoke(new MethodInvoker(delegate () { Status_Connection.BackColor = Color.LightGreen; }));
                _thread = new Thread(new ThreadStart(Process))
                {
                    IsBackground = true
                };
                _thread.Start();
            }
            //if unable to connect, allow the user to reconnect
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
                Invoke(new MethodInvoker(delegate () { Status_Connection.Text = "Disconnected"; }));
                Invoke(new MethodInvoker(delegate () { Status_Connection.BackColor = Color.Red; }));
                _connection = null;
            }
        }

        /// <summary>
        /// continuous loop to always listen to the server as long as the socket is connected. Process the data and display them
        /// </summary>
        private void Process()
        {
            MemoryStream ms = new MemoryStream();
            byte[] data = new byte[65536];

            while (true)
            {
                try
                {
                    int count = _socket.Receive(data);  //store the received data into a byte array
                    int destack = 0;        //reset the destacked value
                    _totalBytes += count;   //increment the total bytes received by the server

                    //convert the value of total bytes received into engineering notation
                    switch (Math.Floor(Math.Log10(_totalBytes)))
                    {
                        case 0:
                        case 1:
                        case 2:
                            Invoke(new MethodInvoker(delegate () { Status_Bytes.Text = $"Bytes RX'ed : {_totalBytes:.00}"; }));
                            break;
                        case 3:
                        case 4:
                        case 5:
                            Invoke(new MethodInvoker(delegate () { Status_Bytes.Text = $"Bytes RX'ed : {_totalBytes / 1e3:.00}K"; }));
                            break;
                        case 6:
                        case 7:
                        case 8:
                            Invoke(new MethodInvoker(delegate () { Status_Bytes.Text = $"Bytes RX'ed : {_totalBytes / 1e6:.00}M"; }));
                            break;
                        case 9:
                        case 10:
                        case 11:
                            Invoke(new MethodInvoker(delegate () { Status_Bytes.Text = $"Bytes RX'ed : {_totalBytes / 1e9:.00}G"; }));
                            break;
                        case 12:
                        case 13:
                        case 14:
                            Invoke(new MethodInvoker(delegate () { Status_Bytes.Text = $"Bytes RX'ed : {_totalBytes / 1e12:.00}T"; }));
                            break;
                    }

                    //ad received data to end of the receiver stream, put the stream position back to where it last was at
                    long pos = ms.Position;
                    ms.Seek(0, SeekOrigin.End);
                    ms.Write(data, 0, count);
                    ms.Position = pos;

                    //reset the receiving data array
                    data = new byte[65536];

                    //attempt to process one or more objects
                    do
                    {
                        destack++;

                        //save the current position of memory stream in case deserialization fails
                        long startPos = ms.Position;

                        try
                        {
                            //get the line segment the server has sent back
                            LineSegment line = (LineSegment)_format.Deserialize(ms);

                            //render the line on to the canvas
                            line.Render(_bg.Graphics);
                            _bg.Render();

                            //increment the frame received
                            Invoke(new MethodInvoker(delegate () { Status_Frame.Text = $"Frames RX'ed : {++_totalRx}"; }));
                        }
                        catch (SerializationException ex)
                        {
                            //deserialization failed, put the memory stream position back to the saved position
                            ms.Position = startPos;
                            Console.WriteLine($"Thread Desrialization::ERROR - {ex.Message}");

                            //increment fragment
                            Invoke(new MethodInvoker(delegate () { Status_Fragment.Text = $"Fragments : {++_totalFrag}"; }));
                            break;
                        }
                    } while (ms.Position < ms.Length);

                    //display the calculated average number of frames destacked per receive
                    _avgDestack = (_avgDestack + destack) / 2;
                    Invoke(new MethodInvoker(delegate () { Status_Destack.Text = $"Destack Avg. : {_avgDestack:.00}"; }));
                    
                    //clear the stream if all of the data is processed
                    if (ms.Position == ms.Length)
                    {
                        ms.Position = 0;
                        ms.SetLength(0);
                    }
                }
                catch(Exception ex)
                {
                    //reinitialize the connection dialog and the connection socket before exiting to allow the user to reconnect to the server
                    Console.WriteLine($"Thread::ERROR - {ex.Message}");
                    _connection = null;
                    _socket = null;
                    break;
                }
            }
            //display that the client has disconnected from the server
            Invoke(new MethodInvoker(delegate () { Status_Connection.Text = "Disconnected"; }));
            Invoke(new MethodInvoker(delegate () { Status_Connection.BackColor = Color.Red; }));
        }

        /// <summary>
        /// Mouse button is held down. The initial start position will be the position of the initial button press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _down = true;   //the button is held down
            _start = new PointF(e.X, e.Y);  //initial starting point of the line
        }

        /// <summary>
        /// remove the button down flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            //remove the flag that indicates that the mouse button in held down
            if (_down)
                _down = false;
        }

        /// <summary>
        /// the position of the mouse has moved while the button is held down. Send the lines to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_down && _socket != null)
            {
                //create a line that will be sent to the server
                LineSegment line = new LineSegment();
                _end = new PointF(e.X, e.Y);    //end position of the line will be the current position
                line.Colour = _colour;          //colour will be selected by the user
                line.Start = _start;            //the start position will be decided after creating a line
                line.End = _end;                //end position of the line
                line.Thickness = _thickness;    //thickness of the line will be chosen by the user using a dialog
                line.Alpha = _alph;             //alpha value of the line will be chosen by the user using a dialog
                _start = _end;                  //the starting position of the line will now hold the end position
                                                //of the line

                using (MemoryStream data = new MemoryStream())
                {
                    //serializes the line to be sent
                    _format.Serialize(data, line);

                    //attempts to send the data
                    try
                    {
                        _socket.Send(data.ToArray());
                    }

                    //if sending failed, log it in the console
                    catch (SocketException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// if the user either left or right clicks the 'colour' label, show a dialog that corresponds to the click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_Colour_MouseDown(object sender, MouseEventArgs e)
        {
            //if left click, open up a colour dialog for the user to pick a colour on
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                    _colour = colorDialog1.Color;
                Status_Colour.ForeColor = _colour;
            }

            //if right click, open up a new dialog with trackbar for the user to pick an alpha value on
            if (e.Button == MouseButtons.Right)
            {
                if (_alpha == null)
                {
                    _alpha = new Alpha()
                    {
                        _alpha = new delVoidByte(GetAlpha)
                    };
                    //shows the dialog to the user
                    _alpha.Show();
                }
                //if user closes the dialog, instead of getting rid of it, just hide it from the user
                else
                {
                    _alpha.Show();
                    _alpha.BringToFront();
                }
            }
        }

        /// <summary>
        /// if the 'clear' label is clicked, clear the canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            //clear the canvas
            _bg.Graphics.Clear(Color.White);
            _bg.Render();
        }

        /// <summary>
        /// if 'stress test' label is clicked, create 2500 random lines to the canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Status_stress_Click(object sender, EventArgs e)
        {
            Random ran = new Random();

            if (_socket != null)
            {
                //create random lines to be sent to the server as a stress test
                for (int i = 0; i < 2501; i++)
                {
                    LineSegment line = new LineSegment
                    {
                        Colour = Color.FromArgb(ran.Next(256), ran.Next(256), ran.Next(256)),
                        Start = new PointF(ran.Next(ClientRectangle.Width), ran.Next(ClientRectangle.Height)),
                        End = new PointF(ran.Next(ClientRectangle.Width), ran.Next(ClientRectangle.Height)),
                        Thickness = _thickness,
                        Alpha = (byte)ran.Next(1, 256)
                    };

                    using (MemoryStream data = new MemoryStream())
                    {
                        _format.Serialize(data, line);
                        try
                        {
                            _socket.Send(data.ToArray());
                        }
                        catch (SocketException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// used to retrieve the alpha value from the dialog
        /// </summary>
        /// <param name="s">invoked alpha value</param>
        private void GetAlpha(byte s)
        {
            _alph = s;
        }

        /// <summary>
        /// used to retrieve the address from the dialog
        /// </summary>
        /// <param name="s">returned address</param>
        private void GetAddress(string s)
        {
            _addrs = s;
        }
    }
}
