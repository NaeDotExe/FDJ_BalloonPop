using System;
using UnityEngine;

namespace BalloonPop
{

    [Serializable]
    public class DartLevelData
    {
        public int TotalBallons;
        public int ValidBallons;
        public int BalloonsToPop;
        public float Frequency;
        public float Chance;

        //public int TrappedBallons
        //{
        //    get { return TotalBallons - ValidBallons; }
        //}
    }

}