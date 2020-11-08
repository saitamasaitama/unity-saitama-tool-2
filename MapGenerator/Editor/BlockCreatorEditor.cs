using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

[CustomEditor(typeof(BlockCreator))]
public class BlockCreatorEditor:GenericCreatorEditor<BlockGeneratorParam,BlockData,BlockCreator>
{
}
