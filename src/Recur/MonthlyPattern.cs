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

namespace Recur
{
    /// <summary>
    /// provides a fluent and flexible API for creating and customizing recurring patterns for recurring events.
    /// </summary>
    public class MonthlyPattern : MonthdayPattern, IRecurringPattern
    {
        internal MonthlyPattern(RecurringPattern recurringPattern) : base(recurringPattern) { }
        /// <summary>
        ///  Creates recurring pattern that recurring at the specified day of every month.
        /// </summary>
        /// <param name="day">The day of month in which the event will occur (1-31).</param>
        /// <returns>Recurring pattern</returns>
        public MonthdayPattern OnDay(int day)
        {
            Validator.CheckInput("day", day, 1, 31);
            pattern.AllowedDays = new List<Monthday>();
            pattern.AllowedDays.Add(new Monthday { Day = day });
            return this;
        }

        /// <summary>
        ///  Creates recurring pattern that recurring on the specified day of specific week of every month.
        /// </summary>
        /// <param name="weekOfMonth">The week on a month in which the event will occur.</param>
        /// <param name="dayOfWeek">The day of week in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public TimePattern OnWeek(int weekOfMonth, DayOfWeek dayOfWeek)
        {
            Validator.CheckInput("weekOfMonth", weekOfMonth, 1, 5);
            pattern.AllowedWeekdays = new List<Weekday>();
            pattern.AllowedWeekdays.Add(new Weekday { Day = dayOfWeek, WeekOfMonth = weekOfMonth });
            return this;
        }

        /// <summary>
        ///  Creates recurring pattern that recurring on the specified day of last week of every month.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public TimePattern OnLastWeek(DayOfWeek dayOfWeek)
        {
            pattern.AllowedWeekdays = new List<Weekday>();
            pattern.AllowedWeekdays.Add(new Weekday { Day = dayOfWeek, IsLastWeek = true });
            return this;
        }

        public new RecurringPattern Build()
        {
            pattern.AllowedDays = new List<Monthday>();
            pattern.AllowedDays.Add(new Monthday { Day = 1 });
            return base.Build();
        }
   }
}