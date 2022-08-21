using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Newtonsoft.Json;
namespace ExcelChan {
    public enum Comparer {
        [EnumText("=")]
        Equals,
        [EnumText("<")]
        Less,
        [EnumText(">")]
        Greater,
        [EnumText("<=")]
        LessOrEquals,
        [EnumText(">=")]
        GreaterOrEquals,
        [EnumText("!=")]
        NotEqual,
    }
    public class TestPair {
        private TestPair() {
        }
        public Comparer Comparer {
            get;
            private set;
        }
        public string[] ActualExpression {
            get;
            private set;
        }
        public object Expected {
            get;
            private set;
        }
        public object Actual {
            get;
            private set;
        }
        public bool? TestResult {
            get;
            private set;
        }
        public static TestPair Create(Comparer comparer, object expected, string[] actual) {
            var test = new TestPair();
            test.Comparer = comparer;
            test.Expected = expected;
            test.ActualExpression = actual;
            return test;
        }
        public void EvaluateByJson(string json) {
            Evaluate(JsonConvert.DeserializeObject(json));
        }
        private Comparer GetComparer(int value) {
            if (value == 0) {
                return Comparer.Equals;
            }else if(value > 0) {
                return Comparer.Greater;
            }else {
                return Comparer.Less;
            }
        }
        private Comparer GetCompareValue(object expected, object actual) {
            var compareResult = Extensions.Compare(expected, actual);
            if (!compareResult.HasValue) {
                return Comparer.NotEqual;
            }else if (compareResult.Value > 0) {
                return Comparer.Greater;
            }else if (compareResult.Value < 0) {
                return Comparer.Less;
            } else {
                return Comparer.Equals;
            }
        }
        
        public void Evaluate(object response) {
            try {
                this.Actual = this.GetActualValue(response);
                var comparer = GetCompareValue(this.Expected, this.Actual);
                switch (this.Comparer) {
                    case Comparer.Equals:
                        this.TestResult = comparer == Comparer.Equals;
                        break;
                    case Comparer.Less:
                        this.TestResult = comparer == Comparer.Less;
                        break;
                    case Comparer.LessOrEquals:
                        this.TestResult = (comparer == Comparer.Less || comparer == Comparer.Equals);
                        break;
                    case Comparer.Greater:
                        this.TestResult = comparer == Comparer.Greater;
                        break;
                    case Comparer.GreaterOrEquals:
                        this.TestResult = comparer == Comparer.Greater || comparer == Comparer.Equals;
                        break;
                    case Comparer.NotEqual:
                        this.TestResult = comparer != Comparer.Equals;
                        break;
                    default:
                        throw new NotImplementedException($"{this.Comparer} not implemented.");
                }
            } catch (Exception ex) {
                throw new EvaluateTestException(ex.Message);
            }
        }

        private PropertyInfo GetPropery(object value, string jsonProperty) {
            var p = value.GetType().GetProperties().
                Where(x => jsonProperty.Equals(x.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName)).
                FirstOrDefault();
            if (p == null) {
                throw new ArgumentException($"`{jsonProperty}` field not found in response");
            }
            return p;
        }

        private object GetActualValue(object response) {
            var value = response;
            int? index = null;
            string name = "";
            for (var i = 0; i < this.ActualExpression.Length; i++) {
                if (i < this.ActualExpression.Length - 1 && this.ActualExpression[i + 1].IsInt32()) {
                    name = this.ActualExpression[i];
                    index = this.ActualExpression[i + 1].ToIntN().Value;
                    continue;
                }else if (index.HasValue) {
                    var p = GetPropery(value, name);
                    if (p.PropertyType.IsArray) {
                        value = ((object[])p.GetValue(value))[index.Value];
                    } else {
                        var indexer = p.PropertyType.GetProperty("Item");
                        if (indexer != null) {
                            value = indexer.GetValue(p.GetValue(value), new object[] { index.Value });
                        } else {
                            throw new NotImplementedException("only `Item` property is supported.");
                        }     
                    }
                    index = null;
                } else {
                    name = this.ActualExpression[i];
                    var p = GetPropery(value, name);
                    value = p.GetValue(value, null);
                    index = null;
                }
            }
            return value;
        }
        private object GetActualValueByJson(string json) {
            return GetActualValue(JsonConvert.DeserializeObject(json));
        }
    }
}
