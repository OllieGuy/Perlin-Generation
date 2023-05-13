using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using Color = UnityEngine.Color;

public class PerlinImage : MonoBehaviour
{
    [SerializeField] GameObject PGO;
    PerlinGeneration PG;
    Texture2D texture;
    public bool GenTexture;
    public bool GenColourMap;
    public TerrainType[] regions;
    void Start()
    {
        PG = PGO.GetComponent<PerlinGeneration>();
        GenTexture = false;
        GenColourMap = false;
    }
    void Update()
    {
        if (GenTexture)
        {
            texture = PG.returnTexture();
            texture.filterMode = FilterMode.Point;
            GetComponent<Renderer>().material.mainTexture = texture;
            texture.Apply();
            GenTexture = false;
        }
        if (GenColourMap)
        {
            float[,] heightMap = PG.returnNormalisedHeightMap();
            int xSize = heightMap.GetLength(0);
            int zSize = heightMap.GetLength(1);
            Texture2D texture = new Texture2D(xSize, zSize);
            for (int x = 0; x < xSize; x++)
            {
                for (int z = 0; z < zSize; z++)
                {
                    Color color = Color.red;
                    for (int i = 1; i < regions.Length; i++)
                    {
                        if (heightMap[z, x] >= regions[i-1].height && heightMap[z, x] <= regions[i].height)
                        {
                            color = regions[i].color;
                        }
                        texture.SetPixel(z, x, color);
                    }
                }
            }
            texture.filterMode = FilterMode.Point;
            GetComponent<Renderer>().material.mainTexture = texture;
            texture.Apply();
            GenColourMap = false;
        }
    }
}


[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
