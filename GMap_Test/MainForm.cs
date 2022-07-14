using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GMap_Test.Map;

namespace GMap_Test
{
    public partial class MainForm : Form
    {
        MySqlConnection sql = new MySqlConnection("Server=localhost;Database=gpsmapping;Uid=root;Pwd=admin");
        Map map;
        Thread gpsReadThread;
        public GMapOverlay MarkerOverlay = new GMapOverlay("markers");
        List<double> gpsNow1 = new List<double>();
        List<double> gpsNow2 = new List<double>();
        List<PointLatLng> gpsRoute = new List<PointLatLng>();
        double gpsAllDistance = 0;
        int DBcount;


        public MainForm()
        {
            InitializeComponent();
            map = new Map(gMapControl1);
            btnConnectGPS.Text = "GPS 연결";
            label1.Text = "GPS is Disconnected";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            map.Position = new PointLatLng(37.387688, 127.123137);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            map.SetPositionByKeywords("Seoul Station");
            Console.WriteLine(map.Position);
        }

        private void btnConnectGPS_Click(object sender, EventArgs e)
        {
            if (btnConnectGPS.Text == "GPS 연결")
            {
                if (cbGPSPort.Text != "")
                {
                    if (!GPSsp.IsOpen)
                    {
                        GPSsp.PortName = cbGPSPort.Text;
                        GPSsp.BaudRate = 9600;
                        GPSsp.Parity = Parity.None;
                        GPSsp.DataBits = 8;
                        GPSsp.StopBits = StopBits.One;
                        //GPSsp.DataReceived += new SerialDataReceivedEventHandler(GPSsp_DataReceived);

                        GPSsp.Open();

                        cbGPSPort.Enabled = false;
                        btnConnectGPS.Text = "GPS 끊기";
                        label1.Text = "GPS is Connected";

                        gpsReadThread = new Thread(gpsRead);
                        gpsReadThread.Start();
                    }
                    else
                    {
                        MessageBox.Show("이미 연결되어있습니다.");
                    }
                }
                else
                {
                    MessageBox.Show("포트를 선택하십시오.");
                }
            }
            else
            {
                gpsReadThread.Abort();
                GPSsp.Close();
                btnConnectGPS.Text = "GPS 연결";
                cbGPSPort.Enabled = true;
                label1.Text = "GPS is Disconnected";
            }
        }

        string[] GPS_Data;
        List<string> GPS_Data_List = new List<string>();
        private void gpsRead()
        {
            string readData;
            while (true)
            {
                try
                {
                    string st2 = string.Format("SELECT count(*) FROM gps_data;");
                    sql.Open();
                    MySqlCommand cm2 = new MySqlCommand(st2, sql);
                    MySqlDataReader md2 = cm2.ExecuteReader();
                    while (md2.Read())
                    {
                        DBcount = Int32.Parse(md2[0].ToString());
                    }
                    sql.Close();

                    if (DBcount >= 4)
                    {
                        string path = @"D:/FileExport_Test/GPS_Record_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                        //string path = @"D:/FileExport_Test/test.csv";
                        string st3 = string.Format("SELECT * FROM gps_data INTO OUTFILE \'{0}\' FIELDS TERMINATED BY ',' LINES TERMINATED BY '\r\n';", path);
                        sql.Open();
                        MySqlCommand cm3 = new MySqlCommand(st3, sql);
                        MySqlDataReader md3 = cm3.ExecuteReader();
                        sql.Close();

                        DeleteDB();
                    }


                    readData = GPSsp.ReadLine();
                    if (richTextBox1.InvokeRequired)
                    {
                        if (readData.Contains("GNGGA"))
                        {
                            GPS_Data_List.Clear();
                            GPS_Data = readData.Split(',');
                            for (int i = 0; i > GPS_Data.Length; i++)
                            {
                                GPS_Data_List.Add(GPS_Data[i]);
                            }
                            richTextBox1.Invoke(new MethodInvoker(delegate { richTextBox1.Text = ""; }));

                            if (GPS_Data[3] == "N" && GPS_Data[5] == "E")
                            {
                                string[] GPS_DATA_2 = GPS_Data[2].Split('.');
                                GPS_DATA_2[1] = GPS_DATA_2[0].Substring(GPS_DATA_2[0].Length - 2) + GPS_DATA_2[1];
                                GPS_DATA_2[0] = GPS_DATA_2[0].Substring(0, GPS_DATA_2[0].Length - 2);
                                Double LatLow = Double.Parse(GPS_DATA_2[1]) / 60;
                                if (LatLow >= 1)
                                {
                                    GPS_DATA_2[1] = LatLow.ToString().Replace(".", "");
                                }
                                else
                                {
                                    LatLow = Double.Parse(LatLow.ToString().Replace("0.", ""));
                                    GPS_DATA_2[1] = LatLow.ToString();
                                }
                                GPS_Data[2] = GPS_DATA_2[0] + "." + GPS_DATA_2[1];

                                string[] GPS_DATA_4 = GPS_Data[4].Split('.');
                                GPS_DATA_4[1] = GPS_DATA_4[0].Substring(GPS_DATA_4[0].Length - 2) + GPS_DATA_4[1];
                                GPS_DATA_4[0] = GPS_DATA_4[0].Substring(0, GPS_DATA_4[0].Length - 2);
                                Double LngLow = Double.Parse(GPS_DATA_4[1]) / 60;
                                if (LngLow >= 1)
                                {
                                    GPS_DATA_4[1] = LngLow.ToString().Replace(".", "");
                                }
                                else
                                {
                                    LngLow = Double.Parse(LngLow.ToString().Replace("0.", ""));
                                    GPS_DATA_4[1] = LngLow.ToString();
                                }
                                GPS_Data[4] = GPS_DATA_4[0] + "." + GPS_DATA_4[1];

                                
                                string st1 = string.Format("INSERT INTO gps_data VALUES (\'{0}\', \'{1}\', \'{2}\');", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), GPS_Data[2], GPS_Data[4]);
                                sql.Open();
                                MySqlCommand cm1 = new MySqlCommand(st1, sql);
                                MySqlDataReader md1 = cm1.ExecuteReader();
                                sql.Close();

                                PointLatLng p = new PointLatLng(Double.Parse(GPS_Data[2]), Double.Parse(GPS_Data[4]));
                                //map.AddMarker_LightBlue(p, "\n" + DateTime.Now.ToString());

                                // GPS 거리 구하기
                                GPS_Distance();

                                // GPS 루트 그리기
                                map.AddRoute(gpsRoute);
                            }
                            else if (GPS_Data[3] == "" && GPS_Data[5] == "")
                            {
                                GPS_Data[2] = "GPS signal is weak";
                            }

                            richTextBox1.Invoke(new MethodInvoker(delegate {
                                //richTextBox1.AppendText(GPS_Data[0] + "\n");
                                //richTextBox1.AppendText(GPS_Data[1] + "\n");
                                richTextBox1.AppendText(GPS_Data[2] + "\n");
                                //richTextBox1.AppendText(GPS_Data[3] + "\n");
                                richTextBox1.AppendText(GPS_Data[4] + "\n");
                                //richTextBox1.AppendText(GPS_Data[5] + "\n");
                                //richTextBox1.AppendText(GPS_Data[6] + "\n");
                                //richTextBox1.AppendText(GPS_Data[7] + "\n");
                                //richTextBox1.AppendText(GPS_Data[8] + "\n");
                                //richTextBox1.AppendText(GPS_Data[9] + "\n");
                                //richTextBox1.AppendText(GPS_Data[10] + "\n");
                                //richTextBox1.AppendText(GPS_Data[11] + "\n");
                                //richTextBox1.AppendText(GPS_Data[12] + "\n");
                                //richTextBox1.AppendText(GPS_Data[13] + "\n");
                                //richTextBox1.AppendText(GPS_Data[14] + "\n");
                            }));
                            Thread.Sleep(5000);
                        }
                    }
                    else
                    {
                        richTextBox1.Text = string.Empty;
                    }
                }
                catch (System.TimeoutException)
                {

                }
            }
        }

