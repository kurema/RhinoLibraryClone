using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    //just a wrapper for PointGeneral
    public struct Point2d : IEnumerable<double>, IComparable
    {
        private VectorGeneral _Content;

        public double X { get { return _Content[0]; } set { _Content[0] = value; } }
        public double Y { get { return _Content[1]; } set { _Content[1] = value; } }

        public double this[int index]
        {
            get
            {
                return _Content[index];
            }
            set
            {
                _Content[index] = value;
            }
        }
        public int Dimension
        {
            get { return _Content.Dimension; }
        }
        public bool IsValid
        {
            get
            {
                return _Content.IsValid;
            }
        }

        public double MinimumCoordinate
        {
            get
            {
                return _Content.MinimumCoordinate;
            }
        }

        public double MaximumCoordinate
        {
            get
            {
                return _Content.MaximumCoordinate;
            }
        }

        public Point2d(double x, double y)
        {
            this._Content = new VectorGeneral(x, y);
        }

        public Point2d(Point2d original)
        {
            this._Content = new VectorGeneral(original.X, original.Y);
        }

        public static Point2d Multiply(Point2d a, double b)
        {
            return new Point2d(a.X * b, a.Y * b);
        }

        public static Point2d Multiply(double b, Point2d a)
        {
            return Multiply(a, b);
        }

        public static Point2d operator *(Point2d a, double b)
        {
            return Multiply(a, b);
        }

        public static Point2d operator *(double b, Point2d a)
        {
            return Multiply(a, b);
        }

        public static Point2d Divide(Point2d a, double b)
        {
            return new Point2d(a.X / b, a.Y / b);
        }

        public static Point2d operator /(double b, Point2d a)
        {
            return Divide(a, b);
        }

        public static Point2d Add(Point2d a, Point2d b)
        {
            return new Point2d(a.X + b.X, a.Y + b.Y);
        }

        public static Point2d operator +(Point2d a, Point2d b)
        {
            return Add(a, b);
        }

        public static Point2d Subtract(Point2d a, Point2d b)
        {
            return new Point2d(a.X - b.X, a.Y - b.Y);
        }

        public static Point2d operator -(Point2d a, Point2d b)
        {
            return Subtract(a, b);
        }

        public static bool operator ==(Point2d a, Point2d b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(Point2d a, Point2d b)
        {
            return a.CompareTo(b) != 0;
        }

        public static bool operator <(Point2d a, Point2d b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator <=(Point2d a, Point2d b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >(Point2d a, Point2d b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(Point2d a, Point2d b)
        {
            return a.CompareTo(b) >= 0;
        }


        public override bool Equals(object obj)
        {
            return obj is Point2d && this == (Point2d)obj;
        }

        public bool Equal(Point2d target)
        {
            return target == this;
        }

        public override int GetHashCode()
        {
            int result = 0;
            for (int i = 0; i < this.Dimension; i++)
            {
                result = result ^ this[i].GetHashCode();
            }
            return result;
        }

        public bool EpsilonEquals(Point2d target, double epsilon)
        {
            return _Content.EpsilonEquals(target.ToPointGeneral(), epsilon);
        }

        public override string ToString()
        {
            return _Content.ToString();
        }

        public VectorGeneral ToPointGeneral()
        {
            return new VectorGeneral(this.X, this.Y);
        }

        public int CompareTo(Point2d target)
        {
            return _Content.CompareTo(target.ToPointGeneral());
        }

        public int CompareTo(object obj)
        {
            return _Content.CompareTo(obj);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_Content).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)_Content).GetEnumerator();
        }
        public double DistanceTo(Point3d other)
        {
            return this._Content.DistanceTo(other.ToPointGeneral());
        }

    }
}
