public class Day4
{
    public static (int?, int?) Result()
    {
        // Setup
        var horizontal = File.ReadAllLines("data/4.txt");
        int? result1 = Calculate1(horizontal); // 2260, for lavt
        int? result2 = Calculate2(horizontal);
        return (result1, result2);
    }

    private static int? Calculate2(string[] horizontal)
    {
        int sum = 0;
        for (int line = 0; line < horizontal.Length; line++)
        {
            for (int letter = 0; letter < horizontal[line].Length; letter++)
            {
                if (horizontal[line][letter] != 'A') continue;
                if (IsX(horizontal, line, letter)) sum++;
            }
        }
        return sum;
    }

    private static bool IsX(string[] horizontal, int line, int letter)
    {
        List<char> blacklisted = ['X', 'A'];
        try
        {
            char upperLeft = horizontal[line - 1][letter - 1];
            char lowerRight = horizontal[line + 1][letter + 1];
            char upperRight = horizontal[line - 1][letter + 1];
            char lowerLeft = horizontal[line + 1][letter - 1];
            if (blacklisted.Contains(upperRight)
                || blacklisted.Contains(lowerRight)
                || blacklisted.Contains(upperLeft)
                || blacklisted.Contains(lowerLeft)) return false;
            else if (upperLeft == lowerRight) return false;
            else if (upperRight == lowerLeft) return false;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static int? Calculate1(string[] horizontal)
    {
        int sum = 0;
        for (int line = 0; line < horizontal.Length; line++)
        {
            for (int letter = 0; letter < horizontal[line].Length; letter++)
            {
                if (horizontal[line][letter] != 'X') continue;
                if (IsMatchHorizontalForward(horizontal, line, letter)) sum++;
                if (IsMatchHorizontalBackward(horizontal, line, letter)) sum++;
                if (IsMatchVerticalDown(horizontal, line, letter)) sum++;
                if (IsMatchVerticalUp(horizontal, line, letter)) sum++;
                if (IsMatchDiagonalDownRight(horizontal, line, letter)) sum++;
                if (IsMatchDiagonalDownLeft(horizontal, line, letter)) sum++;
                if (IsMatchDiagonalUpLeft(horizontal, line, letter)) sum++;
                if (IsMatchDiagonalUpRight(horizontal, line, letter)) sum++;
            }
        }
        return sum;
    }

    private static bool IsMatchDiagonalUpRight(string[] horizontal, int line, int letter)
    {
        // if (line < 3 || letter > horizontal[line].Length - 3) return false;
        try
        {
            return horizontal[line - 1][letter + 1] == 'M' &&
                horizontal[line - 2][letter + 2] == 'A' &&
                horizontal[line - 3][letter + 3] == 'S';
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool IsMatchDiagonalUpLeft(string[] horizontal, int line, int letter)
    {
        // if (line < 3 || letter < 3) return false;
        try
        {
            return horizontal[line - 1][letter - 1] == 'M' &&
                horizontal[line - 2][letter - 2] == 'A' &&
                horizontal[line - 3][letter - 3] == 'S';
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool IsMatchDiagonalDownLeft(string[] horizontal, int line, int letter)
    {
        // if (line > horizontal.Length - 3 || letter < 3) return false;
        try
        {
            return horizontal[line + 1][letter - 1] == 'M' &&
                horizontal[line + 2][letter - 2] == 'A' &&
                horizontal[line + 3][letter - 3] == 'S';
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool IsMatchDiagonalDownRight(string[] horizontal, int line, int letter)
    {
        // if (line > horizontal.Length - 3 || letter > horizontal[line].Length - 3) return false;
        try
        {
            return horizontal[line + 1][letter + 1] == 'M' &&
                horizontal[line + 2][letter + 2] == 'A' &&
                horizontal[line + 3][letter + 3] == 'S';
        }
        catch (Exception)
        {
            return false;
        }
    }
    private static bool IsMatchVerticalUp(string[] horizontal, int line, int letter)
    {
        // if (line < 3) return false;
        try
        {
            return horizontal[line - 1][letter] == 'M' &&
                horizontal[line - 2][letter] == 'A' &&
                horizontal[line - 3][letter] == 'S';
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static bool IsMatchVerticalDown(string[] horizontal, int line, int letter)
    {
        // if (line > horizontal.Length - 3) return false;
        try
        {
            return horizontal[line + 1][letter] == 'M' &&
                horizontal[line + 2][letter] == 'A' &&
                horizontal[line + 3][letter] == 'S';
        }
        catch (Exception)
        {
            return false;
        }
    }
    private static bool IsMatchHorizontalBackward(string[] horizontal, int line, int letter)
    {
        if (letter < 3) return false;
        return horizontal[line][letter - 1] == 'M' &&
            horizontal[line][letter - 2] == 'A' &&
            horizontal[line][letter - 3] == 'S';
    }

    private static bool IsMatchHorizontalForward(string[] horizontal, int line, int letter)
    {
        if (letter > horizontal.Length - 3) return false;
        return horizontal[line][letter + 1] == 'M' &&
            horizontal[line][letter + 2] == 'A' &&
            horizontal[line][letter + 3] == 'S';
    }
}