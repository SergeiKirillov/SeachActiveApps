using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace SeachActiveApp
{
    class clData1Hour
    {
        [BsonId]

        public Guid ID { get; set; }
        public DateTime dtApp { get; set; }
        public String strApp { get; set; }
        public int Raz1Minut { get; set; }
    }
}
