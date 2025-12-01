namespace AdventOfCode;

enum Direction
{
  Left,
  Right
}

struct Turn(Direction dir, int amount)
{
  public Direction dir = dir;
  public int amount = amount;
}

public class Day01 : BaseDay
{
  private readonly List<Turn> turns = [];
  private int dial = 50;

  public Day01()
  {
    string input = File.ReadAllText(InputFilePath);
    string[] lines = input.Split([Environment.NewLine], StringSplitOptions.None);

    foreach (var line in lines)
    {
      if (line == "")
        return;

      Direction dir = line[0] == 'L' ? Direction.Left : Direction.Right;
      int amount = int.Parse(line[1..]);
      turns.Add(new Turn(dir, amount));
    }
  }

  public override ValueTask<string> Solve_1()
  {
    dial = 50;

    int result = 0;
    foreach (var turn in turns)
    {
      (int exactZero, int _) = Rotate(turn);
      result += exactZero;
    }

    return new(result.ToString());
  }

  public override ValueTask<string> Solve_2()
  {
    dial = 50;

    int result = 0;
    foreach (var turn in turns)
    {
      (int _, int passedZero) = Rotate(turn);
      result += passedZero;
    }

    return new(result.ToString());
  }

  private (int exactZero, int passedZero) Rotate(Turn turn)
  {
    int exactZero = 0;
    int passedZero = 0;

    var step = turn.dir == Direction.Left ? -1 : 1;
    for (var i = 0; i < turn.amount; i++)
    {
      dial = (dial + step + 100) % 100;
      if (dial == 0)
      {
        passedZero++;
      }
    }

    if (dial == 0)
    {
      exactZero++;
    }

    return (exactZero, passedZero);
  }
}
