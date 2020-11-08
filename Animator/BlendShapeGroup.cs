using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


[ExecuteInEditMode]
public class BlendShapeGroup: MonoBehaviour
{
  public SkinnedMeshRenderer skinmesh => GetComponentInChildren<SkinnedMeshRenderer>();

  [SerializeField, Range(0, 5)]
  public Dictionary<string,float> BlendShape=new Dictionary<string, float>();


  public void Update()
  {

    foreach(SkinnedMeshRenderer skin in this.GetComponentsInChildren<SkinnedMeshRenderer>())
    {
      
      foreach(KeyValuePair<string,float> kv in BlendShape)
      {
        //名前探し



        int index = findBlendShapeNameIndex(skin.sharedMesh, kv.Key);
        if (-1 != index)
        {          
          skin.SetBlendShapeWeight(index, kv.Value);
        }
      }
    }

    
  }

  private int findBlendShapeNameIndex(Mesh m,string name)
  {
    for(int i = 0; i < m.blendShapeCount; i++)
    {
      if(Regex.IsMatch(m.GetBlendShapeName(i), $".+?{name}$"))
      {
        return i;
      }
    }
    return -1;
  }


}
