using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityRegSystem.Shared.Utility
{
    public class StaticFunctions
    {
        public static string RemoveTT(string time)
        {
            if (time.Contains("AM"))
            {
                return time.Replace("AM", "");
            }

            return time.Replace("PM", "");
        }
    }
}
