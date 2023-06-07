using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TimeClockDataSync.Tools;

namespace TimeClockDataSync
{
    public partial class TimeClockDataSyncService : ServiceBase
    {
        public TimeClockDataSyncService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Jobs.InitializeJobs(IoTools.GetConnectionString(), "Konuşacağız");
        }

        public void RunAsConsole(string[] args)
        {
            OnStart(args);
            Console.WriteLine("Debug Mode");
            Console.ReadLine();
            Jobs.InitializeJobs(IoTools.GetConnectionString(), "Konuşacağız");
            OnStop();
        }

        protected override void OnStop()
        {
        }

       
    }
}
