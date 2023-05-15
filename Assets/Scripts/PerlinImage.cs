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
            texture = PG.returnColourTexture(regions);
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
