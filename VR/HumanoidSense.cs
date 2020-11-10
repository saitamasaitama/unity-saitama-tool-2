using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HumanoidSense : MonoBehaviour
{

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log($"ENTER {other.name}");    
  }

  private void OnTriggerExit(Collider other)
  {
    Debug.Log($"EXIT {other.name}");
  }

  private void OnTriggerStay(Collider other)
  {
    Debug.Log($"STay {other.name}");
  }
}
