using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace DLL
{
    public class GeometricClass
    {
        public static double GetLineAngle(PointF APointS, PointF APointE)
        {
            double angleOfLine = Math.Round((Math.Atan2((APointE.Y - APointS.Y), (APointE.X - APointS.X))) * 180 / Math.PI, 2);

            return angleOfLine;
        }

        public static double GetLinesAngle(PointF APointS ,PointF APointE , PointF BPointS ,PointF BPointE)
        {
            double angleOfLine = Math.Round((Math.Atan2((BPointE.Y - BPointS.Y), (BPointE.X - BPointS.X)) - Math.Atan2((APointE.Y - APointS.Y), (APointE.X - APointS.X))) * 180 / Math.PI, 2);

            return angleOfLine;
        }

        public static double GetLineLengh(PointF APointS, PointF APointE)
        {
            double value = Math.Sqrt(Math.Abs(APointS.X - APointE.X) * Math.Abs(APointS.X - APointE.X) + Math.Abs(APointS.Y - APointE.Y) * Math.Abs(APointS.Y - APointE.Y));

            return value;
        }

        public static double Area(params PointF[] Points)
        {
            List<PointF> points = new List<PointF>();


            foreach (var item in Points)
            {
                points.Add(item);
            }

            points.Add(points[0]);
            var area = Math.Abs(points.Take(points.Count - 1)
               .Select((p, i) => (points[i + 1].X - p.X) * (points[i + 1].Y + p.Y))
               .Sum() / 2);



            return area;

        }

    }
}
