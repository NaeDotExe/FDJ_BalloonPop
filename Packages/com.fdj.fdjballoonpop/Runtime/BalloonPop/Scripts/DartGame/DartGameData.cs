using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DartGameData", menuName = "Scriptable Objects/DartGameData")]
public class DartGameData : ScriptableObject
{
    public List<DartLevelData> LevelDatas;
    public MultiplierData MultiplierData;

    public int LevelsCount
    {
        get { return LevelDatas.Count; }
    }

    public int GetBalloonsToPop(int level)
    {
        if (level < 0 || level >= LevelDatas.Count)
        {
            Debug.LogError("Invalid level index");
            return 0;
        }
        return LevelDatas[level].BalloonsToPop;
    }

    public float GetRandomMultiplier(int levelIndex)
    {
        return MultiplierData.GetRandomMultiplier(levelIndex);
    }

    public float GetMinMultiplier(int levelIndex)
    {
        return MultiplierData.GetMinMultiplier(levelIndex);
    }
    public float GetMaxMultiplier(int levelIndex)
    {
        return MultiplierData.GetMaxMultiplier(levelIndex);
    }

}
