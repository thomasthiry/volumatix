namespace Evolve11.Volumatix
{
    public class PresetManager
    {
        public static void Apply(DevicePreset devicePreset)
        {
            using var client = new HttpClient();

            client.GetAsync($"http://{devicePreset.Host}/Volume?level={devicePreset.Volume}&tell_slaves=0").GetAwaiter().GetResult();
        }

        public static void Apply(Preset preset)
        {
            foreach (var devicePreset in preset.DevicePresets)
            {
                Apply(devicePreset);
            }
        }
    }

    public partial class MainPage : ContentPage
    {
        private static DevicePreset DevicePreset1 = new ("192.168.129.1:11000", 20); // Salon
        private static DevicePreset DevicePreset2 = new ("192.168.129.1:11000", 15);
        private static DevicePreset DevicePreset3 = new ("192.168.129.4:11000", 15); // Cuisine gauche

        private Preset preset1 = new(new List<DevicePreset> { DevicePreset1 });

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSetVolume1Clicked(object sender, EventArgs e)
        {
            PresetManager.Apply(DevicePreset1);
        }

        private void OnSetVolume2Clicked(object? sender, EventArgs e)
        {
            PresetManager.Apply(preset1);
        }
    }

    public class Preset
    {
        public List<DevicePreset> DevicePresets { get; }

        public Preset(List<DevicePreset> devicePresets)
        {
            DevicePresets = devicePresets;
        }
    }

    public class DevicePreset
    {
        public DevicePreset(string host, int volume)
        {
            Host = host;
            Volume = volume;
        }

        public string Host { get; }
        public int Volume { get; }
    }
}
