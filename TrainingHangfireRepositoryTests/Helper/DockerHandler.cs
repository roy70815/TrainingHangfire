using CliWrap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrainingHangfireRepositoryTests.Helper
{
    public class DockerHandler
    {
        private static string _linux_mssql_images = "mcr.microsoft.com/mssql/server"; //本機的sql-linux-images

        private static string _linux_mssql_containerneme = "testsmssql";

        public static string _linux_mssql_port = "12345";
        public static async Task<bool> CreateContainer()
        {
            if (!await CheckMSSQLImage())
            {
                throw new InvalidOperationException("no mcr.microsoft.com/mssql/server images");
            }

            await RemoveExistContainer();
            await Cli.Wrap("docker")
                .WithArguments($@"run --name {_linux_mssql_containerneme} -e SA_PASSWORD=!@#QWEasd -e ACCEPT_EULA=Y -p {_linux_mssql_port}:1433 -d {_linux_mssql_images}")
                .ExecuteAsync();
            
            var containerReadyLog = "The default language (LCID 0) has been set for engine and full-text services.";//"Service Broker manager has started.";

            var retrySecond = 120;
            for (int i = 0; i < retrySecond; i++)
            {
                var logsStrBuider = new StringBuilder();
                await Cli.Wrap("docker")
                .WithArguments($" logs {_linux_mssql_containerneme}")
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(logsStrBuider))
                .ExecuteAsync();
                var logs = logsStrBuider.ToString();
                if (logs.Contains(containerReadyLog))
                {
                    return true;
                }

                Thread.Sleep(1000);
            }
            return false;
        }

        public static async Task RemoveExistContainer()
        {
            await Cli.Wrap("docker")
               .WithArguments($" stop {_linux_mssql_containerneme}")
               .WithValidation(CommandResultValidation.None)//忽略非0代碼
               .ExecuteAsync();
            
            await Cli.Wrap("docker")
               .WithArguments($" rm {_linux_mssql_containerneme}")
               .WithValidation(CommandResultValidation.None)//忽略非0代碼
               .ExecuteAsync();
        }
        private static async Task<bool> CheckMSSQLImage()
        {
            var imagesStrBuilder = new StringBuilder();
            await Cli.Wrap("docker")
               .WithArguments($" images {_linux_mssql_images}")
               .WithStandardOutputPipe(PipeTarget.ToStringBuilder(imagesStrBuilder))
               .ExecuteAsync();
            
            using (var reader = new StringReader(imagesStrBuilder.ToString()))
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
