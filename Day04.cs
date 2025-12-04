namespace AdventOfCode;

public readonly record struct Neighbor(int X, int Y);

public class Day04 : BaseDay
{
  private readonly int[,] Rolls;
  private readonly int Rows;
  private readonly int Cols;

  private static readonly Neighbor[] Neighbors =
    [
        new(-1, -1), new(-1,  0), new(-1, 1),
        new( 0, -1),              new( 0, 1),
        new( 1, -1), new( 1,  0), new( 1, 1)
    ];

  public Day04()
  {
    var lines = File.ReadAllLines(InputFilePath)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .ToArray();

    Rows = lines.Length;
    Cols = lines[0].Length;
    Rolls = new int[Rows, Cols];

    for (int r = 0; r < Rows; r++)
      for (int c = 0; c < Cols; c++)
        Rolls[r, c] = lines[r][c] == '@' ? 1 : 0;
  }

  private bool InBounds(int r, int c)
  {
    return r >= 0 && r < Rows && c >= 0 && c < Cols;
  }

  private int CountNeighbors(int r, int c)
  {
    int count = 0;

    foreach (var (dx, dy) in Neighbors)
    {
      int nr = r + dx;
      int nc = c + dy;

      if (InBounds(nr, nc))
        count += Rolls[nr, nc];
    }

    return count;
  }

  public override ValueTask<string> Solve_1()
  {
    int result = 0;

    for (int r = 0; r < Rows; r++)
      for (int c = 0; c < Cols; c++)
        if (Rolls[r, c] == 1 && CountNeighbors(r, c) < 4)
          result++;

    return ValueTask.FromResult(result.ToString());
  }

  public override ValueTask<string> Solve_2()
  {
    int result = 0;
    bool changed;

    do
    {
      changed = false;

      for (int r = 0; r < Rows; r++)
        for (int c = 0; c < Cols; c++)
          if (Rolls[r, c] == 1 && CountNeighbors(r, c) < 4)
          {
            Rolls[r, c] = 0;
            result++;
            changed = true;
          }
    } while (changed);

    return ValueTask.FromResult(result.ToString());
  }
}
