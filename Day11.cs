namespace AdventOfCode;


public record Node(string Name, string[] Connections) { }

public class Day11 : BaseDay
{
  private readonly Dictionary<string, string[]> nodes = [];
  public Dictionary<(string, bool, bool), long> memo = [];

  public Day11()
  {
    foreach (var line in File.ReadLines(InputFilePath))
    {
      if (string.IsNullOrWhiteSpace(line))
        continue;

      string name = line[..3];

      string[] connections = line[4..]
          .Split(' ', StringSplitOptions.RemoveEmptyEntries);

      nodes[name] = connections;
    }
  }

  private long Dfs(string current, string target, Dictionary<string, string[]> graph, bool part2, bool seenDac = false, bool seenFft = false)
  {
    if (current == target)
    {
      if (part2)
        return (seenDac && seenFft) ? 1 : 0;
      else
        return 1;
    }

    var key = (current, seenDac, seenFft);
    if (memo.TryGetValue(key, out long cached))
      return cached;

    bool nextSeenDac = seenDac || (current == "dac");
    bool nextSeenFft = seenFft || (current == "fft");

    long count = 0;

    foreach (var next in graph[current])
    {
      count += Dfs(next, target, graph, part2, nextSeenDac, nextSeenFft);
    }

    memo[key] = count;
    return count;
  }

  public override ValueTask<string> Solve_1()
  {
    memo = [];
    long paths = Dfs("you", "out", nodes, false);
    return new(paths.ToString());
  }

  public override ValueTask<string> Solve_2()
  {
    memo = [];
    long paths = Dfs("svr", "out", nodes, true);
    return new(paths.ToString());
  }
}
