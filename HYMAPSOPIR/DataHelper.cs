using System;
using System.Collections.Generic;
using System.Text;

namespace HYMAPSOPIR
{
    public static class DataHelper
    {
       
    
        public static T CariBerdasarkanId<T, TId>(List<T> sumberData, TId idYangDicari) where T : EntitasDasar<TId>
        {
            if (sumberData == null) 
                return default;

            foreach (var item in sumberData)
            {
                if (item.Id != null && item.Id.Equals(idYangDicari))
                {
                    return item;
                }
            }

            return default; 
        }
    }
}
