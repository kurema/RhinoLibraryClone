using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    //just a wrapper for PointGeneral
    public struct Point3d : IEnumerable<double>, IComparable
    {
        private VectorGeneral _Content;

        public double X { get { return _Content[0]; } set { _Content[0] = value; } }
        public double Y { get { return _Content[1]; } set { _Content[1] = value; } }
        public double Z { get { return _Content[2]; } set { _Content[2] = value; } }

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

        public Point3d(double x,double y, double z)
        {
            this._Content = new VectorGeneral(x, y, z);
        }

        public Point3d(Point3d original)
        {
            this._Content = new VectorGeneral(original.X, original.Y, original.Z);
        }

        public static Point3d Multiply(Point3d a, double b)
        {
            return new Point3d(a.X * b, a.Y * b, a.Z * b);
        }

        public static Point3d Multiply(double b, Point3d a)
        {
            return Multiply(a, b);
        }

        public static Point3d operator *(Point3d a, double b)
        {
            return Multiply(a, b);
        }

        public static Point3d operator *(double b, Point3d a)
        {
            return Multiply(a, b);
        }

        public static Point3d Divide(Point3d a, double b)
        {
            return new Point3d(a.X / b, a.Y / b, a.Z / b);
        }

        public static Point3d operator /(double b, Point3d a)
        {
            return Divide(a, b);
        }

        public static Point3d Add(Point3d a, Point3d b)
        {
            return new Point3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point3d Add(Point3d a, Vector3d b)
        {
            return new Point3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Point3d operator +(Point3d a, Point3d b)
        {
            return Add(a, b);
        }

        public static Point3d operator +(Point3d a, Vector3d b)
        {
            return Add(a, b);
        }

        public static Point3d operator +(Vector3d a, Point3d b)
        {
            return Add(b, a);
        }

        public static Vector3d Subtract(Point3d a, Point3d b)
        {
            return new Vector3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Point3d Subtract(Point3d a, Vector3d b)
        {
            return new Point3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3d operator -(Point3d a, Point3d b)
        {
            return Subtract(a, b);
        }
        public static Point3d operator -(Point3d a, Vector3d b)
        {
            return Subtract(a, b);
        }

        public static bool operator ==(Point3d a, Point3d b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(Point3d a, Point3d b)
        {
            return a.CompareTo(b) != 0;
        }

        public static bool operator <(Point3d a, Point3d b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator <=(Point3d a, Point3d b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >(Point3d a, Point3d b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(Point3d a, Point3d b)
        {
            return a.CompareTo(b) >= 0;
        }


        public override bool Equals(object obj)
        {
            return obj is Point3d && this == (Point3d)obj;
        }

        public bool Equal(Point3d target)
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

        public bool EpsilonEquals(Point3d target, double epsilon)
        {
            return _Content.EpsilonEquals(target.ToPointGeneral(), epsilon);
        }

        public override string ToString()
        {
            return _Content.ToString();
        }

        public VectorGeneral ToPointGeneral()
        {
            return new VectorGeneral(this.X, this.Y, this.Z);
        }

        public int CompareTo(Point3d target)
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
