using CliWrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace TrainingHangfireRepositoryTests.Helper
{
    public class DockerHandler
    {
        private static string _linux_mssql_images = "mcr.microsoft.com/mssql/server"; //本機的sql-linux-images

        private static string _linux_mssql_containerneme = "testsmssql";

        public static string _linux_mssql_port = "12345";
        public static bool CreateContainer()
        {
            if (!CheckMSSQLImage())
            {
                throw new InvalidOperationException("no mcr.microsoft.com/mssql/server images");
            }

            RemoveExistContainer();
            var cmd = new Cli("docker")
                .SetArguments($@"run --name {_linux_mssql_containerneme} 
                                -e SA_PASSWORD=!@#QWEasd 
                                -e ACCEPT_EULA=Y 
                                -p {_linux_mssql_port}:1433 
                                -d {_linux_mssql_images}")
                .Execute();
            var containerReadyLog = "The default language (LCID 0) has been set for engine and full-text services.";//"Service Broker manager has started.";

            var retrySecond = 120;
            for (int i = 0; i < retrySecond; i++)
            {
                cmd = new Cli("docker")
                    .SetArguments($" logs {_linux_mssql_containerneme}")
                    .Execute();
                var logs = cmd.StandardOutput;
                if (logs.Contains(containerReadyLog))
                {
                    return true;
                }

                Thread.Sleep(1000);
            }
            return false;
        }

        public static void RemoveExistContainer()
        {
            var cmd = new Cli("docker")
                .SetArguments($" stop {_linux_mssql_containerneme}")
                .EnableExitCodeValidation(false)//在非零退出馬上拋出異常(預設是true)
                .EnableStandardErrorValidation(false)//在非零退出馬上拋出異常(預設是true)
                .Execute();

            cmd = new Cli("docker")
                .SetArguments($" rm {_linux_mssql_containerneme}")
                .EnableExitCodeValidation(false)
                .EnableStandardErrorValidation(false)
                .Execute();
        }
        private static bool CheckMSSQLImage()
        {
            var cmd = new Cli("docker")
                        .SetArguments($" images {_linux_mssql_images}")
                        .EnableExitCodeValidation(false)//不加會錯
                        .Execute();
            using (var reader = new StringReader(cmd.StandardOutput))
            {
                string line;
                while ((line = reader.ReadLine())!= null)
                {
                    if (line.Contains(_linux_mssql_images))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
