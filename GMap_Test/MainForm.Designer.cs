
namespace GMap_Test
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.btn1 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGPSPort = new System.Windows.Forms.ComboBox();
            this.btnConnectGPS = new System.Windows.Forms.Button();
            this.GPSsp = new System.IO.Ports.SerialPort(this.components);
            this.btnSearchPort = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, 0);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 24;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(1297, 919);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(1210, 12);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 23);
            this.btn1.TabIndex = 1;
            this.btn1.Text = "여암빌딩";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // btn2
            // 
            this.btn2.Location = new System.Drawing.Point(1210, 41);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 23);
            this.btn2.TabIndex = 2;
            this.btn2.Text = "button2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.btn2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(846, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "GPS State";
            // 
            // cbGPSPort
            // 
            this.cbGPSPort.FormattingEnabled = true;
            this.cbGPSPort.Location = new System.Drawing.Point(922, 17);
            this.cbGPSPort.Name = "cbGPSPort";
            this.cbGPSPort.Size = new System.Drawing.Size(121, 20);
            this.cbGPSPort.TabIndex = 4;
            // 
            // btnConnectGPS
            // 
            this.btnConnectGPS.Location = new System.Drawing.Point(841, 15);
            this.btnConnectGPS.Name = "btnConnectGPS";
            this.btnConnectGPS.Size = new System.Drawing.Size(75, 23);
            this.btnConnectGPS.TabIndex = 5;
            this.btnConnectGPS.Text = "GPS 연결";
            this.btnConnectGPS.UseVisualStyleBackColor = true;
            this.btnConnectGPS.Click += new System.EventHandler(this.btnConnectGPS_Click);
            // 
            // GPSsp
            // 
            this.GPSsp.PortName = "COM3";
            // 
            // btnSearchPort
            // 
            this.btnSearchPort.Location = new System.Drawing.Point(760, 15);
            this.btnSearchPort.Name = "btnSearchPort";
            this.btnSearchPort.Size = new System.Drawing.Size(75, 23);
            this.btnSearchPort.TabIndex = 6;
            this.btnSearchPort.Text = "포트 검색";
            this.btnSearchPort.UseVisualStyleBackColor = true;
            this.btnSearchPort.Click += new System.EventHandler(this.btnSearchPort_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1049, 17);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(151, 43);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1047, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "GPS Distance";
            // 
            // btnDeleteDB
            // 
            this.btnDeleteDB.Location = new System.Drawing.Point(1079, 90);
            this.btnDeleteDB.Name = "btnDeleteDB";
            this.btnDeleteDB.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDB.TabIndex = 9;
            this.btnDeleteDB.Text = "DB 초기화";
            this.btnDeleteDB.UseVisualStyleBackColor = true;
            this.btnDeleteDB.Click += new System.EventHandler(this.btnDeleteDB_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1297, 919);
            this.Controls.Add(this.btnDeleteDB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnSearchPort);
            this.Controls.Add(this.btnConnectGPS);
            this.Controls.Add(this.cbGPSPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.gMapControl1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGPSPort;
        private System.Windows.Forms.Button btnConnectGPS;
        private System.IO.Ports.SerialPort GPSsp;
        private System.Windows.Forms.Button btnSearchPort;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteDB;
    }
}

