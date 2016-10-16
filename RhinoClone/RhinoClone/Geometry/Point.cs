using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    public struct VectorGeneral:IEnumerable<double>,IComparable
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

        public VectorGeneral(params double[] coordinate)
        {
            this._Content = coordinate;
        }

        public VectorGeneral(VectorGeneral original)
        {
            var result = new double[original.Dimension];
            for(int i = 0; i < original.Dimension; i++)
            {
                result[i] = original[i];
            }
            this._Content = result;
        }

        public static VectorGeneral Multiply(VectorGeneral a, double b)
        {
            var temp = new double[a.Dimension];
            for(int i = 0; i < a.Dimension; i++)
            {
                temp[i] = a[i] * b;
            }
            return new VectorGeneral(temp);
        }

        public static VectorGeneral Multiply(double b,VectorGeneral a)
        {
            return Multiply(a, b);
        }

        public static VectorGeneral operator *(VectorGeneral a,double b)
        {
            return Multiply(a, b);
        }

        public static VectorGeneral operator *(double b,VectorGeneral a)
        {
            return Multiply(a, b);
        }

        public static VectorGeneral Divide(VectorGeneral a,double b)
        {
            var temp = new double[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
            {
                temp[i] = a[i] / b;
            }
            return new VectorGeneral(temp);
        }

        public static VectorGeneral operator /(double b, VectorGeneral a)
        {
            return Divide(a, b);
        }

        public static VectorGeneral Add(VectorGeneral a,VectorGeneral b)
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
            return new VectorGeneral(temp);
        }

        public static VectorGeneral operator +(VectorGeneral a,VectorGeneral b)
        {
            return Add(a, b);
        }

        public static VectorGeneral Subtract(VectorGeneral a, VectorGeneral b)
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
            return new VectorGeneral(temp);
        }

        public static VectorGeneral operator -(VectorGeneral a, VectorGeneral b)
        {
            return Subtract(a, b);
        }

        public static bool operator ==(VectorGeneral a,VectorGeneral b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator !=(VectorGeneral a, VectorGeneral b)
        {
            return a.CompareTo(b) != 0;
        }

        public static bool operator <(VectorGeneral a, VectorGeneral b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator <=(VectorGeneral a, VectorGeneral b)
        {
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >(VectorGeneral a, VectorGeneral b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(VectorGeneral a, VectorGeneral b)
        {
            return a.CompareTo(b) >= 0;
        }


        public override bool Equals(object obj)
        {
            return obj is VectorGeneral && this == (VectorGeneral)obj;
        }

        public bool Equal(VectorGeneral target)
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

        public bool EpsilonEquals(VectorGeneral target,double epsilon)
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

        public int CompareTo(VectorGeneral target)
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
            if(obj is VectorGeneral)
            {
                return this.CompareTo((VectorGeneral)obj);
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

        public double DistanceTo(VectorGeneral other)
        {
            return (this - other).Length;
        }
        public double Length
        {
            get
            {
                return Math.Sqrt(this.SquareLength);
            }
        }

        public double SquareLength
        {
            get
            {
                double result = 0;
                foreach (var item in this)
                {
                    result += item * item;
                }
                return result;
            }
        }

        public void SetByFunction(Func<int, double, double> f)
        {
            for (int i = 0; i < Dimension; i++)
            {
                this[i] = f(i, this[i]);
            }
        }

        public static VectorGeneral GenerateMatrix(Func<int, double> f, int dimension)
        {
            double[] array = new double[dimension];
            var result = new VectorGeneral(array);
            result.SetByFunction((a, b) => f(a));
            return result;
        }

        public bool Unitize()
        {
            double length = this.Length;
            if (!this.IsValid) return false;
            this.SetByFunction((a, b) => b / length);
            return true;
        }

        public VectorGeneral Unitized { get
            {
                var result = this;
                result.Unitize();
                return result;
            }
        }
    }
}
