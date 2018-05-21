using System.Drawing;

namespace RGZ
{
    public class Point2D
    {
        public double X, Y;

        public Point2D()
        {
            X = 0;
            Y = 0;
        }

        public Point2D(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Point2D(Point P)
        {
            X = P.X;
            Y = P.Y;
        }

        public static Point2D operator +(Point2D P1, Point2D P2)
        {
            var temp = new Point2D
            {
                X = P1.X + P2.X,
                Y = P1.Y + P2.Y
            };
            return temp;
        }

        public static Point2D operator -(Point2D P1, Point2D P2)
        {
            var temp = new Point2D
            {
                X = P1.X - P2.X,
                Y = P1.Y - P2.Y
            };
            return temp;
        }

        public static Point2D operator *(Point2D P, double D)
        {
            var temp = new Point2D(P.X, P.Y);
            temp.X *= D;
            temp.Y *= D;
            return temp;
        }

        public static Point2D operator /(Point2D P, double D)
        {
            var temp = new Point2D(P.X, P.Y);
            temp.X /= D;
            temp.Y /= D;
            return temp;
        }

        public float[] ToArray()
        {
            var result = new float[2];
            result[0] = (float)X;
            result[1] = (float)Y;
            return result;
        }
    }
}
