using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeClockDataSync.Dto;

namespace TimeClockDataSync.DataTransfer
{
    public static  class FromDb
    {
        public static void SyncSales(string ConnectionString)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                List<JsonDataModel> models = new List<JsonDataModel>();

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("select * from TicketsTemp NOLOCK  ", conn);
                adp.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (var syncTable in dt.Rows               )
                    {
                        //Eksik Alan 1

                        //Add token companyId  belongs to the 7shifts
                        // And add json data to data field
                        models.Add(new JsonDataModel
                        {
                             AccessToken = "",
                              Data  = dt.Rows[i]["json"].ToString(),
                        });

                    }
                }
                foreach (var model in models) { 
                      
                    //her bir model Json a serialize edilip 
                    //apiye gönderilecek
                
                }
            }
        }

    }
}
