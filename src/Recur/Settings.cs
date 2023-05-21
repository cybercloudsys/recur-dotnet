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
using System.Collections.Generic;

namespace Recur
{
    public static class Settings
    {
        static List<DayOfWeek> weeklyOffDays;
        public static List<DayOfWeek> WeeklyOffDays
        {
            get
            {
                if (weeklyOffDays == null)
                    weeklyOffDays = new List<DayOfWeek>(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday });
                return weeklyOffDays;
            }
            set
            {
                var offDays = value.Distinct().ToArray();
                if (offDays.Length > 0 && offDays.Length < 4)
                    weeklyOffDays = new List<DayOfWeek>(offDays);
                else
                    throw new InvalidWeeklyOffDaysException(offDays.Length);
            }
        }

        public static DateTimeKind DateTimeKind { get; set; }
    }
}