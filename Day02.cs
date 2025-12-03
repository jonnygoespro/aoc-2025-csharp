using System.Text.RegularExpressions;

namespace AdventOfCode;

public struct Range(long first, long last)
{
  public long first = first;
  public long last = last;
}

// Needs refactoring
public class Day02 : BaseDay
{
  private List<Range> ranges = [];

  public Day02()
  {
    string _input = File.ReadAllText(InputFilePath);
    string[] pairs = _input.Split(",");
    foreach (var pair in pairs)
    {
      string[] ids = pair.Split("-");
      ranges.Add(new Range(long.Parse(ids[0]), long.Parse(ids[1])));
    }
  }

  private bool IsInvalidId(long id)
  {
    string idString = id.ToString();
    if (idString.Length % 2 != 0)
    {
      return false;
    }

    int pivot = idString.Length / 2;
    string firstHalf = idString[0..pivot];
    string secondHalf = idString[pivot..];

    if (firstHalf == secondHalf)
    {
      return true;
    }

    return false;
  }

  public override ValueTask<string> Solve_1()
  {
    long result = 0;
    foreach (var range in ranges)
    {
      for (long i = range.first; i <= range.last; i++)
      {
        if (IsInvalidId(i))
        {
          result += i;
        }
      }
    }

    return new(result.ToString());
  }

  private static bool IsInvalidId2(long id)
  {
    string idString = id.ToString();
    for (int i = 1; i < idString.Length; i++)
    {
      if (idString.Length % i != 0)
      {
        continue;
      }

      string lookup = idString[0..i];
      int occurences = Regex.Count(idString, lookup);
      if (occurences == idString.Length / i)
      {
        return true;
      }
    }

    return false;
  }

  public override ValueTask<string> Solve_2()
  {
    long result = 0;
    foreach (var range in ranges)
    {
      for (long i = range.first; i <= range.last; i++)
      {
        if (IsInvalidId2(i))
        {
          result += i;
        }
      }
    }

    return new(result.ToString());
  }
}
