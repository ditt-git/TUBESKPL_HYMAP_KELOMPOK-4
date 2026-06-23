using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class TambahPelangganCommand : ICommand
    {
        private string _nama;
        private string _alamat;
        private string _noTelp;
        private int _idWilayah;
        private int _galonDipinjam;

        public TambahPelangganCommand(string nama, string alamat, string noTelp, int idWilayah, int galonDipinjam)
        {
            _nama = nama;
            _alamat = alamat;
            _noTelp = noTelp;
            _idWilayah = idWilayah;
            _galonDipinjam = galonDipinjam;
        }

        public string LogMessage => $"Menambah pelanggan baru: {_nama}";

        public bool Execute()
        {
            return AdminDAO.TambahPelanggan(_nama, _alamat, _noTelp, _idWilayah, _galonDipinjam);
        }
    }
}
