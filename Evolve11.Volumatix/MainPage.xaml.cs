using System.Reactive.Linq;
using System.Xml.Linq;
using Blu4Net;

namespace Evolve11.Volumatix
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSetVolume1Clicked(object sender, EventArgs e)
        {
            var url = "http://192.168.129.1:11000/Volume?level=20&tell_slaves=1";
            using var client = new HttpClient();

            client.GetAsync(url).GetAwaiter().GetResult();
        }
    }

}
