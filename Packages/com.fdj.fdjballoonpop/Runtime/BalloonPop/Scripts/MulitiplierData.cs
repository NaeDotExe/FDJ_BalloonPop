using System;
using UnityEngine;
using System.Collections.Generic;

/*
 Example :

Level 1 :
- Path 1 : Probability 49.55% 
    - Multiplier 1 : Probability 10.05% , Value 1.5
    - Multiplier 2 : Probability 10.06% , Value 2
    - Multiplier 3 : Probability 86.32% , Value 3

- Path 2 : Probability 50.45%
    - Multiplier 1 : Probability 10.05% , Value 1.5
    - Multiplier 2 : Probability 51.57% , Value 2
    - Multiplier 3 : Probability 38.38% , Value 3
 */

namespace BalloonPop
{

    [CreateAssetMenu(fileName = "MultiplierData", menuName = "Data/MultiplierData")]
    public class MultiplierData : ScriptableObject
    {
        // Contains all levels data
        [SerializeField]
        private List<MultiplierLevelData> m_levels = new List<MultiplierLevelData>();

        public List<MultiplierLevelData> Levels
        {
            get { return m_levels; }
        }

        public float GetRandomMultiplier(int levelIndex)
        {
            float value = 1.0f;
            MultiplierLevelData crtLevel = m_levels[levelIndex];

            // select path
            MultiplierPathData pathId = crtLevel.GetRandomPath();

            // select multiplier
            value = pathId.GetRandomMultiplier();

            return value;
        }
        public float GetMinMultiplier(int levelIndex)
        {
            MultiplierLevelData crtLevel = m_levels[levelIndex];
            return crtLevel.PathDatas[0].Multipliers[0].Value;
        }
        public float GetMaxMultiplier(int levelIndex)
        {
            MultiplierLevelData crtLevel = m_levels[levelIndex];
            return crtLevel.PathDatas[0].Multipliers[crtLevel.PathDatas[0].Multipliers.Count - 1].Value;
        }
    }

    // A level contains a list of paths
    [Serializable]
    public class MultiplierLevelData
    {
        [SerializeField]
        private List<MultiplierPathData> m_pathData = new List<MultiplierPathData>();

        public List<MultiplierPathData> PathDatas
        {
            get { return m_pathData; }
        }

        public MultiplierPathData GetRandomPath()
        {
            float rand = UnityEngine.Random.Range(0.0f, 100.0f);
            float cumulativeProbability = 0.0f;
            foreach (var pathData in m_pathData)
            {
                cumulativeProbability += pathData.Probability;
                if (rand <= cumulativeProbability)
                {
                    return pathData;
                }
            }
            // Fallback in case of rounding errors
            return m_pathData[m_pathData.Count - 1];
        }
    }

    // A path contains a probability and a list of multipliers
    [Serializable]
    public class MultiplierPathData
    {
        [Serializable]
        public class MultiplierSubData
        {
            public float Probability;
            public float Value;
        }

        public float Probability;
        public List<MultiplierSubData> Multipliers = new List<MultiplierSubData>();

        public float GetRandomMultiplier()
        {
            float rand = UnityEngine.Random.Range(0.0f, 100.0f);
            float cumulativeProbability = 0.0f;
            foreach (var subData in Multipliers)
            {
                cumulativeProbability += subData.Probability;
                if (rand <= cumulativeProbability)
                {
                    return subData.Value;
                }
            }
            // Fallback in case of rounding errors
            return Multipliers[Multipliers.Count - 1].Value;
        }
    }

}