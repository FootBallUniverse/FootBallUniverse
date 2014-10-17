using UnityEngine;
using UnityEditor;
using System.Collections;

public class BakeMesh
{
	[MenuItem ("Assets/Bake Mesh")]
	static void Bake()
	{
		GameObject selectedGameObject = Selection.activeGameObject;
		if( selectedGameObject == null )
		{
			return;
		}
		
		MeshFilter[] meshFilters = selectedGameObject.GetComponentsInChildren<MeshFilter>();
		if( meshFilters == null )
		{
			return;
		}
		
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		
		for( int i = 0;i<meshFilters.Length;i++ )
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
		}
		
		Mesh saveMesh = new Mesh();
		saveMesh.name = selectedGameObject.name;
		
		saveMesh.CombineMeshes( combine );
		
		AssetDatabase.CreateAsset(saveMesh, "Assets/" + saveMesh.name + ".asset");
		AssetDatabase.Refresh();
	}
}
