using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeClockDataSync.Tools
{
    public static class IoTools
    {
        public static string GetConnectionString()
        {

            if (!string.IsNullOrWhiteSpace(AppStatics.SambaDbConnectionString))
            {
                return AppStatics.SambaDbConnectionString;
            }

            string ConnectionString = "";
            string TagStart = "<connectionstring>";
            string TagEnd = "</connectionstring>";
            try
            {
                if (File.Exists(AppStatics.SambaSettingsFilePath))
                {
                    using (StreamReader Sr = new StreamReader(AppStatics.SambaSettingsFilePath))
                    {
                       
                        string FileContent = Sr.ReadToEnd();
                        int CsStartIndex = FileContent.ToLower().IndexOf(TagStart);
                        int CsEndIndex = FileContent.ToLower().IndexOf(TagEnd);
                            ConnectionString = FileContent.ToLower().Substring((CsStartIndex + TagStart.Length), ((CsEndIndex-TagEnd.Length)-CsStartIndex));
                        var DbAuthParameters = ConnectionString.Split(';').ToList();
                        string NewConnectionString = "";
                        bool HasDatabaseFiled = false;
                        foreach(var str in DbAuthParameters)
                        {
                            if(str.Length > 0)
                            {
                                var prop = str.Split('=').ToArray();
                                if (prop[0].Trim() == "database")
                                {
                                    HasDatabaseFiled |= true;
                                    if (string.IsNullOrWhiteSpace(prop[1].Trim()))
                                    {
                                        NewConnectionString += string.Format("{0}={1};", prop[0].Trim(), "SambaPOS5");
                                    }
                                    else
                                    {
                                        NewConnectionString += string.Format("{0}={1};", prop[0].Trim(), prop[1].Trim());
                                    }
                                }
                                else
                                {
                                    NewConnectionString += string.Format("{0}={1};", prop[0].Trim(), prop[1].Trim());
                                }
                            }
                        }

                        if (!HasDatabaseFiled)
                        {
                            NewConnectionString += string.Format("{0}={1};", "database", "SambaPOS5");
                        }

                        return NewConnectionString;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Sambapos settings file is not exist.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.ToString());
            }
            return ConnectionString;
        }

      
    }
}
