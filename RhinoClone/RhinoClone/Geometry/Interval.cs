using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhino.Geometry
{
    public struct Interval : IComparable<Interval>  ,IEquatable<Interval>
    {
        private double _T0;
        private double _T1;

        public Interval(double T0,double T1)
        {
            this._T0 = T0;
            this._T1 = T1;
        }

        public Interval(Interval original)
        {
            this._T0 = original.T0;
            this._T1 = original.T1;
        }

        public double T0 { get { return _T0; } set { _T0 = value; } }
        public double T1 { get { return _T1; } set { _T1 = value; } }
        public double this[int index]
        {
            get
            {
                if (index == 0) { return T0; }
                else if (index == 1) { return T1; }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index == 0) { T0 = value; }
                else if (index == 1) { T1 = value; }
                else { throw new IndexOutOfRangeException(); }
            }
        }

        public int CompareTo(Interval target)
        {
            if (T0 < target.T0) { return -1; }
            if (T0 > target.T0) { return 1; }
            if (T1 < target.T1) { return -1; }
            if (T1 > target.T1) { return 1; }
            return 0;
        }
        public override bool Equals(object obj)
        {
            return obj is Interval && this == (Interval)obj;
        }
        public bool Equals(Interval target)
        {
            return this.CompareTo(target) == 0;
        }

        public bool IsValid
        {
            get
            {
                return RhinoMath.IsValidDouble(T0) && RhinoMath.IsValidDouble(T1);
            }
        }

        public double Min { get { return Math.Min(T0, T1); } }
        public double Max { get { return Math.Max(T0, T1); } }
        public double Mid { get { return (T0 + T1) / 2.0; } }
        public double Length { get { return T1 - T0; } }
        public bool IsSingleton { get { return T0 == T1; } }
        public bool IsIncreasing { get { return T0 < T1; } }
        public bool IsDecreasing { get { return T0 > T1; } }

        public override string ToString()
        {
            return string.Format("{0},{1}", T0.ToString(), T1.ToString());
        }
        public override int GetHashCode()
        {
            return T0.GetHashCode() ^ T1.GetHashCode();
        }

        public void Swap()
        {
            var temp = T0;
            T0 = T1;
            T1 = temp;
        }
        public void MakeIncreasing()
        {
            if (this.IsDecreasing) { this.Swap(); }
        }
        public void Grow(double value)
        {
            this.MakeIncreasing();
            if (T0 > value) { T0 = value; }
            if (T1 < value) { T1 = value; }
        }
        public void Reverse()
        {
            double temp = T0;
            T0 = -T1;
            T1 = -temp;
        }

        public double ParameterAt(double noramlizedParameter)
        {
            return (1.0 - noramlizedParameter) * T0 + noramlizedParameter * T1;
        }
        public Interval ParameterIntervalAt(Interval normalizedInterval)
        {
            double t0 = this.ParameterAt(normalizedInterval.T0);
            double t1 = this.ParameterAt(normalizedInterval.T1);
            return new Interval(t0, t1);
        }
        public double NormalizedParameterAt(double parameter)
        {
            if (this.IsSingleton) { return T0; }
            else { return (parameter - T0) / this.Length; }
        }
        public Interval NormalizedIntervalAt(Interval parameter)
        {
            return new Interval(NormalizedParameterAt(parameter.T0), NormalizedParameterAt(parameter.T1));
        }
        public bool IncludesParameter(double t)
        {
            return IncludesParameter(t, false);
        }

        public bool IncludesParameter(double t,bool strict)
        {
            if (strict)
            {
                if (T0 < t && t < T1) { return true; }
                if (T1 < t && t < T0) { return true; }
            }else
            {
                if (T0 <= t && t <= T1) { return true; }
                if (T1 <= t && t <= T0) { return true; }
            }
            return false;
        }
        public bool IncludesInterval(Interval intarval,bool strict)
        {
            return this.IncludesParameter(T0, strict) && this.IncludesParameter(T1, strict);
        }
        public static Interval FromIntersection(Interval a,Interval b)
        {
            var a2 = new Interval(a);
            a2.MakeIncreasing();
            var b2 = new Interval(b);
            b2.MakeIncreasing();

            if(a2.Max < b2.Min || b2.Max < a2.Min) { return default(Interval); }
            return new Interval(Math.Max(a2.Min, b2.Min), Math.Min(a2.Max, b2.Max));
        }
        public static Interval FromUnion(Interval a, Interval b)
        {
            var a2 = new Interval(a);
            a2.MakeIncreasing();
            var b2 = new Interval(b);
            b2.MakeIncreasing();

            if (a2.Max < b2.Min || b2.Max < a2.Min) { return default(Interval); }
            return new Interval(Math.Min(a2.Min, b2.Min), Math.Max(a2.Max, b2.Max));
        }



        public static bool operator ==(Interval a,Interval b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Interval a, Interval b)
        {
            return !(a == b);
        }
        public static Interval operator +(Interval interval,double number)
        {
            return new Interval(interval.T0 + number, interval.T1 + number);
        }
        public static Interval operator +(double number,Interval interval)
        {
            return interval + number;
        }
        public static Interval operator -(Interval interval, double number)
        {
            return new Interval(interval.T0 - number, interval.T1 - number);
        }
        public static Interval operator -(double number, Interval interval)
        {
            return new Interval(number - interval.T0, number - interval.T1);
        }
        public static bool operator <(Interval a,Interval b)
        {
            return a.CompareTo(b) < 0;
        }
        public static bool operator >=(Interval a, Interval b)
        {
            return !(a < b);
        }
        public static bool operator >(Interval a, Interval b)
        {
            return a.CompareTo(b) > 0;
        }
        public static bool operator <=(Interval a, Interval b)
        {
            return !(a < b);
        }
        public bool EpsilonEquals(Interval target,double epsilon)
        {
            return RhinoMath.EpsilonEquals(this.T0, target.T0, epsilon) && RhinoMath.EpsilonEquals(this.T1, target.T1, epsilon);
        }
    }
}
