using System.Globalization;
using RecruitmentTask_1611.Models;

namespace RecruitmentTask_1611.Helpers;

using System;

public static class BaseHelpers
{
    private static readonly Random _random = new Random();

    public static int GetRandomTenDigitInt()
    {
        return _random.Next(1000000000, int.MaxValue);
    }
    
    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString();
    }
    public static bool ActivitiesDueYesterdayOrEarlier(List<Activity> activities)
    {
        DateTime today = DateTime.Today;

        return activities.Any(a =>
            DateTime.Parse(a.DueDate, null, DateTimeStyles.AssumeUniversal).Date <= today.AddDays(-1));
    }
}