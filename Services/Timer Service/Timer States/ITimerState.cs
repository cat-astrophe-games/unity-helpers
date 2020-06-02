using UnityEngine;
using System.Collections;
using System;

public interface ITimerState
{
    void Dispose();
    void AddListener(Action<int> onTick);
}
