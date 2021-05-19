using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication1.Data
{
    public class DbObject
    {
        public string ID { get; set; }

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
