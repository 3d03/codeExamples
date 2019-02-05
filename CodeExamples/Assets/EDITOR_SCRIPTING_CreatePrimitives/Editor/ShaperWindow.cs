using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shapes;

public class ShaperWindow : EditorWindow{

    private Rect shapeIconSection;
    private Texture2D shapeIconTexture;

    private Texture2D currentShape;

    private Texture2D cubeIcon;
    private Texture2D sphereIcon;
    private Texture2D capsuleIcon;
    private Texture2D cylinderIcon;


    private ShapeTypes shapeType;
    private Vector3 shapePos;
    private Vector3 shapeRot;

    private Color shapeColor;

    private float  shapeScale;
    private string shapeName;

    private bool showRandomOptions;
    private float minPos;
    private float maxPos;
    private float maxRot;
    private float maxScale;

    private bool ShowSpacingOptions;
    private int numOfShapes;
    private Vector3 spacing;

    [MenuItem("Window/Shape Designer")]
	static void OpenWindow()
    {
        ShaperWindow window = (ShaperWindow)GetWindow(typeof(ShaperWindow));
        window.minSize = new Vector2Int(150, 150);
        window.Show();
            
    }

    private void OnEnable()
    {
        InitTextures();
    }
    private void InitTextures()
    {
        shapeIconTexture = new Texture2D(1, 1);
        shapeIconTexture.SetPixel(0, 0, Color.blue);
        shapeIconTexture.Apply();
        shapeName = "Test Cube";

        cubeIcon = Resources.Load<Texture2D>("icons/cube_icon");
        sphereIcon= Resources.Load<Texture2D>("icons/sphere_icon");
        capsuleIcon = Resources.Load<Texture2D>("icons/capsule_icon");
        cylinderIcon= Resources.Load<Texture2D>("icons/cylinder_icon");
    }
    private void OnGUI()
    {
        ChangeShape();
        DrawLayouts();
        DrawShapeSettings();
        
    }
    private void ChangeShape()
    {
        switch(shapeType)
        {
            case ShapeTypes.CUBE:
                {
                    currentShape = cubeIcon;
                }
                break;
            case ShapeTypes.SPHERE:
                {
                    currentShape = sphereIcon;
                }
                break;
            case ShapeTypes.CAPSULE:
                {
                    currentShape = capsuleIcon;
                }
                break;
            case ShapeTypes.CYLINDER:
                {
                    currentShape = cylinderIcon;
                }
                break;
        }
    }


    private void DrawLayouts()
    {
        shapeIconSection.x = 15; // Screen.width/10f;
        shapeIconSection.y = 20;//Screen.height /3f;
        shapeIconSection.width = 100;
        shapeIconSection.height = 100;
        GUI.DrawTexture(shapeIconSection, currentShape);
    }


