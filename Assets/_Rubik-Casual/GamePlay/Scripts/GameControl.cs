using System.Collections;
using System.Collections.Generic;
using Pixelplacement.TweenSystem;
using Rubik.Axie;
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
            foreach (var hero in BattleController.instance.mapBattleController.lsPosHeroSlot.lsPosCharacterSlot)
            {
                // CharacterInBattle hero = gbhero.GetComponent<CharacterInBattle>();
                if (pos.x < hero.transform.position.x + 1f && pos.x > hero.transform.position.x - 1f && pos.y < hero.transform.position.y + 1f && pos.y > hero.transform.position.y - 1f)
                {
                    //Debug.Log(pos);
                    return index;
                }
                index++;
            }
            return -1;
        }
        public void swapCharacter(int indexOri, int indexSwap)
        {
            MapBattleController map = BattleController.instance.mapBattleController;
            GameObject characterGbOri = BattleController.instance.lsSlotGbHero[indexOri];
            characterGbOri.transform.SetParent(map.lsPosHeroSlot.lsPosCharacterSlot[indexSwap].transform);
            characterGbOri.transform.position = map.lsPosHeroSlot.lsPosCharacterSlot[indexSwap].transform.position;

            CharacterInBattle characterOri = characterGbOri.GetComponent<CharacterInBattle>();
            characterOri.indexOfSlot = indexSwap;

            if (BattleController.instance.lsSlotGbHero[indexSwap] != null)
            {
                GameObject characterGbSwap = BattleController.instance.lsSlotGbHero[indexSwap];
                GameObject Swap = characterGbSwap;
                characterGbSwap.transform.SetParent(map.lsPosHeroSlot.lsPosCharacterSlot[indexOri].transform);
                characterGbSwap.transform.position = map.lsPosHeroSlot.lsPosCharacterSlot[indexOri].transform.position;
                CharacterInBattle characterSwap = characterGbSwap.GetComponent<CharacterInBattle>();
                BattleController.instance.lsSlotGbHero[indexOri] = Swap;
                characterSwap.indexOfSlot = indexOri;
                characterSwap.skeletonCharacterAnimation.gameObject.GetComponent<CharacterDragPosition>().oriPos = characterSwap.skeletonCharacterAnimation.gameObject.GetComponent<CharacterDragPosition>().transform.position;
                characterSwap.skeletonCharacterAnimation.gameObject.GetComponent<CharacterDragPosition>().oriIndex = indexOri;
            }
            else
            {
                if (indexSwap != indexOri)
                {
                    BattleController.instance.lsSlotGbHero[indexOri] = null;
                }
            }

            BattleController.instance.lsSlotGbHero[indexSwap] = characterGbOri;


        }

    }
}
