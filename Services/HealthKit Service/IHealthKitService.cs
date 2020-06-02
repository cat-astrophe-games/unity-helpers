using strange.extensions.promise.api;
using System;
using System.Collections.Generic;

public interface IHealthKitService
{
    bool IsHealthDataAvailable();
    IPromise<bool> Authorize();
    bool IsAuthorized();
    IPromise<List<StepSample>> ReadSteps(DateTimeOffset startDate, DateTimeOffset endDate);
}
