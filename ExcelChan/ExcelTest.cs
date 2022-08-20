using System;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using System.Diagnostics;
namespace ExcelChan {
    public class ExcelTest {
        public ExcelTest(string excelPath) {
            this.ExcelPath = excelPath;
        }
        public string ExcelPath {
            get;
        }
        public void EvaluateAll(object response) {
            foreach(var test in this.TestPairs) {
                test.Evaluate(response);
            }
        }
        public int TestCount {
            get {
                return this.TestPairs.Count();
            }
        }
        public void Load() {
            var book = new XLWorkbook(this.ExcelPath);
            var sheet = book.Worksheet("Test");
            var used = sheet.RangeUsed();
            var expected = used.Range(
                used.FirstCellUsed().Address.RowNumber,
                used.FirstCellUsed().Address.ColumnNumber,
                used.LastCellUsed().Address.RowNumber,
                used.FirstCellUsed().Address.ColumnNumber);
            var oper = used.Range(
                used.FirstCellUsed().Address.RowNumber,
                used.FirstCellUsed().Address.ColumnNumber + 1,
                used.LastCellUsed().Address.RowNumber,
                used.FirstCellUsed().Address.ColumnNumber + 1);
            var actual = used.Range(
                used.FirstCellUsed().Address.RowNumber,
                used.FirstCellUsed().Address.ColumnNumber + 2,
                used.LastCellUsed().Address.RowNumber,
                used.LastCellUsed().Address.ColumnNumber);
            var maxActualColCount = actual.ColumnCount();
            var testCount = expected.RowCount();
            var list = new List<TestPair>();
            for(var i = 1; i <= testCount; i++) {
                list.Add(
                    TestPair.Create(
                        oper.Cell(i, 1).GetString().ToOperator(),
                        expected.Cell(i, 1).Value,
                        actual.Range(i, 1, i, actual.ColumnCount()).Cells(true).Select(x => x.GetString()).ToArray())
                    );
            }
            this.TestPairs = list.ToArray();
        }
        public TestPair[] TestPairs {
            get;
            private set;
        }        
    }
}