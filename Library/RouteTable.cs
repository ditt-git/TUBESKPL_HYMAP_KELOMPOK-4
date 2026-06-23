using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HYMAPSOPIR
{

    public static class RouteTable
    {
        public static string[] GetRute(Wilayah wilayah, List<Pelanggan> pelangganDb)
        {

            string[][] tabelRute = new string[4][];

            tabelRute[(int)Wilayah.Denpasar] = pelangganDb.Where(p => p.Wilayah == Wilayah.Denpasar).Select(p => p.IdPelanggan).ToArray();
            tabelRute[(int)Wilayah.Karangasem] = pelangganDb.Where(p => p.Wilayah == Wilayah.Karangasem).Select(p => p.IdPelanggan).ToArray();
            tabelRute[(int)Wilayah.Gianyar] = pelangganDb.Where(p => p.Wilayah == Wilayah.Gianyar).Select(p => p.IdPelanggan).ToArray();
            tabelRute[(int)Wilayah.Tabanan] = pelangganDb.Where(p => p.Wilayah == Wilayah.Tabanan).Select(p => p.IdPelanggan).ToArray();

            return tabelRute[(int)wilayah] ?? new string[0];
        }
    }
}
