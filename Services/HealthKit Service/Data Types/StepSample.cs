using BeliefEngine.HealthKit;
using System;

public struct StepSample
{
    public DateTimeOffset from;
    public int steps;

    internal static StepSample FromQuantitySample(QuantitySample sample)
    {
        return new StepSample()
        {
            from = sample.startDate,
            steps = (int)sample.quantity.doubleValue
        };
    }
}
