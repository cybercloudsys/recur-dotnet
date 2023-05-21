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
    public class RecurringPatterns
    {
        /// <summary>
        ///  Creates recurring pattern that is recurring every specified time.
        /// </summary>
        /// <param name="delay">The time span in which the event will occur for the next time.</param>
        /// <returns>Recurring pattern.</returns>
        public static RecurringPattern Every(TimeSpan delay) => new Pattern().Every(delay);

        /// <summary>
        ///  Creates recurring pattern that is recurring every day.
        /// </summary>
        /// <returns>Recurring pattern.</returns>
        public static TimePattern Daily() => new Pattern().Daily();

        /// <summary>
        ///  Creates recurring pattern that is recurring every day.
        /// </summary>
        /// <param name="from">The day of week in which the event will start to occur.</param>
        /// <param name="to">The last day of week in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public static TimePattern Daily(DayOfWeek from, DayOfWeek to) => new Pattern().Daily(from, to);

        /// <summary>
        ///  Creates recurring pattern that is recurring every number of days.
        /// </summary>
        /// <returns>Recurring pattern.</returns>
        public static TimePattern EveryDays(int days) => new Pattern().EveryDays(days);

        /// <summary>
        ///  Creates recurring pattern that recurring once every week.
        /// </summary>
        /// <returns>Recurring pattern.</returns>
        public static WeeklyPattern Weekly() => new Pattern().Weekly();
        
        /// <summary>
        ///  Creates recurring pattern that recurring every number of weeks.
        /// </summary>
        /// <param name="weeks">The numbers of weeks in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public static WeeklyPattern EveryWeeks(int weeks) => new Pattern().EveryWeeks(weeks);

        /// <summary>
        ///  Creates recurring pattern that recurring every number of hours.
        /// </summary>
        /// <param name="hours">The number of hours in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public static MinutesPattern EveryHours(int hours) => new Pattern().EveryHours(hours);

        /// <summary>
        ///  Creates recurring pattern that recurring every number of minutes.
        /// </summary>
        /// <param name="minutes">The number of minutes in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public static SecondsPattern EveryMinutes(int minutes) => new Pattern().EveryMinutes(minutes);

        /// <summary>
        ///  Creates recurring pattern that recurring every number of seconds.
        /// </summary>
        /// <param name="seconds">The number of seconds in which the event will occur.</param>
        /// <returns>Recurring pattern.</returns>
        public static RecurringPattern EverySeconds(int seconds) => new Pattern().EverySeconds(seconds);

        /// <summary>
        ///  Creates recurring pattern that recurring at the specified day of every month.
        /// </summary>
        /// <returns>Recurring pattern.</returns>
        public static MonthlyPattern Monthly() => new Pattern().Monthly();

        /// <summary>
        ///  Creates recurring pattern that recurring at the specified months.
        /// </summary>
        /// <returns>Recurring pattern.</returns>
        public static MonthlyPattern OnMonths(params int[] month) => new Pattern().OnMonths(month);

        /// <summary>
        ///  Creates recurring pattern that recurring at the specified day of specified months.
        /// </summary>
        /// <param name="months">The number of months in which the job will wait to be enqueued (1-300).</param>
        /// <returns>Recurring pattern.</returns>
        public static MonthlyPattern EveryMonths(int months) => new Pattern().EveryMonths(months);

        /// <summary>
        ///  Creates recurring pattern that recurring every year.
        /// </summary>
        /// <returns>Recurring pattern.</returns>
        public static YearlyPattern Yearly() => new Pattern().Yearly();

        /// <summary>
        ///  Creates recurring pattern that recurring every number of years.
        /// </summary>
        /// <param name="years">The number of years in which the event will wait to occur (1-50).</param>
        /// <returns>Recurring pattern.</returns>
        public static YearlyPattern EveryYears(int years) => new Pattern().EveryYears(years);

        public static DateTime Now
        {
            get
            {
                return new DateTime(
                    (Settings.DateTimeKind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now).Ticks
                        / TimeSpan.TicksPerSecond * TimeSpan.TicksPerSecond, Settings.DateTimeKind);
            }
        }
    }
}