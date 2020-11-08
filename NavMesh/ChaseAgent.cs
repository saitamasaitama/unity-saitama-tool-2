using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class ChaseAgent : MonoBehaviour
{
  public NavMeshAgent NavMeshAgent => this.GetComponent<NavMeshAgent>();
  public Transform target;

  // Start is called before the first frame update
  void Start()
  {
    NavMeshAgent.SetDestination(target.position);
    
  }

}
