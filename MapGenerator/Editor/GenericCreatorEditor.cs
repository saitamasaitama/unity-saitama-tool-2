using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.AI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

//[CustomEditor(typeof(CityCreator))]
public abstract class GenericCreatorEditor<PARAM,DATA,CREATOR>:Editor
  where DATA : MonoBehaviour
  where PARAM : GenericParameters
  where CREATOR :GenericCreator<PARAM,DATA>
{
  [MenuItem("Assets/Create/CreateCreator", priority = 42, validate = false)]
  public static void CreateCreator()
  {
    string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);
    if (Regex.IsMatch(selectedPath, "\\^MapGenerator"))
    {
      string dir = Path.GetDirectoryName(selectedPath);
      var source = new CSSource()
      {
        className = "Any"
      };

      File.WriteAllText($"{dir}/AnyCreator.cs",
        source.ToString(),
        Encoding.UTF8);
      AssetDatabase.Refresh();
    }
  }


  [MenuItem("Assets/Create/SaitamaCreateGenerator", priority = 43, validate = false)]
  public static void CreateGenerator()
  {
    string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);
    if (Regex.IsMatch(selectedPath, "\\.*Creator.cs$"))
    {

    }
    //Creatorを作る

    //Editorを作る

    //Dataを作る

    //Generatorを作る



  }


  public override void OnInspectorGUI()
  {
    CREATOR target = this.target as CREATOR;
    base.OnInspectorGUI();
    if (GUILayout.Button($"Gen From {typeof(CREATOR)}"))
    {
      var o= target.Generate();
    }
  }  

  private struct CreatorSource
  {
    public override string ToString() => 
$@"
using UnityEngine;
[ExecuteInEditMode]
public class CityCreator : GenericCreator<CityGeneratorParam, CityData>
{{
  public override IMapGenerator<CityData> getGenerator(CityGeneratorParam param)
  {{
    return new CityGenerator(param);
  }}
}}
";
  }

  private struct CSSource
  {
    public string className;
    public string classRearfix;
    public string[] usings;
    public string extends;
    public string attribute;
    public CSMethod[] methods;

    public override string ToString() =>
$@"{string.Join("\n",usings.Select(use=>$"using {use};").ToList())}

{attribute}
public class {this.className}{classRearfix}:{extends}
{{
  {string.Join("\n\n",methods)}

  public override void OnInspectorGUI()
  {{

    {this.className} target = this.target as {this.className};
    base.OnInspectorGUI();
    if (GUILayout.Button(""Button""))
    {{
      //code
    }}
    
  }}  
}}
";

  }
  private struct CSMethod
  {
    public string retType;
    public bool isOverride;
    public string methodName;


    public override string ToString() =>
$@"
  public {(isOverride?"override":"")} {retType} {methodName}()
  {{

  }}  
";

  }
}

