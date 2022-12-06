using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obius.Service.Converters
{
    public static class TimeStampConverter
    {
        public static DateTime? FromTimeStampToDateTime(int timestamp )
        {
            if (timestamp <= 0)
                return null;
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp).ToLocalTime();
            return dt;
        }
    }
}
