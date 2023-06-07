using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeClockDataSync.Dto
{
    public class BaseModel
    {
        /// <summary>
        /// 7Shifts Access Token
        /// </summary>
      public string AccessToken { get; set; }
        /// <summary>
        /// 7Shifts CompanyId
        /// </summary>
      public string CompanyId { get; set; }
       
    }
}
