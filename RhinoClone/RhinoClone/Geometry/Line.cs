using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    public struct Line : IEquatable<Line>
    {
        private Point3d _From;
        private Point3d _To;
        public Point3d From
        {
            get { return _From; }
            set { _From = value; }
        }
        public Point3d To
        {
            get { return _To; }
            set { _To = value; }
        }

        public double FromX
        {
            get { return _From.X; }
            set { _From.X = value; }
        }
        public double FromY
        {
            get { return _From.Y; }
            set { _From.Y = value; }
        }
        public double FromZ
        {
            get { return _From.Z; }
            set { _From.Z = value; }
        }
        public double ToX
        {
            get { return _To.X; }
            set { _To.X = value; }
        }
        public double ToY
        {
            get { return _To.Y; }
            set { _To.Y = value; }
        }
        public double ToZ
        {
            get { return _To.Z; }
            set { _To.Z = value; }
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

        public Vector3d Direction
        {
            get
            {
                return this.To - this.From;
            }
        }
        public Vector3d UnitTangent
        {
            get
            {
                return Direction.Unitized;
            }
        }
        public Line(Point3d from, Point3d to)
        {
            _From = from;
            _To = to;
        }
        public Line(Point3d from, Vector3d span)
        {
            _From = from;
            _To = from + span;
        }
        public Line(Point3d from, Vector3d direction, double length)
        {
            _From = from;
            _To = from + (direction.Unitized * length);
        }

        public Line(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            _From = new Point3d(x0, y0, z0);
            _To = new Point3d(x1, y1, z1);
        }

        //ToDo:TryFitLineToPoints
        //ToDo:TryCreateBetweenCurves

        public static bool operator ==(Line a, Line b)
        {
            return a.From == b.From && a.To == b.To;
        }

        public static bool operator !=(Line a, Line b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is Line && this == (Line)obj;
        }

        public bool Equals(Line other)
        {
            return other == this;
        }

        public bool EpsilonEquals(Line other, double epsilon)
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

        public Point3d PointAt(double t)
        {
            return (1 - t) * From + t * To;
        }

        public double ClosestParameter(Point3d point)
        {
            throw new NotImplementedException();
        }

        public Point3d ClosestPoint(Point3d point, bool limitToFiniteSegment)
        {
            double param = ClosestParameter(point);
            if (limitToFiniteSegment)
            {
                param = Math.Min(1.0, Math.Max(0.0, param));
            }
            return PointAt(param);
        }

        public double DistanceTo(Point3d point, bool limitToFiniteSegment)
        {
            return this.ClosestPoint(point, limitToFiniteSegment).DistanceTo(point);
        }

        public double MinimumDistanceTo(Point3d point)
        {
            return DistanceTo(point, true);
        }

        public double MinimumDistanceTo(Line line)
        {
            throw new NotImplementedException();
        }
        public double MaximumDistanceTo(Point3d point)
        {
            return Math.Max(this.From.DistanceTo(point), this.To.DistanceTo(point));
        }

        public double MaximumDistanceTo(Line testLine)
        {
            return Math.Max(Math.Max(this.From.DistanceTo(testLine.From), this.From.DistanceTo(testLine.To)), Math.Max(this.To.DistanceTo(testLine.From), this.To.DistanceTo(testLine.To)));
        }

//ToDo: Transform
//ToDo: ToNurbsCurve

        public bool Extend(double start,double end)
        {
            if(!this.IsValid || this.Length == 0)
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