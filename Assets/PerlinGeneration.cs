using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using Color = UnityEngine.Color;

public class PerlinGeneration : MonoBehaviour
{
    float[,] heightMap;
    PseudoRNG PR;
    int height;
    int width;
    MyVector2[,] theCellGrid;
    int cellSize;
    Texture2D texture;
    public int seed = 23;
    PseudoRNG pRNG;
    void Start()
    {
        texture = new Texture2D(height, width);
        texture.filterMode = FilterMode.Point;
        generateImgOfNoise(true,64, 64, 8,1f,2,4f,0.5f,2f);
        //simpleGenerateImgOfNoise(true, 64, 64, 8);
    }

    MyVector2[,] gridVectorGeneration(int inHeight, int inWidth, int cellSize, int frequency)
    {
        int heightVecGrid = (inHeight / cellSize) * frequency;
        int widthVecGrid = (inWidth / cellSize) * frequency;
        MyVector2[,] rv = new MyVector2[heightVecGrid+1, widthVecGrid+1];
        System.Random rand = new System.Random(); // REPLACE WITH OWN
        for (int y = 0; y <= heightVecGrid; y++)
        {
            for (int x = 0; x <= widthVecGrid; x++)
            {
                float random = (float)rand.NextDouble();
                double angle = random * 2 * Math.PI;
                rv[y, x] = new MyVector2((float)Math.Sin(angle), (float)Math.Cos(angle));
            }
        }
        return rv;
    }

    MyVector2 generateVectorFromCorner(int x, int y)
    {
        MyVector2 rv;
        double frac = (Math.Sin(x * 2916.68857 + seed) + Math.Sin(y * 2916.68857+ seed)) % 1;
        string stringToPassRNG = frac.ToString().Substring(5,4);
        int seedToPass = Int32.Parse(stringToPassRNG);
        //pRNG = new PseudoRNG(stringToPassRNG);
        //float ranNum = pRNG.genRand(0, 1);
        System.Random rand = new System.Random(seedToPass);
        double ranNum = rand.NextDouble();
        double angle = ranNum * 2 * Math.PI;
        rv = new MyVector2((float)Math.Sin(angle), (float)Math.Cos(angle));
        return rv;
    }

    void simpleGenerateImgOfNoise(bool textureMapping, int xSize, int ySize, int cellSize)
    {
        heightMap = new float[xSize, ySize];
        texture = new Texture2D(xSize, ySize);
        theCellGrid = gridVectorGeneration(xSize, ySize, cellSize, 1);
        float incrementX = ((float)cellSize / (xSize)), incrementY = ((float)cellSize / (ySize));
        float x = 0;
        while (x < theCellGrid.GetLength(0) - 1)
        {
            float y = 0;
            while (y < theCellGrid.GetLength(1) - 1)
            {
                float value = perlin(x, y);
                heightMap[MyMath.floor(x * cellSize), MyMath.floor(y * cellSize)] = value;
                if (textureMapping)
                {
                    Color color = new Color(value, value, value);
                    texture.SetPixel(MyMath.floor(x * cellSize), MyMath.floor(y * cellSize), color);
                }
                y += incrementY;
            }
            x += incrementX;
        }
    }

