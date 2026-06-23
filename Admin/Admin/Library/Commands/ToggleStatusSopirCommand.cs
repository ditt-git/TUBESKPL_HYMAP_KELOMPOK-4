using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class ToggleStatusSopirCommand : ICommand
    {
        private int _idUser;
        private bool _isActive;

        public ToggleStatusSopirCommand(int idUser, bool isActive)
        {
            _idUser = idUser;
            _isActive = isActive;
        }

        public string LogMessage => "Mengubah status sopir ID: " + _idUser + " menjadi " + (_isActive ? "Aktif" : "Nonaktif");

        public bool Execute()
        {
            return AdminDAO.ToggleStatusSopir(_idUser, _isActive);
        }
    }
}
