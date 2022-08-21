using Xunit;
using ExcelChan;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;

namespace ExcelTest {
    public class UnitTest1 {
        [Theory]
        [InlineData("ExcelTest")]
        public void Test1(string testName) {
            var excelPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"{testName}.xlsx");
            try {
                var test = new ExcelChan.ExcelTest(excelPath);
                var res = Response.CreateSample();
                test.Load();
                test.EvaluateAll(res);
                foreach (var t in test.TestPairs.Select((item,index)=>(item,index))) {e
                    Assert.True(t.item.TestResult ?? false, 
                        $"{testName}[{t.index}] `{t.item.Expected}` {t.item.Comparer.GetEnumText()} `{t.item.Actual}` ({string.Join(".",t.item.ActualExpression)})");
                }
            } catch (Exception ex) {
                throw new Xunit.Sdk.XunitException(ex.Message);
            }
        }
    }
}