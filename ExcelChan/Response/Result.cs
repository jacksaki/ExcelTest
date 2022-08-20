using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ExcelChan {
    public class Result {
        [JsonProperty("code")]
        public string Code {
            get;
            set;
        }
        [JsonProperty("message")]
        public string Message {
            get;
            set;
        }
        [JsonProperty("status")]
        public string Status {
            get;
            set;
        }
    }
}
