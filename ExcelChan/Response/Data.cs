using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace ExcelChan {
    public class Data {
        [JsonProperty("cd")]
        public int Cd {
            get;
            set;
        }
        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }
        [JsonProperty("items")]
        public DataItem[] Items {
            get;
            set;
        }
    }
}
