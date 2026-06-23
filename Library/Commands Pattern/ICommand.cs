using System;

namespace Library.Commands
{
    public interface ICommand
    {
        string LogMessage { get; }
        bool Execute();
    }
}
