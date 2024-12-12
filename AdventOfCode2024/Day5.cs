
using System.Data;
public class Day5
{
    public static (int?, int?) Result()
    {
        // Setup
        var data = File.ReadAllLines("data/5.txt");
        List<string> rules = new();
        List<string> updates = new();
        foreach (var line in data)
        {
            if (line.Contains('|'))
                rules.Add(line);
            else if (line.Contains(','))
                updates.Add(line);
        }
        return CalculateEasy(rules, updates);
    }
    private static (int?, int?) CalculateEasy(List<string> rules, List<string> updates)
    {
        int sum1 = 0;
        List<string> successfulUpdates = new();
        List<string> failedUpdates = new();
        foreach (var update in updates)
        {
            if (ValidUpdate(update, rules))
            {
                sum1 += GetMiddleValue(update);
                successfulUpdates.Add(update);
            }
            else
            {
                failedUpdates.Add(update);
            }
        }

        // Part 2
        int sum2 = 0;
        foreach (var failedUpdate in failedUpdates)
        {
            List<string> numbers = failedUpdate.Split(',').ToList();
            List<string> correctOrder = SortNumbers(numbers, rules);
            string correctUpdate = string.Join(',', correctOrder);
            sum2 += GetMiddleValue(correctUpdate);
        }
        return (sum1, sum2);
    }

    private static List<string> GetRelevantRules(List<string> rules, List<string> numbers, List<string> descendingOrder)
    {
        List<string> relevantRules = new();
        foreach (var rule in rules)
        {
            var first = GetFirstInRule(rule);
            var last = GetLastInRule(rule);
            if (descendingOrder.Contains(first) || descendingOrder.Contains(last)) // ignore if already in order
            {
                continue;
            }
            if (numbers.Contains(first) && numbers.Contains(last))
            {
                relevantRules.Add(rule);
            }
        }
        return relevantRules;
    }

    private static List<string> SortNumbers(List<string> numbers, List<string> rules)
    {
        List<string> descendingOrder = new();
        for (int i = 0; i < numbers.Count; i++)
        {
            List<string> relevantRules = GetRelevantRules(rules, numbers, descendingOrder);
            var greatest = FindGreatest(numbers, relevantRules, descendingOrder) ?? throw new Exception("No greatest found");
            descendingOrder.Add(greatest);
        }
        return descendingOrder;
    }
    private static string? FindGreatest(List<string> numbers, List<string> rules, List<string> completed)
    {
        foreach (var num in numbers)
        {
            if (completed.Contains(num)) continue;

            List<string> largerThan = rules.Where(rule => GetLastInRule(rule) == num).ToList();
            List<string> smallerThan = rules.Where(rule => GetFirstInRule(rule) == num).ToList();
            if (smallerThan.Count == 0) // greatest
            {
                return num;
            }
        }
        return null;
    }

    private static bool ValidUpdate(string update, List<string> rules)
    {
        List<int> nums = update.Split(',').Select(int.Parse).ToList();
        List<int> prevNums = new();
        foreach (var num in nums)
        {
            foreach (var prev in prevNums)
            {
                var rule = $"{num}|{prev}";
                if (rules.Contains(rule))
                {
                    return false;
                }
            }
            prevNums.Add(num);
        }
        return true;
    }
    private static int GetMiddleValue(string update)
    {
        List<int> nums = update.Split(',').Select(int.Parse).ToList();
        return nums[nums.Count / 2];
    }

    private static string GetFirstInRule(string rule)
    {
        return rule.Split('|').First();
    }
    private static string GetLastInRule(string rule)
    {
        return rule.Split('|').Last();
    }
}