using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SkinnedMeshRenderer))]
public class groupModify : MonoBehaviour
{
  public SkinnedMeshRenderer skinmesh => GetComponent<SkinnedMeshRenderer>();

  [SerializeField, Range(0, 5)]
  public List<float> jj;
}
