using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float runtime;
    public Instantable eagle;
    public Instantable tiger;
    public Instantable monkey;
    public int symbols;
    public Vector2 boundary;

    private List<GameObject> predators;
    private List<GameObject> monkeys;

    public List<GameObject> Predators
    {
        get
        {
            return predators;
        }
    }
    public List<GameObject> Monkeys
    {
        get
        {
            return monkeys;
        }
    }

    private void Awake()
    {
        predators = new List<GameObject>();
        monkeys = new List<GameObject>();
    }

    private void Start()
    {
        SpawnPosition(eagle, predators);
        SpawnPosition(tiger, predators);
        SpawnPosition(monkey, monkeys);

        Debug.Log("Iteração Inicial\n" + PredatorsTablesText());
    }

    private void SpawnPosition(Instantable instantable, List<GameObject> instancesReferences)
    {
        for (int i = 0; i < instantable.amount; i++)
        {
            Vector2 position = new Vector2(Random.Range(-boundary.x, boundary.x), Random.Range(-boundary.y, boundary.y));
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-180, 180));
            instancesReferences.Add(Instantiate(instantable.prefab, position, rotation));
        }
    }

    public string PredatorsTablesText()
    {
        string text = "";

        for(int i = 0; i < Monkeys.Count; i++)
        {
            MonkeyController monkeyController = Monkeys[i].GetComponent<MonkeyController>();

            text += "\tMonkey " + i + "\n";
            text += monkeyController.PredatorsTableText();
            text += "\n\n";
        }

        return text;
    }
}