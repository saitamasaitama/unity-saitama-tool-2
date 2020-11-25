using UnityEngine;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LiteStringRenderUpdate: MonoBehaviour
{
  public LineRenderer LineRenderer => GetComponent<LineRenderer>();

  // Update is called once per frame
  void Update()
  {
    LineRenderer.SetPositions(
      this.transform.GetComponentsInChildren<Rigidbody>().Select(o => o.transform.position).ToArray()
    );
  }
}
