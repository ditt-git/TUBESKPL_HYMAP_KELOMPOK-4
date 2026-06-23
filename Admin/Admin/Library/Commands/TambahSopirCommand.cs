using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class TambahSopirCommand : ICommand
    {
        private string _nama;
        private string _noTelp;
        private string _username;
        private string _password;
        private int _idArmada;

        public TambahSopirCommand(string nama, string noTelp, string username, string password, int idArmada)
        {
            _nama = nama;
            _noTelp = noTelp;
            _username = username;
            _password = password;
            _idArmada = idArmada;
        }

        public string LogMessage => $"Menambah sopir baru: {_nama}";

        public bool Execute()
        {
            return AdminDAO.TambahSopir(_nama, _noTelp, _username, _password, _idArmada);
        }
    }
}
