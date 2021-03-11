using LogReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogReader.Utils
{
    public static class LogFileHelper
    {
        public static IEnumerable<LogMessage> GetAllLogMessages(IEnumerable<LogFile> logFiles)
        {
            var messages = new List<LogMessage>();
            foreach(var file in logFiles)
            {
                messages.AddRange(GetAllLogMessages(file));
            }
            return messages;
        }

        public static IEnumerable<LogMessage> GetAllLogMessages(LogFile logFile)
        {
            return logFile.Messages;
        }

        public static void EnsureDirectoryExists(string directory)
        {
            var destDirs = directory.Split(new char[] { '\\', '/' });

            for (int i = 0; i < destDirs.Length; i++)
            {
                var path = "";
                if (i > 0)
                {
                    for (int x = 0; x < i; x++)
                    {
                        path += $"{destDirs[x]}/";
                    }
                }
                path += destDirs[i];
                if (!Directory.Exists(path) && i != destDirs.Length - 1)
                {
                    Directory.CreateDirectory(path);
                }
                else if (i == destDirs.Length - 1)
                {
                    var fs = File.Create(path);
                    fs.Close();
                }
            }
        }
    }
}
