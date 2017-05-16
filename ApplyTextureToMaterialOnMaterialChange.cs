using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTextureToMaterialOnMaterialChange : MonoBehaviour {

    public Renderer thisRenderer;
    public Texture textureWeWantToApply;
    public Shader shaderWeShouldLookOutFor;

    // Use this for initialization
    void Start()
    {
        Material[] tempList = new Material[thisRenderer.materials.Length];

        for( int i = 0; i < thisRenderer.materials.Length; i++ )
        {
            if( thisRenderer.materials[i].shader.Equals(shaderWeShouldLookOutFor))
            {
                Material tempMaterial = thisRenderer.materials[i];
                tempMaterial.SetTexture("_MainTex", textureWeWantToApply);
                tempMaterial.SetInt("_HasTexture", 1);
                tempList[i] = tempMaterial;
            }
            else
            {
                tempList[i] = thisRenderer.materials[i];
            }
        }

        thisRenderer.materials = tempList;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
