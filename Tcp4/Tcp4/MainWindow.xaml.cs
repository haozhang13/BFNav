using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Windows.Threading;

namespace Tcp4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
       private TcpClient client;
       private const int bufferSize = 256;
       private NetworkStream sendStream;
      
       public delegate void showData(string msg);
       private DispatcherTimer timer;
       private NavData navData = new NavData();
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {

                if (client != null)
                {
                    if (client.Connected)
                    {
                        byte[] buffer = Encoding.Default.GetBytes("GETPOSE\x0A");
                        sendStream.Write(buffer, 0, buffer.Length);
                        ListenerServer();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip = IPAddress.Parse("192.168.1.101");
            client = new TcpClient();
            client.Connect(ip, 2111);
            sendStream = client.GetStream();

            if (client.Connected)
                MessageBox.Show("连接导航仪成功");       
        }

        private void ListenerServer()
        {
            try
            {             
                int readSize;
                byte[] buffer = new byte[bufferSize];
                lock (sendStream)
                {
                    readSize = sendStream.Read(buffer, 0, bufferSize);
                }
                if (readSize == 0)
                    return;
                string recvMsg = Encoding.Default.GetString(buffer, 0, readSize);

                richTextbox.Dispatcher.Invoke(new showData(richTextbox.AppendText), recvMsg);

                string[] recvData = recvMsg.Split(' ');
                
                navData.Reflectors = Convert.ToInt32(recvData[1]);
                navData.X = Convert.ToDouble(recvData[2]);
                navData.Y = Convert.ToDouble(recvData[3]);
                navData.Angle = Convert.ToDouble(recvData[4])* 0.01;

                this.laylout.DataContext = navData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + ex.Message);
            }                     
        }

        private void Pos_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(1000000);
            timer.Start();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            client.Close();
            sendStream.Close();
            timer.Stop();
        }

        private void clear_Click_1(object sender, RoutedEventArgs e)
        {
           
        }
     
    }
}
