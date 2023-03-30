using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Owned
{
    [Owned]
    public class AddressType
    {
        public string Unit { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public short Postcode { get; set; }
        public string State { get; set; }

        [DefaultValue("Australia")] public string Country { get; set; }
    }
}