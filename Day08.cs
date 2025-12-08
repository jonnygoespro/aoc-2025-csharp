namespace AdventOfCode;

public record Coordinate(int X, int Y, int Z)
{
  public double GetDistance(Coordinate other)
  {
    return Math.Sqrt(Math.Pow(other.X - X, 2) + Math.Pow(other.Y - Y, 2) + Math.Pow(other.Z - Z, 2));
  }
}

public record Distance(int FirstIndex, int SecondIndex, double EucildeanDistance)
{ }

public class Day08 : BaseDay
{
  private readonly Coordinate[] coords;
  private readonly List<Distance> distances = [];
  private readonly int shortestConnectionCount = 1000;

  public Day08()
  {
    coords = File.ReadAllLines(InputFilePath)
      .Where(l => l.Length > 0)
      .Select(l => l.Split(','))
      .Select(p => new Coordinate(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2])))
      .ToArray();

    for (int i = 0; i < coords.Length - 1; i++)
    {
      for (int j = i + 1; j < coords.Length; j++)
      {
        distances.Add(new Distance(i, j, coords[i].GetDistance(coords[j])));
      }
    }

    distances.Sort((a, b) => a.EucildeanDistance.CompareTo(b.EucildeanDistance));
  }

  public override ValueTask<string> Solve_1()
  {
    List<HashSet<int>> circuits = [];

    int connectionsMade = 0;
    int i = 0;
    while (connectionsMade < shortestConnectionCount)
    {
      var first = distances[i].FirstIndex;
      var second = distances[i].SecondIndex;
      var foundCircuits = circuits.FindAll(list => list.Contains(first) || list.Contains(second));

      if (foundCircuits.Count == 1)
      {
        if (!foundCircuits[0].Contains(first) || !foundCircuits[0].Contains(second))
        {
          foundCircuits[0].Add(first);
          foundCircuits[0].Add(second);
        }
        connectionsMade++;
      }
      else if (foundCircuits.Count > 1)
      {
        // merge
        foreach (var elem in foundCircuits[1])
        {
          foundCircuits[0].Add(elem);
        }
        foundCircuits[0].Add(first);
        foundCircuits[0].Add(second);
        circuits.Remove(foundCircuits[1]);
        connectionsMade++;
      }
      else
      {
        circuits.Add([first, second]);
        connectionsMade++;
      }

      i++;
    }

    var orderedCircuits = circuits.OrderBy(c => c.Count).Reverse().ToArray();
    int result = orderedCircuits[0].Count * orderedCircuits[1].Count * orderedCircuits[2].Count;

    return new(result.ToString());
  }

  public override ValueTask<string> Solve_2()
  {
    List<HashSet<int>> circuits = [];

    int connectionsMade = 0;
    int i = 0;
    int first = 0;
    int second = 0;
    while (circuits.Count == 0 || circuits[0].Count < coords.Length)
    {
      first = distances[i].FirstIndex;
      second = distances[i].SecondIndex;
      var foundCircuits = circuits.FindAll(list => list.Contains(first) || list.Contains(second));

      if (foundCircuits.Count == 1)
      {
        if (!foundCircuits[0].Contains(first) || !foundCircuits[0].Contains(second))
        {
          foundCircuits[0].Add(first);
          foundCircuits[0].Add(second);
        }
        connectionsMade++;
      }
      else if (foundCircuits.Count > 1)
      {
        // merge
        foreach (var elem in foundCircuits[1])
        {
          foundCircuits[0].Add(elem);
        }
        foundCircuits[0].Add(first);
        foundCircuits[0].Add(second);
        circuits.Remove(foundCircuits[1]);
        connectionsMade++;
      }
      else
      {
        circuits.Add([first, second]);
        connectionsMade++;
      }

      i++;
    }

    long result = (long)coords[first].X * coords[second].X;
    return new(result.ToString());
  }
}
