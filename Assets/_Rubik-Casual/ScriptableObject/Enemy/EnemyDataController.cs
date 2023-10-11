using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace RubikCasual.EnemyData
{
    [CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObject/EnemyData")]
    public class EnemyDataController : ScriptableObject
    {
        public List<Enemy> enemy;
        public List<ColorEnemy> colorEnemies;
    }
    [Serializable]
    public class ColorEnemy
    {
        public int idColor;
        public Color backgroundColor, frameColor;
        public Sprite backgroundValue;
    }
    [Serializable]
    public class Enemy
    {
        public int idEnemy, idColor;
        public string NameEnemyid, skinName, Name;
        public CharacterType CharacterType;
        public EffectType EffectType;
        public Rare Rarity;
        public int Role;
        public List<SkillName> skills;
        public int Critical, AttackDamage, Depense, Health, MoveSpeed, numberStar, Ascend, Level, Exp, MaxExp, Target = 1;


    }
    [Serializable]
    public enum CharacterType
    {
        Melee = 0,
        Ranged = 1
    }
    public enum EffectType
    {
        None = -1,
        Frost = 0,
        Fire = 1,
        Poison = 2,
        Lightning = 3,
        Multi
    }

    public enum Rare
    {
        UnCommon = 0,
        Common = 1,
        Rare = 2,
        Epic = 3,
        Legend = 4
    }
    public enum SortingType
    {
        None,
        Lever,
        Rarity,
        Power
    }


}