using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class EditPelangganCommand : ICommand
    {
        private int _idPelanggan;
        private string _nama;
        private string _alamat;
        private string _noTelp;
        private int _idWilayah;
        private int _galonDipinjam;

        public EditPelangganCommand(int idPelanggan, string nama, string alamat, string noTelp, int idWilayah, int galonDipinjam)
        {
            _idPelanggan = idPelanggan;
            _nama = nama;
            _alamat = alamat;
            _noTelp = noTelp;
            _idWilayah = idWilayah;
            _galonDipinjam = galonDipinjam;
        }

        public string LogMessage => $"Mengedit data pelanggan: {_nama} (ID: {_idPelanggan})";

        public bool Execute()
        {
            return AdminDAO.EditPelanggan(_idPelanggan, _nama, _alamat, _noTelp, _idWilayah, _galonDipinjam);
        }
    }
}
