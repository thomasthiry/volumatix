using Newtonsoft.Json;

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
        private static readonly Dictionary<string, Speaker> _speakers = new();

        public MainPage()
        {
            InitializeComponent();

            var presets = LoadPresets();

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

        private static List<Preset> LoadPresets()
        {
            Speaker speaker1 = new("Salon", "192.168.129.1", 11000);
            Speaker speaker2 = new("Salle à manger", "192.168.129.4", 11000);
            _speakers.Add(speaker1.Host, speaker1);
            _speakers.Add(speaker2.Host, speaker2);

            var json = @"{
                    ""presets"": [
                        {
                            ""name"": ""Matin"",
                            ""devicePresets"": [
                                {
                                    ""volume"": 15,
                                    ""deviceHost"": ""192.168.129.1""
                                }
                            ]
                        }
                    ]
                }";

            return DeserializePresets(json);
        }
        static List<Preset> DeserializePresets(string json)
        {
            var jsonObject = JsonConvert.DeserializeObject<RootObjectDto>(json);
            var presets = new List<Preset>();

            foreach (var presetData in jsonObject.Presets)
            {
                var devicePresets = new List<DevicePreset>();
                foreach (var devicePresetData in presetData.DevicePresets)
                {
                    var speaker = _speakers[devicePresetData.DeviceHost];
                    var devicePreset = new DevicePreset(speaker, devicePresetData.Volume);
                    devicePresets.Add(devicePreset);
                }
                var preset = new Preset(presetData.Name, devicePresets);
                presets.Add(preset);
            }

            return presets;
        }
    }

    public class RootObjectDto
    {
        public List<PresetDataDto> Presets { get; set; }
    }

    public class PresetDataDto
    {
        public string Name { get; set; }
        public List<DevicePresetDataDto> DevicePresets { get; set; }
    }

    public class DevicePresetDataDto
    {
        public int Volume { get; set; }
        public string DeviceHost { get; set; }
    }

    public class PresetDto
    {
        public string Name { get; }
        public List<DevicePresetDto> DevicePresets { get; }

        public PresetDto(string name, List<DevicePresetDto> devicePresets)
        {
            Name = name;
            DevicePresets = devicePresets;
        }
    }

    public class DevicePresetDto
    {
        public string Device { get; }
        public int Volume { get; }

        public DevicePresetDto(string device, int volume)
        {
            Device = device;
            Volume = volume;
        }
    }
}
