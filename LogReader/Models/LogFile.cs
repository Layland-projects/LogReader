using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LogReader.Models
{
    public class LogFile
    {
        public string FilePath { get; }
        public IEnumerable<LogMessage> Messages { get; }

        public LogFile(string filePath)
        {
            FilePath = filePath;
            var lines = File.ReadAllLines(filePath);
            var messages = new List<LogMessage>();

            foreach (var line in lines)
            {
                messages.Add(new LogMessage(line));
            }
            Messages = messages;
        }
    }
}
