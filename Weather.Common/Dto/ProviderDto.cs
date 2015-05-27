
using System;

using Weather.Common.Enums;

namespace Weather.Common.Dto
{
    public class ProviderDto
    {
        public ProviderType Provider { get; set; }

        public DateTime DateTime { get; set; }
    }
}
