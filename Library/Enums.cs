using System;

namespace HYMAPSOPIR
{
    public enum StatusPengiriman
    {
        BelumTerkirim = 0,
        SudahTerkirim = 1,
        Gagal = 2
    }

    public enum StatusPembayaran
    {
        Bon, Cash, Transfer
    }

    public enum PrioritasPengiriman
    {
        Normal, Terlambat, Darurat
    }

    public enum Wilayah
    {
        Denpasar,
        Karangasem,
        Gianyar,
        Tabanan
    }
}