        private void btnSearchPort_Click(object sender, EventArgs e)
        {
            try
            {
                string[] portName = SerialPort.GetPortNames();

                cbGPSPort.Items.Clear();

                foreach (string port in portName)
                {
                    cbGPSPort.Items.Add(port);
                }
                cbGPSPort.Text = portName[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (GPSsp.IsOpen)
                {
                    if (gpsReadThread.IsAlive)
                    {
                        gpsReadThread.Abort();
                        GPSsp.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void GPS_Distance()
        {
            //GMapMarker currentMarker = new MapViewPointMarker(gpsRoute[1], 13, Color.LightBlue, MapLegendShapes.Circle);
            MarkerOverlay.Markers.Clear();

            string st1 = string.Format("SELECT * FROM gps_data WHERE OccurTime IN (SELECT MAX(OccurTime) FROM gps_data);");
            sql.Open();
            MySqlCommand cm1 = new MySqlCommand(st1, sql);
            MySqlDataReader md1 = cm1.ExecuteReader();
            while (md1.Read())
            {
                if (gpsNow1.Count < 1 && gpsNow2.Count < 1)
                {
                    gpsNow1.Add((double)md1[1]);
                    gpsNow1.Add((double)md1[2]);
                    gpsNow2.Add((double)md1[1]);
                    gpsNow2.Add((double)md1[2]);
                    gpsRoute.Add(new PointLatLng(gpsNow1[0], gpsNow1[1]));
                    gpsRoute.Add(new PointLatLng(gpsNow2[0], gpsNow2[1]));
                }
                else
                {
                    gpsNow1[0] = gpsNow2[0];
                    gpsNow1[1] = gpsNow2[1];
                    gpsNow2[0] = (double)md1[1];
                    gpsNow2[1] = (double)md1[2];
                    gpsRoute[0] = new PointLatLng(gpsNow1[0], gpsNow1[1]);
                    gpsRoute[1] = new PointLatLng(gpsNow2[0], gpsNow2[1]);
                }
            }
            sql.Close();

            GeoCoordinate pin1 = new GeoCoordinate(gpsNow1[0], gpsNow1[1]);
            GeoCoordinate pin2 = new GeoCoordinate(gpsNow2[0], gpsNow2[1]);
            double distanceBetween = pin1.GetDistanceTo(pin2);

            gpsAllDistance += distanceBetween;

            double truncateResult = Math.Truncate(gpsAllDistance);

            // 결과값을 반올림하여 출력(x.xxx)
            label2.Invoke(new MethodInvoker(delegate { label2.Text = "오늘의 측정 거리 : " + string.Format("{0:0.##0}", gpsAllDistance) + "m"; }));

            // 현재 위치는 아이콘으로 표시
            map.AddMarker_LightBlue(gpsRoute[1], "\n" + DateTime.Now.ToString());
        }

        private void btnDeleteDB_Click(object sender, EventArgs e)
        {
            DeleteDB();
        }

        public void DeleteDB()
        {
            string st1 = string.Format("TRUNCATE TABLE gps_data;");
            sql.Open();
            MySqlCommand cm1 = new MySqlCommand(st1, sql);
            MySqlDataReader md1 = cm1.ExecuteReader();
            sql.Close();
        }
    }
}
