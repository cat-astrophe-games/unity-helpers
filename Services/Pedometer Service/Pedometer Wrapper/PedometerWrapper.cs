using System;
using System.Collections.Generic;
using BeliefEngine.HealthKit;

public class PedometerWrapper : IPedometerWrapper
{
    private HealthStore healthStore;
    public PedometerWrapper(HealthStore healthStore)
    {
        this.healthStore = healthStore;
    }

    public void BeginReadingData(DateTimeOffset startDate, ReceivedHealthData<List<PedometerData>> handler)
    {
        this.healthStore.BeginReadingPedometerData(startDate, handler);
    }

    public void StopReadingData()
    {
        this.healthStore.StopReadingPedometerData();
    }
}
