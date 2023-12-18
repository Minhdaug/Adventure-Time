using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    private enum BattleState { START, PLAYERTURN, ENEMYTURN, VICTORY, DEFEAT }

    [SerializeField] List<GameObject> playerPrefab;
	[SerializeField] List<GameObject> enemyPrefab;

    [SerializeField] List<Transform> playerTransforms;
	[SerializeField] List<Transform> enemyTransforms;

    [SerializeField] int actionDisplayLimit;

    [SerializeField] GameObject HUD;
    private int avUpdateValue = 0;

    public List<Transform> PlayerTransforms { get; private set; }

    //private GameObject playerUnitsGO;
	//private GameObject enemyUnitsGO;

    private List<GameObject> goList = new List<GameObject>();

    //protected Queue<Tuple<int, string>> actionValue = new Queue<Tuple<int, string>>();
    private Queue<GameObject> actionQueue = new Queue<GameObject>();

    public Queue<GameObject> ActionQueue { get; private set; }

	private BattleState state = BattleState.START;

    // Start is called before the first frame update
    void Start()
    {
        if (state == BattleState.START)
        {
			SetupBattle();
		} 
        
    }

    void SetupBattle()
    {
        for (int i = 0; i < playerPrefab.Count; i++) {
            GameObject newPlayerGO = Instantiate(playerPrefab[i], playerTransforms[i]);
            SpriteRenderer newSR = newPlayerGO.GetComponent<SpriteRenderer>();
            newSR.sortingOrder = i;

            //Unit tempGO = newPlayerGO.GetComponent<Unit>();
            //tempGO.CalculateActionValue();
            newPlayerGO.GetComponent<Unit>().CalculateActionValue();

            goList.Add(newPlayerGO);
        }

        for (int i = 0; i < enemyPrefab.Count; i++)
        {
            GameObject newEnemyGO = Instantiate(enemyPrefab[i], enemyTransforms[i]);
            SpriteRenderer newSR = newEnemyGO.GetComponent<SpriteRenderer>();
			switch (i)
			{
				case 0:
					newSR.sortingOrder = 1;
					break;
                case 1:
                    newSR.sortingOrder = 0;
                    break;
                case 2:
                    newSR.sortingOrder = 2;
                    break;
                case 3:
                    newSR.sortingOrder = 0;
                    break;
                case 4:
                    newSR.sortingOrder = 2;
                    break;
			}

            //Unit tempGO = newEnemyGO.GetComponent<Unit>();
            newEnemyGO.GetComponent<Unit>().CalculateActionValue();
			goList.Add(newEnemyGO);
		}
        UpdateActionList();
    }
    //public void InitActionValue()
    //{
    //    foreach (GameObject go in goList)
    //    {
    //        go.GetComponent<Unit>().CalculateActionValue();
    //    }
    //}
    public void UpdateActionList()
    {
        while (actionQueue.Count < actionDisplayLimit)
        {
			//List<Unit> newList = new List<Unit>();

			goList.Sort((unit1, unit2) => unit1.GetComponent<Unit>().CurrentActionValue.CompareTo(unit2.GetComponent<Unit>().CurrentActionValue));
            avUpdateValue = goList[0].GetComponent<Unit>().CurrentActionValue;

			foreach (GameObject go in goList)
			{
				Unit tempGOUnit = go.GetComponent<Unit>();
				tempGOUnit.CalculateActionValue(avUpdateValue);
			}

            actionQueue.Enqueue(goList[0]);

            //Debug.Log($"Turn {actionQueue.Count} AV:");

            //foreach (GameObject go in goList)
            //{
            //    Debug.Log($"{go.name}, AV = {go.GetComponent<Unit>().CurrentActionValue}");
            //}
            goList[0].GetComponent<Unit>().CalculateActionValue();
        }

        //hud.UpdateSpeedFrames(actionQueue.ToList());
        //Debug.Log($"{HUD.GetType()}");
        HUD.GetComponent<HUDSystem>().UpdateSpeedFrames(actionQueue.ToList());
        //Debug.Log("Action order:");
        //Debug.Log($"{actionQueue.Count}");


        //foreach (GameObject go in actionQueue)
        //{
        //    Unit unit = go.GetComponent<Unit>();
        //    Debug.Log($"{unit.name}, {unit.UnitType}");
        //}
    }
}
