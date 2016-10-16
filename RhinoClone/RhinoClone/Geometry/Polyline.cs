using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino.Geometry
{
    public class Polyline:Collections.Point3dList
    {
        public bool IsValid
        {
            get
            {
                if (this.Count <= 1) { return false; }
                if (!this[0].IsValid) return false;
                for (int i = 1; i < this.Count; i++)
                {
                    if (!this[i].IsValid) return false;
                    if (this[i - 1] == this[i]) return false;
                }
                return true;
            }
        }

        public bool IsClosed
        {
            get
            {
                if (this.Count > 2) {
                    return this.First() == this.Last();
                }
                return false;
            }
        }

        public double Length
        {
            get
            {
                double result = 0;
                for(int i = 1; i < this.Count; i++)
                {
                    result += this[i - 1].DistanceTo(this[i]);
                }
                return result;
            }
        }

        public Polyline() { }

        public Polyline(int capacity) : base(capacity) { }

        public Polyline(IEnumerable<Point3d> collection) : base(collection) { }

        public bool IsClosedWithinTolerance(double tolerance)
        {
            if (this.Count <= 2) { return false; }
            if (this.Last().DistanceTo(this.First()) <= Math.Max(tolerance, 0)) { return true; }
            return false;
        }

    }
}
