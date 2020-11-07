using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using MapGen.City;
using Random = UnityEngine.Random;


[Serializable]
public class CityMapGeneratorParam
{
  public int count = 100;
  public float Width = 1000f;
  public float Height = 4000f;
  public float minLengthX = 10f;
  public float minLengthY = 10f;
  public float minStreetWidth = 10f;
  public float minStreetHeight = 10f;
}

public class CityMapGenerator : IMapGenerator
{

  private CityMapGeneratorParam param;
  private Graph<Line> lines = new Graph<Line>();
  private List<Line> avenues = new List<Line>();
  private List<Line> streets = new List<Line>();


  public CityMapGenerator(CityMapGeneratorParam param)
  {
    this.param = param;
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
    List<Line> YsortAvenues = avenues.OrderBy(line => line.from.x).ToList();
    //とりあえず頭に0サイズを追加
    YsortAvenues.Insert(0,
      Line.From(
      Point2F.From(0, 0),
      Point2F.From(0, param.Height)
      ));
    //末尾に最大サイズを追加
    YsortAvenues.Add(
      Line.From(
      Point2F.From(param.Width, 0),
      Point2F.From(param.Width, param.Height)
      ));


    //幅を計算
    List<(Line, float)> WidthIndexedAvenues =
    YsortAvenues
      .Select((Line line, int index) => {
          //次のindex獲得
          float width = 0;
        if (index < YsortAvenues.Count - 1)
        {
          width = YsortAvenues[index + 1].from.x - YsortAvenues[index].from.x;
        }

        return (line, width);
      }).ToList();
    //最低幅に満たないlineは削除　＆　１本に絞る
    List<(Line, float)> draftLines =
      WidthIndexedAvenues
        .Where(v => (param.minStreetWidth * 2) < v.Item2)
        .ToList();

    //引けそうもない場合、
    if (draftLines.Count == 0)
    {
      return null;
    }
    (Line, float) draftLine = draftLines.OrderBy(v => Random.value).First();//ランダム.First(); 

    var start = draftLine.Item1.from.x + param.minStreetWidth;
    var end = start + draftLine.Item2 - (param.minStreetWidth * 2);
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
    //一本もない場合は「ランダム
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
      Point2F.From(0, 0),
      Point2F.From(param.Width,0)
      ));
    //末尾に最大サイズを追加
    YSortStreets.Add(
      Line.From(
      Point2F.From(0, param.Height),
      Point2F.From(param.Width, param.Height)
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
        .Where(v => (param.minStreetHeight * 2) < v.Item2)
        .ToList();

    //引けそうもない場合、null
    if (draftLines.Count == 0)
    {
      return null;
    }
    //シャッフル
    (Line, float) draftLine = draftLines.OrderBy(v => Random.value).First();


    var start = draftLine.Item1.from.y + param.minStreetHeight;
    var end = start + draftLine.Item2 - (param.minStreetHeight * 2);
    //とりあえず基準線を引く
    var y = Random.Range(start, end);

    //とりあえず距離最大で線を引く
    return Line.From(
      Point2F.From(0, y),
      Point2F.From(param.Width, y)
    );
  }


  public void Generate(GameObject o)
  {
    //面を追加
    GameObject plane= GameObject.CreatePrimitive(PrimitiveType.Plane);
    plane.transform.localScale = new Vector3(param.Width*0.1f,1,param.Height*0.1f);
    plane.transform.position = new Vector3(param.Width / 2, 0, param.Height / 2);
    plane.transform.SetParent(o.transform);
    plane.GetComponent<MeshRenderer>().material
      =MaterialHelper.GenerateUnlit(Color.green*0.42f);

    //マンハッタン式（道路は必ず直線）で直交する

    for (int i = 0; i < param.count; i++)
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

    MapData map = o.AddComponent<MapData>();
    map.Avenues = avenues;
    map.Streets = streets;
  }

}
