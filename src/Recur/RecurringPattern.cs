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
    /// The time pattern of recurring events.
    /// </summary>
    public class RecurringPattern : IRecurringPattern
    {
        public RecurringPattern()
        {
            Start = RecurringPatterns.Now;
            offDays = Settings.WeeklyOffDays;
        }

        /// <summary>
        /// Get or set the time for the recurring job to start calculating the schedule time.
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// Get or set the waiting time for next schedule.
        /// </summary>
        internal TimeSpan? WaitTime { get; set; }

        public int? WaitSeconds
        {
            get => WaitTime.HasValue ? (int?)WaitTime.Value.TotalSeconds : null;
            set
            {
                if (value.HasValue)
                    WaitTime = TimeSpan.FromSeconds(value.Value);
                else
                    WaitTime = null;
            }
        }

        /// <summary>
        /// Get or set the waiting months for next schedule.
        /// </summary>
        ///<remarks>
        /// This property will be ignored if WaitTime has value
        ///<remarks>
        public int? WaitMonths { get; set; }

        /// <summary>
        /// Get or set the waiting years for next schedule.
        /// </summary>
        ///<remarks>
        /// This property will be ignored if WaitTime or WaitMonth has value
        ///<remarks>
        public int? WaitYears { get; set; }

        /// <summary>
        /// Get or set the list of allowed days of week
        /// </summary>
        ///<remarks>
        /// All days are allowed if this list is null or empty
        ///</remarks>
        public List<Weekday> AllowedWeekdays { get; set; }

        /// <summary>
        /// Get or set the list of allowed weeks on month
        /// </summary>
        ///<remarks>
        /// All weeks on month are allowed if this list is null or empty
        ///</remarks>
        public List<int> Weeks { get; set; }

        /// <summary>
        /// Get or set the list of allowed days of month
        /// </summary>
        ///<remarks>
        /// All days of month are allowed if this list is null or empty
        ///</remarks>
        public List<Monthday> AllowedDays { get; set; }

        /// <summary>
        /// Get or set the list of allowed months of year
        /// </summary>
        ///<remarks>
        /// All months are allowed if this list is null or empty
        ///</remarks>
        public List<int> AllowedMonths { get; set; }

        private List<DayOfWeek> offDays;

        /// <summary>
        /// Get or set the list of weekly off days. The default Saterday and Sunday.
        /// </summary>
        ///<remarks>
        /// This will only accept minimum 1 day and maximum 3 days, other than that it will throw InvalidRecurringPatternException
        ///<remarks>
        public List<DayOfWeek> WeeklyOffDays {
            get { return offDays; }
            set
            {
                var newDays = value.Distinct().ToList();
                if (newDays.Count > 0 && newDays.Count < 4)
                    offDays = newDays;
                else
                    throw new InvalidRecurringPatternException("weeklyOffDays", "(1-3) DayOfWeek", newDays.Count);
            }
        }

        public DateTime NextTime()
        {
            return PatternMatcher.NextTime(this);
        }

        public bool IsMatching(DateTime time)
        {
            return PatternMatcher.IsMatching(this, time);
        }

        public override string ToString()
        {
            return Humanizer.Humanize(this);
        }

        public RecurringPattern Build() => this;
    }
}