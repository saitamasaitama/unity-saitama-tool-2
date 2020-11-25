using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class OneTimeForce: MonoBehaviour
{
  public Vector3 force;
  public ForceMode mode;

  public void Start()
  {
    GetComponent<Rigidbody>().AddForce(force, mode);

    GameObject.Destroy(this);
  }
}
