using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MapGen.City;
using System;
using Random = UnityEngine.Random;


[ExecuteInEditMode]
public class BlockCreator : GenericCreator<BlockGeneratorParam, BlockData>
{
  public override IMapGenerator<BlockData> getGenerator(BlockGeneratorParam param)
  {
    return new BlockGenerator(param);
  }

  public void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.magenta * 0.5f;
    Gizmos.DrawCube(

      this.data.block.Center.toVector3()
      + Vector3.up * 5f,
      new Vector3(
        data.block.Width,
        10,
        data.block.Height)
        );
  }

}
