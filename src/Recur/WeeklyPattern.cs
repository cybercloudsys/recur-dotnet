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

namespace Recur
{
    /// <summary>
    /// provides a fluent and flexible API for creating and customizing recurring patterns for recurring events.
    /// </summary>
    public class WeeklyPattern : IRecurringPattern
    {
        protected RecurringPattern recurrPattern;

        internal WeeklyPattern(RecurringPattern recurringPattern)
        {
            recurrPattern = recurringPattern;
        }

        public RecurringPattern Build() => OnDayOfWeek(DayOfWeek.Monday).Build();

        /// <summary>
        ///  Creates recurring pattern that recurring at specified day of week.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public TimePattern OnDayOfWeek(DayOfWeek dayOfWeek)
        {
            while (recurrPattern.Start.DayOfWeek != dayOfWeek)
                recurrPattern.Start = recurrPattern.Start.AddDays(1);
            return new TimePattern(recurrPattern);
        }
   }
}