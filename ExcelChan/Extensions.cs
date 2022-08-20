using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelChan {
    public static class Extensions {
        public static bool IsNull(this object value) {
            return value == null || value == DBNull.Value || (value is string && string.IsNullOrEmpty((string?)value));
        }

        public static Comparer ToOperator(this string value) {
            try {
                return Enum.GetValues<Comparer>().Where(x => value.Equals(x.GetEnumText())).First();
            } catch (Exception) {
                throw new InvalidCastException($"Invalid Operator {value}");
            }
        }
        public static int? ToIntN(this object value) {
            if (value.IsNull()) {
                return null;
            }
            return int.TryParse(value.ToString(), out int ret) ? ret : (int?)null;
        }

        internal static int ToInt32(this object value) {
            var result = value.ToIntN();
            if (!result.HasValue) {
                throw new InvalidCastException($"convert failed `{value}` to int");
            }
            return result.Value;
        }

        internal static DateTime? ToDateTimeN(this object value) {
            if (value.IsNull()) {
                return null;
            }
            if (value is DateTime) {
                return (DateTime)value;
            }
            if (value is DateTime?) {
                return (DateTime?)value;
            }
            return null;
        }
        internal static DateTime ToDateTime(this object value) {
            var result = value.ToDateTimeN();
            if (!result.HasValue) {
                throw new InvalidCastException($"convert failed `{value}` to DateTime");
            }
            return result.Value;
        }

        internal static decimal? ToDecimalN(this object value) {
            if (value.IsNull()) {
                return null;
            }
            return decimal.TryParse(value.ToString(), out decimal ret) ? ret : (decimal?)null;
        }

        internal static decimal ToDecimal(this object value) {
            var result = value.ToDecimalN();
            if (!result.HasValue) {
                throw new InvalidCastException($"convert failed `{value}` to decimal");
            }
            return result.Value;
        }
        internal static float? ToFloatN(this object value) {
            if (value.IsNull()) {
                return null;
            }
            return float.TryParse(value.ToString(), out float ret) ? ret : (float?)null;
        }

        internal static float ToFloat(this object value) {
            var result = value.ToFloatN();
            if (!result.HasValue) {
                throw new InvalidCastException($"convert failed `{value}` to float");
            }
            return result.Value;
        }

        internal static double? ToDoubleN(this object value) {
            if (value.IsNull()) {
                return null;
            }
            return double.TryParse(value.ToString(), out double ret) ? ret : (double?)null;
        }

        internal static double ToDouble(this object value) {
            var result = value.ToDoubleN();
            if (!result.HasValue) {
                throw new InvalidCastException($"convert failed `{value}` to double");
            }
            return result.Value;
        }
        internal static long? ToLongN(this object value) {
            if (value.IsNull()) {
                return null;
            }
            return long.TryParse(value.ToString(), out long ret) ? ret : (long?)null;
        }

        internal static long ToLong(this object value) {
            var result = value.ToLongN();
            if (!result.HasValue) {
                throw new InvalidCastException($"convert failed `{value}` to long");
            }
            return result.Value;
        }

        public static bool IsInt32(this object value) {
            return value.ToIntN().HasValue;
        }

        public static int? Compare(object a, object b) {
            var t = b.GetType();
            if (t == typeof(string)) {
                return Compare(a, (string)b);
            } else if (t == typeof(int)) {
                return Compare(a, (int)b);
            } else if (t == typeof(int?)) {
                return Compare(a, (int?)b);
            } else if (t == typeof(long)) {
                return Compare(a, (long)b);
            } else if (t == typeof(long?)) {
                return Compare(a, (long?)b);
            } else if (t == typeof(decimal)) {
                return Compare(a, (decimal)b);
            } else if (t == typeof(decimal?)) {
                return Compare(a, (decimal?)b);
            } else if (t == typeof(float)) {
                return Compare(a, (float)b);
            } else if (t == typeof(float?)) {
                return Compare(a, (float?)b);
            } else if (t == typeof(double)) {
                return Compare(a, (double)b);
            } else if (t == typeof(double?)) {
                return Compare(a, (double?)b);
            } else if (t == typeof(DateTime)) {
                return Compare(a, (DateTime)b);
            } else if (t == typeof(DateTime?)) {
                return Compare(a, (DateTime?)b);
            } else {
                throw new NotImplementedException($"{t.Name} Comparer not implemented.");
            }
        }
        public static int? Compare(object a, string b) {
            if (a.IsNull() && string.IsNullOrEmpty(b)) {
                return 0;
            } else if (a.IsNull() || string.IsNullOrEmpty(b)) {
                return null;
            } else {
                return a.ToString().CompareTo(b);
            }
        }
        public static int? Compare(object a, int b) {
            if (a.IsNull()) {
                return null;
            } else {
                return a.ToInt32().CompareTo(b);
            }
        }
        public static int? Compare(object a, int? b) {
            if (a.IsNull() && !b.HasValue) {
                return 0;
            } else if (a.IsNull() || !b.HasValue) {
                return null;
            } else {
                return a.ToInt32().CompareTo(b.Value);
            }
        }
        public static int? Compare(object a, long b) {
            if (a.IsNull()) {
                return null;
            } else {
                return a.ToLong().CompareTo(b);
            }
        }
        public static int? Compare(object a, long? b) {
            if (a.IsNull() && !b.HasValue) {
                return 0;
            } else if (a.IsNull() || !b.HasValue) {
                return null;
            } else {
                return a.ToLong().CompareTo(b.Value);
            }
        }
        public static int? Compare(object a, float b) {
            if (a.IsNull()) {
                return null;
            } else {
                return a.ToFloat().CompareTo(b);
            }
        }
        public static int? Compare(object a, float? b) {
            if (a.IsNull() && !b.HasValue) {
                return 0;
            } else if (a.IsNull() || !b.HasValue) {
                return null;
            } else {
                return a.ToFloat().CompareTo(b.Value);
            }
        }
        public static int? Compare(object a, double b) {
            if (a.IsNull()) {
                return null;
            } else {
                return a.ToDouble().CompareTo(b);
            }
        }
        public static int? Compare(object a, double? b) {
            if (a.IsNull() && !b.HasValue) {
                return 0;
            } else if (a.IsNull() || !b.HasValue) {
                return null;
            } else {
                return a.ToDouble().CompareTo(b.Value);
            }
        }
        public static int? Compare(object a, decimal b) {
            if (a.IsNull()) {
                return null;
            } else {
                return a.ToDecimal().CompareTo(b);
            }
        }
        public static int? Compare(object a, decimal? b) {
            if (a.IsNull() && !b.HasValue) {
                return 0;
            } else if (a.IsNull() || !b.HasValue) {
                return null;
            } else {
                return a.ToDecimal().CompareTo(b.Value);
            }
        }
        public static int? Compare(object a, DateTime b) {
            if (a.IsNull()) {
                return null;
            } else {
                return a.ToDateTime().CompareTo(b);
            }
        }
        public static int? Compare(object a, DateTime? b) {
            if (a.IsNull() && !b.HasValue) {
                return 0;
            } else if (a.IsNull() || !b.HasValue) {
                return null;
            } else {
                return a.ToDateTime().CompareTo(b.Value);
            }
        }
    }
}
