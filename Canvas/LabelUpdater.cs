using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LabelUpdater : MonoBehaviour
{
  public Text Text => GetComponentInChildren<Text>();
  public string StartText = "";

  public string FloatFormat = "0.00";
  public string DateTimeFormat = "yyyy-MM-dd(ddd) HH:mm";
  // Start is called before the first frame update
  void Start()
  {
    Text.text = StartText;
  }

  public void OnChangeByFloat(float f)
  {
    Text.text = f.ToString(FloatFormat);
  //  s.onValueChanged
  }

  public void OnChangeByDateTime(DateTime d)
  {
    Text.text = d.ToString(DateTimeFormat);
  }


}
