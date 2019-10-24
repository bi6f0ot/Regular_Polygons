using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RegularPolygons {
    public partial class ViewingWindow: Form {
        public ViewingWindow() { InitializeComponent(); }

        private Timer UpdateTimer;

        private List<Polygon> Shapes;
        private long NumOfShapes = 0;

        private const long MaxWidthOfPolygons = 150;

        private void SetPolygons() {
            NumOfShapes = ClientSize.Width / MaxWidthOfPolygons;

            Shapes = new List<Polygon>();
            float X, Y;
            PointF Location;
            long NumOfSides;
            for ( int i = 0; i < NumOfShapes; i++ ) {
                X = ClientSize.Width / (NumOfShapes + 1) * (i + 1);
                Y = ClientSize.Height / 2;

                Location = new PointF(X, Y);
                NumOfSides = i + 2;

                Polygon newPolygon = new Polygon(PointToClient(MousePosition)) {
                    Loc = Location,
                    Sides = NumOfSides,
                    Radius = MaxWidthOfPolygons * .33f
                };

                Shapes.Add(newPolygon);
            };
        }

        private void ViewingWindow_Load( object sender, EventArgs e ) {
            SetPolygons();

            UpdateTimer = new Timer { Interval = 10 };
            UpdateTimer.Tick += ( s, ea ) => { Invalidate(); };
            UpdateTimer.Start();
        }

        private void ViewingWindow_Resize( object sender, EventArgs e ) {
            float X, Y;
            for ( int i = 0; i < NumOfShapes; i++ ) {
                X = ClientSize.Width / (NumOfShapes + 1) * (i + 1);
                Y = ClientSize.Height / 2;

                Shapes[ i ].Loc = new PointF(X, Y);
            };
        }

        private void ViewingWindow_ResizeEnd( object sender, EventArgs e ) {
            SetPolygons();
        }

        private void ViewingWindow_Paint( object sender, PaintEventArgs e ) {
            foreach ( Polygon P in Shapes ) {
                P.Update(PointToClient(MousePosition));
                P.Display(e.Graphics);
            };
        }
    }
}
