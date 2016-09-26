using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

{
    public class Matrix
    {
        //warning: Matrix can be huge. So copying can be costy.
        private double[,] _Body;

        public Matrix(int rowCount, int columnCount)
        {
            if (rowCount < 0 || columnCount < 0) { throw new ArgumentOutOfRangeException(); }
            _Body = new double[rowCount, columnCount];
        }

        public double this[int row, int column]
        {
            get
            {
                CheckIndexRange(row, column);
                return _Body[row, column];
            }
            set
            {
                CheckIndexRange(row, column);
                _Body[row, column] = value;
            }
        }

        private void CheckIndexRange(int row, int column)
        {
            if (row < 0 || this.RowCount <= row)
            {
                throw new IndexOutOfRangeException();
            }
            else if (column < 0 || this.ColumnCount <= column)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var i in _Body)
                {
                    if (!Rhino.RhinoMath.IsValidDouble(i)) { return false; }
                }
                return true;
            }
        }

        public bool IsSquare
        {
            get
            {
                return RowCount == ColumnCount;
            }
        }

        public int RowCount { get { return _Body.GetLength(0); } }
        public int ColumnCount { get { return _Body.GetLength(1); } }

        #region ToDo: Implement Orthogonal.
        public bool IsRowOrthogonal
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsColumnOrthogonal
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsRowOrthoNormal
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public bool IsColumnOrthoNormal
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        public Matrix Duplicate()
        {
            var result = new Matrix(this.RowCount, this.ColumnCount);
            result.SetByFunction((a, b, c) => this[a, b]);
            return result;
        }

        public void Zero()
        {
            SetByFunction((a, b, c) => 0);
        }

        public void SetDiagonal(double d)
        {
            SetByFunction((a, b, c) => a == b ? d : 0);
        }

        public bool Transpose()
        {
            var newBody = new double[ColumnCount, RowCount];
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    newBody[j, i] = this[i, j];
                }
            }
            this._Body = newBody;
            return true;
        }

        public bool SwapRows(int RowA, int RowB)
        {
            if (RowA >= RowCount || RowB >= RowCount)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (RowA == RowB)
            {
                return true;
            }
            for (int i = 0; i < ColumnCount; i++)
            {
                var tmp = this[RowA, i];
                this[RowA, i] = this[RowB, i];
                this[RowB, i] = tmp;
            }
            return true;
        }

        public bool SwapColumns(int ColumnA, int ColumnB)
        {
            if (ColumnA >= ColumnCount || ColumnB >= ColumnCount)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (ColumnA == ColumnB)
            {
                return true;
            }
            for (int i = 0; i < RowCount; i++)
            {
                var tmp = this[i, ColumnA];
                this[i, ColumnA] = this[i, ColumnB];
                this[i, ColumnB] = tmp;
            }
            return true;
        }

        public bool Invert(double zeroTolerance)
        {
            throw new NotImplementedException();
        }
        //ToDo:Implement RowReduce and BackSolve when you use.

        public override int GetHashCode()
        {
            int num = (-this.RowCount).GetHashCode() ^ this.ColumnCount.GetHashCode();

            for(int i = 0; i < this.RowCount; i++)
            {
                for(int j = 0; j < this.ColumnCount; j++)
                {
                    num = num.GetHashCode() ^ this[i, j].GetHashCode();
                }
            }
            return num;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.ColumnCount != b.RowCount)
            {
                throw new ArgumentException("a.ColumnCount is not equal b.RowCount");
            }
            return GenerateMatrix((r, c) =>
            {
                double result = 0;
                for (int i = 0; i < a.ColumnCount; i++)
                {
                    result += a[r, i] * b[i, c];
                }
                return result;
            }, a.RowCount, b.ColumnCount);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.RowCount != b.RowCount || a.ColumnCount != b.ColumnCount)
            {
                throw new ArgumentException("size of a and b does not match.");
            }
            return GenerateMatrix((r, c) => a[r, c] + b[r, c], a.RowCount, b.ColumnCount);
        }

        public void Scale(double s)
        {
            SetByFunction((a, b, c) => s * c);
        }


        /// <summary>
        /// Set value from row, column, original value.
        /// <para>This function is not in original Rhino Common.</para>
        /// </summary>
        /// <param name="f">function that returns new value.</param>
        public void SetByFunction(Func<int, int, double, double> f)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    this[i, j] = f(i, j, this[i, j]);
                }
            }
        }

        /// <summary>
        /// Generate function defined Matrix.
        /// <para>This function is not in original Rhino Common.</para>
        /// </summary>
        /// <param name="f">return: value of cell.
        /// arg1: row count.
        /// arg2: column count.</param>
        /// <param name="row">Row count.</param>
        /// <param name="column">Column count.</param>
        /// <returns></returns>
        public static Matrix GenerateMatrix(Func<int, int, double> f, int row, int column)
        {
            var result = new Matrix(row, column);
            result.SetByFunction((a, b, c) => f(a, b));
            return result;
        }
    }
}
