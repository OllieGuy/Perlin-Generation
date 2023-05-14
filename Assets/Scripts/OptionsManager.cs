using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    int seed = 0;
    int xSize = 64;
    int ySize = 64;
    int cellSize = 8;
    int octaves = 2;
    float frequency = 1f;
    float amplitude = 3f;
    float persistance = 0.5f;
    float lacunarity = 0.5f;
    [SerializeField] GameObject imageOptions;
    [SerializeField] GameObject meshOptions;
    [SerializeField] GameObject imgPlane;
    [SerializeField] GameObject meshObject;
    [SerializeField] GameObject generator;
    [SerializeField] GameObject seedTextIn;
    [SerializeField] GameObject cam;
    CameraRotation cr;

    void Start()
    {
        cr = cam.GetComponent<CameraRotation>();
    }
    public void onGenerateImageButtonClick()
    {
        cr.resetToStatic();
        meshObject.SetActive(false);
        imgPlane.SetActive(true);
        onChangeSeed();
        getImgSliderValues();
        PerlinGeneration pg = generator.GetComponent<PerlinGeneration>();
        pg.genExternal(true, xSize, ySize, cellSize, octaves, frequency, amplitude, persistance, lacunarity, seed);
        PerlinImage pi = imgPlane.GetComponent<PerlinImage>();
        pi.GenTexture = true;
    }
    public void onGenerateMeshButtonClick()
    {
        imgPlane.SetActive(false);
        meshObject.SetActive(true);
        onChangeSeed();
        getMeshSliderValues();
        PerlinGeneration pg = generator.GetComponent<PerlinGeneration>();
        pg.genExternal(true, xSize, ySize, cellSize, octaves, frequency, amplitude, persistance, lacunarity, seed);
        GroundMeshSetup gm = meshObject.GetComponent<GroundMeshSetup>();
        gm.drawNow = true;
    }
    public void switchMenus()
    {
        imageOptions.SetActive(!imageOptions.activeInHierarchy);
        meshOptions.SetActive(!meshOptions.activeInHierarchy);
        cr.staticCam = !cr.staticCam;
    }

    public void onChangeSeed()
    {
        string inSeed = seedTextIn.GetComponent<TMP_InputField>().text;
        try
        {
            seed = Int32.Parse(inSeed);
        }
        catch (Exception)
        {
            DateTime currentDate = DateTime.Now;
            long ticks = currentDate.Ticks;
            string t = ticks.ToString();
            seed = Int32.Parse(t.Substring(t.Length - 4));
        }
    }
    void getImgSliderValues()
    {
        Slider xSizeSlider = GameObject.Find("xSize Slider").GetComponent<Slider>();
        xSize = MyMath.floor(xSizeSlider.value);
        Slider ySizeSlider = GameObject.Find("ySize Slider").GetComponent<Slider>();
        ySize = MyMath.floor(ySizeSlider.value);
        Slider cellSizeSlider = GameObject.Find("cellSize Slider").GetComponent<Slider>();
        cellSize = MyMath.floor(cellSizeSlider.value);
        Slider octavesSlider = GameObject.Find("octaves Slider").GetComponent<Slider>();
        octaves = MyMath.floor(octavesSlider.value);
        Slider frequencySlider = GameObject.Find("frequency Slider").GetComponent<Slider>();
        frequency = frequencySlider.value;
        Slider amplitudeSlider = GameObject.Find("amplitude Slider").GetComponent<Slider>();
        amplitude = amplitudeSlider.value;
        Slider persistanceSlider = GameObject.Find("persistance Slider").GetComponent<Slider>();
        persistance = persistanceSlider.value;
        Slider lacunaritySlider = GameObject.Find("lacunarity Slider").GetComponent<Slider>();
        lacunarity = lacunaritySlider.value;
    }
    void getMeshSliderValues()
    {
        Slider sizeSlider = GameObject.Find("size Slider").GetComponent<Slider>();
        xSize = MyMath.floor(sizeSlider.value);
        ySize = MyMath.floor(sizeSlider.value);
        Slider cellSizeSlider = GameObject.Find("cellSize Slider").GetComponent<Slider>();
        cellSize = MyMath.floor(cellSizeSlider.value);
        Slider octavesSlider = GameObject.Find("octaves Slider").GetComponent<Slider>();
        octaves = MyMath.floor(octavesSlider.value);
        Slider frequencySlider = GameObject.Find("frequency Slider").GetComponent<Slider>();
        frequency = frequencySlider.value;
        Slider amplitudeSlider = GameObject.Find("amplitude Slider").GetComponent<Slider>();
        amplitude = amplitudeSlider.value;
        Slider persistanceSlider = GameObject.Find("persistance Slider").GetComponent<Slider>();
        persistance = persistanceSlider.value;
        Slider lacunaritySlider = GameObject.Find("lacunarity Slider").GetComponent<Slider>();
        lacunarity = lacunaritySlider.value;

        Slider radiusSlider = GameObject.Find("radius Slider").GetComponent<Slider>();
        cr.radius = radiusSlider.value;
        Slider speedSlider = GameObject.Find("speed Slider").GetComponent<Slider>();
        cr.speed = speedSlider.value;
        Slider offsetSlider = GameObject.Find("offset Slider").GetComponent<Slider>();
        cr.yOffset = offsetSlider.value;
    }
}
