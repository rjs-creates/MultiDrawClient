using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RajGenericSocket;

namespace MultiDrawClient
{
    public partial class ClientForm : Form
    { 
        private Socket _socket = null;                                        //Socket
        private GenericSocket genSock = null;                                 //Generic Socket
        private BinaryFormatter bf = new BinaryFormatter();                   //Binary formatter
        private Color _cl = Color.Red;                                        //starting color
        private ushort width = 1;                                             //starting width
        Graphics gr = null;                                                   //Window graphics
        bool akey = false;                                                    //bool to keep track of A key
        private byte alpha = (byte)255;                                       //intial alpha value
        Point last = Point.Empty;                                             //initial last value

        public ClientForm()
        {
            InitializeComponent();

            Shown += ClientForm_Shown;                          //Event handler for form shown
            MouseWheel += ClientForm_MouseWheel;                //if the mouse wheel is scrolled
            KeyPreview = true;                                  //Detect key strokes
            KeyDown += ClientForm_KeyDown;                      //IF a key is pressed
            KeyUp += ClientForm_KeyUp;                          //if a key is released
            MouseMove += ClientForm_MouseMove;                  //if a mouse move
            timertick.Tick += Timertick_Tick;                   //Everytime timer ticks
            timertick.Interval = 100;                           //interval of timer
            timertick.Start();                                  //start the timer
        }

        //Everytime mouse moves over the screen
        private void ClientForm_MouseMove(object sender, MouseEventArgs e)
        {
            //if a connection is set up
            if (genSock != null && genSock.Connected)
            {
                //if a line is being created
                if (e.Button == MouseButtons.Left)
                {
                    //set the object reference
                    mdtypes.LineSegment line = new mdtypes.LineSegment();
                    line.Start = last;                        //start will the previous last
                    line.End = e.Location;                    //end will be current location
                    line.Alpha = alpha;                       //alpha of color
                    line.Colour = _cl;                        //the color
                    line.Thickness = width;                   //thickness
                    //send the data through generic socket method
                    genSock.Send(line);
                }

                //last location updated
                last = e.Location;
            }

        }

        //Everytime a key is released
        private void ClientForm_KeyUp(object sender, KeyEventArgs e)
        {
            //if the key was a
            if (e.KeyCode == Keys.A)
            {
                //set the bool to false
                akey = false;
            }
        }

        //Everytime a key is pressed
        private void ClientForm_KeyDown(object sender, KeyEventArgs e)
        {
            //if the key was a
            if(e.KeyCode == Keys.A)
            {
                //a is presseed
                akey = true;
            }
        }

        //Everytime mouse scrolls
        private void ClientForm_MouseWheel(object sender, MouseEventArgs e)
        {
            //if a is pressed
            if (akey)
            {
                //Add to the alpha
                alpha += (byte)(e.Delta / 100);

                //if alpha is too big restrict
                if (alpha > 254)
                    alpha = (byte)1;

                //if alpha is too small restrict

                else if (alpha < 1)
                    alpha = (byte)255;

                //Show the alpha
                toolStripAlpha.Text = $"Alpha : {alpha}";
                return;
            }

            //Add to width
            width += (ushort)(e.Delta/100);

            //Restrict width if its too big or small
            if (width > 50)
                width = 50;
            else if (width < 1)
                width = 1;

            //Show the thickness
            toolStripLabelThickness.Text = $"Thickness : {width}";

        }

        //When form is shown
        private void ClientForm_Shown(object sender, EventArgs e)
        {
            //Event handler for Button connection
            toolStripButtonConnection.Click += ToolStripButtonConnection_Click;

            //Event handler for button Color
            toolStripButtonColor.Click += ToolStripButtonColor_Click;

            //Set color as foreground
            toolStripButtonColor.ForeColor = _cl;
        }

        //If color button is pressed
        private void ToolStripButtonColor_Click(object sender, EventArgs e)
        {
            //Open color dialog 
            ColorDialog cl = new ColorDialog();

            //Set Color value to new color selected
            if(cl.ShowDialog() == DialogResult.OK)
            {
                _cl = cl.Color;
                toolStripButtonColor.ForeColor = _cl;
            }
        }

