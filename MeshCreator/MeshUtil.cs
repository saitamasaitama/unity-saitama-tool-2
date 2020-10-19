using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace MeshUtil
{
  public struct Line
  {
    public Vector3 A, B;

    public double Length => Vector3.Distance(A, B);


    public void Draw()
    {
      Debug.DrawLine(A, B);
    }

    public Vector3 Vector => B - A;

  }

  public struct Triangle
  {
    public Vector3 A, B, C;

    public void Draw()
    {
      Debug.DrawLine(A, B);
      Debug.DrawLine(A, C);
      Debug.DrawLine(B, C);
    }
  }

  public struct Square
  {

    public static int[] Quad2TriIndex(int A, int B, int C, int D)
    {
      return new int[] {
      A,B,D,C
    };
    }
  }

  public interface IMeshCreator
  {
    List<Vector3> Vertics();
    List<Vector3> Normals();
    List<Vector2> UV();
    List<int> Indices();
  }


  public class CubeCreator : IMeshCreator
  {
    public Vector3[] Face => new Vector3[]{
      new Vector3(-1f,1,1),//左上
      new Vector3(1f,1,1),//右上
      new Vector3(-1f,1,-1),//左下
      new Vector3(1f,1,-1),//左上
    };


    public List<int> Indices() =>
       Square.Quad2TriIndex(0, 1, 2, 3)
       .Concat(Square.Quad2TriIndex(4, 5, 6, 7))
       .Concat(Square.Quad2TriIndex(6, 7, 0, 1))
       .Concat(Square.Quad2TriIndex(2, 3, 4, 5))
       .Concat(Square.Quad2TriIndex(3, 1, 5, 7))
       .Concat(Square.Quad2TriIndex(0, 2, 6, 4))
       .ToList();

    public List<Vector3> Normals() =>
      Enumerable.Repeat(Vector3.up, 8).ToList();

    public List<Vector2> UV()
    {
      return new List<Vector2>()
      {
        Vector2.left+Vector2.up,
        Vector2.left+Vector2.down,
        Vector2.right+Vector2.up,
        Vector2.right+Vector2.down,

        Vector2.left+Vector2.up,
        Vector2.left+Vector2.down,
        Vector2.right+Vector2.up,
        Vector2.right+Vector2.down,

      };
    }

    public List<Vector3> Vertics() =>
      Face
      .Concat(Face.Select(s => Quaternion.Euler(180, 0, 0) * s))
      .ToList()
    ;
  }



  public class PipeCreator : IMeshCreator
  {
    public float Length;
    public float Thick;
    public int Cut;//
    public int Rounds;//兆点数


    public PipeCreator(
      float length,
      float thick,
      int cut,
      int rounds)
    {
      this.Length = length;
      this.Thick = thick;
      this.Cut = cut;
      this.Rounds = rounds;


    }

    public List<Vector3> Vertics()
    {
      //まず合計の兆点数を計算する

      //0カット＝ 2段
      //1カット＝ 3段

      // (cut +2
      int verticsCount = (Cut + 2) * Rounds;
      Debug.Log($"兆点数は {verticsCount}個");
      List<Vector3> result = new List<Vector3>();


      float step =(float)(Length / (Cut + 1.0f));
      for(int c = 0; c < Cut+2; c++)
      {
        for(int i = 0; i < Rounds; i++)
        {
          //カット処理
          result.Add(Vector3.up * step * c +
            Quaternion.Euler(0, (360 / Rounds) * i, 0) * Vector3.forward*(Thick/2f)
            );
        }
      }


      return result;
    }


    public List<int> Indices()
    {
      //面の処理
      var result= new List<int>();
      int verticsCount = (Cut + 2) * Rounds;

      for (int i = 0; i < Rounds; i++)
      {


        //カット
        for (int j = 0; j < Cut + 1; j++)
        {
          int A = (j + 1) * Rounds + ((i + 1) % Rounds);
          int B = (j + 1) * Rounds + (i % Rounds);


          int C = j + ((i + 1) % Rounds);
          int D = j + i;
         
          result.AddRange(Square.Quad2TriIndex(A, B, C, D).ToList());


        }
      }

      //Square.Quad2TriIndex(0, 1, 2, 3)


      return result;
    }

    public List<Vector3> Normals()
    {
      return new List<Vector3>();
      //throw new System.NotImplementedException();
    }

    public List<Vector2> UV()
    {
      return new List<Vector2>();
    }
  }


}



public class PolygonUtilLoad
{



}



