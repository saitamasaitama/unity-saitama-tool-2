using System.Collections;
using System.Collections.Generic;

namespace System.Collections.Generic
{
  public class RingItem<T>
  {

    private List<RingItem<T>> owner;
    public RingItem<T> Next;
    public RingItem<T> Prev;
    private T item;
    public T Value => item;

    public RingItem(T item,List<RingItem<T>> owner)
    {
      this.owner = owner;
      this.item = item;
    }

  }

  public class RingList<T> : List<RingItem<T>>
  {
    public RingItem<T> First=>0<this.Count?this[0]:null;

    public RingItem<T> Last => 0 < this.Count ? this[this.Count-1] : null;

    //末尾に追加
    public RingList<T> Append(T item)
    {
      var append=new RingItem<T>(item,this)
      {
        Next = First,
        Prev = Last
      };

      if (append.Next == null) append.Next = append;
      if (append.Prev == null) append.Prev = append;
      

      //サイズが2以上ならインデックス張り替え
      if (1 <= this.Count)
      {
        append.Next.Prev = append;
        append.Prev.Next = append;

      }

      this.Add(append);

      return this;
    }


    //演算子
    public static RingList<T> operator+(RingList<T> A, T B)
    {
      return A.Append(B);
    }


    public List<RingItem<T>> SingleLoop => this;


  }



}

