using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeClockDataSync.Dto
{
    public class JsonDataModel : BaseModel
    {
        public string Data { get; set; }
        /// <summary>
        /// Insert, Update, Delete
        /// </summary>
        public int ProcessId { get; set; }
        public string OperationName { get; set; }
        /// <summary>
        /// Merchant ID
        /// </summary>
        public int MerchantId { get; set; }

    }
}
