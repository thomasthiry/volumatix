namespace Evolve11.Volumatix;

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