using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {
        
        // Textura a ser criada
	public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
	
	public void DrawNoiseMap(Texture2D texture){
        	
        // Aplica a textura no material
		textureRender.sharedMaterial.mainTexture = texture;
		textureRender.transform.localScale = new Vector3 (texture.width, 1, texture.height);

	}

    public void DrawMesh(MeshData meshData, Texture2D texture)  {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }



}