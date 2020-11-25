using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 *  
 */
public class Vertexes : MonoBehaviour
{
  public List<Vector3> vertexes=new List<Vector3>();

  private void OnDrawGizmos(){

  }
  
  private void OnDrawGizmosSelected()
  {
    Gizmos.color=Color.red;
    foreach (Vector3 v in vertexes)
    {
      Gizmos.DrawSphere(this.transform.position+v,0.01f);
    }
  }
}
