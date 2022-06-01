using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;
using DI.Domain.Enums;
using DI.Domain.Owned;

namespace DI.Domain.Contacts
{
    public abstract class ContactBaseEntity : AuditBaseEntity
    {
        [MaxLength(255)] public string Title { get; set; }

        [Required] [MaxLength(255)] public string FirstName { get; set; }

        [Required] [MaxLength(255)] public string LastName { get; set; }

        [MaxLength(255)] public string MiddleName { get; set; }

        public GenderEnum Gender { get; set; }

        [MaxLength(10)] public string HomePhone { get; set; }

        [MaxLength(10)] public string MobilePhone { get; set; }

        [MaxLength(10)] public string FaxNumber { get; set; }


        [MaxLength(50)] public string Email1 { get; set; }

        [MaxLength(50)] public string Email2 { get; set; }

        public AddressType StreetAddress { get; set; }
        public AddressType PostalAddress { get; set; }


        public string FullName
        {
            get
            {
                var rv = string.Empty;
                if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
                    rv = $"{FirstName} {LastName}";
                if (!string.IsNullOrEmpty(Title))
                    rv = $"{Title} {rv}";
                return rv;
            }
        }
        public override string GetName()
        {
            return FullName;
        }
        public override string GetKey()
        {
            return FullName;
        }
    }
}