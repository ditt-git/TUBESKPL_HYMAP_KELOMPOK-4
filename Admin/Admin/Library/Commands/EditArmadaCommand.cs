using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class EditArmadaCommand : ICommand
    {
        private int _idArmada;
        private string _namaWilayah;
        private decimal _hargaPengiriman;

        public EditArmadaCommand(int idArmada, string namaWilayah, decimal hargaPengiriman)
        {
            _idArmada = idArmada;
            _namaWilayah = namaWilayah;
            _hargaPengiriman = hargaPengiriman;
        }

        public string LogMessage => $"Mengedit data armada wilayah ID: {_idArmada} menjadi {_namaWilayah}";

        public bool Execute()
        {
            return AdminDAO.EditArmada(_idArmada, _namaWilayah, _hargaPengiriman);
        }
    }
}
