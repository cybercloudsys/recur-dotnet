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
using System.Collections.Generic;
using System.Linq;

namespace Recur
{
    /// <summary>
    /// provides a fluent and flexible API for creating and customizing recurring patterns for recurring events.
    /// </summary>
    public class YearlyPattern : IRecurringPattern
    {
        protected RecurringPattern recurrPattern;
        internal YearlyPattern(RecurringPattern recurringPattern)
        {
            recurrPattern = recurringPattern;
        }

        public RecurringPattern Every(TimeSpan delay)
        {
            recurrPattern.WaitTime = delay;
            return recurrPattern;
        }

        public TimePattern Daily()
        {
            recurrPattern.Start = recurrPattern.Start.Date;
            recurrPattern.WaitTime = TimeSpan.FromDays(1);
            return new TimePattern(recurrPattern);
        }

        public TimePattern Daily(DayOfWeek from, DayOfWeek to)
        {
            Daily();
            if (from != to && from != Weekdays.NextDay(to))
            {
                recurrPattern.AllowedWeekdays = new List<Weekday>();
                recurrPattern.AllowedWeekdays.Add(new Weekday { Day = from });
                var nextDay = from;
                do
                {
                    nextDay = Weekdays.NextDay(nextDay);
                    recurrPattern.AllowedWeekdays.Add(new Weekday { Day = nextDay });
                }
                while (nextDay != to);
            }
            return new TimePattern(recurrPattern);
        }

        public TimePattern EveryDays(int days)
        {
            Validator.CheckInput("days", days, 1, int.MaxValue);
            recurrPattern.WaitTime = TimeSpan.FromDays(days);
            return new TimePattern(recurrPattern);
        }

        public WeeklyPattern Weekly()
        {
            recurrPattern.WaitTime = TimeSpan.FromDays(7);
            return new WeeklyPattern(recurrPattern);
        }

        public WeeklyPattern EveryWeeks(int weeks)
        {
            Validator.CheckInput("weeks", weeks, 1, int.MaxValue);
            recurrPattern.WaitTime = TimeSpan.FromDays(weeks * 7);
            return new WeeklyPattern(recurrPattern);
        }

        public MinutesPattern EveryHours(int hours)
        {
            Validator.CheckInput("hours", hours, 1, int.MaxValue);
            recurrPattern.WaitTime = TimeSpan.FromHours(hours);
            return new MinutesPattern(recurrPattern);
        }
        
        public SecondsPattern EveryMinutes(int minutes)
        {
            Validator.CheckInput("minutes", minutes, 1, int.MaxValue);
            recurrPattern.WaitTime = TimeSpan.FromMinutes(minutes);
            return new SecondsPattern(recurrPattern);
        }

        public RecurringPattern EverySeconds(int seconds)
        {
            Validator.CheckInput("seconds", seconds, 1, int.MaxValue);
            recurrPattern.WaitTime = TimeSpan.FromSeconds(seconds);
            return recurrPattern;
        }

        public MonthlyPattern Monthly()
        {
            recurrPattern.Start = recurrPattern.Start.Date;
            return new MonthlyPattern(recurrPattern);
        }

        public MonthlyPattern OnMonths(params int[] month)
        {
            var months = month.Distinct().ToList();
            foreach (int mnth in months)
                Validator.CheckInput("month", mnth, 1, 12);
            recurrPattern.AllowedMonths = months;
            return Monthly();
        }

        public MonthlyPattern EveryMonths(int months)
        {
            Validator.CheckInput("months", months, 1, 300);
            recurrPattern.WaitMonths = months;
            return Monthly();
        }

        public RecurringPattern Build()
        {
            recurrPattern = OnMonths(1).Build();
            if (!recurrPattern.WaitYears.HasValue)
                recurrPattern.WaitYears = 1;
            return recurrPattern;
        }
   }
}