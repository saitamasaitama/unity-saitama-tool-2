using UnityEngine;
using MapGen.City;

[ExecuteInEditMode]
public class CityCreator : GenericCreator<CityGeneratorParam, CityData>
{
  public override IMapGenerator<CityData> getGenerator(CityGeneratorParam param)
  {
    return new CityGenerator(param);
  }

  //デバッグ用表示→Generatorに持たせるべき
  private void OnDrawGizmos()
  {
    if (this.data == null) return;
    Gizmos.color = Color.blue;
    foreach (Line l in this.data.Streets)
    {
      Gizmos.DrawLine(
        new Vector3(l.from.x, 0, l.from.y),
        new Vector3(l.to.x, 0, l.to.y)
      );
    }
    Gizmos.color = Color.red;
    foreach (Line l in this.data.Avenues)
    {
      Gizmos.DrawLine(
        new Vector3(l.from.x, 0, l.from.y),
        new Vector3(l.to.x, 0, l.to.y)
      );
    }

    Gizmos.color = Color.red;
    foreach (CrossPoint p in this.data.CrossPoints)
    {
      Gizmos.DrawSphere(new Vector3(p.Point.x, 0, p.Point.y), 1);
    }
  }
}

