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
using System.IO.Ports;
using System.Threading;

namespace WpfWb
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    

    public partial class MainWindow : Window
    {
        private SerialPort SpCom2 = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
        private delegate  void HandleInterface();
        private HandleInterface deletest;

        public static void CallToChildThread()
        {
            MessageBox.Show("hello");
            while(true);
        }

        public MainWindow()
        {
            InitializeComponent();
            

            
            //SpCom2.Write("hello word");

            SpCom2.DataReceived += new SerialDataReceivedEventHandler(SpCom_DataReceive);
            SpCom2.Open();
            ThreadStart childref = new ThreadStart(CallToChildThread);
            Thread th = Thread.CurrentThread;
            Thread childThread = new Thread(childref);
            //childThread.Start();
            th.Name = "MAIN THREAD";
            

        }

        private void SpCom_DataReceive(object sender, SerialDataReceivedEventArgs e)
        {
            deletest = new HandleInterface(delegateOne);
            this.Dispatcher.Invoke(deletest);
            //MessageBox.Show(data);
        }


        private void delegateOne()
        {
            string data = SpCom2.ReadExisting();
            textBox1.Text = data;   
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string data = null;
            data = textBox2.Text;

            SpCom2.Write(data);
        }
    }
}
