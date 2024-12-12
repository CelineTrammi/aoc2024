
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime;

public class Day9
{
    public static (long?, long?) Result()
    {
        // Setup
        var data = File.ReadAllText("/Users/celinetrammi/Documents/Personal/AdventOfCode/AdventOfCode2024/data/9.txt");
        // data = "233313312141413140292";
        // data = "2333133121414131402";
        List<string> edited = Decrypt(data);
        string editedAsString = string.Join("", edited);
        File.WriteAllText("/Users/celinetrammi/Documents/Personal/AdventOfCode/AdventOfCode2024/data/9edited.txt", editedAsString);

        Stopwatch sw1 = Stopwatch.StartNew();
        List<string> blocks1 = CreateBlocksStar1(edited);
        long? result1 = Calculate(blocks1); // 1645060967, too low. 8060750351457 too high
        sw1.Stop();
        Console.WriteLine($"Part 1: {sw1.ElapsedMilliseconds}ms");

        Stopwatch sw2 = Stopwatch.StartNew();
        List<string> blocks2 = CreateBlocksStar2(edited);
        long? result2 = Calculate(blocks2); // 419902
        sw2.Stop();
        Console.WriteLine($"Part 2: {sw2.ElapsedMilliseconds}ms");

        return (result1, result2);
    }
    private static long? Calculate(List<string> movedBlocks)
    {
        long sum = 0;
        for (int i = 0; i < movedBlocks.Count; i++)
        {
            if (movedBlocks[i] is ".") continue;
            sum += i * int.Parse(movedBlocks[i]);
        }
        return sum;
    }

    private static List<string> CreateBlocksStar1(List<string> data)
    {
        var elements = new List<string>(data);
        try
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var num = elements[i];
                if (num is not ".")
                {
                    continue;
                }
                string largestNumber = elements.Skip(i).Where(x => x != ".").OrderByDescending(int.Parse).First();
                int largestNumberIndex = elements.LastIndexOf(largestNumber);
                (elements[i], elements[largestNumberIndex]) = (elements[largestNumberIndex], elements[i]);

                // Swapping the dots with respect to length of the number
                /*
                List<string> elementsWithoutDots = elements.Skip(i).Where(x => x != ".").ToList();
                List<string> elementsWithoutDotsDescending = elementsWithoutDots.OrderByDescending(int.Parse).ToList();
                foreach (var item in elementsWithoutDotsDescending)
                {
                    var coverage = item.ToString().Length;
                    var possibleDots = elements[i..(i + coverage)];
                    if (possibleDots.All(x => x is "."))
                    {
                        elements.RemoveRange(i, coverage);
                        elements.Remove(item);
                        elements.Insert(i, item);
                        // i--;
                        break;
                    }
                }
                */
            }
        }
        catch (System.Exception)
        {
            return elements;
        }
        return elements;
    }
    private static List<string> CreateBlocksStar2(List<string> data)
    {
        // Wrong approach
        /*
        try
        {
            for (int i = 0; i < elements.Count; i++)
            {
                var num = elements[i];
                if (num is not ".")
                {
                    continue;
                }

                // string largestNumber = elements.Skip(i).Where(x => x != ".").OrderByDescending(int.Parse).First();
                // int firstIndexLargest = elements.IndexOf(largestNumber);
                // int lastIndexLargest = elements.LastIndexOf(largestNumber);
                // int amount = lastIndexLargest - firstIndexLargest + 1;
                // var possibleDots = elements[i..(i + amount)];
                // if (possibleDots.All(x => x is "."))
                // {
                //     SwapMultiple(elements, i, lastIndexLargest, amount);
                // }
            }
        }
        catch (System.Exception)
        {
            return elements;
        }
        */
        List<string> elements = new List<string>(data);
        List<string> elementsDescendingWithoutDots = elements.Where(x => x != ".").OrderByDescending(int.Parse).ToList();
        int largestFileId = int.Parse(elementsDescendingWithoutDots.First());
        for (int number = largestFileId; number >= 0; number--)
        {
            int amount = elements.Count(x => x == number.ToString());
            for (int index = 0; index < elements.Count - amount; index++)
            {
                if (elements[index] is not ".") continue;
                int firstIndexLargest = elements.IndexOf(number.ToString());
                int lastIndexLargest = elements.LastIndexOf(number.ToString());
                var a = elements.Skip(index).Take(amount).ToList();
                if (a.All(x => x is "."))
                {
                    if (a.Count != amount) break;
                    if (index > firstIndexLargest) break;
                    SwapMultiple(elements, index, lastIndexLargest, amount);
                    break;
                }
            }
        }
        return elements;
    }

    private static void SwapMultiple(List<string> elements, int currentIndex, int lastIndex, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var left = elements[currentIndex + 1];
            var right = elements[lastIndex - 1];
            (elements[currentIndex + i], elements[lastIndex - i]) = (elements[lastIndex - i], elements[currentIndex + i]);
        }
    }

    private static List<string> MoveBlocks(List<string> edited)
    {
        for (int i = edited.Count - 1; i >= 0; i--)
        {
            if (edited[i] is ".") continue;
            try
            {
                for (int j = 0; j < edited.Count - 1; j++)
                {
                    // hopefully correct
                    var swappableNumber = edited[i];
                    var numLength = swappableNumber.Length;
                    var possibleDots = edited[j..(j + numLength)];
                    if (edited[j..(j + numLength)].All(x => x is "."))
                    {
                        (edited[j], edited[i]) = (edited[i], edited[j]); // swap
                        edited.RemoveRange(j + 1, numLength - 1); // remove dots
                        i -= numLength - 1; // skip the dots
                        break;
                    }
                }
            }
            catch (System.Exception)
            {
                continue;
            }

        }
        return edited;
    }
    private static string SwapChars(string input, int index1, int index2)
    {
        char[] charArray = input.ToCharArray();
        char temp = charArray[index1];
        charArray[index1] = charArray[index2];
        charArray[index2] = temp;
        return new string(charArray);
    }

    private static List<string> Decrypt(string data)
    {
        int startID = 0;
        bool file = true;
        List<string> decryptedList = new();
        foreach (char c in data)
        {
            int num = int.Parse(c.ToString());
            if (file)
            {
                // decrypted += string.Concat(Enumerable.Repeat(startID.ToString(), num));
                decryptedList.AddRange(Enumerable.Repeat(startID.ToString(), num));
                startID++;
                file = false;
            }
            else
            {
                decryptedList.AddRange(Enumerable.Repeat(".", num));
                // decrypted += string.Concat(Enumerable.Repeat('.', num));
                file = true;
            }
        }
        return decryptedList;
    }

    private static int? Calculate2(string[] horizontal)
    {
        throw new NotImplementedException();
    }

}