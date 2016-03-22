using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace graphAirplain
{
    class UDPListener
    {

        private const int listenPort = 256;
        public List<UserData> data;
        public UDPListener()
        {
            Thread thr = new Thread(StartListener);
            thr.Start();

        }


        public void StartListener()
        {
            bool done = false;
            UserData tmpData = null;
            String tmpStr = null;

            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

            try
            {
                while (!done)
                {
                //    Console.WriteLine("Waiting for broadcast");
                    byte[] bytes = listener.Receive(ref groupEP);
                    // *TODO* сделать расшифровку UDP  пакете в класс UserData для нескольких параметров
                    tmpData.time = DateTime.Now;
                    tmpStr = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    tmpData.x = Convert.ToDouble(tmpStr);
                    data.Add(tmpData);
                /*    Console.WriteLine("Received broadcast from {0} :\n {1}\n",
                        groupEP.ToString(),
                        Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                        */
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                listener.Close();
            }

        }
        
        // очишение list после сохранения данных
        public void DataIsSave()
        {

        }
    }
}
