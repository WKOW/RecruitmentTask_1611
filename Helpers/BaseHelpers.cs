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
}