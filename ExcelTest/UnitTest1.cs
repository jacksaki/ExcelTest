using Xunit;
using ExcelChan;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace ExcelTest {
    public class UnitTest1 {
        [Fact]
        public void Test1() {
            var excelPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "ExcelTest.xlsx");
            try {
                var test = new ExcelChan.ExcelTest(excelPath);
                var res = Response.CreateSample();
                test.Load();
                test.EvaluateAll(res);
                Assert.True(true);
            } catch (Exception ex) {
                throw new Xunit.Sdk.XunitException(ex.Message);
            }
        }
    }
}