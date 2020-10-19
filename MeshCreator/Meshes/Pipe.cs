using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MeshUtil;



public class PipeCreator : IMeshCreator
{
  public float Length;
  public float innerDiameter;
  public float outerDiameter;

  public int Cut;//
  public int Rounds;//兆点数


  public PipeCreator(
  float length,
  float outerDiameter,
  float innerDiameter,
  
  int cut,
  int rounds)
  {
    this.Length = length;
    this.Cut = cut;
    this.Rounds = rounds;
    this.innerDiameter = innerDiameter;
    this.outerDiameter = outerDiameter;

  }

  public List<Vector3> Vertics()
  {
    //まず合計の兆点数を計算する

    //0カット＝ 2段

    // (cut +2
    int verticsCount = (Cut + 2) * Rounds * 2;
    Debug.Log($"兆点数は {verticsCount}個");
    List<Vector3> result = new List<Vector3>();


    float step = (float)(Length / (Cut + 1.0f));
    for (int c = 0; c < Cut + 2; c++)
    {
      for (int i = 0; i < Rounds; i++)
      {

        //外
        result.Add(Vector3.up * step * c +
          Quaternion.Euler(0, (360 / Rounds) * i, 0) * Vector3.forward * (outerDiameter / 2f)
        );
        //内側
        result.Add(Vector3.up * step * c +
          Quaternion.Euler(0, (360 / Rounds) * i, 0) * Vector3.forward * (innerDiameter / 2f)
        );
      }


    }
    return result;
  }

  public List<int> Indices()
  {
    var result = new List<int>();
    int verticsCount = (Cut + 2) * Rounds;
    int Rounds2 = Rounds * 2;

    for (int i = 0; i < Rounds; i++)
    {

      //カット
      for (int j = 0; j < Cut + 1; j++)
      {
        int i2 = i * 2;
        //表


        int C = j + ((i + 1) % Rounds2);
        int D = j + i;

        int A = (j + 1) * (Rounds2) + ((i + 1) % (Rounds2));
        int B = (j + 1) * (Rounds2) + (i % (Rounds2));


        result.AddRange(
          Square.Quad2TriIndex(A, B, C, D).ToList()
         );
      }




    }
    return result;
  }

  public List<Vector3> Normals()
  {
    return new List<Vector3>();
  }

  public List<Vector2> UV()
  {
    return new List<Vector2>();
  }


}


public class BowCreator : IMeshCreator
{
  public float Length;
  public float Thick;
  public int Cut;//
  public int Rounds;//兆点数


  public BowCreator(
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


    float step = (float)(Length / (Cut + 1.0f));
    for (int c = 0; c < Cut + 2; c++)
    {
      for (int i = 0; i < Rounds; i++)
      {
        //カット処理
        result.Add(Vector3.up * step * c +
          Quaternion.Euler(0, (360 / Rounds) * i, 0) * Vector3.forward * (Thick / 2f)
          );
      }
    }


    return result;
  }


  public List<int> Indices()
  {
    //面の処理
    var result = new List<int>();
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

        result.AddRange(
          Square.Quad2TriIndex(A, B, C, D).ToList()
         );


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