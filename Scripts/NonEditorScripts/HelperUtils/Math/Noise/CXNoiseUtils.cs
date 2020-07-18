﻿using UnityEngine;

namespace CXUtils.CodeUtils
{
    ///<summary> A simple noise helper class </summary>
    public class NoiseUtils : IBaseUtils
    {
        #region Perlin noise

        /// <summary> Procedural noise generation, Perlin noise (scale cannot be 0)
        /// <para>QUICK NOTE:seed will be default to 0</para></summary>
        public static float PerlinNoise(float x, float y, float scale, float? seed = null)
        {
            float currentSeed = seed ?? 0;
            return Mathf.PerlinNoise(x / scale + currentSeed, y / scale + currentSeed);
        }

        /// <summary> Procedural noise generation, Perlin noise (scale cannot be 0)
        /// <para>QUICK NOTE:seed will be default to 0</para></summary>
        public static float PerlinNoise(Vector2 position, float scale, float? seed = null) =>
            PerlinNoise(position.x, position.y, scale, seed);

        /// <summary> Generates a boolean value that the threshHold gives.
        /// <para>QUICK NOTEs:</para>
        /// <para>clamps threshHold value between 0 ~ 1</para>
        /// <para>seed will be default to 0</para></summary>
        public static bool PerlinNoise_FlipCoin(float x, float y, float scale, float threshHold = .5f,
         float? seed = null)
        {
            threshHold = Mathf.Clamp01(threshHold);
            return PerlinNoise(x, y, scale, seed) > threshHold;
        }

        /// <summary> Generates a boolean value that the threshHold gives.
        /// <para>QUICK NOTEs:</para>
        /// <para>clamps threshHold value between 0 ~ 1</para>
        /// <para>seed will be default to 0</para></summary>
        public static bool PerlinNoise_FlipCoin(Vector2 position, float scale, float threshHold = .5f,
         float? seed = null) =>
            PerlinNoise_FlipCoin(position.x, position.y, scale, threshHold, seed);

        #endregion

        #region Noise maps

        ///<summary> Generates a simple perlin noise map and returns the int array 
        ///<para>QUICK NOTE: format: float[x, y]</para></summary>
        public static float[,] PerlinNoiseMap(int width, int height, int start_x, int start_y, float scale,
         float? seed = null)
        {
            float[,] map = new float[width, height];

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    map[x, y] = PerlinNoise(x + start_x, y + start_y, scale, seed);

            return map;
        }

        ///<summary> Combines two perlin noise maps 
        ///<para>QUICK NOTE: returns a 0 by 0 map if failed</para> </summary>
        public static bool TryCombinePerlinNoiseMap(float[,] map1, float[,] map2, out float[,] noiseMap)
        {
            int MP1_Width = map1.GetLength(0);
            int MP1_Height = map1.GetLength(1);

            if (!MP1_Width.Equals(map2.GetLength(0)) || !MP1_Height.Equals(map2.GetLength(1)))
            {
                noiseMap = default;
                return false;
            }

            noiseMap = new float[MP1_Width, MP1_Height];

            //*else
            for (int x = 0; x < MP1_Width; x++)
                for (int y = 0; y < MP1_Height; y++)
                    noiseMap[x, y] = map1[x, y] * map2[x, y];

            return true;
        }

        #endregion

    }

}
