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

namespace Recur
{
    /// <summary>
    /// provides a fluent and flexible API for creating and customizing recurring patterns for recurring events.
    /// </summary>
    public class TimePattern : MinutesPattern, IRecurringPattern
    {
        internal TimePattern(RecurringPattern pattern) : base(pattern) { }

        /// <summary>
        ///  Creates recurring pattern that is recurring at the specified hour.
        /// </summary>
        /// <param name="hour">The hour in which the event will occur (0-23).</param>
        /// <returns>Recurring pattern.</returns>        
        public MinutesPattern AtHour(int hour)
        {
            Validator.CheckInput("hour", hour, 0, 23);
            pattern.Start = pattern.Start.Date.AddHours(hour);
            return this;
        }

        public new RecurringPattern Build() => AtHour(0).Build();
   }
}