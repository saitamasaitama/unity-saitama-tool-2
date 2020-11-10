
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapGen.City;

[Serializable]
public class BuildingData : GenericData
{
  [SerializeField]
  public Block block;
  [SerializeField]
  public Building building;

  public override void Reset()
  {
    throw new NotImplementedException();
  }


}
