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

            tabelRute[(int)Armada.Denpasar] = new string[] { "P001", "P004", "P005", "P006", "P007", "P008", "P009" };
            tabelRute[(int)Armada.Karangasem] = new string[] { "P002", "P010", "P011", "P012", "P013", "P014" };
            tabelRute[(int)Armada.Gianyar] = new string[] { "P003", "P015", "P016", "P017", "P018", "P019" };
            tabelRute[(int)Armada.Tabanan] = new string[] { "P020", "P021", "P022", "P023", "P024" };

            return tabelRute[(int)armada];
        }
    }
}
