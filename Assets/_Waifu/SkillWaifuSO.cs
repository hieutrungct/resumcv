using RubikCasual.CreateSkill;
using Spine.Unity;
using UnityEngine;

namespace RubikCasual.Waifu
{

    [CreateAssetMenu(fileName = "NewInfoWaifuSO", menuName = "ScriptableObject/WaifuSO/New InfoWaifuSO", order = 0)]
    public class SkillWaifuSO : ScriptableObject
    {
        public int Index;
        public string Code;
        public float percentDameSkill;
        public int Row;
        public int Column;
        public TypeSkill typeSkill;
        public int NumberTurn;
        public float DurationAttacked;
        public float DurationWave;

    }
}