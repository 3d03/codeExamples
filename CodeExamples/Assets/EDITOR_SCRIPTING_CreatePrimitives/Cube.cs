using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]

public class Cube : MonoBehaviour
{


    public Vector3 pos;
    public Vector3 rot;
    [Range(1, 10)]
    public float size;


    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.localScale = Vector3.one * size;
        this.gameObject.transform.position = pos;
        this.gameObject.transform.eulerAngles = rot;

    }
    public void ResetCube()
    {
        pos = Vector3.zero;
        rot = Vector3.zero;
        size = 1;

    }

}






/*
#if UNITY_EDITOR
    [CustomEditor(typeof(Cube))]
    public class CubeEditor:Editor
{

    public override void OnInspectorGUI()
    {
        Cube cube = (Cube)target;
        if (GUILayout.Button("Reset"))
        {
            cube.ResetCube();
        }

        cube.pos = (GameObject)EditorGUILayout.Vector3Field("GameObjectToApply", cube.pos,  typeof(Vec), true);
        cube.rot = (Texture)EditorGUILayout.Vector3Field("TextureToApply", cube.rot, typeof(Vector3), false);
        cube.size = (Texture)EditorGUILayout.FloatField( cube.size, typeof(float), false);

    }
}
#endif
*/