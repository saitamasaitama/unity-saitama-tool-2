using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.AI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using MapGen.City;


public class ShowCreatorNameWindow : EditorWindow
{
  public string name;
  [MenuItem("Assets/Create/CreateCreator", priority = 43, validate = false)]
  public static void Init()
  {
    ShowCreatorNameWindow window = ScriptableObject.CreateInstance<ShowCreatorNameWindow>();

    window.position = new Rect(0, 0, 200, 70);
    window.ShowPopup();
  }

  void OnGUI()
  {
    EditorGUILayout.LabelField("EnterCreatorName", EditorStyles.wordWrappedLabel);
    this.name = EditorGUILayout.TextField(name);
    if (GUILayout.Button("Agree!"))
    {
      //ここに処理を書く
      Debug.Log(name);
      this.CreateCreator(name);
      this.Close();
    }
  }


  private void CreateCreator(string creatorName)
  {
    string selectedPath = AssetDatabase.GetAssetPath(Selection.activeObject);
    if (Regex.IsMatch(selectedPath, "MapGenerator"))
    {
      string dir = Path.GetDirectoryName(selectedPath);
      var source = new CreatorSource()
      {
        name = creatorName
      };
      var editorSource = new EditorSource()
      {
        name = creatorName
      };
      var dataSource = new DataSource()
      {
        name = creatorName
      };
      var generatorSource = new GeneratorSource()
      {
        name = creatorName
      };


      File.WriteAllText($"{dir}/MapGenerator/{name}Creator.cs",
        source.ToString(),
        Encoding.UTF8);

      File.WriteAllText($"{dir}/MapGenerator/Editor/{name}CreatorEditor.cs",
        editorSource.ToString(),
        Encoding.UTF8);

      File.WriteAllText($"{dir}/MapGenerator/Data/{name}Data.cs",
        dataSource.ToString(),
        Encoding.UTF8);
      File.WriteAllText($"{dir}/MapGenerator/City/{name}Generator.cs",
        generatorSource.ToString(),
        Encoding.UTF8);

      AssetDatabase.Refresh();
    }
    else
    {
      Debug.LogError("No Match");
    }
  }


  private struct CreatorSource
  {
    public string name;
    public override string ToString() =>
$@"
using UnityEngine;
[ExecuteInEditMode]
public class {name}Creator : GenericCreator<{name}GeneratorParam, {name}Data>
{{
  public override IMapGenerator<{name}Data> getGenerator({name}GeneratorParam param)
  {{
    return new {name}Generator(param);
  }}
}}
";
  }

  private struct EditorSource
  {
    public string name;
    public override string ToString() =>
$@"
using UnityEditor;

[CustomEditor(typeof({name}Creator))]
public class {name}CreatorEditor:GenericCreatorEditor
<{name}GeneratorParam,{name}Data,{name}Creator>
{{
}}
";
  }

  private struct DataSource
  {
    public string name;
    public override string ToString() =>
$@"
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MapGen.City;

[Serializable]
public class {name}Data : GenericData
{{
}}
";
  }

  private struct GeneratorSource
  {
    public string name;
    public override string ToString() =>
$@"
using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using MapGen.City;
using Random = UnityEngine.Random;

[Serializable]
public class {name}GeneratorParam:GenericParameters
{{
}}


[ExecuteInEditMode]
public class {name}Generator : IMapGenerator<{name}Data>
{{
  private {name}GeneratorParam param;

  public {name}Generator({name}GeneratorParam param)
  {{

  }}

  public {name}Data Generate(GameObject o)
  {{    
    throw new NotImplementedException();
  }}
}}

";
  }


}



//[CustomEditor(typeof(CityCreator))]
public abstract class GenericCreatorEditor<PARAM,DATA,CREATOR>:Editor
  where DATA : GenericData
  where PARAM : GenericParameters
  where CREATOR :GenericCreator<PARAM,DATA>
{

  


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

