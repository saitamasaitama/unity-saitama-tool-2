using UnityEngine;
using System.Collections;


public abstract class GenericGenerator<PARAM, DATA> : IMapGenerator<DATA>
{
  protected PARAM param;
  protected GenericGenerator(PARAM param)
  {
    this.param = param;
  }
  public abstract DATA Generate(GameObject o, DATA oldData);
}

