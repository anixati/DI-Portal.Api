using System;
using System.Collections.Generic;
using Boards.Domain.Contacts;
using DI.Services.Handlers;
using Newtonsoft.Json;
using Xunit;

namespace Framework.Tests
{
    public class FormActionTests
    {
        [Fact]
        public void Ensure_LoadEntityfromDataDict()
        {
            var payload = @"{""IsRegional"":""1"",""IsAboriginal"":""1"",""IsDisabled"":"""",""ExecutiveSearch"":""0"",""Biography"":""fwefwefwefwefwefwe"",""PostNominals"":""wef"",""ResumeLink"":""wef"",""LinkedInProfile"":"""",""0c8934df39304707886f55eea978c3ee"":"""",""StreetAddress.Unit"":""wqr"",""StreetAddress.Street"":""qwqwrqwqwr"",""StreetAddress.City"":""qwrqwrqw"",""StreetAddress.Postcode"":4123,""StreetAddress.State"":""asfsf"",""StreetAddress.Country"":""sdfsdgsdg"",""707f4cdbc59740fca4ed773b779a5f07"":"""",""PostalAddress.Unit"":"""",""PostalAddress.Street"":"""",""PostalAddress.City"":"""",""PostalAddress.Postcode"":"""",""PostalAddress.State"":"""",""PostalAddress.Country"":"""",""Email1"":""aa@hh.com"",""Email2"":"""",""Mobile"":"""",""HomePhone"":"""",""FaxNumber"":"""",""Title"":"""",""FirstName"":""ERWERWER"",""MiddleName"":""WERWERWQE"",""LastName"":""RWERWER"",""Gender"":""1""}";

            //covert to dict 

            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(payload);
            Assert.NotNull(data);

            var entity = data.CreateEntity<Appointee>();
            Assert.NotNull(entity);


        }
    }
}
