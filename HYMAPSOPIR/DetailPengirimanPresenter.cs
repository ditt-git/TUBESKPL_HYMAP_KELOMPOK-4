using System;
using Library.Commands;

namespace HYMAPSOPIR
{
    public class DetailPengirimanPresenter
    {
        private Pengiriman _tugasPengiriman;

        public DetailPengirimanPresenter(Pengiriman tugas)
        {
            _tugasPengiriman = tugas;
        }

        public void UpdateData(bool isBelumTerkirim, bool isSudahTerkirim, string metodeBayarInput, int galonKembali)
        {
            // 1. Parsing Status Pengiriman
            StatusPengiriman statusKirimBaru = (StatusPengiriman)2; // Gagal by default
            if (isBelumTerkirim) statusKirimBaru = (StatusPengiriman)0;
            else if (isSudahTerkirim) statusKirimBaru = (StatusPengiriman)1;

            // 2. Parsing Status Pembayaran
            StatusPembayaran statusBayarBaru = StatusPembayaran.Bon;
            if (metodeBayarInput == "Cash") statusBayarBaru = StatusPembayaran.Cash;
            else if (metodeBayarInput == "Transfer") statusBayarBaru = StatusPembayaran.Transfer;

            // 3. Eksekusi Command
            ICommand updateCommand = new UpdatePengirimanCommand(
                _tugasPengiriman,
                statusKirimBaru,
                statusBayarBaru,
                galonKembali
            );

            updateCommand.Execute();
        }
    }
}
