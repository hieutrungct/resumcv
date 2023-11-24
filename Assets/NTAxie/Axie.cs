using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace Rubik.Axie
{
    public enum Class
    {
        Knight = 1,
        Warrior = 2,
        Assassin = 3,
        Archer = 4,
        Warlock = 5,
        Bishop = 6,
        Tanker = 7,
        Titan = 8,
        ArchMage = 9,
        Marksman = 10,
    }

    public enum Origin
    {
        Aqua = 1,
        Beast = 2,
        Bird = 3,
        Bug = 4,
        Plant = 5,
        Reptile = 6,
    }
    [System.Serializable]
    public class Axie
    {
        public AxieData AxieData;
        public AxieStats AxieStats;
        public SkeletonAnimation Axie2D;
        public SkeletonGraphic AxieUI;
    }
}