    void generateImgOfNoise(bool textureMapping, int xSize, int ySize, int cellSize, float frequency, int octaves, float amplitude, float persistance, float lacunarity)
    {
        heightMap = new float[xSize, ySize];
        texture = new Texture2D(xSize, ySize);
        float tmpAmp = amplitude;
        float tmpFreq = frequency;
        float divisor = (float)(1 - Math.Pow(persistance, octaves)) / (1 - persistance);
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                float value = 0;
                for (int oct = 0; oct < octaves; oct++)
                {
                    value += perlinWithPseudoRand((x / (float)cellSize) * tmpFreq, (y / (float)cellSize) * tmpFreq) * tmpAmp;
                    tmpAmp = tmpAmp * persistance;
                    tmpFreq = tmpFreq * lacunarity;
                }
                value = value / (divisor);
                heightMap[x,y] = value;
                if (textureMapping)
                {
                    value = MyMath.lerp(0,1,value/amplitude); //when amplitude > 1, value needs to be normalised
                    Color color = new Color(value, value, value); 
                    texture.SetPixel(x, y, color);
                }
                tmpAmp = amplitude;
                tmpFreq = frequency;
            }
        }

    }

    float perlin(float x, float y)
    {
        int cellX = MyMath.floor(x);
        int cellY = MyMath.floor(y);

        MyVector2 thisVec = new MyVector2(x, y);
        float dot00 = MyVector2.DotProduct(theCellGrid[cellX, cellY],           new MyVector2(thisVec - new MyVector2(cellX, cellY)),           false);
        float dot01 = MyVector2.DotProduct(theCellGrid[cellX, cellY + 1],       new MyVector2(thisVec - new MyVector2(cellX, cellY + 1)),       false);
        float dot10 = MyVector2.DotProduct(theCellGrid[cellX + 1, cellY],       new MyVector2(thisVec - new MyVector2(cellX + 1, cellY)),       false);
        float dot11 = MyVector2.DotProduct(theCellGrid[cellX + 1, cellY + 1],   new MyVector2(thisVec - new MyVector2(cellX + 1, cellY + 1)),   false);

        float fx =          MyMath.smoothStep(x - cellX);
        float fy =          MyMath.smoothStep(y - cellY);
        float dot0 =        MyMath.lerp(dot00, dot10, fx);
        float dot1 =        MyMath.lerp(dot01, dot11, fx);
        float value =       MyMath.lerp(dot0, dot1, fy);
        float finalVal =    (value + 1) / 2;
        return finalVal;
    }

    float perlinWithPseudoRand(float x, float y)
    {
        int cellX = MyMath.floor(x);
        int cellY = MyMath.floor(y);

        MyVector2 thisVec = new MyVector2(x, y);
        float dot00 = MyVector2.DotProduct(generateVectorFromCorner(cellX, cellY),          new MyVector2(thisVec - new MyVector2(cellX, cellY)),           false);
        float dot01 = MyVector2.DotProduct(generateVectorFromCorner(cellX, cellY + 1),      new MyVector2(thisVec - new MyVector2(cellX, cellY + 1)),       false);
        float dot10 = MyVector2.DotProduct(generateVectorFromCorner(cellX + 1, cellY),      new MyVector2(thisVec - new MyVector2(cellX + 1, cellY)),       false);
        float dot11 = MyVector2.DotProduct(generateVectorFromCorner(cellX + 1, cellY + 1),  new MyVector2(thisVec - new MyVector2(cellX + 1, cellY + 1)), false);

        float fx = MyMath.smoothStep(x - cellX);
        float fy = MyMath.smoothStep(y - cellY);
        float dot0 = MyMath.lerp(dot00, dot10, fx);
        float dot1 = MyMath.lerp(dot01, dot11, fx);
        float value = MyMath.lerp(dot0, dot1, fy);
        float finalVal = (value + 1) / 2;
        return finalVal;
    }

    public float[,] returnHeightMap()
    {
        return heightMap;
    }

    public float[,] returnNormalisedHeightMap()
    {
        float[,] returnedHeightMap = new float[heightMap.GetLength(0), heightMap.GetLength(1)];
        for (int x = 0; x < heightMap.GetLength(0); x++)
        {
            for (int y = 0; y < heightMap.GetLength(1); y++)
            {
                returnedHeightMap[x,y] = MyMath.lerp(0, 1, heightMap[x,y] / 4f); //hard coded to test if it worked
                Debug.Log(returnedHeightMap[x, y]);
            }
        }
        return returnedHeightMap;
    }

    public Texture2D returnTexture()
    {
        return texture;
    }
}
