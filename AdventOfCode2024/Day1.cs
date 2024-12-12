public class Day1
{
    public static (int?, int?) Result()
    {
        // Setup
        var data = File.ReadAllLines("data/1.txt");
        var first = new List<int>();
        var second = new List<int>();
        foreach (var line in data)
        {
            List<int> numbers = line.Split("   ").Select(int.Parse).ToList();
            first.Add(numbers.First());
            second.Add(numbers.Last());
        }

        // First star
        int result1 = Calculate1(first, second);
        // Second star
        int result2 = Calculate2(first, second);
        return (result1, result2);
    }
    private static int Calculate1(List<int> firstLine, List<int> secondLine)
    {
        int sum = 0;
        firstLine.Sort();
        secondLine.Sort();
        for (int i = 0; i < firstLine.Count; i++)
        {
            int difference = Math.Abs(firstLine[i] - secondLine[i]);
            sum += difference;
        }
        // sum = firstLine.Zip(secondLine, (first, second) => Math.Abs(first - second)).Sum(); // holy moly
        return sum;
    }
    private static int Calculate2(List<int> firstLine, List<int> secondLine)
    {
        int sum = 0;
        foreach (var num in firstLine)
        {
            List<int> similarInSecond = secondLine.Where(x => x == num).ToList();
            sum += num * similarInSecond.Count;
        }
        // sum = firstLine.Sum(firstNum => secondLine.Count(secondNum => secondNum == firstNum) * firstNum); // nice
        return sum;
    }
}