        //Everytime connection button is pressed
        private void ToolStripButtonConnection_Click(object sender, EventArgs e)
        {

            //if a connection is not made already
            if(genSock == null || !genSock.Connected)
            {
                //Make new Connection Dialog
                ConnectDialog conn = new ConnectDialog();

                //if Ok is pressed
                if (conn.ShowDialog() == DialogResult.OK)
                {
                    //try to create a socket and start connecting
                    try
                    {
                        //Connecting
                        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _socket.BeginConnect(conn.textAddress, Convert.ToInt32(conn.textPort), cbConnect, null);
                        toolStripButtonConnection.Text = "Connecting";
                        toolStripBytesReceived.Text = $"Bytes RX'ed:0 ";

                    }
                    //If connection cannot be made
                    catch (Exception exc)
                    {
                        //failure
                        toolStripButtonConnection.Text = "Connect";
                    }
                }
            }
            //if connection is already there
            else
            {
                //Disconnect
                toolStripButtonConnection.Text = "Connect";
                genSock.Connected = false;
            }
        }

        //Once socket is made
        void cbConnect(IAsyncResult ar)
        {
            //Try to establish the connection
            try
            {
                //Establishing
                _socket.EndConnect(ar);
                _socket.NoDelay = true;

                //Create a generic socket
                genSock = new GenericSocket(_socket);
                genSock.Connected = true;

                //Change button text and start processing
                Invoke(new Action(() => { toolStripButtonConnection.Text = "Disconnect"; ConnectionComplete(); }));

                //Set the form as graphics window and clear it everytime new connection is established
                gr = CreateGraphics();
                gr.Clear(Color.FromKnownColor(KnownColor.Control));


            }
            //If cannot connect
            catch (Exception exc)
            {
                return;
            }
        }

        //Everytime timer ticks
        private void Timertick_Tick(object sender, EventArgs e)
        {
            //if generic socket is created and connected
            if (genSock != null && genSock.Connected)
            {
                //Change labels for Frames and fragments
                toolStripLabelFramesReceived.Text = $"Frames RX'ed:{ genSock.FramesReceived}";
                toolStripLabelFragments.Text = $"Fragments:{  genSock.Fragments}";

                //Labels for Bytes received based on the value i.e B,Kb,Mb
                if(genSock.BytesReceived > 0 && genSock.BytesReceived < 1024)
                   toolStripBytesReceived.Text = $"Bytes RX'ed:{(genSock.BytesReceived)}B";
                else if(genSock.BytesReceived > 1024 && genSock.BytesReceived < 1024*1024)
                  toolStripBytesReceived.Text = $"Bytes RX'ed:{((float)genSock.BytesReceived / 1024):n2}KB";
                else if (genSock.BytesReceived > 1024*1024)
                    toolStripBytesReceived.Text = $"Bytes RX'ed:{((float)(genSock.BytesReceived / 1024) / 1024):n2}MB";

                //make sure bytes are received
                if (genSock.BytesReceived > 0)
                    //Calcutate and show destack value
                toolStripDestackAvg.Text = $"DeStack Avg:{((float)genSock.FramesReceived /(float)(genSock.BytesReceived/1024)):n2}";
            }
        }

        //Once the connection is complete
        void ConnectionComplete()
        {
            //Event handler for data ready and Socket error
            genSock.DataReady += GenSock_DataReady;
            genSock.SocketError += GenSock_SocketError;


        }

        //if an error was encountered
        private void GenSock_SocketError(object sender, EventArgs e)
        {
            //Create a Message box and disconnect
            //MessageBox.Show(sender.ToString());
            toolStripButtonConnection.Text = "Connect";
            genSock.Connected = false;
        }

        //if data we get back is good
        private void GenSock_DataReady(object obj, GenericSocket genSock)
        {
            //Check if the obj is line segment
            if (!(obj is mdtypes.LineSegment line))
                return;

            //render if it is line
            line.Render(gr);

        }
    }
}
