using System.Globalization;
using RecruitmentTask_1611.Models;

namespace RecruitmentTask_1611.Helpers;

public class CompareDays
{
    public static bool ActivitiesDueYesterdayOrEarlier(List<Activity> activities)
    {
        DateTime today = DateTime.Today;

        return activities.Any(a =>
            DateTime.Parse(a.DueDate, null, DateTimeStyles.AssumeUniversal).Date <= today.AddDays(-1));
    }
}