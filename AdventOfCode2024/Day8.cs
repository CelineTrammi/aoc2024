public class Day8
{
    private static int rowLength;
    private static int colLength;
    public static (int?, int?) Result()
    {
        var data = File.ReadAllLines("/Users/celinetrammi/Documents/Personal/AdventOfCode/AdventOfCode2024/data/8.txt");
        rowLength = data[0].Length;
        colLength = data.Length;
        Dictionary<char, List<Coordinates>> pairs = new();
        for (int row = 0; row < data.Length; row++)
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                if (data[row][col] == '.') continue;
                if (!pairs.ContainsKey(data[row][col]))
                {
                    pairs.Add(data[row][col], new List<Coordinates> { new Coordinates { row = row, col = col } });
                }
                else
                {
                    pairs[data[row][col]].Add(new Coordinates { row = row, col = col });
                }
            }
        }
        int? result1 = Calculate1(pairs);
        int? result2 = Calculate2(pairs);
        return (result1, result2);
    }

    private static int? Calculate1(Dictionary<char, List<Coordinates>> pairs)
    {
        List<Coordinates> existingAntiNodes = new();
        foreach (var letter in pairs.Values)
        {
            for (int index = 0; index < letter.Count; index++)
            {
                for (int i = index; i < letter.Count; i++)
                {
                    if (i == index) continue;
                    var A = letter[index];
                    var B = letter[i];

                    int rowDist = A.row - B.row;
                    int colDist = A.col - B.col;
                    var B2A = new Coordinates { row = rowDist, col = colDist };
                    var A2B = new Coordinates { row = -rowDist, col = -colDist };
                    while (AntiNodeInRange(A, B2A, out Coordinates antiNodePosition))
                    {
                        existingAntiNodes.Add(antiNodePosition);
                        A = new Coordinates { row = antiNodePosition.row, col = antiNodePosition.col };
                    }
                    while (AntiNodeInRange(B, A2B, out Coordinates antiNodePosition2))
                    {
                        existingAntiNodes.Add(antiNodePosition2);
                        B = new Coordinates { row = antiNodePosition2.row, col = antiNodePosition2.col };
                    }
                    // if (AntiNodeInRange(A, B2A, out Coordinates antiNodePosition))
                    // {
                    //     existingAntiNodes.Add(antiNodePosition);
                    // }
                    // if (AntiNodeInRange(B, A2B, out Coordinates antiNodePosition2))
                    // {
                    //     existingAntiNodes.Add(antiNodePosition2);
                    // }
                }
            }
        }
        foreach (var pair in pairs)
        {
            foreach (var item in pair.Value)
            {
                existingAntiNodes.Add(item);
            }
        }
        var uniqueResult = new List<Coordinates>();
        foreach (var item in existingAntiNodes)
        {
            if (uniqueResult.Any(x => x.row == item.row && x.col == item.col)) continue;
            uniqueResult.Add(item);
        }
        // var letterNodes = pairs.Values.Select(x => x.Count).Sum();
        
        // var jakob = letterNodes + uniqueResult.Count;

        return uniqueResult.Count;
    }

    private static bool AntiNodeInRange(Coordinates pos, Coordinates vec, out Coordinates antiNodePosition)
    {
        antiNodePosition = new Coordinates { row = pos.row + vec.row, col = pos.col + vec.col };
        if (antiNodePosition.row < 0 || antiNodePosition.row >= rowLength) return false;
        if (antiNodePosition.col < 0 || antiNodePosition.col >= colLength) return false;
        return true;
    }

    private static int? Calculate2(Dictionary<char, List<Coordinates>> pairs)
    {
        return 0;
    }
    public class Coordinates
{
    public int row;
    public int col;
}
}