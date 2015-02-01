
using Weather.Common.Enums;

namespace Weather.Parser
{
    public class ParseInfo
    {
        public Day DayKey { get; set; }

        public int Day { get; set; }

        public TimeOfDay TimeOfDayKey { get; set; }

        public int TimeOfDay { get; set; }

        public string Url { get; set; }
    }
}
