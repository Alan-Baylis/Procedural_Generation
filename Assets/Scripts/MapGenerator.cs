using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode { NoiseMap, ColourMap, Mesh };
    public DrawMode drawMode;

      // Parâmetros a serem ajustados no editor
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public float heightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;
   
    public TerrainType[] regions;

    // Gera o array Perlin Noise
    public void GenerateMap()  {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colourMap = new Color[mapWidth * mapHeight];

        for(int y = 0; y < mapHeight; y++) {
            for(int x = 0; x < mapWidth; x++) {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < regions.Length; i++) {

                    if (currentHeight <= regions[i].height) {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
                
        MapDisplay display = FindObjectOfType<MapDisplay>();
        
        // Exibe a textura aplicada
        if (drawMode == DrawMode.NoiseMap) {
            display.DrawNoiseMap(TextureGenerator.TextureFromHeightMap(noiseMap));

        } else if (drawMode == DrawMode.ColourMap) {
            display.DrawNoiseMap(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));

        } else if (drawMode == DrawMode.Mesh) {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, heightMultiplier, meshHeightCurve),
                TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }

    }

    
    void OnValidate() {
        if (mapWidth < 1) {
            mapWidth = 1;
        }
        if (mapHeight < 1) {
            mapHeight = 1;
        }
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 0) {
            octaves = 0;
        }
    }

}

[System.Serializable]
public struct TerrainType {
    public string name;
    [Range(0, 1)]
    public float height;
    public Color colour;
}