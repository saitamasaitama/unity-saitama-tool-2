using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour
{
  public float kmParSec;
  public float speed = 0;
  public float power = 0;
  public float accell = 0;

  public float turn = 0;
  public float accellRatio = 0.5f;
  public float turnRatio = 90;//1秒間に回転する角度

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    accell = Input.GetAxis("Vertical")*accellRatio;
    turn = Input.GetAxis("Horizontal")*turnRatio;

    if(speed < -0.1 || 0.1 < speed)
    {
      this.transform.position += this.transform.forward * speed*Time.deltaTime;
    }

    this.transform.rotation *= Quaternion.Euler(0,turn*Time.deltaTime, 0);
  }

  private void FixedUpdate()
  {

    float baseEntropy = (Time.fixedDeltaTime * 0.05f);

    power += (0<accell?accell:accell*4) * Time.fixedDeltaTime;
    power *= (1.0f - baseEntropy);

    if (power < 0) power = 0;

    speed = Mathf.Pow(power,0.88f);

    kmParSec = speed * 3.6f;
  }
}
