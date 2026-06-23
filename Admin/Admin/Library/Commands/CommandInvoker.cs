using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class CommandInvoker
    {
        public bool ExecuteCommand(ICommand command)
        {
            if (command != null)
            {
                bool success = command.Execute();
                if (success)
                {
                    AdminDAO.LogAktivitas(command.LogMessage);
                }
                return success;
            }
            return false;
        }
    }
}