    private void DrawShapeSettings()
    {
        GUILayout.Label("Create your shapes");
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        shapeType = (ShapeTypes)EditorGUILayout.EnumPopup(shapeType);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        shapePos = (Vector3)EditorGUILayout.Vector3Field("Shape Position:", shapePos);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        shapeRot = (Vector3)EditorGUILayout.Vector3Field("Shape Rotation:", shapeRot);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Shape scale: ");
        shapeScale = (float)EditorGUILayout.Slider(shapeScale, 1, 10);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        shapeColor = EditorGUILayout.ColorField("Shape color:", shapeColor);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        shapeName = (string)EditorGUILayout.TextField("Shape name:", shapeName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        showRandomOptions = EditorGUILayout.Foldout(showRandomOptions, "Show variable options");
        if (showRandomOptions)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            minPos = EditorGUILayout.FloatField("Minimum Position Value", minPos);
            maxPos = EditorGUILayout.FloatField("Maximum Position Value", maxPos);
            maxRot = EditorGUILayout.FloatField("Maximum Rotation  Value", maxRot);
            maxScale = EditorGUILayout.FloatField("Maximum Scale Value", maxScale);
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        ShowSpacingOptions = EditorGUILayout.Foldout(ShowSpacingOptions, "Show spacing options");
        if (ShowSpacingOptions)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            numOfShapes = EditorGUILayout.IntField("Num of shapes", numOfShapes);
            

            
            spacing = EditorGUILayout.Vector3Field ("Spacing:", spacing);
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();



        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Randomize Variables"))
        {
            CreateShape(true);
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Shape"))
            {
                CreateShape(false);
            }
        EditorGUILayout.EndHorizontal();


        if (shapeName.Length==0)
        {
            EditorGUILayout.HelpBox("The shape needs a name", MessageType.Warning);
        }
        else
        {
             if (shapeName.StartsWith(" "))
            {
                EditorGUILayout.HelpBox("Name cannot start with a space", MessageType.Warning);
            }
        }

    }


    private void CreateShape(bool Randomize )
    {
        if ((shapeName.Length == 0) || shapeName.StartsWith(" "))
        {
            return;
        } 

        switch (shapeType)
        {
            case ShapeTypes.CUBE:
                {
                    if (!Randomize) 
                    {
                        if (numOfShapes > 0)
                        {

                            for (int i=0; i <numOfShapes; i++)
                            {
                                GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                shape.transform.position = spacing * i;

                                var newMaterial = new Material(shape.GetComponent<Renderer>().sharedMaterial);
                                newMaterial.color = shapeColor;
                                shape.GetComponent<Renderer>().sharedMaterial = newMaterial;
                                shape.name = shapeName+ " "+i.ToString();
                            }
                        }
                        else
                        {
                            GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            SetTransform(shape);
                        }
                    }
                    else
                    {
                        GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        RandomizeTransform(shape);
                    }

                    
                    break;
                }
            case ShapeTypes.SPHERE:
                {
                    
                    if (!Randomize)
                    {
                        if (numOfShapes > 0)
                        {

                            for (int i = 0; i < numOfShapes; i++)
                            {
                                GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                shape.transform.position = spacing * i;

                                var newMaterial = new Material(shape.GetComponent<Renderer>().sharedMaterial);
                                newMaterial.color = shapeColor;
                                shape.GetComponent<Renderer>().sharedMaterial = newMaterial;
                                shape.name = shapeName + " " + i.ToString();
                            }
                        }
                        else
                        {
                            GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            SetTransform(shape);
                        }
                    }
                    else
                    {
                        GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        RandomizeTransform(shape);
                    }
                    break;
                }
            case ShapeTypes.CAPSULE:
                {
                    
                    if (!Randomize)
                    {
                        if (numOfShapes > 0)
                        {

                            for (int i = 0; i < numOfShapes; i++)
                            {
                                GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                                shape.transform.position = spacing * i;

                                var newMaterial = new Material(shape.GetComponent<Renderer>().sharedMaterial);
                                newMaterial.color = shapeColor;
                                shape.GetComponent<Renderer>().sharedMaterial = newMaterial;
                                shape.name = shapeName + " " + i.ToString();
                            }
                        }
                        else
                        {
                            GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                            SetTransform(shape);
                        }
                    }
                    else
                    {
                        GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        RandomizeTransform(shape);
                    }
                    break;
                }
            case ShapeTypes.CYLINDER:
                {
                    
                    if (!Randomize)
                    {
                        if (numOfShapes > 0)
                        {

                            for (int i = 0; i < numOfShapes; i++)
                            {
                                GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                                shape.transform.position = spacing * i;

                                var newMaterial = new Material(shape.GetComponent<Renderer>().sharedMaterial);
                                newMaterial.color = shapeColor;
                                shape.GetComponent<Renderer>().sharedMaterial = newMaterial;
                                shape.name = shapeName + " " + i.ToString();
                            }
                        }
                        else
                        {
                            GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                            SetTransform(shape);
                        }
                    }
                    else
                    {
                        GameObject shape = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        RandomizeTransform(shape);
                    }
                    break;
                }

        }
    }


    private void RandomizeTransform(GameObject NewShape)
    {
        float newPos = Random.Range(minPos, maxPos);
        float newRot = Random.Range(0, maxRot);
        float newScale = Random.Range(0, maxScale);
        NewShape.transform.position = Vector3.one * newPos;
        NewShape.transform.eulerAngles = Vector3.one * newRot;
        NewShape.transform.localScale = Vector3.one * newScale;

        var newMaterial = new Material(NewShape.GetComponent<Renderer>().sharedMaterial);
        newMaterial.color = shapeColor;
        NewShape.GetComponent<Renderer>().sharedMaterial = newMaterial;

        NewShape.name = shapeName;
    }


    private void SetTransform(GameObject NewShape)
    {
        NewShape.transform.position = shapePos;
        NewShape.transform.eulerAngles = shapeRot;
        NewShape.transform.localScale = Vector3.one * shapeScale;

        var newMaterial = new Material(NewShape.GetComponent<Renderer>().sharedMaterial);
        newMaterial.color = shapeColor;
        NewShape.GetComponent<Renderer>().sharedMaterial = newMaterial;

        NewShape.name = shapeName; 
    }


}
