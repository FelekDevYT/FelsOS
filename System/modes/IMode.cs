namespace FenixOS.System.modes;

public interface IMode
{
    void start();
    void update();
    void stop();
}