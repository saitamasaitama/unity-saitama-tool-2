using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[ExecuteInEditMode]
public class MeshSelector : MonoBehaviour
{
    public Vector3 Center = Vector3.zero;
    public float radius = 0.1f;
    public SkinnedMeshRenderer SkinnedMeshRenderer=>GetComponent<SkinnedMeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mesh m = SkinnedMeshRenderer.sharedMesh;
        Debug.Log($"Bone-Count={SkinnedMeshRenderer.bones.Length} BONE-Size={m.boneWeights.Length} V-size={m.vertices.Length}");
        Debug.Log($"POSES-Size={m.bindposes.Length}　HumanoidBoneSize={Enum.GetValues(typeof(HumanBodyBones)).Length}　");

    }

    void OnDrawGizmos() {
        Mesh m = SkinnedMeshRenderer.sharedMesh;
        Debug.DrawLine(Vector3.zero, Vector3.one);

        Gizmos.color = new Color(0.2f,0.2f,0.2f,0.2f);
        Gizmos.DrawSphere(this.transform.position+Center, radius);

        Gizmos.color = Color.red;


        m.vertices
        .Select((v,index) =>
            (transform.rotation * VxV(v, transform.lossyScale) + transform.position,index)
        )
        .Where(v=>Vector3.Distance(v.Item1,Center+this.transform.position)<radius)
        .ToList().ForEach(v=>
        {
            Gizmos.DrawSphere(v.Item1, 0.01f);
        });


    }
    private Vector3 VxV(Vector3 a,Vector3 b)
    {
        a.x*=b.x;
        a.y *= b.y;
        a.z *= b.z;
        return a;
    }
}
