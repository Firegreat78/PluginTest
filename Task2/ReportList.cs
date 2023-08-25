using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ReportList : ICommand
    {
        public string Command => "reportlist";
        public string[] Aliases => new string[] {"reports", "rlist"};
        public string Description => "Список всех отправленных репортов в течение данного раунда. Команда не принимает аргументов.\nИспользование: reportlist";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 0) 
            {
                response = "Команда не принимает аргументов.";
                return false;
            }
            else if (Plugin.ReportsList.Count == 0) 
            {
                response = "Во время данного раунда ни один игрок не отправлял репорт.";
                return true;
            }
            else
            {
                response = "Список отправленных репортов:\n";
                foreach (var ReportMsg in Plugin.ReportsList) response += (ReportMsg + "\n");
                return true;
            }
        }
    }
}
