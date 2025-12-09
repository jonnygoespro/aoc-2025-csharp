namespace AdventOfCode;

public record Position(long X, long Y)
{
  public long GetRectangleArea(Position other)
  {
    long width = Math.Abs(other.X - X) + 1;
    long height = Math.Abs(other.Y - Y) + 1;
    return width * height;
  }
}

public record Line(Position From, Position To)
{
  public bool Intersects(Position Pos1, Position Pos2)
  {
    long minX = Math.Min(Pos1.X, Pos2.X);
    long maxX = Math.Max(Pos1.X, Pos2.X);
    long minY = Math.Min(Pos1.Y, Pos2.Y);
    long maxY = Math.Max(Pos1.Y, Pos2.Y);
    long lineMinX = Math.Min(From.X, To.X);
    long lineMaxX = Math.Max(From.X, To.X);
    long lineMinY = Math.Min(From.Y, To.Y);
    long lineMaxY = Math.Max(From.Y, To.Y);

    return lineMaxX > minX && lineMinX < maxX && lineMaxY > minY && lineMinY < maxY;
  }
}

public class Day09 : BaseDay
{
  private readonly Position[] positions;

  public Day09()
  {
    positions = File.ReadAllLines(InputFilePath)
      .Where(l => l.Length > 0)
      .Select(l => l.Split(','))
      .Select(p => new Position(long.Parse(p[0]), long.Parse(p[1])))
      .ToArray();
  }

  public override ValueTask<string> Solve_1()
  {
    long biggestArea = 0;
    for (int i = 0; i < positions.Length - 1; i++)
    {
      for (int j = i + 1; j < positions.Length; j++)
      {
        long area = positions[i].GetRectangleArea(positions[j]);
        if (area > biggestArea)
          biggestArea = area;
      }
    }

    return new(biggestArea.ToString());
  }

  public override ValueTask<string> Solve_2()
  {
    Line[] lines = new Line[positions.Length];
    for (int i = 0; i < positions.Length; i++)
    {
      var start = positions[i];
      var end = positions[(i + 1) % positions.Length];
      lines[i] = new Line(start, end);
    }

    long biggestArea = 0;
    for (int i = 0; i < positions.Length - 1; i++)
    {
      for (int j = i + 1; j < positions.Length; j++)
      {
        long area = positions[i].GetRectangleArea(positions[j]);
        if (area > biggestArea && !lines.Any(l => l.Intersects(positions[i], positions[j])))
          biggestArea = area;
      }
    }

    return new(biggestArea.ToString());
  }
}
