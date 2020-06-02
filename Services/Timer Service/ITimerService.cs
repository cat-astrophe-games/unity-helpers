using System;

public interface ITimerService
{
    void AddListenerOnTimer(Action<int> listener, int seconds);
    void RemoveListenerOnTimer(Action<int> listener);
    void SwitchToBackground();
    void SwitchToForeground();
    void AddListenerOnce(Action listener, int delay);
}
