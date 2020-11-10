
using UnityEngine;
[ExecuteInEditMode]
public class islandCreator : GenericCreator<islandGeneratorParam, islandData>
{
  public override IMapGenerator<islandData> getGenerator(islandGeneratorParam param)
  {
    return new islandGenerator(param);
  }
}
