using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureApplier : MonoBehaviour {

    public List<TextureApplyInfo> textureApplyInfos;
}

[System.Serializable]
public class TextureApplyInfo
{
    public Texture Image;
    public GameObject GO_toApply;
    public void ApplyTexture()
    {
        GO_toApply.GetComponent<Renderer>().sharedMaterial.mainTexture = Image;
    }

}