using UnityEngine;
using System.Collections;
using strange.extensions.promise.api;
using System;
using strange.extensions.promise.impl;
using System.Collections.Generic;

public class MockHealthKitService : IHealthKitService
{
    public IPromise<bool> Authorize()
    {
        Promise<bool> promise = new Promise<bool>();
        promise.Dispatch(true);
        return promise;
    }

    public bool IsAuthorized()
    {
        return true;
    }

    public bool IsHealthDataAvailable()
    {
        return true;
    }

    IPromise<List<StepSample>> IHealthKitService.ReadSteps(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        Promise<List<StepSample>> promise = new Promise<List<StepSample>>();

        List<StepSample> samples = new List<StepSample>();
        //let's have a sample per hour (plus one in case total hours is zero)
        int hours = (int)endDate.Subtract(startDate).TotalHours + 1;

        DateTimeOffset currentDate = startDate;
        for (int i = 0; i < hours; i++)
        {
            samples.Add(new StepSample()
            {
                from = currentDate,
                steps = UnityEngine.Random.Range(0, 500)
            });
            currentDate = new DateTimeOffset(currentDate.AddHours(1).DateTime);
        }

        promise.Dispatch(samples);
        return promise;
    }
}
