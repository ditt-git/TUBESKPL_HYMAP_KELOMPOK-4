using System;
using System.Collections.Generic;
using System.Text;

namespace HYMAPSOPIR
{
    public enum StatusPengiriman { 
        BelumTerkirim, SudahTerkirim, Gagal 
    }
    public enum StatusPembayaran { 
        BelumBayar, Cash, Transfer, Bon 
    }
    public enum PrioritasPengiriman {
        Normal, Terlambat, Darurat 
    }

    public enum Armada { 
        Denpasar, Karangasem, Gianyar, Tabanan 
    }
}
