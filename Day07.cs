namespace AdventOfCode;

public class Day07 : BaseDay
{
  private readonly string[] input;
  private readonly int width;
  private readonly Dictionary<(int col, int row), long> cache = [];

  public Day07()
  {
    input = [.. File.ReadLines(InputFilePath).Where(l => l.Length > 0)];
    width = input[0].Length;
  }

  public override ValueTask<string> Solve_1()
  {
    int result = 0;
    HashSet<int> beams = [input[0].IndexOf('S')];

    for (int row = 1; row < input.Length; row++)
    {
      var nextBeams = new HashSet<int>();
      var line = input[row];

      foreach (int col in Enumerable.Range(0, width))
      {
        if (!beams.Contains(col))
          continue;

        switch (line[col])
        {
          case '^':
            result++;
            if (col > 0) nextBeams.Add(col - 1);
            if (col < width - 1) nextBeams.Add(col + 1);
            break;
          default:
            nextBeams.Add(col);
            break;
        }
      }

      beams = nextBeams;
    }

    return new(result.ToString());
  }

  private long CountPaths(int col, int row)
  {
    if (row == input.Length - 1)
      return 1;

    if (cache.TryGetValue((col, row), out long cached))
      return cached;

    var line = input[row];
    long total = 0;

    void Add(int nextCol)
    {
      total += CountPaths(nextCol, row + 1);
    }

    if (line[col] == '^')
    {
      if (col > 0) Add(col - 1);
      if (col < width - 1) Add(col + 1);
    }
    else 
    {
      Add(col);
    }

    cache[(col, row)] = total;
    return total;
  }

  public override ValueTask<string> Solve_2()
  {
    long result = CountPaths(input[0].IndexOf('S'), 1);
    return new(result.ToString());
  }
}
