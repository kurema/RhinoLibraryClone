using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    public struct PointGeneral:IEnumerable<double>,IComparable
    {
        private double[] _Content;
        public double this[int index]
        {
            get
            {
                if (index < this.Dimension) { return _Content[index]; } else { throw new IndexOutOfRangeException(); }
            }
            set
            {
                if (index < this.Dimension) { _Content[index]=value; } else { throw new IndexOutOfRangeException(); }

            }
        }
        public int Dimension
        {
            get { return _Content.Length; }
        }
        public bool IsValid
        {
            get
            {
                foreach(var item in _Content)
                {
                    if (!RhinoMath.IsValidDouble(item)) return false;
                }
                return true;
            }
        }

        public double MinimumCoordinate
        {
            get
            {
                var result = double.MaxValue;
                foreach (var item in _Content)
                {
                    if (RhinoMath.IsValidDouble(item))
                    {
                        result = Math.Min(result, Math.Abs(item));
                    }
                }
                return result;
            }
        }

        public double MaximumCoordinate
        {
            get
            {
                var result = 0.0;
                foreach (var item in _Content)
                {
                    if (RhinoMath.IsValidDouble(item))
                    {
                        result = Math.Max(result, Math.Abs(item));
                    }
                }
                return result;
            }
        }

        public PointGeneral(params double[] coordinate)
        {
            this._Content = coordinate;
        }

        public PointGeneral(PointGeneral original)
        {
            var result = new double[original.Dimension];
            for(int i = 0; i < original.Dimension; i++)
            {
                result[i] = original[i];
            }
            this._Content = result;
        }

        public static PointGeneral Multiply(PointGeneral a, double b)
        {
            var temp = new double[a.Dimension];
            for(int i = 0; i < a.Dimension; i++)
            {
                temp[i] = a[i] * b;
            }
            return new PointGeneral(temp);
        }

        public static PointGeneral Multiply(double b,PointGeneral a)
        {
            return Multiply(a, b);
        }

        public static PointGeneral operator *(PointGeneral a,double b)
        {
            return Multiply(a, b);
        }

        public static PointGeneral operator *(double b,PointGeneral a)
        {
            return Multiply(a, b);
        }

        public static PointGeneral Divide(PointGeneral a,double b)
        {
            var temp = new double[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
            {
                temp[i] = a[i] / b;
            }
            return new PointGeneral(temp);
        }

        public static PointGeneral operator /(double b, PointGeneral a)
        {
            return Divide(a, b);
        }

        public static PointGeneral Add(PointGeneral a,PointGeneral b)
        {
            var dimensionMax = Math.Max(a.Dimension, b.Dimension);
            var dimensionMin = Math.Min(a.Dimension, b.Dimension);
            var temp = new double[dimensionMax];
            for (int i = 0; i < dimensionMin; i++)
            {
                temp[i] = a[i] + b[i];
            }
            for (int i = dimensionMin; i < dimensionMax; i++)
            {
                if (a.Dimension > b.Dimension) { temp[i] = a[i]; }
                else { temp[i] = b[i]; }
            }
            return new PointGeneral(temp);
        }

        public static PointGeneral operator +(PointGeneral a,PointGeneral b)
        {
            return Add(a, b);
        }

        public static PointGeneral Subtract(PointGeneral a, PointGeneral b)
        {
            var dimensionMax = Math.Max(a.Dimension, b.Dimension);
            var dimensionMin = Math.Min(a.Dimension, b.Dimension);
            var temp = new double[dimensionMax];
            for (int i = 0; i < dimensionMin; i++)
            {
                temp[i] = a[i] - b[i];
            }
            for (int i = dimensionMin; i < dimensionMax; i++)
            {
                if (a.Dimension > b.Dimension) { temp[i] = a[i]; }
                else { temp[i] = -b[i]; }
            }
            return new PointGeneral(temp);
        }

        public static PointGeneral operator -(PointGeneral a, PointGeneral b)
        {
            return Subtract(a, b);
        }

        public static bool operator ==(PointGeneral a,PointGeneral b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(PointGeneral a, PointGeneral b)
        {
            return a.CompareTo(b) != 0;
        }

        public static bool operator <(PointGeneral a, PointGeneral b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator <=(PointGeneral a, PointGeneral b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >(PointGeneral a, PointGeneral b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(PointGeneral a, PointGeneral b)
        {
            return a.CompareTo(b) >= 0;
        }


        public override bool Equals(object obj)
        {
            return obj is PointGeneral && this == (PointGeneral)obj;
        }

        public bool Equal(PointGeneral target)
        {
            return target == this;
        }

        public override int GetHashCode()
        {
            int result = 0;
            for(int i = 0; i < this.Dimension; i++)
            {
                result = result ^ this[i].GetHashCode();
            }
            return result;
        }

        public bool EpsilonEquals(PointGeneral target,double epsilon)
        {
            for (int i = 0; i < this.Dimension; i++)
            {
                if (!RhinoMath.EpsilonEquals(this[i], target[i], epsilon)) { return false; }
            }
            return true;
        }

        public override string ToString()
        {
            if (this.Dimension == 0) { return ""; }
            string result="";
            for (int i = 0; i < this.Dimension -1; i++)
            {
                result += "{" + this[i].ToString() + "},";
            }
            result += "{" + this[this.Dimension-1].ToString() + "}";
            return result;
        }

        public int CompareTo(PointGeneral target)
        {
            for(int i = 0; i < target.Dimension; i++)
            {
                if (this[i] < target[i]) { return -1; }
                if (this[i] > target[i]) { return 1; }
            }
            return 0;
        }

        public int CompareTo(object obj)
        {
            if(obj is PointGeneral)
            {
                return this.CompareTo((PointGeneral)obj);
            }
            throw new ArgumentException();
        }

        public IEnumerator<double> GetEnumerator()
        {
            return ((IEnumerable<double>)_Content).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<double>)_Content).GetEnumerator();
        }


    }
}
