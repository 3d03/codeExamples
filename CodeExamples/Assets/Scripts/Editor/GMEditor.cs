using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GMEditor : Editor {

    private GameManager gm;

    public bool myBool;
    public void OnEnable()
    {
        gm = (GameManager)target;
    }
    public override void OnInspectorGUI()
    {
      
        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  Info", MessageType.Info);
        EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  Error", MessageType.Error);
        EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  Warning", MessageType.Warning);
        EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  None", MessageType.None);


        EditorGUILayout.Space();
        if (GUILayout.Button("Добавить", GUILayout.Height(40))) gm.items.Add(new Item());
        EditorGUILayout.Space();
        if (GUILayout.Button("Удалить первый"))

        {
            if (gm.items.Count>0 )
            {
                gm.items.RemoveAt(0);
            }
        }

        EditorGUILayout.Space(); EditorGUILayout.Space();
        EditorGUILayout.Space(); EditorGUILayout.Space();
        if (gm.items.Count>0)
        {

            foreach (Item item in gm.items)
            {

                EditorGUILayout.BeginVertical("box");
                
                EditorGUILayout.BeginHorizontal();

                if (GUILayout.Button("X", GUILayout.Width(65), GUILayout.Height(35)))
                {

                    gm.items.Remove(item);
                    break;
                }

                if (GUILayout.Button("Default", GUILayout.Width(65), GUILayout.Height(35)))
                {

                    item.Name = "Default";
                }

                EditorGUILayout.Space();
                if (GUILayout.Button("Apply Texture", GUILayout.Width(105), GUILayout.Height(35)))
                {
                    if ((item.Image != null)&& (item.go != null))
                    {
                        item.ApplyTexture();
                        item.go.name = item.Image.name+"_GameObject";
                    }
                }

                EditorGUILayout.EndHorizontal();

                item.Id= EditorGUILayout.IntField("ID", item.Id);
                item.Name = EditorGUILayout.TextField("Name", item.Name);
                
                item.go = (GameObject) EditorGUILayout.ObjectField("GameObjectToApply",  item.go, typeof(GameObject),true);
                item.Image = (Texture)EditorGUILayout.ObjectField("TextureToApply", item.Image, typeof(Texture), false);
                EditorGUILayout.EndVertical();
            }

            
        }
        else

        {
            
            EditorGUILayout.LabelField("There is no elements");
        }

        if (GUI.changed)
        {
            SetObjectDirty(gm.gameObject);
        }


    }

    //пометить сцену измененной
    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene);
    }
}
