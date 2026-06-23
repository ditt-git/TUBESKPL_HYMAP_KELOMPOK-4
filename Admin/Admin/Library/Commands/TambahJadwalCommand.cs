using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class TambahJadwalCommand : ICommand
    {
        private DateTime _tanggal;
        private int _idPelanggan;
        private int _idUser;
        private int _jumlahPesanan;

        public TambahJadwalCommand(DateTime tanggal, int idPelanggan, int idUser, int jumlahPesanan)
        {
            _tanggal = tanggal;
            _idPelanggan = idPelanggan;
            _idUser = idUser;
            _jumlahPesanan = jumlahPesanan;
        }

        public string LogMessage => $"Menambah jadwal pengiriman baru untuk Pelanggan ID: {_idPelanggan}, Sopir ID: {_idUser}";

        public bool Execute()
        {
            return AdminDAO.TambahJadwal(_tanggal, _idPelanggan, _idUser, _jumlahPesanan);
        }
    }
}
