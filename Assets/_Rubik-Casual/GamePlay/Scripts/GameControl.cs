using System.Collections;
using System.Collections.Generic;
using Pixelplacement.TweenSystem;
using RubikCasual.Lobby;
using UnityEngine;

namespace RubikCasual.Battle
{
    public enum GameState
    {
        START = 0,
        WAIT_BATTLE = 1,
        BATTLE = 2,
        END = 3
    }
    public class GameControl : MonoBehaviour
    {
        float duaration = 10f;

        public static GameControl instance;

        void Awake()
        {
            instance = this;
        }
        public int CheckNearPos(Vector2 pos)
        {
            int index = 0;
            foreach (GameObject gbhero in BattleController.instance.lsSlotGbHero)
            {
                CharacterInBattle hero = gbhero.GetComponent<CharacterInBattle>();
                if (hero.indexOfSlot < 5)
                {
                    if (pos.x < hero.transform.position.x + 1f && pos.x > hero.transform.position.x - 1f && pos.y < hero.transform.position.y + 1f && pos.y > hero.transform.position.y - 1f)
                    {
                        //Debug.Log(pos);
                        return index;
                    }
                }
                else
                {
                    if (pos.x < hero.transform.position.x + 1f && pos.x > hero.transform.position.x - 1f && pos.y < hero.transform.position.y + .7f && pos.y > hero.transform.position.y - .7f)
                    {
                        //Debug.Log(pos);
                        return index;
                    }
                }
                index++;
            }
            return -1;
        }
        public void swapCharacter(int indexOri, int indexSwap)
        {
            MapBattleController map = BattleController.instance.mapBattleController;

            GameObject characterGbOri = BattleController.instance.lsSlotGbHero[indexOri];
            GameObject characterGbSwap = BattleController.instance.lsSlotGbHero[indexSwap];
            GameObject Swap = characterGbSwap;

            characterGbOri.transform.SetParent(map.lsPosHeroSlot.lsPosCharacterSlot[indexSwap].transform);
            characterGbOri.transform.position = map.lsPosHeroSlot.lsPosCharacterSlot[indexSwap].transform.position;

            characterGbSwap.transform.SetParent(map.lsPosHeroSlot.lsPosCharacterSlot[indexOri].transform);
            characterGbSwap.transform.position = map.lsPosHeroSlot.lsPosCharacterSlot[indexOri].transform.position;

            CharacterInBattle characterOri = characterGbOri.GetComponent<CharacterInBattle>();
            CharacterInBattle characterSwap = characterGbSwap.GetComponent<CharacterInBattle>();

            characterSwap.indexOfSlot = indexOri;
            characterOri.indexOfSlot = indexSwap;

            BattleController.instance.lsSlotGbHero[indexSwap] = characterGbOri;
            BattleController.instance.lsSlotGbHero[indexOri] = Swap;


        }

    }
}
