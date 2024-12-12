using System.Numerics;
public class Day7
{
    public static (long?, long?) Result()
    {
        var data = File.ReadAllLines("/Users/celinetrammi/Documents/Personal/AdventOfCode/AdventOfCode2024/data/7.txt");
        // var data = File.ReadAllLines("data/7.txt");

        // long? result1 = Calculate1(data);
        long? result2 = Calculate2(data);
        return (null, result2);
    }

    private static long? Calculate2(string[] data)
    {
        List<long> validSums = new();
        foreach (var line in data)
        {
            var parts = line.Split(": ");
            long sum = long.Parse(parts.First());
            int[] numbers = parts.Last().Split(" ").Select(int.Parse).ToArray();
            bool valid = CheckSumWithNumbers2(sum, numbers);
            if (valid)
            {
                validSums.Add(sum);
            }
        }
        return validSums.Sum();
    }

    private static long? Calculate1(string[] data)
    {
        List<long> validSums = new();
        foreach (var line in data)
        {
            var parts = line.Split(": ");
            long sum = long.Parse(parts.First());
            int[] numbers = parts.Last().Split(" ").Select(int.Parse).ToArray();
            bool valid = CheckSumWithNumbers(sum, numbers);
            if (valid)
            {
                validSums.Add(sum);
            }
        }
        return validSums.Sum();
    }

    private static bool CheckSumWithNumbers2(long sum, int[] numbers)
    {
        int maxCombinations = (int)Math.Pow(3, numbers.Length - 1);
        for (int i = 0; i < maxCombinations; i++)
        {
            string combination = CovertToBase3(i, numbers.Length - 1);
            List<char> operations = combination.ToList();
            BigInteger result = CalculateWithThree(sum, numbers, operations);
            if (result == sum)
            {
                return true;
            };
        }
        return false;
    }

    private static string CovertToBase3(int number, int length)
    {
        string result = "";
        while (number > 0)
        {
            result = (number % 3).ToString() + result;
            number /= 3;
        }
        return result.PadLeft(length, '0');
    }

    private static BigInteger CalculateWithThree(long target, int[] numbers, List<char> operations)
    {
        BigInteger currentSum = 0;
        foreach (var (number, index) in numbers.Select((value, i) => (value, i)))
        {
            if (currentSum > target)
            {
                return -1;
            }
            if (index == 0)
            {
                currentSum = number;
                continue;
            }
            if (operations[index - 1] == '0')
            {
                string currentSumString = currentSum.ToString();
                string numberString = number.ToString();
                string combined = currentSumString + numberString;
                currentSum = long.Parse(combined);
            }
            else if (operations[index - 1] == '1')
            {
                currentSum *= number;
            }
            else if (operations[index - 1] == '2')
            {
                currentSum += number;
            }
        }
        return currentSum;
    }

    private static bool CheckSumWithNumbers(long sum, int[] numbers)
    {
        int maxCombinations = (int)Math.Pow(2, numbers.Length - 1);
        for (int i = 0; i < maxCombinations; i++)
        {
            string combination = Convert.ToString(i, 2).PadLeft(numbers.Length - 1, '0');
            List<bool> multiply = combination.Select(c => c == '0').ToList();
            long result = Calculate(sum, numbers, multiply);
            if (result == sum)
            {
                return true;
            };
        }
        return false;
    }

    private static long Calculate(long target, int[] numbers, List<bool> multiply)
    {
        long currentSum = 0;
        foreach (var (number, index) in numbers.Select((value, i) => (value, i)))
        {
            if (currentSum > target)
            {
                return -1;
            }
            if (index == 0)
            {
                currentSum = number;
                continue;
            }
            if (multiply[index - 1])
            {
                currentSum *= number;
            }
            else
            {
                currentSum += number;
            }
        }
        return currentSum;
    }
}