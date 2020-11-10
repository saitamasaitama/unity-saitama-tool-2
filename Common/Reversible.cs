using System;

namespace ReverseWorld
{
  public interface Reversible
  {
    Reverser Reverse();
  }

  public abstract class Reverser : Reversible
  {
    public static void Main(string[] args)
    {
      Reverser r = new ADA();
      Console.WriteLine($"{r.Name()}沢直樹「{r.Reverse().Name()}返しだ！」");
    }

    public abstract string Name();
    public abstract Reverser Reverse();
    public override string ToString()
    {
      return Name();
    }
  }

  public class Num : Reverser
  {
    public float num;
    public Num(float num)
    {
      this.num = num;
    }

    public override string Name()
    {
      return $"{num:0.000}";
    }

    public override Reverser Reverse()
    {
      return new Num(num = 1.0f / num);
    }
  }

  public class ADA : Reverser
  {
    public override string Name() => "仇";
    public override Reverser Reverse() => new ON();
  }

  public class ON : Reverser
  {
    public override string Name() => "恩";

    public override Reverser Reverse() => new ADA();
  }
}
