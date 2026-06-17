using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace HYMAPSOPIR
{

    public class EntitasDasar<TId>
    {
        public TId Id { get; protected set; }
    }

    public class Sopir : EntitasDasar<string>
    {
        public int IdUserDb { get; }
        public string Nama { get; }
        public string Username { get; }
        public string Password { get; }
        public Armada ArmadaTugas { get; }
        public List<Pengiriman> DaftarTugasHariIni;

        public Sopir(int idUserDb, string nama, string username, string password, Armada armada)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(nama), "FATAL: Nama sopir tidak boleh null/kosong!");
            Debug.Assert(!string.IsNullOrWhiteSpace(username), "FATAL: Username tidak boleh null/kosong!");
            Debug.Assert(!string.IsNullOrWhiteSpace(password), "FATAL: Password tidak boleh null/kosong!");

            if (string.IsNullOrWhiteSpace(nama)) throw new ArgumentException("Nama tidak boleh kosong.");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username tidak boleh kosong.");

            IdUserDb = idUserDb;
            Id = username;
            Nama = nama;
            Username = username;
            Password = password;
            ArmadaTugas = armada;
            DaftarTugasHariIni = new List<Pengiriman>();

        }

    }
}