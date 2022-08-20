using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ExcelChan {
    public class DataItem {
        [JsonProperty("cd")]
        public int Cd {
            get;
            set;
        }
        [JsonProperty("date")]
        public DateTime Date {
            get;
            set;
        }
        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }
    }
}
