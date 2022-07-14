using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace GMap_Test
{
    class Map
    {
        public GMapControl App;
        public GMapOverlay MarkerOverlay = new GMapOverlay("markers");
        public GMapOverlay RouteOverlay = new GMapOverlay("routes");

        public Map(GMapControl app)
        {
            // App Connection
            this.App = app;
            this.App.MapProvider = GMapProviders.GoogleMap;
            this.App.Overlays.Add(MarkerOverlay);

            // Default Zoom Level
            this.App.Zoom = 16;
            this.App.MaxZoom = 25;
            this.App.MinZoom = 10;

            // Default Position
            this.App.Position = new PointLatLng(37.387688, 127.123137);

            // Default Route
            List<PointLatLng> points = new List<PointLatLng>()
            {
                new PointLatLng(37.387510, 127.122976),
                new PointLatLng(37.387945, 127.122508),
                new PointLatLng(37.388936, 127.123786),
                new PointLatLng(37.390100, 127.122500)
            };

            //// Route Binding
            //// points 변수를 경로화 시킨다. (오버레이에 경로를 추가함) 
            //GMapRoute route = new GMapRoute(points, "경로");
            //route.Stroke = new Pen(Color.Red, 3);
            //route.IsHitTestVisible = true;
            //RouteOverlay.Routes.Add(route);
            //this.App.Overlays.Add(RouteOverlay);
            //RouteOverlay.Markers.Add(new GMarkerCross(new PointLatLng(37.390100, 127.122500))
            //{
            //    ToolTipText = "\n" + route.Distance.ToString() + " km",
            //    IsVisible = false,
            //    ToolTipMode = MarkerTooltipMode.Always
            //});

            // Event Binding
            this.App.MouseDown += MouseDown;
            this.App.OnMarkerClick += OnMarkerClick;
            this.App.OnRouteEnter += OnRouteEnter;
            this.App.OnRouteLeave += OnRouteLeave;

            // Debug
            //Console.WriteLine(route.Distance);

        }

        public void AddMarker_RedDot(PointLatLng p, string text)
        {
            GMarkerGoogle gMarker = new GMarkerGoogle(p, GMarkerGoogleType.red_dot);
            gMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            gMarker.ToolTipText = text;
            gMarker.ToolTip.TextPadding = new Size(10, 10);
            gMarker.ToolTip.Fill = new SolidBrush(Color.DimGray);
            gMarker.ToolTip.Foreground = new SolidBrush(Color.White);
            gMarker.ToolTip.Offset = new Point(10, -30);
            gMarker.ToolTip.Stroke = new Pen(Color.Transparent, .0f);
            MarkerOverlay.Markers.Add(gMarker);
        }

        public void AddMarker_LightBlue(PointLatLng p, string text)
        {
            GMapMarker gMarker = new MapViewPointMarker(p, 13, Color.LightBlue, MapLegendShapes.Circle);
            //GMarkerGoogle gMarker = new GMarkerGoogle(p, GMarkerGoogleType.lightblue);
            //gMarker.ToolTip = new GMapToolTip(gMarker);               // TOS에서 가져온 코드로 진행하니 ToolTip 이 표시되지 않음
            //gMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            //gMarker.ToolTipText = text;
            MarkerOverlay.Markers.Add(gMarker);
        }

        public void AddRoute(List<PointLatLng> p)
        {
            GMapRoute route = new GMapRoute(p, "경로");
            route.Stroke = new Pen(Color.Red, 3);
            //route.IsHitTestVisible = true;
            RouteOverlay.Routes.Add(route);
            //this.App.Overlays.Add(RouteOverlay);
        }

        public void RemoveMarker(GMapMarker gMarker)
        {
            MarkerOverlay.Markers.Remove(gMarker);
        }

        private void OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                RemoveMarker(item);
            }
        }

        private void OnRouteEnter(GMapRoute item)
        {
            RouteOverlay.Markers.First().IsVisible = true;
        }

        private void OnRouteLeave(GMapRoute item)
        {
            RouteOverlay.Markers.First().IsVisible = false;
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            // 현재 마우스 위치의 위도, 경도 받아오기
            PointLatLng p = App.FromLocalToLatLng(e.X, e.Y);
            // 마우스 왼쪽 버튼 클릭 시
            if (e.Button == MouseButtons.Left)
            {
                // 마우스가 놓인 위치에 설명과 함께 마커 생성 - AddMarker(위치, 설명)
                AddMarker_RedDot(p, "\n" + p.Lat.ToString() + "\n" + p.Lng.ToString());
            }
        }

        public PointLatLng Position
        {
            get { return App.Position; }
            set { App.Position = value; }
        }

        public void SetPositionByKeywords(string keys)
        {
            App.SetPositionByKeywords(keys);
        }



        /// <summary>
        /// TOS 프로그램에서 가져온 Marker 설정
        /// </summary>
        public enum MapLegendShapes
        {
            Circle,
            Rectangle
        }

        public class MapViewPointMarker : GMap.NET.WindowsForms.GMapMarker
        {
            PointLatLng _point;
            float _size = 0;
            Color _col = Color.White;
            MapLegendShapes _shape = MapLegendShapes.Circle;

            public PointLatLng Point
            {
                get
                {
                    return _point;
                }
                set
                {
                    _point = value;
                }
            }

            public MapViewPointMarker(PointLatLng p, int size, Color col, MapLegendShapes shape) : base(p)
            {
                _point = p;
                _size = size;
                _col = col;
                _shape = shape;
            }

            public override void OnRender(Graphics g)
            {
                int outlineSize = 0;

                Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y, (int)_size, (int)_size);
                Rectangle rectOutline = new Rectangle(LocalPosition.X - outlineSize, LocalPosition.Y - outlineSize,
                    (int)_size + outlineSize * 2, (int)_size + outlineSize * 2);

                SolidBrush br = new SolidBrush(_col);
                SolidBrush brOutline = new SolidBrush(Color.Black);

                Pen penOutline = new Pen(brOutline, outlineSize);


                if (_shape == MapLegendShapes.Circle)
                {
                    g.FillEllipse(br, rect);
                    g.DrawEllipse(penOutline, rectOutline);
                }
                else
                {
                    g.FillRectangle(br, rect);
                    g.DrawRectangle(penOutline, rectOutline);
                }

                penOutline.Dispose();
                br.Dispose();
                brOutline.Dispose();
            }
        }
    }
}
