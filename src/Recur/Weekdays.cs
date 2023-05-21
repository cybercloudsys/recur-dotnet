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
    internal static class Weekdays
    {
        internal static DayOfWeek NextDay(DayOfWeek day)
        {
            if (day == DayOfWeek.Saturday)
                return DayOfWeek.Sunday;
            else
                return day + 1;
        }

        internal static DayOfWeek PreviousDay(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
                return DayOfWeek.Saturday;
            else
                return day - 1;
        }
    }
}