
using System;

namespace Weather.Website.Models
{
    public class CityRequestViewModel
    {
        public int CityId { get; set; }

        public DateTime Date { get; set; }

        public int[] Providers { get; set; }
    }
}