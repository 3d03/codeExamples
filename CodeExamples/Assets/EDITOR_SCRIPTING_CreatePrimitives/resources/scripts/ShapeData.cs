using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Shapes;

[CreateAssetMenu(fileName ="new shape Data", menuName ="Shape Data")]
public class ShapeData : ScriptableObject {

    public ShapeTypes _shapeTypes;
    public Vector3 pos;
    public string ShapeName;
}
