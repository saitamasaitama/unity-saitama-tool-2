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
   
    meshRenderer.sharedMaterial= BoxMaterial(tex);
    meshRenderer.material = BoxMaterial(tex);

    tex.SetPixel(3, 3, Color.white);
    meshRenderer.material.SetTexture("_MainTex", tex);

  }

  private Texture2D BoxTexture()
  {
    var tex = new Texture2D(16, 48,TextureFormat.ARGB32,false);
    //tex.alphaIsTransparency = true;
    tex.filterMode = FilterMode.Point;
    
    tex.SetPixels(0, 0, 16, 16,
      Enumerable.Repeat(TopColor, 16 * 16)
      .Select(c=> {
        Color.RGBToHSV(c, out float H, out float S, out float V);
        return Color.HSVToRGB(H, S, V * Random.Range(0.8f, 1.2f));
      })
      .ToArray());


    tex.SetPixels(0, 16, 16, 16, Enumerable.Repeat(SideColor,  16 * 16).ToArray());
    tex.SetPixels(0, 32, 16, 16, Enumerable.Repeat(BottomColor, 16 * 16).ToArray());
    tex.Apply();
    return tex;
  }




  private Mesh BoxMesh()
  {
    var result = new Mesh();
    //頂点は16
    result.SetVertices(new List<Vector3>() {
      //TOP
      Cube.LEFT_BACK_UP,
      Cube.RIGHT_BACK_UP,      
      Cube.LEFT_FORE_UP,
      Cube.RIGHT_FORE_UP,

      //Side L-R      
      Cube.LEFT_FORE_UP,
      Cube.RIGHT_FORE_UP,
      Cube.LEFT_FORE_DOWN,
      Cube.RIGHT_FORE_DOWN,

      Cube.LEFT_BACK_UP,
      Cube.RIGHT_BACK_UP,
      Cube.LEFT_BACK_DOWN,
      Cube.RIGHT_BACK_DOWN,



      //Bottom
      Cube.LEFT_BACK_DOWN,
      Cube.RIGHT_BACK_DOWN,
      Cube.LEFT_FORE_DOWN,
      Cube.RIGHT_FORE_DOWN,

    });

    result.SetUVs(0, new List<Vector2>()
    {
      Vector2.zero,
      Vector2.right,
      Vector2.zero+(Vector2.up/3),
      Vector2.right+(Vector2.up/3),



      Vector2.zero+(Vector2.up/3),
      Vector2.right+(Vector2.up/3),
      Vector2.zero+(Vector2.up/3)*2,
      Vector2.right+(Vector2.up/3)*2


    });

    result.SetIndices(new int[] {
      1, 3, 2, 0,
      7, 5, 4, 6,
    }, MeshTopology.Quads, 0);


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

public class Quad
{
  public Vector3 A, B, C, D;




  public int[] toIndexes => new int[]
  {
    1,3,2,0
  };
}