namespace Evolve11.Volumatix
{
    public class PresetManager
    {
        public static void Apply(Preset preset)
        {
            using var client = new HttpClient();

            client.GetAsync($"http://{preset.Host}/Volume?level={preset.Volume}&tell_slaves=1").GetAwaiter().GetResult();
        }
    }

    public partial class MainPage : ContentPage
    {
        private Preset Preset1 = new ("192.168.129.1:11000", 20);
        private Preset Preset2 = new ("192.168.129.1:11000", 15);

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSetVolume1Clicked(object sender, EventArgs e)
        {
            PresetManager.Apply(Preset1);
        }

        private void OnSetVolume2Clicked(object? sender, EventArgs e)
        {
            PresetManager.Apply(Preset2);
        }
    }

    public class Preset
    {
        public Preset(string host, int volume)
        {
            Host = host;
            Volume = volume;
        }

        public string Host { get; }
        public int Volume { get; }
    }
}
