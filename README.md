# Recur

Provides advanced functionality for calculating the timing of recurring events. You can easily use it to create simple or complex patterns for events that repeat at regular intervals, including events that occur on custom days or months. You can also customize the weekly off days to be used in the pattern, and generate a human-readable text representation of the pattern.\
It gives easy way to check if a given time matches a specific pattern, and calculate the next time that a pattern will occur.\
Recur is ideal for use in scheduling systems, task automation, and any other application that requires the precise timing of recurring events.

## Features

- Define recurrence patterns for daily, weekly, monthly, and yearly occurrences.
- Specify specific days of the week or month for recurring events.
- Handle complex recurrence scenarios with customizable options.
- Calculate the next occurrence based on the current time.
- Lightweight and easy to integrate into existing .NET projects.
- Serializable recurring patterns for utilization in distributed systems.

## Installation

To install Recur, you can use either the package manager or the CLI.

### Package Manager

Run the following command in the NuGet Package Manager Console:

```
Install-Package Recur
```

### CLI

Run the following command in a command prompt or terminal:

```
dotnet add package Recur
```

Choose the method that suits your preference or development environment.


## Usage

To get started with Recur, you need to create a recurrence pattern. Recur supports various recurrence patterns such as daily, weekly, monthly, and yearly. Once you have defined the pattern, you can use the `NextTime` method to calculate the next occurrence.

Here's an example of creating a daily recurrence pattern:

```csharp
var recurPattern = RecurringPatterns.Daily().Build();
DateTime nextOccurrence = recurPattern.NextTime();
// nextOccurrence will be tomorrow date
```

In this example, we created a daily recurrence pattern that calculates the next occurrence.

You can also create more complex recurrence patterns. Here's an example of creating a recurrence pattern that occurs on the closest working day 2 days before the end of a quarter at 10:30 AM:

```csharp
var recurPattern = RecurringPatterns.OnMonths(3, 6, 9, 12).OnDay(2).FromLastDay().OnWorkday().AtHour(10).AtMinute(30).Build();
```

Similarly, you can create monthly and yearly recurrence patterns with specific day or month constraints for example last Friday or second Monday of every month.

These are just a few examples of the basic usage of the Recur. You can explore the various options and methods provided by Recur to create more complex and customized recurrence patterns.

## License

Recur\
Copyright © 2023 Cyber Cloud Systems LLC

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.