using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[HelpURL("https://docs.unity3d.com/ScriptReference/HeaderAttribute.html")]
public class GameManager : MonoBehaviour {

    [HideInInspector]
    public int HidedInt;

    [SerializeField]
	private int ShownInInspectorPrivateInt;

    
    [Header("Health Settings")]
    public int health = 0;
    public int maxHealth = 100;

    public enum RotationAxisEnum { x, y, z };
	public RotationAxisEnum RotationAxis;





    public List<Item> items;

	void Start () {
		
	}
	 
}

[System.Serializable]
public class Item
{
    public int Id;
    public string Name;
    public Texture Image;
    public GameObject go;
    public void ApplyTexture()
    {
        go.GetComponent<Renderer>().sharedMaterial.mainTexture = Image;
    }

}