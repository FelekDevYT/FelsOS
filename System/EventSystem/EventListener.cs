namespace FenixOS.System.EventSystem;

public interface EventListener<TЕ> where TЕ : Event //i hate C# #2
{
    void onEvent(TЕ e);
}