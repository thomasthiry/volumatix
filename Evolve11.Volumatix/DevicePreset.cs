namespace Evolve11.Volumatix;

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