using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelChan {
    public class EvaluateTestException :ApplicationException{
        public EvaluateTestException():base() {
        }
        public EvaluateTestException(string message) : base(message) {
        }
        public EvaluateTestException(string message, Exception innerException) : base(message, innerException) {
        }
    }
}
