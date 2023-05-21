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
    public class MinutesPattern : SecondsPattern, IRecurringPattern
    {
        internal MinutesPattern(RecurringPattern pattern) : base(pattern) { }

        /// <summary>
        ///  Creates recurring pattern that is recurring at the specified minute.
        /// </summary>
        /// <param name="minute">The minute in which the event will occur (0-59).</param>
        /// <returns>Recurring pattern.</returns>
        public SecondsPattern AtMinute(int minute)
        {
            Validator.CheckInput("minute", minute, 0, 59);
            pattern.Start = pattern.Start.AddMinutes(minute-pattern.Start.Minute);
            return this;
        }

        public new RecurringPattern Build() => AtMinute(0).Build();
   }
}