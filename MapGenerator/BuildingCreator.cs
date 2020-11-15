
using UnityEngine;
using MapGen.City;

[ExecuteInEditMode]
public class BuildingCreator : GenericCreator<BuildingGeneratorParam, BuildingData>
{
  public override IMapGenerator<BuildingData> getGenerator(BuildingGeneratorParam param)
  {
    return new BuildingGenerator(param);
  }

  private void OnDrawGizmos()
  {
    if (this.data == null) return;

    Gizmos.color = Color.yellow * 0.5f;
    Gizmos.DrawCube(
      this.data.block.Center.toVector3()//まずはブロック位置から
      + this.data.building.Center.toVector3()
      +Vector3.up * 6f,
      new Vector3(
        data.building.Width,
        12,
        data.building.Height)
        );

  }
}
