using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[ExecuteInEditMode]
public class SkinMeshSelector : MonoBehaviour
{
    public Vector3 Center = Vector3.zero;
    public float radius = 0.1f;
    public SkinnedMeshRenderer SkinnedMeshRenderer=>GetComponent<SkinnedMeshRenderer>();
    public Animator Animator => GetComponentInParent<Animator>();
    public HumanBodyBones bone;
    public Transform BoneTransform => Animator.GetBoneTransform(bone);

    public int BoneIndex => SkinnedMeshRenderer.bones
    .Select((t, index) => (t, index))
    .First(t => Animator.GetBoneTransform(bone) == t.t).index;


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

        Gizmos.color = new Color(0.2f,0.2f,0.2f,0.2f);
        Gizmos.DrawSphere(this.transform.position+Center, radius);

        Gizmos.color = Color.red;

        int target = BoneIndex;
        m.boneWeights
        .Select((v, index) => (v, index))
        .Where(t =>
        {
          return t.v.boneIndex0 == target
          || t.v.boneIndex1 == target
          || t.v.boneIndex2 == target
          || t.v.boneIndex3 == target
          ;
        })
        //影響量を取得
        .Select(v=> {
          float result = 0;

          if(v.v.boneIndex0 == target) result += v.v.weight0;
          if (v.v.boneIndex1 == target) result += v.v.weight1;
          if (v.v.boneIndex2 == target) result += v.v.weight2;
          if (v.v.boneIndex3 == target) result += v.v.weight3;
          return (result, v.index);
        })
        .ToList().ForEach(v=>
        {
          //頂点を取得する
          Vector3 vert= this.transform.rotation * VxV(m.vertices[v.index], transform.lossyScale) + transform.position;
          Gizmos.color = Color.HSVToRGB(0,1,v.result);
          Gizmos.DrawSphere(vert, 0.01f);
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
