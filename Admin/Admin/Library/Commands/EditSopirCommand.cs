using System;
using Admin.Library.Database;

namespace Library.Commands
{
    public class EditSopirCommand : ICommand
    {
        private int _idUser;
        private string _nama;
        private string _noTelp;
        private string _username;
        private int _idArmada;
        private string _password;

        public EditSopirCommand(int idUser, string nama, string noTelp, string username, int idArmada, string password)
        {
            _idUser = idUser;
            _nama = nama;
            _noTelp = noTelp;
            _username = username;
            _idArmada = idArmada;
            _password = password;
        }

        public string LogMessage => $"Mengedit data sopir: {_nama} (ID: {_idUser})";

        public bool Execute()
        {
            return AdminDAO.EditSopir(_idUser, _nama, _noTelp, _username, _idArmada, _password);
        }
    }
}
