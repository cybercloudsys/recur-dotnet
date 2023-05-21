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
using System.Globalization;
using System.Linq;
using System.Text;

namespace Recur
{
    internal static class Humanizer
    {
        internal static string Humanize(RecurringPattern recurPattern)
        {
            StringBuilder output = new StringBuilder();
            if (recurPattern.WaitTime.HasValue)
            {
                output.Append("Every ");
                var time = recurPattern.WaitTime.Value;
                double days = Math.Floor(time.TotalDays);
                if (days >= 1)
                {
                    output.AppendFormat("{0} day{1}", days, days > 1 ? "s" : null);
                    time = time.Add(-TimeSpan.FromDays(days));
                    if (time.TotalSeconds > 0)
                        output.Append(" ");
                }
                if (time.TotalSeconds > 0)
                    output.Append(time);

                if (recurPattern.AllowedWeekdays != null && recurPattern.AllowedWeekdays.Count > 0)
                    AppendStringOfWeekdays(new List<DayOfWeek>(recurPattern.AllowedWeekdays.Select(wd => wd.Day)), output);
            }
            else
            {
                if (recurPattern.AllowedMonths == null && !recurPattern.WaitMonths.HasValue && !recurPattern.WaitYears.HasValue
                    && (recurPattern.AllowedDays != null || recurPattern.AllowedWeekdays != null))
                    output.Append("Monthly, ");
                else if (recurPattern.AllowedMonths != null)
                {
                    if (recurPattern.WaitYears.HasValue)
                        output.AppendFormat("Every {0} year{1}, ", recurPattern.WaitYears, recurPattern.WaitYears > 1 ? "s" : null);
                    else if(recurPattern.AllowedMonths.Count == 1)
                        output.Append("Yearly, ");
                }
                if (recurPattern.AllowedDays != null)
                {
                    if (recurPattern.WaitMonths.HasValue)
                        output.AppendFormat("Every {0} month{1}, ", recurPattern.WaitMonths, recurPattern.WaitMonths > 1 ? "s" : null);
                    else if (recurPattern.AllowedMonths != null)
                        output.AppendFormat("[{0}] ", string.Join(",", recurPattern.AllowedMonths
                            .Select(m => CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(m))));
                    foreach (var day in recurPattern.AllowedDays)
                    {
                        if (day.IsLastDay)
                        {
                            if (day.Day.HasValue)
                                output.AppendFormat("{0} day{1} before last day", day.Day, day.Day > 1 ? "s" : null);
                            else
                                output.Append("last day");
                        }
                        else
                            output.AppendFormat("{0} day", Nth(day.Day.Value));
                        if (day.IsWorkday) 
                        {
                            List<DayOfWeek> weekDays = new List<DayOfWeek>();
                            foreach(DayOfWeek item in Enum.GetValues(typeof(DayOfWeek)))
                            {
                                if (!(recurPattern.WeeklyOffDays ?? Settings.WeeklyOffDays).Contains(item))
                                    weekDays.Add(item);
                            }
                            AppendStringOfWeekdays(weekDays, output);
                        }
                    }
                }
                if (recurPattern.AllowedWeekdays != null && recurPattern.AllowedWeekdays.Count > 0)
                {
                    output.AppendFormat("on {0}", string.Join(", ", recurPattern.AllowedWeekdays.Select(d =>
                        string.Format("{0} {1}", d.IsLastWeek ? "last" : Nth(d.WeekOfMonth.Value),
                            d.Day.ToString().Substring(0, 3)))));
                }
            }
            return output.ToString();
        }

        private static void AppendStringOfWeekdays(List<DayOfWeek> days, StringBuilder output)
        {
            DayOfWeek? offDay = null;
            DayOfWeek[] sortedDays = new DayOfWeek[days.Count];
            short i = 0;
            for (DayOfWeek day = DayOfWeek.Sunday; day <= DayOfWeek.Saturday; day++)
            {
                if (days.Contains(day))
                {
                    if (offDay.HasValue)
                    {
                        sortedDays[i] = day;
                        i++;
                    }
                }
                else if (!offDay.HasValue)
                    offDay = day;
            }
            for (DayOfWeek day = DayOfWeek.Sunday; day <= DayOfWeek.Saturday; day++)
            {
                if (days.Contains(day) && (!offDay.HasValue || day < offDay.Value))
                {
                    sortedDays[i] = day;
                    i++;
                }
            }
            output.Append(" (");
            bool started = false;
            foreach(var day in sortedDays)
            {
                bool prev = days.Contains(Weekdays.PreviousDay(day));
                if (started && !prev)
                    output.Append(",");
                if (!prev || (!offDay.HasValue && day == DayOfWeek.Sunday))
                {
                    output.Append(day.ToString().Substring(0,3));
                    started = true;
                }
                else if (started && (!days.Contains(Weekdays.NextDay(day))
                    || (!offDay.HasValue && day == DayOfWeek.Saturday)))
                {
                    output.AppendFormat("-{0}", day.ToString().Substring(0,3));
                }
            }
            output.Append(")");
        }

        private static string Nth(int number)
        {
            string result = number.ToString();
            if (result.EndsWith("1") && !result.EndsWith("11"))
                return string.Format("{0}{1}", result, "st");
            else if (result.EndsWith("2") && !result.EndsWith("12"))
                return string.Format("{0}{1}", result, "nd");
            else if (result.EndsWith("3") && !result.EndsWith("13"))
                return string.Format("{0}{1}", result, "rd");
            return string.Format("{0}{1}", result, "th");
        }
    }
}