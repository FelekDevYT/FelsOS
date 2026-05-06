namespace FenixOS.System.WindowSystem;

public interface IApp
{
    string Name { get; }
    AbstractWindow CreateInstance();
}