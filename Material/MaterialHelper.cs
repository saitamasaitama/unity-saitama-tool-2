using UnityEngine;
using System.Collections;

public class MaterialHelper { 

  public static Material GenerateUnlit(Color c) {
    var result = new Material(Shader.Find("Unlit/Color"));
    result.color = c;
    return result;
  }

}