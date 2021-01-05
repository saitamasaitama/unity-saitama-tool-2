using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[ExecuteInEditMode]
public class boxcelCreator : MonoBehaviour
{
  public const string textureDirectoryPath = "Assets/Resources/Texture/";
  public string name = "texture";
  public Color TopColor = Color.green * 0.5f;
  public Color SideColor = Color.yellow * 0.5f;
  public Color BottomColor = Color.black;


  public void Create()
  {
    DirectoryInfo dir = new DirectoryInfo(textureDirectoryPath);
    if (!dir.Exists)
    {
      dir.Create();
    }

    Texture2D tex = BoxTexture();
    File.WriteAllBytes($"{textureDirectoryPath}{name}.png", tex.EncodeToPNG());
    AssetDatabase.Refresh();

    GameObject result = new GameObject("result Cube");

    var meshFilter = result.AddComponent<MeshFilter>();
    meshFilter.mesh = BoxMesh();

    var meshRenderer = result.AddComponent<MeshRenderer>();

    //meshRenderer.sharedMaterial= BoxMaterial(tex);
    meshRenderer.material = BoxMaterial(tex);

    tex.SetPixel(3, 3, Color.white);
    //meshRenderer.material.SetTexture("_MainTex", tex);

  }

  private Texture2D BoxTexture()
  {
    var tex = new Texture2D(16, 48, TextureFormat.ARGB32, false);
    //tex.alphaIsTransparency = true;
    tex.filterMode = FilterMode.Point;

    tex.SetPixels(0, 0, 16, 16,
      Enumerable.Repeat(TopColor, 16 * 16)
      .Select(c =>
      {
        Color.RGBToHSV(c, out float H, out float S, out float V);
        return Color.HSVToRGB(H, S, V * Random.Range(0.8f, 1.2f));
      })
      .ToArray());


    tex.SetPixels(0, 16, 16, 16, Enumerable.Repeat(SideColor, 16 * 16).ToArray());
    tex.SetPixels(0, 32, 16, 16, Enumerable.Repeat(BottomColor, 16 * 16).ToArray());
    tex.Apply();
    return tex;
  }




  private Mesh BoxMesh()
  {
    var result = new Mesh();
    //頂点は16
    result.SetVertices(
      Enumerable.Range(0, 6)
      .Select(i => (i, Quad.VERTEX))
      .Aggregate(new List<Vector3>(),
        (carry, item) =>
        {
          Debug.Log($"i={item.i}");
          switch (item.i)
          {
            //0は無回転
            case 1:
              item.VERTEX = item.VERTEX.Select(v => Quaternion.Euler(-90, 0, 0) * v).ToArray();
              break;
            case 2:
              item.VERTEX = item.VERTEX.Select(v => Quaternion.Euler(-90, 90, 0) * v).ToArray();
              break;
            case 3:
              item.VERTEX = item.VERTEX.Select(v => Quaternion.Euler(-90, 180, 0) * v).ToArray();
              break;
            case 4:
              item.VERTEX = item.VERTEX.Select(v => Quaternion.Euler(-90, 270, 0) * v).ToArray();
              break;
            case 5:
              item.VERTEX = item.VERTEX.Select(v => Quaternion.Euler(180, 0, 0) * v).ToArray();
              break;
            default: break;
          }
          carry.AddRange(item.VERTEX);
          return carry;
        })

    );
    Debug.Log($"{result.vertexCount}");


    result.SetUVs(0,
      Enumerable.Range(0, 6)
      .Select(index => Quad.UV.Select(v2 => v2))
      .Aggregate(new List<Vector2>(),
      (carrry, item) =>
      {
        carrry.AddRange(item);
        return carrry;
      })
     );

    result.SetIndices(
    Enumerable.Range(0, 6).Select(i => Quad.INDEX_FORE.Select(index => index + (i * 4)).ToArray())
    .Aggregate(new List<int>(), (carry, item) =>
    {
      carry.AddRange(item);
      return carry;
    }).ToArray()
    ,
      MeshTopology.Quads, 0
    );

    return result;
  }

  private Material BoxMaterial(Texture2D tex)
  {
    var mat = new Material(Shader.Find("Unlit/Texture"));

    //mat.SetTexture("_Base", tex);
    mat.mainTexture = tex;


    return mat;
  }
}
