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
        Speaker _speaker1 = new("Salon", "192.168.129.1", 11000);
        Speaker _speaker2 = new("Salle à manger", "192.168.129.4", 11000);
        private readonly Preset _preset1;
        private readonly Preset _preset2;

        public MainPage()
        {
            InitializeComponent();

            _preset1 = new("Low volume", new List<DevicePreset>
            {
                new (_speaker1, 5),
                new (_speaker2, 5)

            });
            _preset2 = new("Normal volume",new List<DevicePreset>
            {
                new (_speaker1, 20),
                new (_speaker2, 10)
            });
            var presets = new List<Preset> { _preset1, _preset2 };

            foreach (var preset in presets)
            {
                var button = new Button
                {
                    Text = preset.Name,
                    HorizontalOptions = LayoutOptions.Fill
                };

                button.Clicked += (s, e) =>
                {
                    PresetManager.Apply(preset);
                };

                PresetsVerticalStackLayout.Children.Add(button);
            }
        }

        private void OnSetVolume1Clicked(object sender, EventArgs e)
        {
            PresetManager.Apply(_preset1);
        }

        private void OnSetVolume2Clicked(object? sender, EventArgs e)
        {
            PresetManager.Apply(_preset2);
        }
    }

    public class Speaker
    {
        public string Name { get; }
        public string Host { get; }
        public int Port { get; }

        public Speaker(string name, string host, int port)
        {
            Name = name;
            Host = host;
            Port = port;
        }
    }

    public class Preset
    {
        public List<DevicePreset> DevicePresets { get; }
        public string Name { get; set; }

        public Preset(string name, List<DevicePreset> devicePresets)
        {
            Name = name;
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
        public DevicePreset(Speaker device, int volume)
        {
            Host = $"{device.Host}:{device.Port}";
            Volume = volume;
        }

        public string Host { get; }
        public int Volume { get; }
    }
}
