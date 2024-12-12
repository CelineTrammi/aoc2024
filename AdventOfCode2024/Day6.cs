
using System.Diagnostics;
public class Day6
{
    public static (int?, int?) Result()
    {
        var map = File.ReadAllLines("data/6.txt");
        Position p = new();
        List<Position> hashes = new();
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[row].Length; col++)
            {
                if (map[row][col] == '^')
                {
                    p.col = col;
                    p.row = row;
                }
                else if (map[row][col] == '#')
                {
                    hashes.Add(new() { col = col, row = row });
                }
            }
        }
        Stopwatch sw1 = Stopwatch.StartNew();
        int result1 = Calculate1(p, map, out List<Position> visited);
        sw1.Stop();
        Console.WriteLine($"Part 1: {sw1.ElapsedMilliseconds}ms");
        Stopwatch sw2 = Stopwatch.StartNew();
        int result2 = Calculate2(p, map, visited);
        sw2.Stop();
        Console.WriteLine($"Part 2: {sw2.ElapsedMilliseconds}ms");

        return (result1, result2);
    }

    private static int Calculate2(Position p, string[] map, List<Position> visited)
    {
        List<Position> successFullObstructions = new List<Position>();
        Position startPosition = new Position { col = p.col, row = p.row };
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[row].Length; col++)
            {
                if (map[row][col] == '#' || (row == startPosition.row && col == startPosition.col)) // impossible to place obstruction
                {
                    continue;
                }
                if (visited.Any(p => p.col == col && p.row == row)) // possible obstacles
                {
                    Position obstruction = new Position { col = col, row = row };
                    var c = Calculate1(startPosition, map, out var a, obstruction);
                    if (c == -1)
                    {
                        successFullObstructions.Add(obstruction);
                    }
                }
            }
        }
        return successFullObstructions.Count;
    }

    private static int Calculate1(Position startPosition, string[] map, out List<Position> visited, Position? obstruction = null)
    {
        var dir = "up";
        var p = new Position { col = startPosition.col, row = startPosition.row };
        visited = new List<Position> { new Position { col = p.col, row = p.row } };
        List<PositionWithDir> visitedWithDir = new List<PositionWithDir> { new PositionWithDir { col = p.col, row = p.row, dir = dir } };
        try
        {
            while (true)
            {
                switch (dir)
                {
                    case "up":
                        if (map[p.row - 1][p.col] == '#' || (obstruction?.row == p.row - 1 && obstruction?.col == p.col))
                        {
                            dir = "right";
                        }
                        else
                        {
                            p.row--;
                            visited.Add(new() { col = p.col, row = p.row });
                            PositionWithDir pDir = new() { col = p.col, row = p.row, dir = dir };
                            if (visitedWithDir.Any(p => p.col == pDir.col && p.row == pDir.row && p.dir == pDir.dir))
                            {
                                return -1;
                            }
                            visitedWithDir.Add(pDir);
                        }
                        break;
                    case "right":
                        if (map[p.row][p.col + 1] == '#' || (obstruction?.row == p.row && obstruction?.col == p.col + 1))
                        {
                            dir = "down";
                        }
                        else
                        {
                            p.col++;
                            visited.Add(new() { col = p.col, row = p.row });
                            PositionWithDir pDir = new() { col = p.col, row = p.row, dir = dir };
                            if (visitedWithDir.Any(p => p.col == pDir.col && p.row == pDir.row && p.dir == pDir.dir))
                            {
                                return -1;
                            }
                            visitedWithDir.Add(pDir);
                        }
                        break;
                    case "down":
                        if (map[p.row + 1][p.col] == '#' || (obstruction?.row == p.row + 1 && obstruction?.col == p.col))
                        {
                            dir = "left";
                        }
                        else
                        {
                            p.row++;
                            visited.Add(new() { col = p.col, row = p.row });
                            PositionWithDir pDir = new() { col = p.col, row = p.row, dir = dir };
                            if (visitedWithDir.Any(p => p.col == pDir.col && p.row == pDir.row && p.dir == pDir.dir))
                            {
                                return -1;
                            }
                            visitedWithDir.Add(pDir);
                        }
                        break;
                    case "left":
                        if (map[p.row][p.col - 1] == '#' || (obstruction?.row == p.row && obstruction?.col == p.col - 1))
                        {
                            dir = "up";
                        }
                        else
                        {
                            p.col--;
                            visited.Add(new() { col = p.col, row = p.row });
                            PositionWithDir pDir = new() { col = p.col, row = p.row, dir = dir };
                            if (visitedWithDir.Any(p => p.col == pDir.col && p.row == pDir.row && p.dir == pDir.dir))
                            {
                                return -1;
                            }
                            visitedWithDir.Add(pDir);
                        }
                        break;
                }
            }
        }
        catch (Exception)
        {
        }
        var uniquePositions = new List<Position>();
        foreach (var visit in visited)
        {
            if (uniquePositions.Any(p => p.col == visit.col && p.row == visit.row))
            {
                continue;
            }
            uniquePositions.Add(visit);
        }
        return uniquePositions.Count;
    }
    private static bool IsInBounds(Position p, string[] map)
    {
        return p.col < map.Length && p.col > 0 && p.row < map.Length && p.row > 0;
    }
}
public class Position
{
    public int col;
    public int row;
}
public class PositionWithDir
{
    public int col;
    public int row;
    public string dir;
}