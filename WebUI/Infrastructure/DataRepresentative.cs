using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Infrastructure
{
    public static class DataRepresentative
    {
        public static string HandleLotDescriptionForSummary(string dscn)
            => CutStringForSummary(dscn, 100);

        public static string HandleLotTitleForSummary(string tle)
            => CutStringForSummary(tle, 30);
        
        private static string CutStringForSummary(string s, int defaultLength)
        {
            if (s.Length < defaultLength)
                return s;

            int i = defaultLength;
            while (s[i] != ' ' && i != 0)
            {
                i--;
            }

            return s.Substring(0, i) + "...";
        }
    }

}
