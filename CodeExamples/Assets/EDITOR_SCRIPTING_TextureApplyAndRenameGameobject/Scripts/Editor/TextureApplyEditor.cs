using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

namespace EDITOR_SCRIPTING { 


    [CustomEditor(typeof(TextureApplier))]
    public class TextureApplyEditor : Editor {

        private TextureApplier ta;

        public bool myBool;
        public void OnEnable()
        {
            ta = (TextureApplier)target;
        }
        public override void OnInspectorGUI()
        {
      
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  Info", MessageType.Info);
            EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  Error", MessageType.Error);
            EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  Warning", MessageType.Warning);
            EditorGUILayout.HelpBox("This is EditorGUILayout.HelpBox  None", MessageType.None);


            EditorGUILayout.Space();
            if (GUILayout.Button("Add", GUILayout.Height(40))) ta.textureApplyInfos.Add(new TextureApplyInfo());
            EditorGUILayout.Space();
            if (GUILayout.Button("Delete the first"))

            {
                if (ta.textureApplyInfos.Count>0 )
                {
                    ta.textureApplyInfos.RemoveAt(0);
                }
            }

            EditorGUILayout.Space(); EditorGUILayout.Space();
            EditorGUILayout.Space(); EditorGUILayout.Space();
            if (ta.textureApplyInfos.Count>0)
            {

                foreach (TextureApplyInfo item in ta.textureApplyInfos)
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button("X", GUILayout.Width(65), GUILayout.Height(35)))
                    {
                        ta.textureApplyInfos.Remove(item);
                        break;
                    }

                    EditorGUILayout.Space();
                    if (GUILayout.Button("Apply Texture", GUILayout.Width(105), GUILayout.Height(35)))
                    {
                        if ((item.Image != null)&& (item.GO_toApply != null))
                        {
                            item.ApplyTexture();
                            item.GO_toApply.name = item.Image.name+"_GameObject";
                        }
                        else
                        {
                            if (item.Image == null)
                            {
                                Debug.LogWarning("Not assigned texture");
                            }

                            if (item.GO_toApply == null)
                            {
                                Debug.LogWarning("Not assigned gameobject");
                            }
                        }
                    }

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    item.GO_toApply = (GameObject) EditorGUILayout.ObjectField("GameObjectToApply",  item.GO_toApply, typeof(GameObject),true);
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
                SetObjectDirty(ta.gameObject);
            }
        }

        //mark scene as changed
        public static void SetObjectDirty(GameObject obj)
        {
            EditorUtility.SetDirty(obj);
            EditorSceneManager.MarkSceneDirty(obj.scene);
        }
    }
}