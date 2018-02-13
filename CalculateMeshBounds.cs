using UnityEngine;
using System.Collections;

public    class CalculateMeshBounds: MonoBehaviour
{
    public  float xSize;
    public float ySize;
    public float zSize;
    void Start()
    {
        xSize=this.GetComponent<Renderer>().bounds.size.x;
        ySize = this.GetComponent<Renderer>().bounds.size.y;
        zSize = this.GetComponent<Renderer>().bounds.size.z;
    }


}
