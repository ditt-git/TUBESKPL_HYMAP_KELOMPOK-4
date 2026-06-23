using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class ToggleStatusPelangganCommand : ICommand
    {
        private int _idPelanggan;
        private bool _isActive;

        public ToggleStatusPelangganCommand(int idPelanggan, bool isActive)
        {
            _idPelanggan = idPelanggan;
            _isActive = isActive;
        }

        public string LogMessage => "Mengubah status pelanggan ID: " + _idPelanggan + " menjadi " + (_isActive ? "Aktif" : "Nonaktif");

        public bool Execute()
        {
            return AdminDAO.ToggleStatusPelanggan(_idPelanggan, _isActive);
        }
    }
}
