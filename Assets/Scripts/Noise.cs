using UnityEngine;
using System.Collections;

public static class Noise
{

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // Criando novas sementes
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        //Offset
        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0) {
            scale = 0.01f; // Impedir da escala ser negativa ou nula
        }

        float maxNoiseHeight = float.MinValue; 
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;
        
        // percorrendo e preenchendo o array bi dimensional com os valores
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                // Adicionando as oitavas
                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                //  Impede que o valor exceda o comportado pelo ponto flutuante
                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                // Resultado final do array Perlin Nose
                noiseMap[x, y] = noiseHeight;
            }
        }
        // Normaliza os valores antes de retornar
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }

}
/* O array irá retornar os pontos e posição e a escala do mesmo. Quanto maior a escala mais branco é adicionado
 * Quanto menor, mais preto.
 * 
 *  |--|--|--|--|--|--|--|--|--|
 *  |--|--|--|--|--|--|--|--|--|
 *  |--|--|--|--|--|--|--|--|--|
 *  |--|--|--|--|--|--|--|--|--|
 *  |--|--|--|--|--|--|--|--|--|
 *  |--|--|--|--|--|--|--|--|--|
 *  
 */
