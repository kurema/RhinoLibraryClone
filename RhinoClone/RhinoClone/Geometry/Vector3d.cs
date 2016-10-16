using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    //just a wrapper for PointGeneral
    public struct Vector3d : IEnumerable<double>, IComparable
    {
        private VectorGeneral _Content;

        public double X { get { return _Content[0]; } set { _Content[0] = value; } }
        public double Y { get { return _Content[1]; } set { _Content[1] = value; } }
        public double Z { get { return _Content[2]; } set { _Content[2] = value; } }

        public static Vector3d Zero { get { return new Vector3d(0, 0, 0); } }
        public static Vector3d XAxis { get { return new Vector3d(1, 0, 0); } }
        public static Vector3d YAxis { get { return new Vector3d(0, 1, 0); } }
        public static Vector3d ZAxis { get { return new Vector3d(0, 0, 1); } }

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

        public Vector3d(double x, double y, double z)
        {
            this._Content = new VectorGeneral(x, y, z);
        }

        public Vector3d(Vector3d original)
        {
            this._Content = new VectorGeneral(original.X, original.Y, original.Z);
        }

        public static Vector3d Multiply(Vector3d a, double b)
        {
            return new Vector3d(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3d Multiply(double b, Vector3d a)
        {
            return Multiply(a, b);
        }

        public static Vector3d operator *(Vector3d a, double b)
        {
            return Multiply(a, b);
        }

        public static Vector3d operator *(double b, Vector3d a)
        {
            return Multiply(a, b);
        }

        public static Vector3d Divide(Vector3d a, double b)
        {
            return new Vector3d(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3d operator /(double b, Vector3d a)
        {
            return Divide(a, b);
        }

        public static Vector3d Add(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3d operator +(Vector3d a, Vector3d b)
        {
            return Add(a, b);
        }

        public static Vector3d Subtract(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3d operator -(Vector3d a, Vector3d b)
        {
            return Subtract(a, b);
        }

        public static bool operator ==(Vector3d a, Vector3d b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(Vector3d a, Vector3d b)
        {
            return a.CompareTo(b) != 0;
        }

        public static bool operator <(Vector3d a, Vector3d b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator <=(Vector3d a, Vector3d b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >(Vector3d a, Vector3d b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(Vector3d a, Vector3d b)
        {
            return a.CompareTo(b) >= 0;
        }


        public override bool Equals(object obj)
        {
            return obj is Vector3d && this == (Vector3d)obj;
        }

        public bool Equal(Vector3d target)
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

        public bool EpsilonEquals(Vector3d target, double epsilon)
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

        public int CompareTo(Vector3d target)
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

        public double DistanceTo(Vector3d other)
        {
            return this._Content.DistanceTo(other.ToPointGeneral());
        }

        public double Length
        {
            get
            {
                return _Content.Length;
            }
        }

        public double SquareLength
        {
            get
            {
                return _Content.SquareLength;
            }
        }
        public bool IsUnitVector
        {
            get
            {
                return Math.Abs(this.Length - 1) < RhinoMath.ZeroTolerance;
            }
        }

        public bool IsZero
        {
            get
            {
                return this == Zero;
            }
        }

        public static Vector3d Negate(Vector3d vector)
        {
            return new Vector3d(-vector.X, -vector.Y, -vector.Z);
        }

        public static Vector3d CrossProduct(Vector3d a,Vector3d b)
        {
            return new Vector3d(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        public static double VectorAngle(Vector3d a,Vector3d b)
        {
            a.Unitize();
            b.Unitize();
            return Math.Acos(a.X * b.X + a.Y * b.Y + a.Z * b.Z);
        }

        public bool Unitize()
        {
            if (!this.IsValid) return false;
            double length = this.Length;
            this.X /= length;
            this.Y /= length;
            this.Z /= length;
            return true;
        }

        /// <summary>
        /// Get unitized vector.
        /// <para>This is not in original Rhino Common SDK.</para>
        /// </summary>
        public Vector3d Unitized
        {
            get
            {
                var copy = this;
                copy.Unitize();
                return copy;
            }
        }

        public bool Reverse()
        {
            if (!IsValid) return false;
            X = -X;
            Y = -Y;
            Z = -Z;
            return true;
        }
        public int IsParallelTo(Vector3d other)
        {
            return IsParallelTo(other, RhinoMath.DefaultAngleTolerance);
        }

        public int IsParallelTo(Vector3d other,double angleTolerance)
        {
            if(this.IsZero || other.IsZero) { return 0; }
            double angle = VectorAngle(this, other);
            if (Math.Min(Math.PI - angle, angle) <= angleTolerance) { return 1; }
            return -1;
        }
        public bool IsPerpendicularTo(Vector3d other, double angleTolerance)
        {
            if (this.IsZero || other.IsZero) { return true; }
            return (Math.Abs(VectorAngle(this, other)) < angleTolerance);
        }

        public bool IsPerpendicularTo(Vector3d other)
        {
            return IsPerpendicularTo(other, RhinoMath.DefaultAngleTolerance);
        }
        //ToDo:PerpendicularTo
        //ToDo:Transform
    }
}
