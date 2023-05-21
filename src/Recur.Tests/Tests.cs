namespace RecurringPattern.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void FirstWorkingDay()
    {
        var recurPattern = RecurringPatterns.Monthly().OnDay(1).OnWorkday().Build();
        recurPattern.Start = DateTime.Parse("2101-01-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(recurPattern.Start.AddDays(2)), "Sat, first working day (1)");
        recurPattern.Start = DateTime.Parse("2101-05-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(recurPattern.Start.AddDays(1)), "Sun, first working day (2)");
        recurPattern.Start = DateTime.Parse("2101-08-10");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-09-01")), "Mon, first working day (3)");
        recurPattern.Start = DateTime.Parse("2100-12-02");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-03")), "Sat, first working day (4)");
        recurPattern.Start = DateTime.Parse("2101-04-02");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-05-02")), "Sun, first working day (5)");
        recurPattern.Start = DateTime.Parse("2101-07-02");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-08-01")), "Mon, first working day (6)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, 1st day (Mon-Fri)"), "first working day monthly 7");
    }

    [Test]
    public void SecondWorkingDay()
    {
        var recurPattern = RecurringPatterns.Monthly().OnDay(2).OnWorkday().Build();
        recurPattern.Start = DateTime.Parse("2101-06-20");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-07-01")), "Sat, 2nd working day (1)");
        recurPattern.Start = DateTime.Parse("2101-09-25");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-10-03")), "Sun, 2nd working day (2)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, 2nd day (Mon-Fri)"), "2nd working day monthly (3)");
    }

    [Test]
    public void ThirteenthWorkingDay()
    {
        var recurPattern = RecurringPatterns.Monthly().OnDay(13).OnWorkday().Build();
        recurPattern.Start = DateTime.Parse("2101-07-20");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-08-12")), "Sat, 13th working day (1)");
        recurPattern.Start = DateTime.Parse("2101-02-25");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-03-14")), "Sun, 13th working day (2)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, 13th day (Mon-Fri)"), "13th working day monthly (3)");
    }

    [Test]
    public void LastWorkingDay()
    {
        var recurPattern = RecurringPatterns.Monthly().FromLastDay().OnWorkday().Build();
        recurPattern.Start = DateTime.Parse("2101-04-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-04-29")), "Sat, last working day (1)");
        recurPattern.Start = DateTime.Parse("2101-07-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-07-29")), "Sun, last working day (2)");
        recurPattern.Start = DateTime.Parse("2101-10-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-10-31")), "Mon, last working day (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, last day (Mon-Fri)"), "last working day (4)");
    }

    [Test]
    public void LastDay()
    {
        var recurPattern = RecurringPatterns.Monthly().FromLastDay().Build();
        recurPattern.Start = DateTime.Parse("2101-01-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-31")), "last day (1)");
        recurPattern.Start = DateTime.Parse("2101-02-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-02-28")), "last day (2)");
        recurPattern.Start = DateTime.Parse("2101-04-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-04-30")), "last day (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, last day"), "last  day (4)");
    }

    [Test]
    public void BeforeLastDay()
    {
        var recurPattern = RecurringPatterns.Monthly().OnDay(2).FromLastDay().Build();
        recurPattern.Start = DateTime.Parse("2101-01-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-29")), "2 days before last day (1)");
        recurPattern.Start = DateTime.Parse("2101-02-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-02-26")), "2 days before last day (2)");
        recurPattern.Start = DateTime.Parse("2101-04-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-04-28")), "2 days before last day (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, 2 days before last day"), "2 days before last day (4)");
    }

    [Test]
    public void OnLastDayOfQuarter()
    {
        var recurPattern = RecurringPatterns.OnMonths(3, 6, 9, 12).FromLastDay().Build();
        recurPattern.Start = DateTime.Parse("2101-01-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-03-31")), "last day in quarter (1)");
        recurPattern.Start = DateTime.Parse("2101-04-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-06-30")), "last day in quarter (2)");
        recurPattern.Start = DateTime.Parse("2101-11-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-12-31")), "last day in quarter (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("[Mar,Jun,Sep,Dec] last day"), "last day in quarter (4)");
    }

    [Test]
    public void DayOnWeek()
    {
        var recurPattern = RecurringPatterns.Monthly().OnWeek(1, DayOfWeek.Saturday).Build();
        recurPattern.Start = DateTime.Parse("2100-12-31");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-01")), "first Saterday (1)");
        recurPattern.Start = DateTime.Parse("2101-04-30");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-05-07")), "first Saterday (2)");
        recurPattern.Start = DateTime.Parse("2101-09-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-09-03")), "first Saterday (3)");
        recurPattern = RecurringPatterns.Monthly().OnWeek(3, DayOfWeek.Monday).Build();
        recurPattern.Start = DateTime.Parse("2101-07-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-07-18")), "3rd Monday (1)");
        recurPattern.Start = DateTime.Parse("2101-07-19");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-08-15")), "3rd Monday (2)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, on 3rd Mon"), "3rd Monday (3)");
    }

    [Test]
    public void DayOnLastWeek()
    {
        var recurPattern = RecurringPatterns.Monthly().OnLastWeek(DayOfWeek.Friday).Build();
        recurPattern.Start = DateTime.Parse("2101-01-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-28")), "last Friday (1)");
        recurPattern.Start = DateTime.Parse("2101-04-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-04-29")), "last Friday (2)");
        recurPattern.Start = DateTime.Parse("2101-09-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-09-30")), "last Friday (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Monthly, on last Fri"), "last Friday (4)");
    }

    [Test]
    public void Weekly()
    {
        var recurPattern = RecurringPatterns.Weekly().OnDayOfWeek(DayOfWeek.Monday).Build();
        Assert.That(recurPattern.NextTime().DayOfWeek, Is.EqualTo(DayOfWeek.Monday), "weekly (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 7 days"), "weekly (2)");
    }

    [Test]
    public void DailyFromTo()
    {
        var recurPattern = RecurringPatterns.Daily(DayOfWeek.Monday, DayOfWeek.Friday).Build();
        recurPattern.Start = DateTime.Parse("2101-01-01");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-03")), "Mon - Fri (1)");
        recurPattern.Start = DateTime.Parse("2101-01-06");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-06")), "Mon - Fri (2)");
        recurPattern.Start = DateTime.Parse("2101-01-08");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-01-10")), "Mon - Fri (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 1 day (Mon-Fri)"), "Mon - Fri (4)");
    }

    [Test]
    public void EveryMonths()
    {
        var recurPattern = RecurringPatterns.EveryMonths(2).Build();
        recurPattern.Start = DateTime.Parse("2101-01-15");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-03-01")), "Every 2 months (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 2 months, 1st day"), "Every 2 months (2)");
        recurPattern = RecurringPatterns.EveryMonths(2).OnDay(15).Build();
        recurPattern.Start = DateTime.Parse("2101-01-16");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-03-15")), "Every 2 months (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 2 months, 15th day"), "Every 2 months (4)");
    }
    
    [Test]
    public void EveryMonthsLastDay()
    {
        var recurPattern = RecurringPatterns.EveryMonths(2).OnDay(1).FromLastDay().Build();
        recurPattern.Start = DateTime.Parse("2101-01-05");
        Assert.That(recurPattern.NextTime(), Is.EqualTo(DateTime.Parse("2101-02-27")), "Every 2 months before last day (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 2 months, 1 day before last day"), "Every 2 months before last day (2)");
    }

    [Test]
    public void Yearly()
    {
        var recurPattern = RecurringPatterns.Yearly().Build();
        Assert.That(recurPattern.NextTime(), Is.EqualTo(new DateTime(recurPattern.Start.Year + 1, 1, 1)), "Yearly (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 1 year, [Jan] 1st day"), "Yearly (2)");
    }

    [Test]
    public void EveryYears()
    {
        var recurPattern = RecurringPatterns.EveryYears(2).Build();
        Assert.That(recurPattern.NextTime(), Is.EqualTo(new DateTime(recurPattern.Start.Year + 2, 1, 1)), "Every 2 years (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 2 years, [Jan] 1st day"), "Every 2 years (2)");
        recurPattern = RecurringPatterns.EveryYears(2).OnMonths(4).OnDay(5).Build();
        Assert.That(recurPattern.NextTime(), Is.EqualTo(new DateTime(recurPattern.Start.Year + 2, 4, 5)), "Every 2 years (3)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 2 years, [Apr] 5th day"), "Every 2 years (4)");
    }

    [Test]
    public void EverySeconds()
    {
        var recurPattern = RecurringPatterns.EverySeconds(7);
        recurPattern.Start = recurPattern.Start.AddSeconds(-1);
        Assert.That(recurPattern.NextTime(), Is.EqualTo(recurPattern.Start.AddSeconds(7)), "Every 7 seconds (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 00:00:07"), "Every 7 seconds (2)");
    }

    [Test]
    public void EveryMinutes()
    {
        var recurPattern = RecurringPatterns.EveryMinutes(7).Build();
        recurPattern.Start = recurPattern.Start.AddMinutes(-1);
        Assert.That(recurPattern.NextTime(), Is.EqualTo(recurPattern.Start.AddMinutes(7)), "Every 7 minutes (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 00:07:00"), "Every 7 minutes (2)");
    }

    [Test]
    public void EveryHours()
    {
        var recurPattern = RecurringPatterns.EveryHours(7).Build();
        recurPattern.Start = recurPattern.Start.AddHours(-1);
        Assert.That(recurPattern.NextTime(), Is.EqualTo(recurPattern.Start.AddHours(7)), "Every 7 hours (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 07:00:00"), "Every 7 hours (2)");
    }

    [Test]
    public void EveryDays()
    {
        var recurPattern = RecurringPatterns.EveryDays(25).Build();
        Assert.That((recurPattern.NextTime()), Is.EqualTo(recurPattern.Start.AddDays(25)), "Every 25 days (1)");
        Assert.That(recurPattern.ToString(), Is.EqualTo("Every 25 days"), "Every 25 days (2)");
    }
}