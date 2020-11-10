using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using MapGen.City;
using Random = UnityEngine.Random;


[Serializable]
public class CityGeneratorParam:GenericParameters
{
  public int RoadCount = 100;
  public float Width = 1000f;
  public float Height = 4000f;
  public float minLengthX = 10f;
  public float minLengthY = 10f;

  public float StreetLineWidth = 1f;
  public float AvenueLineWidth = 1.5f;

  public float minStreetBlockWidth = 10f;
  public float minStreetBlockHeight = 10f;
}

/// <summary>
/// City map generator.
/// </summary>
public class CityGenerator : GenericGenerator<CityGeneratorParam,CityData>
{

  private Graph<Line> lines = new Graph<Line>();
  private List<Line> avenues = new List<Line>();
  private List<Line> streets = new List<Line>();


  public CityGenerator(CityGeneratorParam param):base(param)
  {
  }


  private Line? genAvenue()
  {
    //一本もない場合は「ランダム
    if (avenues.Count == 0)
    {
      var xa = Random.Range(0, param.Width);
      return Line.From(
        Point2F.From(xa, 0),
        Point2F.From(xa, param.Height)
      );
    }

    //まずはターゲットのAvenueを選択

    //通りをYでsortしたマップを作る
    List<Line> XsortAvenue = avenues.OrderBy(line => line.from.x).ToList();
    //とりあえず頭に0サイズを追加
    XsortAvenue.Insert(0,
      Line.From(
      Point2F.From(-param.minStreetBlockWidth, 0),
      Point2F.From(-param.minStreetBlockWidth, param.Height)
      ));
    //末尾に最大サイズを追加
    XsortAvenue.Add(
      Line.From(
      Point2F.From(param.Width+param.minStreetBlockWidth, 0),
      Point2F.From(param.Width+param.minStreetBlockWidth, param.Height)
      ));


    //幅を計算
    List<(Line, float)> WidthIndexedAvenues =
    XsortAvenue
      .Select((Line line, int index) => {
          //次のindex獲得
          float width = 0;
        if (index < XsortAvenue.Count - 1)
        {
          width = XsortAvenue[index + 1].from.x - XsortAvenue[index].from.x;
        }

        return (line, width);
      }).ToList();
    //最低幅に満たないlineは削除　＆　１本に絞る
    List<(Line, float)> draftLines =
      WidthIndexedAvenues
        .Where(v => (param.minStreetBlockWidth * 2) < v.Item2)
        .ToList();

    //引けそうもない場合、
    if (draftLines.Count == 0)
    {
      return null;
    }
    (Line, float) draftLine = draftLines.OrderBy(v => Random.value).First();//ランダム.First(); 

    var start = draftLine.Item1.from.x + param.minStreetBlockWidth;
    var end = start + draftLine.Item2 - (param.minStreetBlockWidth * 2);
    //とりあえず基準線を引く
    var x = Random.Range(start, end);

    //とりあえず距離最大で線を引く
    return Line.From(
      Point2F.From(x, 0),
      Point2F.From(x, param.Height)
    );
  }

  private Line? genStreet()
  {
    //一本もない場合はとりあえず長辺に沿って作る
    if (streets.Count == 0)
    {
      var ya = Random.Range(param.Height,0);
      return Line.From(
        Point2F.From(0, ya),
        Point2F.From(param.Width,ya)
      );


    }

    //通りをYでsortしたマップを作る
    List<Line> YSortStreets = streets.OrderBy(line => line.from.y).ToList();
    //とりあえず頭に0サイズを追加
    YSortStreets.Insert(0,
      Line.From(
      Point2F.From(0, -param.minStreetBlockHeight),
      Point2F.From(param.Width,-param.minStreetBlockHeight)
      ));
    //末尾に最大サイズを追加
    YSortStreets.Add(
      Line.From(
      Point2F.From(0, param.Height+param.minStreetBlockHeight),
      Point2F.From(param.Width, param.Height+param.minStreetBlockHeight)
      ));


    //幅を計算
    List<(Line, float)> WidthIndexedStreets =
    YSortStreets
      .Select((Line line, int index) => {
        //次のindex獲得
        float height = 0;
        if (index < YSortStreets.Count - 1)
        {
          height = YSortStreets[index + 1].from.y - YSortStreets[index].from.y;
        }

        return (line, height);
      }).ToList();
    //最低幅に満たないlineは削除　＆　１本に絞る
    List<(Line, float)> draftLines =
      WidthIndexedStreets
        .Where(v => (param.minStreetBlockHeight * 2) < v.Item2)
        .ToList();

    //引けそうもない場合、null
    if (draftLines.Count == 0)
    {
      return null;
    }
    //シャッフル
    (Line, float) draftLine = draftLines.OrderBy(v => Random.value).First();


    var start = draftLine.Item1.from.y + param.minStreetBlockHeight;
    var end = start + draftLine.Item2 - (param.minStreetBlockHeight * 2);
    //とりあえず基準線を引く
    var y = Random.Range(start, end);

    //とりあえず距離最大で線を引く
    return Line.From(
      Point2F.From(0, y),
      Point2F.From(param.Width, y)
    );
  }



  /// <summary>
  /// まず都市を作り上げる
  /// </summary>
  /// <returns>The ap generator< city data>. generate.</returns>
  /// <param name="o">O.</param>
  public override CityData Generate(GameObject o, CityData oldData)
  {
    //面を追加
    GameObject plane= GameObject.CreatePrimitive(PrimitiveType.Plane);
    plane.transform.localScale = new Vector3(param.Width*0.1f,1,param.Height*0.1f);
    plane.transform.position = new Vector3(param.Width / 2, 0, param.Height / 2);
    plane.transform.SetParent(o.transform);
    plane.GetComponent<MeshRenderer>().material
      =MaterialHelper.GenerateUnlit(Color.green*0.42f);

    //マンハッタン式（道路は必ず直線）で直交する

    for (int i = 0; i < param.RoadCount; i++)
    {
      if (Random.Range(0f, 1f) < 0.5)
      {
        var v = genAvenue();
        if (v.HasValue)
        {
          avenues.Add(v.Value);
        }
      }
      else
      {
        var v = genStreet();
        if (v.HasValue)
        {
          streets.Add(v.Value);
        }
      }
    }

    //この辺改良できるはず
    CityData map = new CityData();
    map.Avenues = avenues;
    map.Streets = streets;
    map.Reculculate();

    //ブロックを生成する
    foreach(Block block in map.Blocks)
    {
      GameObject blockObj= new GameObject($"Block");
      BlockData data=new BlockData();
      data.block=block;
      blockObj.transform.SetParent(o.transform);
      blockObj.transform.position = o.transform.position + block.Center.toVector3();
      BlockCreator creator= blockObj.AddComponent<BlockCreator>();
      creator.data = data;
    }

    return map;
  }

}
