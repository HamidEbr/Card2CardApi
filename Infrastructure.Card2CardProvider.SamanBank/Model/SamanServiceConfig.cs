using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Card2CardProvider.SamanBank.Model
{
    public class SamanServiceConfig
    {
        public decimal MaxTransferAmount { get; set; }
        public string ServiceDisableTimePeriods { get; set; }
        public bool Enabled { get; set; }
        public List<string> PanPrefixes { get; set; }

        public bool IsValid()
        {
            return PanPrefixes.Any()
                   && MaxTransferAmount > 0
                   && SupportedBinsValidation();
        }

        private bool SupportedBinsValidation()
        {
            foreach (var bin in PanPrefixes)
                if (string.IsNullOrEmpty(bin))
                    return false;
            return true;
        }
    }
}
