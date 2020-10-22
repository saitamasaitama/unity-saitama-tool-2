using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleDebugMove : MonoBehaviour
{
  [Range(0.01f,10f)]
  public float Speed=1.0f;
  public float Rotate=30.0f;//秒/度

  private MoveMatrix matrix = new MoveMatrix();
  

  private struct MoveMatrix
  {
    public float X, Y;

  }


  private bool isLeftArrow => Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A);
  private bool isRightArrow => Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D);


  public Vector3 CameraForward => new Vector3(
    Camera.main.transform.forward.x,
    0,
    Camera.main.transform.forward.z
    );


  void Update()
  {
    //左右は一回押したらターン
    if (isLeftArrow)
    {
      this.transform.localRotation *= Quaternion.Euler(0,  -Rotate , 0);
    }
    if (isRightArrow)
    {
      this.transform.localRotation *= Quaternion.Euler(0, +Rotate, 0);
    }

    
    this.transform.position += 
      Input.GetAxis("Vertical")
      * CameraForward 
      * Time.deltaTime * Speed;

  }


}
