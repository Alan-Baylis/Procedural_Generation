using UnityEngine;
using System.Collections;

public static class TextureGenerator {

    public static Texture2D TextureFromColourMap(Color[] colourMap, int mapWidth, int mapHeight)  {

        Texture2D texture = new Texture2D(mapWidth, mapHeight);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap) {

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
              
        Color[] colourMap = new Color[width * height];

        // Preenchendo o mapa com os pixels
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++)  {
                // Faz a interpolação das cores
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }

        }
    
        return TextureFromColourMap(colourMap, width, height);

    }

}
