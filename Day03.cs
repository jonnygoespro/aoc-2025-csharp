namespace AdventOfCode;

public class Day03 : BaseDay
{
  private readonly List<List<int>> banks = [];

  public Day03()
  {
    string input = File.ReadAllText(InputFilePath);
    string[] lines = input.Split([Environment.NewLine], StringSplitOptions.None);

    foreach (var line in lines)
    {
      if (string.IsNullOrWhiteSpace(line))
        continue;

      banks.Add([.. line.Select(ch => ch - '0')]);
    }
  }

  private static long CalcJoltage(List<int> bank, int switchCount)
  {
    long total = 0;
    List<int> slice = [.. bank.Take(bank.Count - (switchCount - 1))];

    for (int pos = switchCount - 1; pos >= 0; pos--)
    {
      int max = slice.Max();

      if (pos != 0)
      {
        int maxIndex = slice.IndexOf(max);
        slice = [.. slice.Skip(maxIndex + 1)];
        slice.Add(bank[^pos]);
      }

      total = total * 10 + max; 
    }

    return total;
  }

  public override ValueTask<string> Solve_1()
  {
    long result = banks.Sum(b => CalcJoltage(b, 2));
    return new(result.ToString());
  }

  public override ValueTask<string> Solve_2()
  {
    long result = banks.Sum(b => CalcJoltage(b, 12));
    return new(result.ToString());
  }
}
