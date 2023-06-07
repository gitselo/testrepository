﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TimeClockDataSync.Tools;

namespace TimeClockDataSync
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
           
            try
            {
                if (args.Length > 0)
                {
     
                    switch (args[0])

                    {

                        case "-install":

                            InstallService();

                            StartService();

                            break;

                        case "-uninstall":

                            StopService();

                            UninstallService();

                            break;

                        default:

                            throw new NotImplementedException();

                    }
                }
                else
                {
                    IoTools.GetConnectionString();
                    TimeClockDataSyncService service = new TimeClockDataSyncService();
                    if (Environment.UserInteractive)
                    {
                        service.RunAsConsole(args);
                    }
                    else
                    {
                        ServiceBase[] ServicesToRun;
                        ServicesToRun = new ServiceBase[] { service };
                        ServiceBase.Run(ServicesToRun);
                    }
                }
            }
            catch (Exception ex)
            {

               
            }


        }

       


        private static void InstallService()
        {
            if (IsInstalled()) return;
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {
                        installer.Install(state);
                        installer.Commit(state);
                    }
                    catch
                    {
                        try
                        {
                            installer.Rollback(state);
                        }
                        catch { }
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }

        }

        private static bool IsInstalled()
        {
            using (ServiceController controller =
                new ServiceController("TimeClockDataSyncService"))
            {
                try
                {
                    ServiceControllerStatus status = controller.Status;
                }
                catch
                {
                    return false;
                }
                return true;
            }

        }


        private static bool IsRunning()

        {
            using (ServiceController controller =
                new ServiceController("TimeClockDataSyncService"))
            {
                if (!IsInstalled()) return false;
                return (controller.Status == ServiceControllerStatus.Running);
            }

        }


        private static AssemblyInstaller GetInstaller()
        {
            AssemblyInstaller installer = new AssemblyInstaller(
                typeof(TimeClockDataSyncService).Assembly, null);
            installer.UseNewContext = true;
            return installer;

        }


        private static void UninstallService()
        {
            if (!IsInstalled()) return;
            try
            {
                using (AssemblyInstaller installer = GetInstaller())
                {
                    IDictionary state = new Hashtable();
                    try
                    {   installer.Uninstall(state);

                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }

        }


        private static void StartService()
        {
            if (!IsInstalled()) return;
            using (ServiceController controller =
                new ServiceController("TimeClockDataSyncService"))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Running)
                    {
                        controller.Start();
                        controller.WaitForStatus(ServiceControllerStatus.Running,
                            TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }

            }

        }


        private static void StopService()
        {
            if (!IsInstalled()) return;
            using (ServiceController controller =
                new ServiceController("TimeClockDataSyncService"))
            {
                try
                {
                    if (controller.Status != ServiceControllerStatus.Stopped)
                    {
                        controller.Stop();
                        controller.WaitForStatus(ServiceControllerStatus.Stopped,
                             TimeSpan.FromSeconds(10));
                    }
                }
                catch
                {
                    throw;
                }
            }

        }

    }
}
