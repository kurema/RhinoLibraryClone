using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    public struct LineGeneral : IEquatable<LineGeneral>
    {
        private VectorGeneral _From;
        private VectorGeneral _To;
        public VectorGeneral From
        {
            get { return _From; }
            set { if (value.Dimension != this.Dimension) { throw new Exception("Dimension missmach."); } _From = value; }
        }
        public VectorGeneral To
        {
            get { return _To; }
            set { if (value.Dimension != this.Dimension) { throw new Exception("Dimension missmach."); } _To = value; }
        }
        public int Dimension
        {
            get
            {
                return From.Dimension;
            }
        }

        public bool IsValid { get { return this.From.IsValid && this.To.IsValid; } }
        public double Length
        {
            get { return this.From.DistanceTo(To); }
            set
            {
                this.To = this.From + Direction.Unitized * value;
            }
        }

        public VectorGeneral Direction
        {
            get
            {
                return this.To - this.From;
            }
        }
        public VectorGeneral UnitTangent
        {
            get
            {
                return Direction.Unitized;
            }
        }
        public LineGeneral(VectorGeneral from, VectorGeneral to)
        {
            if (from.Dimension != to.Dimension) { throw new Exception(); }
            _From = from;
            _To = to;
        }
        public LineGeneral(VectorGeneral from, VectorGeneral direction, double length)
        {
            _From = from;
            _To = from + (direction.Unitized * length);
        }

        //ToDo:TryFitLineToPoints
        //ToDo:TryCreateBetweenCurves

        public static bool operator ==(LineGeneral a, LineGeneral b)
        {
            return a.From == b.From && a.To == b.To;
        }

        public static bool operator !=(LineGeneral a, LineGeneral b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is LineGeneral && this == (LineGeneral)obj;
        }

        public bool Equals(LineGeneral other)
        {
            return other == this;
        }

        public bool EpsilonEquals(LineGeneral other, double epsilon)
        {
            return this.From.EpsilonEquals(other.From, epsilon) && this.To.EpsilonEquals(other.To, epsilon);
        }

        public override int GetHashCode()
        {
            return From.GetHashCode() ^ To.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", this.From, ToString(), this.To.ToString());
        }

        public void Flip()
        {
            var temp = this.From;
            this.From = this.To;
            this.To = temp;
        }

        public VectorGeneral PointAt(double t)
        {
            return (1 - t) * From + t * To;
        }

        public double ClosestParameter(VectorGeneral point)
        {
            throw new NotImplementedException();
        }

        public VectorGeneral ClosestPoint(VectorGeneral point, bool limitToFiniteSegment)
        {
            double param = ClosestParameter(point);
            if (limitToFiniteSegment)
            {
                param = Math.Min(1.0, Math.Max(0.0, param));
            }
            return PointAt(param);
        }

        public double DistanceTo(VectorGeneral point, bool limitToFiniteSegment)
        {
            return this.ClosestPoint(point, limitToFiniteSegment).DistanceTo(point);
        }

        public double MinimumDistanceTo(VectorGeneral point)
        {
            return DistanceTo(point, true);
        }

        public double MinimumDistanceTo(LineGeneral line)
        {
            throw new NotImplementedException();
        }
        public double MaximumDistanceTo(VectorGeneral point)
        {
            return Math.Max(this.From.DistanceTo(point), this.To.DistanceTo(point));
        }

        public double MaximumDistanceTo(LineGeneral testLine)
        {
            return Math.Max(Math.Max(this.From.DistanceTo(testLine.From), this.From.DistanceTo(testLine.To)), Math.Max(this.To.DistanceTo(testLine.From), this.To.DistanceTo(testLine.To)));
        }

        //ToDo: Transform
        //ToDo: ToNurbsCurve

        public bool Extend(double start, double end)
        {
            if (!this.IsValid || this.Length == 0)
            {
                return false;
            }

            var from = this.From;
            var to = this.To;
            var unit = this.UnitTangent;

            from = from - start * unit;
            to = to + end * unit;

            this.From = from;
            this.To = to;
            return true;
        }

        //ToDo: BoundingBox related functions
        //ToDo: TryGetPlane
    }

}