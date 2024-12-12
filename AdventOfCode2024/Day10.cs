
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class Day10
{
    public static (int?, int?) Result()
    {
        // Setup
        var data = File.ReadAllLines("/Users/celinetrammi/Documents/Personal/AdventOfCode/AdventOfCode2024/data/10.txt");
        int? result1 = CalculateStar1(data);
        int? result2 = CalculateStar2(data);

        return (result1, result2);
    }

    private static int? CalculateStar1(string[] data)
    {
        Dictionary<Position, List<Position>> trails = new();
        for (int row = 0; row < data.Length; row++)
        {
            for (int col = 0; col < data[row].Length; col++)
            {
                if (data[row][col] != '9') continue;
                FindTrail(data, row, col, data[row][col], trails);
            }
        }
        return 0;
    }

    private static void FindTrail(string[] data, int startRow, int startCol, char num, Dictionary<Position, List<Position>> trails)
    {
        Position startPosition = new Position { row = startRow, col = startCol };
        int currentRow = startRow;
        int currentCol = startCol;

        List<string> descendingNearby = DescendingNearby(data, currentRow, currentCol, num);
        if (descendingNearby.Count == 0)
        {
            return;
        }
        if (descendingNearby.Count == 1)
        {
            var direction = descendingNearby.First();
            Search(data, startPosition, trails, direction, num);
        }
        else if (descendingNearby.Count == 2)
        {

        }
        else if (descendingNearby.Count == 3)
        {

        }
    }
    private static void Search(string[] data, Position currentPosition, Dictionary<Position, List<Position>> trails, string direction, char num)
    {
        while (num is not '0')
        {
            switch (direction)
            {
                case "above":
                    currentPosition.row--;
                    break;
                case "below":
                    currentPosition.row++;
                    break;
                case "left":
                    currentPosition.col--;
                    break;
                case "right":
                    currentPosition.col++;
                    break;
            }
            if (trails.ContainsKey(currentPosition))
            {
                trails[currentPosition].Add(new Position { row = currentPosition.row, col = currentPosition.col });
            }
            else
            {
                trails.Add(currentPosition, new List<Position> { new Position { row = currentPosition.row, col = currentPosition.col } });
            }
            trails[currentPosition].Add(new Position { row = currentPosition.row, col = currentPosition.col });
            List<string> descendingNearby = DescendingNearby(data, currentPosition.row, currentPosition.col, num);
            if (descendingNearby.Count == 0)
            {
                return;
            }
            else if (descendingNearby.Count == 1)
            {
                Search(data, currentPosition, trails, direction, num);
            }
        }
    }

    private static List<string> DescendingNearby(string[] data, int row, int col, char numAsChar)
    {
        int num = int.Parse(numAsChar.ToString());
        char descending = (num - 1).ToString().First();
        List<string> directions = new List<string>();
        
        
        try
        {
            if (data[row - 1][col] == descending) directions.Add("above");
        }
        catch (IndexOutOfRangeException)
        {
        }
        try
        {
            if (data[row + 1][col] == descending) directions.Add("below");
        }
        catch (IndexOutOfRangeException)
        {
        }
        try
        {
            if (data[row][col - 1] == descending) directions.Add("left");
        }
        catch (IndexOutOfRangeException)
        {
        }
        try
        {
            if (data[row][col + 1] == descending) directions.Add("right");
        }
        catch (IndexOutOfRangeException)
        {
        }
        return directions;
    }

    private static int? CalculateStar2(string[] data)
    {
        throw new NotImplementedException();
    }
}