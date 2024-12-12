using System.Text.RegularExpressions;
public class Day3
{
    public static (int?, int?) Result()
    {
        // Setup
        var data = File.ReadAllText("data/3.txt");
        int? result1 = Calculate1(data);
        int? result2 = Calculate2(data);
        return (result1, result2);
    }
    public static int? Calculate1(string data)
    {
        Regex mulPattern = new(@"mul\(\d{1,3},\d{1,3}\)");
        var mulMatches = mulPattern.Matches(data);
        int sum = mulMatches.Sum(hit => GetProductFromMul(hit.Value));
        return sum;
    }
    public static int? Calculate2(string data)
    {
        Regex mulPattern = new(@"mul\(\d{1,3},\d{1,3}\)");
        Regex doPattern = new(@"do\(\)");
        Regex dontPattern = new(@"don\'t\(\)");
        var mulMatches = mulPattern.Matches(data);
        var doMatches = doPattern.Matches(data);
        var dontMatches = dontPattern.Matches(data);
        var joinedMatches = mulMatches.Concat(doMatches).Concat(dontMatches).OrderBy(match => match.Index);
        bool deactivate = false;
        int sum = 0;
        foreach(var match in joinedMatches)
        {
            if (dontPattern.IsMatch(match.Value))
            {
                deactivate = true;
                continue;
            }
            else if (doPattern.IsMatch(match.Value))
            {
                deactivate = false;
                continue;
            }
            if (deactivate) continue;
            sum += GetProductFromMul(match.Value);
        }
        return sum;
    }

    /// <summary>
    /// Get the product of the two numbers in the mul() function
    /// </summary>
    private static int GetProductFromMul(string hit)
    {
        Regex numberPattern = new(@"\d{1,3}");
        var numbers = numberPattern.Matches(hit);
        int number1 = int.Parse(numbers.First().Value);
        int number2 = int.Parse(numbers.Last().Value);
        return number1 * number2;
    }
}