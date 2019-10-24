using System;
using System.Collections.Generic;
using System.Drawing;

namespace RegularPolygons {
    internal class Polygon {
        internal PointF Loc;
        internal long Sides;
        internal float Radius;

        private float Heading;

        private void SetHeading( Point Cursor ) {
            float Diff_X, Diff_Y;
            Diff_X = Cursor.X - Loc.X;
            Diff_Y = Cursor.Y - Loc.Y;

            Heading = (float)Math.Atan2(Diff_Y, Diff_X);
        }

        public Polygon( Point Cursor ) {
            SetHeading(Cursor);
        }

        internal void Update( Point Cursor ) {
            SetHeading(Cursor);
        }

        internal void Display( Graphics g ) {
            g.DrawEllipse(Pens.White, Loc.X - 1, Loc.Y - 1, 2, 2);

            List<PointF> points = new List<PointF>();
            long Side = 0;
            float X, Y;
            for ( float i = Heading; i < Heading + 2 * Math.PI; i += 360 / Sides * ((float)Math.PI / 180) ) {
                X = Loc.X + Radius * (float)Math.Cos(i);
                Y = Loc.Y + Radius * (float)Math.Sin(i); ;

                points.Add(new PointF(X, Y));
                Side++;
            };
            points.Add(points[ 0 ]);

            g.DrawLines(Pens.White, points.ToArray());
        }
    }
}
