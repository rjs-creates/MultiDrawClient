// Project : Multi Draw Client
// April 18 2021
// By Rajeshwar Singh
//
// Submission Code : 1202_CMPE2800_MDClient
// ///////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RajGenericSocket
{
    public class GenericSocket
    {
        private Queue<object> RxQueue = new Queue<object>();                         //Queue of Received objects
        private Queue<object> TxQueue = new Queue<object>();                         //Queue of Transmitted objects

        public delegate void delObjGenSock(object obj, GenericSocket genSock);      //Eventhandler delegate for DataReady
        public event EventHandler SocketError;                                      //Eventhandler SocketError
        public event delObjGenSock DataReady;                                       //Eventhandler DataReady

        BinaryFormatter _bf = new BinaryFormatter();                                //Binary formatter

        private Socket mySok;                                                       //Socket
        private Thread ReceiveThread;                                               //Receive Thread
        private Thread ProcessThread;                                               //Process Thread
        private Thread SendThread;                                                  //Send Thread

        //automatic propert for Frames Received,Fragments and bytes Received
        public int FramesReceived { get; set; }                                  
        public int Fragments { get; set; }
        public int BytesReceived{ get; set; }

        //private member conn
        private bool _conn;

        //manual property for _conn
        public bool Connected 
        { get
            {
                return _conn;
            }
            set 
            {
                _conn = value;
                if(!_conn)
                {
                    mySok.Shutdown(SocketShutdown.Both);
                    mySok.Close();
                }
            }
        }

        //Send Method used to send line segment as object
        public void Send(object obj)
        {

            //Lock for safety
            lock (TxQueue)
                TxQueue.Enqueue(obj);
        }

        //Constructor
        public GenericSocket(Socket sok)
        {
            //giving socket a value
            mySok = sok;

            //Starting all three threads
            ReceiveThread = new Thread(RxThread);
            ProcessThread = new Thread(ProThread);
            SendThread = new Thread(SenThread);

            ReceiveThread.IsBackground = true;
            ProcessThread.IsBackground = true;
            SendThread.IsBackground = true;

            ReceiveThread.Start();
            ProcessThread.Start();
            SendThread.Start();
        }

        //Receive Thread
        private void RxThread()
        {
            // this memorystream will hold data between fragments
            MemoryStream msRXStream = new MemoryStream();

            // this byte array will hold the data from one Receive action
            byte[] rxBuff = new byte[2048];
            BinaryFormatter bf = new BinaryFormatter();

            // keep the thread going so receiving is always on
            while (true)
            {
                try
                {
                    // sit and wait for data
                    int iRxCount = mySok.Receive(rxBuff);
                    BytesReceived += iRxCount;

                    if (iRxCount == 0)
                    {
                        System.Diagnostics.Trace.WriteLine("Soft Disco");
                        lock (RxQueue)
                            RxQueue.Enqueue(this);
                        break;
                    }

                    // add received data to end of the receiver stream, put
                    // stream position back to where it was
                    long lPos = msRXStream.Position;
                    msRXStream.Seek(0, SeekOrigin.End);
                    msRXStream.Write(rxBuff, 0, iRxCount);
                    msRXStream.Position = lPos;

                    // attempt to extract one or more compete objects
                    do
                    {
                        // save position in event that deserialization fails
                        long lStartPos = msRXStream.Position;
                        try
                        {
                            // pull an object from the stream
                            object o = bf.Deserialize(msRXStream);
                            FramesReceived++;
                            // add this frame to the queue of rx'ed frames
                            // assume another thread will process the frames
                            lock (RxQueue)
                                RxQueue.Enqueue(o);
                        }
                        catch (System.Runtime.Serialization.SerializationException)
                        {
                            // error, so put the ms pointer back to where is started
                            msRXStream.Position = lStartPos;

                            Fragments++;

                            // exit loop, maybe next time there will be enough data
                            break;
                        }
                       
                    }
                    while (msRXStream.Position < msRXStream.Length);

                    // clear stream if all data processed
                    if (msRXStream.Position == msRXStream.Length)
                    {
                        msRXStream.Position = 0;
                        msRXStream.SetLength(0);
                    }
                }
                catch (Exception err)
                {
                    // this would be unexpected, so exit
                    System.Diagnostics.Trace.WriteLine("Hard Disco");
                    lock (RxQueue)
                        RxQueue.Enqueue(this);
                    break;
                }

                Thread.Sleep(0);
            }

            System.Diagnostics.Trace.WriteLine("Receive Terminated");

        }

        //Process thread
        private void ProThread()
        {
            //run while its connected
            while (true)
            {
                //Try to read the data
                try
                {
                    //if recieve queue has objects
                    if (RxQueue.Count > 0)
                    {
                        //lock the queue
                        lock (RxQueue)
                        {
                            //get the object out and invoke the dataready event
                            object obj = RxQueue.Dequeue();
                            if (obj == this)
                            {
                                SocketError?.Invoke(this, new EventArgs());
                                break;
                            }
                            else
                                DataReady?.Invoke(obj, this);
                        }
                    }
                    
                }
                //If not
                catch(Exception exc)
                {
                    
                    System.Diagnostics.Trace.WriteLine(exc.Message);
                    break;
                }
                //sleep
                Thread.Sleep(0);
            }
            System.Diagnostics.Trace.WriteLine("Process Terminated");
        }

        //Send Thread
        private void SenThread()
        {
            //Run while connected
            while (Connected)
            {    
                //Try sending the Line Segment
                try
                {
                    //if there's objects in the queue
                    if (TxQueue.Count > 0)
                    {
                        object obj = null;

                        //lock the queue
                        lock (TxQueue)
                        {
                            //get the object out
                            obj = TxQueue.Dequeue();
                        }
                        
                            //Pack & serialize
                            MemoryStream ms = new MemoryStream();
                            _bf.Serialize(ms, obj);

                            //send the line segment through our socket
                            mySok.Send(ms.GetBuffer(), (int)ms.Length, SocketFlags.None);
                    }
                }
                catch (Exception exc)
                { 
                    break;
                }
                //Sleep
                Thread.Sleep(0);
            }
            //Terminate
            System.Diagnostics.Trace.WriteLine("Send Terminated");
        }
    }
}
