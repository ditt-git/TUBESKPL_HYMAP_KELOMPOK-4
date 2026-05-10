using System;
using System.Collections.Generic;
using System.Text;

namespace HYMAPSOPIR
{
    public static class DataHelper
    {
       
        // IMPLEMENTASI GENERIC METHOD + GENERIC CONSTRAINT
        // fungsi method ini mencari data apa pun (Sopir/Pelanggan) berdasarkan ID-nya
        public static T CariBerdasarkanId<T, TId>(List<T> sumberData, TId idYangDicari) where T : EntitasDasar<TId>
        {
            if (sumberData == null) return null;

            foreach (var item in sumberData)
            {
                // Mencocokkan ID secara dinamis
                if (item.Id != null && item.Id.Equals(idYangDicari))
                {
                    return item;
                }
            }

            return null; // Tidak ketemu
        }
    }
}
