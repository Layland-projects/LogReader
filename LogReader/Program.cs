using LogReader.Models;
using LogReader.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogReader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentNullException("logDirectoryPath and outputLogDestinationPath are both required");
            }
            var logDirectoryPath = args[0];
            var outputLogDestinationPath = args[1];
            var logFiles = new List<string>();

            if (logDirectoryPath.Contains(","))
            {
                var files = logDirectoryPath.Split(',');
                foreach (var file in files)
                {
                    if (!File.Exists(file))
                    {
                        throw new FileNotFoundException("One or more file in logDirectoryPath does not exist");
                    }
                    logFiles.Add(file);
                }
            }
            else
            {
                logFiles = Directory.GetFiles(logDirectoryPath, "*.txt").Concat(Directory.GetFiles(logDirectoryPath, "*.log")).ToList();
            }

            if (logFiles.Count() < 1)
            {
                throw new FileNotFoundException("logDirectoryPath contains no files");
            }

            LogFileHelper.EnsureDirectoryExists(outputLogDestinationPath);
            var fileModels = new List<LogFile>();
            foreach(var file in logFiles)
            {
                fileModels.Add(new LogFile(file));
            }

            var logLines = LogFileHelper.GetAllLogMessages(fileModels).OrderBy(x => x.DateTimeStamp).ToList();
            foreach(var line in logLines)
            {
                File.AppendAllText(outputLogDestinationPath, $"{line.DateTimeStamp:yyyy-MM-dd HH:mm:ss.fff} {line.Content}\n");
            }
        }
    }
}
