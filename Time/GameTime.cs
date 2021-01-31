using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

namespace Saitama
{
  public class GameTime : MonoBehaviour
  {

    [SerializeField]
    private ClickEvent OnUpdate;
    [Serializable]
    public class ClickEvent : UnityEvent<DateTime> { }

    public static DateTime Now { get; private set; }

    public DateTime StartDatetime = DateTime.Now;
    public float TimeScaleBase = 3600 * 24f;
    public float TimeScaleDinamic = 1.0f;

    public void ChangeTimeScale(float f)
    {
      this.TimeScaleDinamic = f;
    }



    // Use this for initialization
    void Start()
    {
      Now = StartDatetime;
    }

    // Update is called once per frame
    void Update()
    {
      Now = Now.AddSeconds(
          TimeScaleBase
        * TimeScaleDinamic
        * Time.deltaTime);
      OnUpdate.Invoke(Now);
    }

  }

}

