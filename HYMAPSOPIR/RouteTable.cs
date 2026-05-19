using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HYMAPSOPIR
{

    public static class RouteTable
    {
        public static string[] GetRute(Armada armada)
        {

            string[][] tabelRute = new string[4][];

            tabelRute[(int)Armada.Denpasar] = new string[] { "P001", "P004", "P007" };
            tabelRute[(int)Armada.Karangasem] = new string[] { "P002", "P005" };
            tabelRute[(int)Armada.Gianyar] = new string[] { "P003", "P006" };
            tabelRute[(int)Armada.Tabanan] = new string[] { "P008", "P009" };

            return tabelRute[(int)armada];
        }
    }

}
