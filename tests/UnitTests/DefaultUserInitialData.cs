using System;

namespace UnitTests;

public static class DefaultUserInitialData
{
    public const string UserId = "id";
    public const string Ip = "100.100.100.100";
    public static DateTime DateTimeNow { get; } = DateTime.Now;
}
