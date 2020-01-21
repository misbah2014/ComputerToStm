using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Threading;
using System.IO;
using System.IO.Ports;

namespace ComputerToStm
{
    public partial class Form1 : MetroForm
    {
        cls objclass = new cls();
        SerialPort port;
        Thread rec_data;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroPanel1.Enabled = false;
            string[] ports = SerialPort.GetPortNames();
            foreach (string portz in ports)
            {
                this.metroComboBox1.Items.Add(portz);
            }
        }
        public void fun()
        { }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (btnSelect.Text == "Connect")
            {
                try
                {
                    metroPanel1.Enabled = true;
                    port = objclass.OpenPort(metroComboBox1.Text, 57600, 300, 300);
                    objclass.ExecCommand(this.port, "Connected", 300, "Error");
                    metroLabel2.ForeColor = Color.Green;
                    metroLabel2.Text = "Port Open Successfully";
                    rec_data = new Thread(fun);
                    rec_data.Start();
                    btnSelect.Text = "Close";

                }
                catch (Exception ex)
                {
                    metroLabel2.Text = "Port Error in Connecting";
                    MessageBox.Show("" + ex.ToString());
                }


            }
            else if (btnSelect.Text == "Close")
            {
                try 
                {
                    metroPanel1.Enabled = false;
                    objclass.ClosePort(port);
                    metroLabel2.Text = "Port Closed";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Port not closed sucessfully");
                }
            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            //red 
            objclass.ExecCommand(port, "RED", 100, "ERROR");
            metroLabel1.Text = "RED LED ON";
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            //green
            objclass.ExecCommand(port, "GREEN", 100, "ERROR");
            metroLabel1.Text = "GREEN LED ON";
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            //blue
            objclass.ExecCommand(port, "BLUE", 100, "ERROR");
            metroLabel1.Text = "BLUE LED ON";
        }
    }
}
