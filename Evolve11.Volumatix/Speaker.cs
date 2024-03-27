namespace Evolve11.Volumatix;

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