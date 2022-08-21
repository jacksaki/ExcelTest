using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
namespace ExcelChan {
    public class Response {
        public static Response CreateSample() {
            return new Response() {
                Result = new Result() {
                    Code = "S001",
                    Message = "Pyaaa",
                    Status = "Success"
                },
                Data = new List<Data>() {
                    new Data() {
                        Cd=1,
                        Name="hoge",
                        Items=new DataItem[] {
                            new DataItem() {
                                Cd=1,
                                Name="Hoge",
                                Date = DateTime.UtcNow
                            },
                            new DataItem() {
                                Cd = 2,
                                Name="piyo",
                                Date = DateTime.UtcNow.AddDays(-1)
                            }
                        }
                    },
                    new Data() {
                        Cd = 2,
                        Name="pyontan",
                        Items = new DataItem[] {
                            new DataItem() {
                                Cd=3,
                                Name="korosuke",
                                Date = DateTime.UtcNow.AddDays(1)
                            },
                            new DataItem() {
                                Cd=4,
                                Name="barbapapa",
                                Date=DateTime.UtcNow
                            }
                        }
                    }
                }
            };
        }

        [JsonProperty("result")]
        public Result Result {
            get;
            set;
        }
        [JsonProperty("data")]
        public List<Data> Data {
            get;
            set;
        }
    }
}
