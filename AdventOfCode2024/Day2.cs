
public class Day2
{
    public static (int?, int?) Result()
    {
        // Setup
        var reports = File.ReadAllLines("data/2.txt");


        // First star
        int result1 = Calculate1(reports.ToList()).Count;
        // Second star
        int result2 = Calculate2(reports.ToList()).Count;

        return (result1, result2);
    }
    private static List<int> Calculate1(List<string> reports)
    {
        List<int> validRows = new();
        foreach (var (report, index) in reports.Select((value, i) => (value, i)))
        {
            List<int> numbers = report.Split(" ").Select(int.Parse).ToList();
            if (!IsValidRow(numbers, true)) continue;

            validRows.Add(index);
        }
        return validRows;
    }
    private static List<int> Calculate2(List<string> reports)
    {
        List<int> validRows = new();
        foreach (var (report, index) in reports.Select((value, i) => (value, i)))
        {
            List<int> numbers = report.Split(" ").Select(int.Parse).ToList();
            if (!IsValidRow(numbers)) continue;
            validRows.Add(index);
        }

        return validRows;
    }
    private static bool IsValidRow(List<int> numbers, bool secondChance = false)
    {
        bool isDescending = numbers.First() > numbers.Last();
        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (isDescending)
            {
                if (numbers[i] < numbers[i + 1])
                {
                    if (secondChance)
                    {
                        return false;
                    }
                    var numberWithoutCurrent = new List<int>(numbers);
                    var numberWithoutNext = new List<int>(numbers);
                    numberWithoutCurrent.RemoveAt(i);
                    numberWithoutNext.RemoveAt(i + 1);
                    return IsValidRow(numberWithoutCurrent, true) || IsValidRow(numberWithoutNext, true);
                }
            }
            else
            {
                if (numbers[i] > numbers[i + 1])
                {
                    if (secondChance)
                    {
                        return false;
                    }
                    var numberWithoutCurrent = new List<int>(numbers);
                    var numberWithoutNext = new List<int>(numbers);
                    numberWithoutCurrent.RemoveAt(i);
                    numberWithoutNext.RemoveAt(i + 1);
                    return IsValidRow(numberWithoutCurrent, true) || IsValidRow(numberWithoutNext, true);
                }
            }
            if (Math.Abs(numbers[i] - numbers[i + 1]) > 3)
            {
                if (secondChance)
                {
                    return false;
                }
                var numberWithoutCurrent = new List<int>(numbers);
                var numberWithoutNext = new List<int>(numbers);
                numberWithoutCurrent.RemoveAt(i);
                numberWithoutNext.RemoveAt(i + 1);
                return IsValidRow(numberWithoutCurrent, true) || IsValidRow(numberWithoutNext, true);
            }
            else if (Math.Abs(numbers[i] - numbers[i + 1]) < 1)
            {
                if (secondChance)
                {
                    return false;
                }
                var numberWithoutCurrent = new List<int>(numbers);
                var numberWithoutNext = new List<int>(numbers);
                numberWithoutCurrent.RemoveAt(i);
                numberWithoutNext.RemoveAt(i + 1);
                return IsValidRow(numberWithoutCurrent, true) || IsValidRow(numberWithoutNext, true);
            }
        }
        return true;
    }
    private static bool IsDescendingOrAscending(List<int> numbers)
    {
        bool isDescending = numbers.First() > numbers.Last();
        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (isDescending)
            {
                if (numbers[i] < numbers[i + 1])
                {
                    return false;
                }
            }
            else
            {
                if (numbers[i] > numbers[i + 1])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static bool IsDescendingOrAscendingLinq(List<int> numbers)
    {
        return numbers.SequenceEqual(numbers.OrderBy(x => x)) || numbers.SequenceEqual(numbers.OrderByDescending(x => x));
    }

    private static bool IsValidSequence(List<int> numbers)
    {
        for (int i = 0; i < numbers.Count - 1; i++)
        {
            if (Math.Abs(numbers[i] - numbers[i + 1]) > 3)
            {
                return false;
            }
            else if (Math.Abs(numbers[i] - numbers[i + 1]) < 1)
            {
                return false;
            }
        }
        return true;
    }


}