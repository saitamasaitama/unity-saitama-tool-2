using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(CityCreator))]
public class CityCreatorEditor:GenericCreatorEditor
<CityGeneratorParam,CityData,CityCreator>
{
}
