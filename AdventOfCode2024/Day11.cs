public class Day11
{
    public static (long?, int?) Result()
    {
        // Setup
        var data = File.ReadAllText("/Users/celinetrammi/Documents/Personal/AdventOfCode/AdventOfCode2024/data/11.txt");
        string[] stones = data.Split(" ");
        // stones = ["125", "17"];
        // stones = ["0"];
        int result1 = CalculateStar1(stones);
        // string lengths = string.Join(", ", result1);
        // File.WriteAllText("/Users/celinetrammi/Documents/Personal/AdventOfCode/AdventOfCode2024/data/11edited.txt", string.Join(" ", result1));
        int? result2 = CalculateStar2(stones);

        return ((long)result1, result2);
    }

    private static int? CalculateStar2(string[] data)
    {
        // int blinks = 75;
        // Dictionary<int, StoneSequenceData> stoneDict = new Dictionary<int, StoneSequenceData()
        // {
        //     {
        //         0, new StoneSequenceData()
        //         {
        //             odds = 3,
        //             evens = 0,
        //             zeros = 1
        //         }
        //     }
        // };

        // for (int blink = 0; blink < blinks; blink++)
        // {
        // }
        return 0;
    }
    private class StoneSequenceData
    {
        public int odds;
        public int evens;
        public int zeros;
    }

    private static int CalculateStar1(string[] data)
    {
        // If the stone is engraved with the number 0, 
        // it is replaced by a stone engraved with the number 1.

        // If the stone is engraved with a number that has an even number of digits, 
        // it is replaced by two stones. 
        // The left half of the digits are engraved on the new left stone, 
        // and the right half of the digits are engraved on the new right stone. 
        // (The new numbers don't keep extra leading zeroes: 
        // 1000 would become stones 10 and 0.)

        // If none of the other rules apply, the stone is replaced by a new stone; 
        // the old stone's number multiplied by 2024 is engraved on the new stone.
        int blinks = 25;
        List<long> lengths = new();
        Dictionary<int, List<string>> stoneDict = new Dictionary<int, List<string>>
        {
            { 0, data.ToList() }
        };
        
        for (int blink = 1; blink < blinks + 1; blink++ )
        {
            // Console.WriteLine(blink);
            List<string> newStones = new List<string>();
            foreach (var stone in stoneDict[blink - 1])
            {
                if (stone is "0")
                {
                    newStones.Add("1");
                }
                else if (stone.Length % 2 == 0)
                {
                    int half = stone.Length / 2;
                    newStones.Add(stone[..half]);
                    var secondHalf = stone[half..];
                    if (secondHalf.All(x => x == '0'))
                    {
                        newStones.Add("0");
                    }
                    else
                    {
                        newStones.Add(secondHalf.TrimStart('0'));
                    }
                }
                else
                {
                    newStones.Add((long.Parse(stone) * 2024).ToString());
                }
            }
            Console.WriteLine(newStones);
            stoneDict.Add(blink, newStones);
            lengths.Add(newStones.Count);
        }
        return stoneDict[blinks].Count;
    }
}