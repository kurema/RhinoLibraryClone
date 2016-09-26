using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhino
{
    public class RhinoMath
    {
        public const double ZeroTolerance = 1E-12;
        public const double DefaultAngleTolerance = Math.PI / 180.0;

        public static double ToRadians(double degrees)
        {
            return Math.PI*degrees/ 180.0;
        }
        public static double ToDegrees(double radians)
        {
            return radians / Math.PI * 180.0;
        }
        public static bool IsValidDouble(double arg)
        {
            return !double.IsInfinity(arg) && !double.IsNaN(arg);
        }
        public static bool IsValidSingle(float arg)
        {
            return !float.IsInfinity(arg) && !float.IsNaN(arg);
        }

        public static bool EpsilonEquals(double x, double y, double epsilon)
        {
            if(double.IsNaN(x) || double.IsNaN(y)) { return false; }
            if (double.IsPositiveInfinity(x)) { return double.IsPositiveInfinity(y); }
            if (double.IsNegativeInfinity(x)) { return double.IsNegativeInfinity(y); }
            return y - epsilon <= x && x <= y + epsilon;
        }

        public static bool EpsilonEquals(float x, float y, float epsilon)
        {
            if (float.IsNaN(x) || float.IsNaN(y)) { return false; }
            if (float.IsPositiveInfinity(x)) { return float.IsPositiveInfinity(y); }
            if (float.IsNegativeInfinity(x)) { return float.IsNegativeInfinity(y); }
            return y - epsilon <= x && x <= y + epsilon;
        }
    }
}
