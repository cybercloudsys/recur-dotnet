// Recur
// Copyright © 2023 Cyber Cloud Systems LLC

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Linq;

namespace Recur
{
    internal static class PatternMatcher
    {
        internal static bool IsMatching(RecurringPattern recurPattern, DateTime time)
        {
            return (time >= recurPattern.Start
                && (!recurPattern.WaitTime.HasValue || (Math.Floor((time - recurPattern.Start).TotalSeconds) % recurPattern.WaitTime.Value.TotalSeconds) == 0)
                && (recurPattern.AllowedDays == null || (recurPattern.AllowedDays.Any(d => GetDay(recurPattern, d, time) == time.Day)
                    && Math.Floor((recurPattern.Start - recurPattern.Start.Date).TotalSeconds) == Math.Floor((time - time.Date).TotalSeconds)))
                && (recurPattern.AllowedWeekdays == null || recurPattern.AllowedWeekdays.Any(d => d.Day == time.DayOfWeek
                    && (!d.WeekOfMonth.HasValue || d.WeekOfMonth == ((time.Day - 1) / 7 + 1))
                    && (!d.IsLastWeek || time.AddDays(1-time.Day).AddMonths(1).AddDays(-1).Day - time.Day < 7)
                    && Math.Floor((recurPattern.Start - recurPattern.Start.Date).TotalSeconds) == Math.Floor((time - time.Date).TotalSeconds)))
                && (recurPattern.AllowedMonths == null || recurPattern.AllowedMonths.Any(m => m == time.Month))
                && (!recurPattern.WaitMonths.HasValue || recurPattern.WaitTime.HasValue
                    || Math.Ceiling((time - recurPattern.Start).TotalDays / 30) % recurPattern.WaitMonths.Value == 0)
                && (!recurPattern.WaitYears.HasValue || recurPattern.WaitMonths.HasValue || recurPattern.WaitTime.HasValue
                    || ((Math.Ceiling((time - recurPattern.Start).TotalDays / 365) % recurPattern.WaitYears.Value) == 0)));
        }

        internal static DateTime NextTime(RecurringPattern recurPattern)
        {
            DateTime nextDate = RecurringPatterns.Now;
            if (nextDate < recurPattern.Start)
                nextDate = recurPattern.Start;
            while (!IsMatching(recurPattern, nextDate))
            {
                if (recurPattern.WaitTime.HasValue)
                    nextDate = nextDate.AddSeconds(recurPattern.WaitTime.Value.TotalSeconds
                        - (Math.Ceiling((nextDate - recurPattern.Start).TotalSeconds)
                        % recurPattern.WaitTime.Value.TotalSeconds));
                else if (recurPattern.AllowedDays != null && recurPattern.AllowedDays.Count > 0
                    || recurPattern.AllowedWeekdays != null && recurPattern.AllowedWeekdays.Count > 0)
                {
                    if (nextDate.Hour != recurPattern.Start.Hour || nextDate.Minute != recurPattern.Start.Minute
                        || nextDate.Second != recurPattern.Start.Second || nextDate.Millisecond > 0)
                    {
                        var newDate = new DateTime(nextDate.Year, nextDate.Month, nextDate.Day,
                            recurPattern.Start.Hour, recurPattern.Start.Minute, recurPattern.Start.Second,
                            (System.DateTimeKind)Settings.DateTimeKind);
                        if (newDate > nextDate)
                            nextDate = newDate;
                        else
                            nextDate = newDate.AddDays(1);
                    }
                    else
                        nextDate = nextDate.AddDays(1);
                }
            }
            return nextDate;
        }

        private static int GetDay(RecurringPattern recurPattern, Monthday dayOfMonth, DateTime time)
        {
            if (dayOfMonth.IsWorkday)
            {
                int day = dayOfMonth.Day ?? 0;
                int lastDay = time.Date.AddDays(1-time.Day).AddMonths(1).AddDays(-1).Day;
                if (dayOfMonth.IsLastDay)
                    day = lastDay - (dayOfMonth.Day ?? 0);
                var dayOfWeek = time.Date.AddDays(day-time.Day).DayOfWeek;
                var offDays = recurPattern.WeeklyOffDays ?? Settings.WeeklyOffDays;
                if (offDays.Contains(dayOfWeek))
                {
                    bool prev = offDays.Contains(Weekdays.PreviousDay(dayOfWeek));
                    if (offDays.Contains(Weekdays.NextDay(Weekdays.NextDay((dayOfWeek)))))
                        return day > 1 ? day - 1 : 4;
                    else if (offDays.Contains(Weekdays.NextDay(dayOfWeek)))
                        return day > (prev ? 2 : 1) ? day - (prev ? 2 : 1) : 3;
                    else if (offDays.Contains(Weekdays.PreviousDay(Weekdays.PreviousDay((dayOfWeek)))))
                        return day + (day < lastDay - 1 ? 2 : -3);
                    else if (prev)
                        return day + (day < lastDay ? 1 : -2);
                }
                return day;
            }
            else if (dayOfMonth.IsLastDay)
            {
                int lastDay = time.Date.AddDays(1-time.Day).AddMonths(1).AddDays(-1).Day;
                if (dayOfMonth.Day.HasValue)
                    return lastDay - dayOfMonth.Day.Value;
                return lastDay;
            }
            else
                return dayOfMonth.Day.Value;
        }
    }
}