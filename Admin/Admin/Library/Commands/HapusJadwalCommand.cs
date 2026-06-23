using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class HapusJadwalCommand : ICommand
    {
        private int _idPengiriman;

        public HapusJadwalCommand(int idPengiriman)
        {
            _idPengiriman = idPengiriman;
        }

        public string LogMessage => $"Menghapus jadwal pengiriman ID: {_idPengiriman}";

        public bool Execute()
        {
            return AdminDAO.HapusJadwal(_idPengiriman);
        }
    }
}
