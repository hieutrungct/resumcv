using System.Collections;
using System.Collections.Generic;
using RubikCasual.Combat;
using RubikCasual.Combat.Character;
using Spine.Unity.Editor;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public bool isStartTurn;
    public List<CharacterCombatUI> slotHeroClone, slotEnemyClone;
    public static GamePlay instance;
    void Awake()
    {
        instance = this;

    }
    void Start()
    {

        slotEnemyClone = CombatController.instance.slotEnemyClone;
        slotHeroClone = CombatController.instance.slotHeroClone;

    }
    void Update()
    {
        if (i == 4)
        {
            i = 0;
        }
        // Input.GetKeyDown(KeyCode.Space) && 
        if (!isStartTurn)
        {

            isStartTurn = true;
            GamePlayStart();
            i++;
        }
    }
    Vector3 backTurnPos;
    float timeScale;
    int i = 0;
    public void GamePlayStart()
    {
        var turnIndex = i;
        var attackIndex = Random.Range(0, 2);
        var turn = slotHeroClone[turnIndex];
        if (turn.characterInCombat.startingAnimation == "Idle")
        {
            turn.doneTurn = false;
            timeScale = turn.characterInCombat.timeScale;
            backTurnPos = turn.transform.position;
            var attack = slotEnemyClone[attackIndex];

            StartCoroutine(PerformTurn(turn, attack));
        }
        else
        {
            isStartTurn = false;
            return;
        }
    }

    IEnumerator PerformTurn(CharacterCombatUI turn, CharacterCombatUI attack)
    {
        yield return new WaitForSeconds(1f);

        turn.transform.position = attack.transform.position - new Vector3(2f, 0, 1f);


        var nhanPham = Random.Range(0, 4);
        turn.characterInCombat.timeScale = 1;
        turn.characterInCombat.startingLoop = false;
        if (nhanPham == 1)
        {
            turn.characterInCombat.startingAnimation = "SkillCast";
            SpineEditorUtilities.ReinitializeComponent(turn.characterInCombat);
        }
        else
        {
            turn.characterInCombat.startingAnimation = "Attack";
            SpineEditorUtilities.ReinitializeComponent(turn.characterInCombat);
        }
        attack.characterInCombat.startingLoop = false;
        attack.characterInCombat.startingAnimation = "Attacked";
        SpineEditorUtilities.ReinitializeComponent(attack.characterInCombat);
        yield return new WaitForSeconds(0.7f);

        attack.characterInCombat.startingLoop = true;
        attack.characterInCombat.startingAnimation = "Idle";
        SpineEditorUtilities.ReinitializeComponent(attack.characterInCombat);

        yield return new WaitForSeconds(0.6f);

        turn.characterInCombat.startingAnimation = "Idle";
        turn.characterInCombat.startingLoop = true;
        SpineEditorUtilities.ReinitializeComponent(turn.characterInCombat);



        yield return new WaitForSeconds(1f);

        EndTurn(turn);
        turn.doneTurn = true;
    }

    void EndTurn(CharacterCombatUI turn)
    {
        turn.characterInCombat.timeScale = timeScale;
        turn.transform.position = backTurnPos;
        isStartTurn = false;
    }
}
