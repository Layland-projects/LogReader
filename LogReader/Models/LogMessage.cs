using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LogReader.Models
{
    public class LogMessage
    {
        public DateTime DateTimeStamp { get; set; }
        public string Content { get; set; }

        public LogMessage(string logLine)
        {
            var regex = new Regex(@"^(\d{4}-\d{1,2}-\d{1,2} \d{2}:\d{2}:\d{2}\.\d{3}) (.*)$");

            var match = regex.Match(logLine);
            if (match.Success)
            {
                DateTimeStamp = Convert.ToDateTime(match.Groups[1].Value.ToString());
                Content = match.Groups[2].Value.ToString();
            }
        }
    }
}
