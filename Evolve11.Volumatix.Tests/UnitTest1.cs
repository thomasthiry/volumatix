using System.Xml.Linq;

namespace Evolve11.Volumatix.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Get()
        {
            var url = "http://192.168.129.1:11000/Status";
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var xmlString = await response.Content.ReadAsStringAsync();
                var xmlDoc = XDocument.Parse(xmlString);
                // Now xmlDoc contains the XML document. You can process it as needed.
            }
            else
            {
                // Handle error
            }
        }

        [Fact]
        public async Task Set()
        {
            var url = "http://192.168.129.1:11000/Volume?level=20&tell_slaves=1";
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var xmlString = await response.Content.ReadAsStringAsync();
                var xmlDoc = XDocument.Parse(xmlString);
                // Now xmlDoc contains the XML document. You can process it as needed.
            }
            else
            {
                // Handle error
            }
        }

        [Fact]
        public async Task Set_only_one()
        {
            var url = "http://192.168.129.4:11000/Volume?level=20";
            using var client = new HttpClient();

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var xmlString = await response.Content.ReadAsStringAsync();
                var xmlDoc = XDocument.Parse(xmlString);
                // Now xmlDoc contains the XML document. You can process it as needed.
            }
            else
            {
                // Handle error
            }
        }
    }
}