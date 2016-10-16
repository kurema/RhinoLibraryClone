using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace Rhino.Collections
{
    public class Point3dList:IList<Geometry.Point3d>
    {
        private List<Geometry.Point3d> _Content;
        public double[] XArray
        {
            get
            {
                double[] result = new double[_Content.Count];
                for(int i = 0; i < _Content.Count; i++)
                {
                    result[i] = _Content[i].X;
                }
                return result;
            }
        }
        public double[] YArray
        {
            get
            {
                double[] result = new double[_Content.Count];
                for (int i = 0; i < _Content.Count; i++)
                {
                    result[i] = _Content[i].Y;
                }
                return result;
            }
        }
        public double[] ZArray
        {
            get
            {
                double[] result = new double[_Content.Count];
                for (int i = 0; i < _Content.Count; i++)
                {
                    result[i] = _Content[i].Z;
                }
                return result;
            }
        }

        public Point3dList()
        {
            _Content = new List<Geometry.Point3d>();
        }

        public Point3dList(int capacity)
        {
            _Content = new List<Point3d>(capacity);
        }

        public Point3dList(params Point3d[] points)
        {
            _Content = points.ToList();
        }

        public Point3dList(IEnumerable<Point3d> original)
        {
            _Content = original.ToList();
        }

        public void Add(double x,double y,double z)
        {
            this.Add(new Point3d(x, y, z));
        }

        public void SetAllX(double x)
        {
            for(int i = 0; i < _Content.Count; i++)
            {
                _Content[i] = new Point3d(x, _Content[i].Y, _Content[i].Z);
            }
        }
        public void SetAllY(double y)
        {
            for (int i = 0; i < _Content.Count; i++)
            {
                _Content[i] = new Point3d(_Content[i].X, y, _Content[i].Z);
            }
        }
        public void SetAllZ(double z)
        {
            for (int i = 0; i < _Content.Count; i++)
            {
                _Content[i] = new Point3d(_Content[i].X, _Content[i].Y, z);
            }
        }

        public static int ClosestIndexInList(IList<Point3d> a,Point3d b)
        {
            double minimum = double.MaxValue;
            int currentResult = 0;
            for(int i = 0; i < a.Count; i++)
            {
                double dist = a[i].DistanceTo(b);
                if (dist == 0) return i;
                if (dist < minimum)
                {
                    currentResult = i;
                    minimum = dist;
                }
            }
            return currentResult;
        }


        #region automatically generated
        public int Count
        {
            get
            {
                return ((IList<Point3d>)_Content).Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList<Point3d>)_Content).IsReadOnly;
            }
        }

        public Point3d this[int index]
        {
            get
            {
                return ((IList<Point3d>)_Content)[index];
            }

            set
            {
                ((IList<Point3d>)_Content)[index] = value;
            }
        }

        public int IndexOf(Point3d item)
        {
            return ((IList<Point3d>)_Content).IndexOf(item);
        }

        public void Insert(int index, Point3d item)
        {
            ((IList<Point3d>)_Content).Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ((IList<Point3d>)_Content).RemoveAt(index);
        }

        public void Add(Point3d item)
        {
            ((IList<Point3d>)_Content).Add(item);
        }

        public void Clear()
        {
            ((IList<Point3d>)_Content).Clear();
        }

        public bool Contains(Point3d item)
        {
            return ((IList<Point3d>)_Content).Contains(item);
        }

        public void CopyTo(Point3d[] array, int arrayIndex)
        {
            ((IList<Point3d>)_Content).CopyTo(array, arrayIndex);
        }

        public bool Remove(Point3d item)
        {
            return ((IList<Point3d>)_Content).Remove(item);
        }

        public IEnumerator<Point3d> GetEnumerator()
        {
            return ((IList<Point3d>)_Content).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<Point3d>)_Content).GetEnumerator();
        }
        #endregion
    }
}
