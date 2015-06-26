using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DefectTracking
{
    public static class Helper
    {
        public static string RemoveWildCard(string input)
        {
            string pattern = @"ctl00.*ctl00";
            
            string techidtype = string.Empty;
            Regex regex= new Regex(pattern, RegexOptions.IgnoreCase);
            if (input.Contains('*'))
            {
                return input;
            }
            return regex.Replace(input, "*");          
        }
    }
}
