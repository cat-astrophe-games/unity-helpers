using UnityEngine;
using BeliefEngine.HealthKit;
using strange.extensions.promise.api;
using strange.extensions.promise.impl;
using System;
using System.Collections.Generic;

public class HealthKitService : IHealthKitService
{
    private HealthStore healthStore;
    private HealthKitDataTypes healthKitDataTypes;

    private bool isReading;
    private bool authorized;

    public HealthKitService(HealthStore healthStore, HealthKitDataTypes healthKitDataTypes)
    {
        this.healthStore = healthStore;
        this.healthKitDataTypes = healthKitDataTypes;
    }

    public bool IsHealthDataAvailable()
    {
        return this.healthStore.IsHealthDataAvailable();
    }

    public IPromise<bool> Authorize()
    {
        Promise<bool> promise = new Promise<bool>();

        if (!this.healthStore.IsHealthDataAvailable())
        {
            Debug.LogWarning("Health data is not available on this device.");
            promise.Dispatch(false);
        }
        else
        {
            this.healthStore.Authorize(healthKitDataTypes, success =>
            {
                Debug.Log("Authorization for health store is: " + success);
                authorized = success;
                promise.Dispatch(success);
            });
        }

        return promise;
    }

    public IPromise<List<StepSample>> ReadSteps(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        Promise<List<StepSample>> promise = new Promise<List<StepSample>>();
        List<StepSample> allPeriods = new List<StepSample>();

        if (isReading)
        {
            Debug.LogWarning("can't read steps right now, because another process is reading at the same time ");
            promise.Dispatch(allPeriods);
        }
        else
        {
            Debug.Log($"reading quantity samples from date: {startDate:yyyy-MM-dd} to now {endDate:yyyy-MM-dd HH:mm ss}");
            this.healthStore.ReadQuantitySamples(HKDataType.HKQuantityTypeIdentifierStepCount, startDate, endDate, delegate (List<QuantitySample> samples)
            {
                foreach (QuantitySample sample in samples)
                {
                    Debug.Log($"{sample.startDate:yyyy-MM-dd HH:mm} to {sample.endDate:yyyy-MM-dd HH:mm} steps {(int)sample.quantity.doubleValue}");
                    allPeriods.Add(StepSample.FromQuantitySample(sample));
                }

                promise.Dispatch(allPeriods);
            });
        }
        return promise;
    }

    public bool IsAuthorized()
    {
        return authorized;
    }

}
