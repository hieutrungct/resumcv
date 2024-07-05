using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RubikCasual.EnemyInGame
{
    public class ListWaifuEnemy : MonoBehaviour
    {
        public static ListWaifuEnemy instance;
        public List<Enemy> lsEnemyInGame;
        public List<EnemyHeroInGame> lsEnemyHeroInGame;
        public List<Enemy> lsEnemyInMap;
        void Awake()
        {
            instance = this;
        }
    }
}

