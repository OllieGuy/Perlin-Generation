using System;
using System.Collections;
using System.Collections.Generic;

public class MyMath
{
    public static int floor(float input)
    {
        int rv = (int)input;
        if (input < 0 && input != rv)
        {
            rv--;
        }
        return rv;
    }
    public static float lerp(float inputNumberA, float inputNumberB, float lerpVal)
    {
        float rv = 0;
        rv = (inputNumberA * (1 - lerpVal)) + (inputNumberB * lerpVal);
        return rv;
    }

    public static float smoothStep(float val)
    {
        return val * val * (3f - 2f * val);
    }
}

public class PseudoRNG
{
    string seed = "0000";
    public PseudoRNG(string inSeed)
    {
        seed = inSeed;
    }
    public float genRand(int min, int max)
    {
        string numGenBySeed = seed.Substring(1, 2);
        int theSeed = Int32.Parse(numGenBySeed);
        float passNum = theSeed / 100f;
        float rv = MyMath.lerp(min,max, passNum);
        seed = (theSeed * theSeed).ToString();
        return rv;
    }
}

