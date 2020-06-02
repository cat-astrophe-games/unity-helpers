using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using BeliefEngine.HealthKit;

public interface IPedometerWrapper
{
    void BeginReadingData(DateTimeOffset startDate, ReceivedHealthData<List<PedometerData>> handler);
    void StopReadingData();
}
