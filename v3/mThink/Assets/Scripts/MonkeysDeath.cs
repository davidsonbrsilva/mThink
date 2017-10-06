using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeysDeath : MonoBehaviour {

    private GameController gameController;
    private MonkeyController monkeyController;
    private ListenSignal listenSignal;
    private GameObject thisMonkey;

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        thisMonkey = gameObject.transform.parent.gameObject;
        monkeyController = thisMonkey.GetComponent<MonkeyController>();
        listenSignal = GetComponent<ListenSignal>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Tiger") || collider.CompareTag("Eagle"))
        {
            GameObject predator = collider.gameObject.transform.parent.gameObject;

            if (listenSignal.LastHeardSignal != -1)
            {
                int indexOfPredator = gameController.Predators.IndexOf(predator);

                float newValue = monkeyController.PredatorsTable[listenSignal.LastHeardSignal, indexOfPredator] - 0.1f;

                if (newValue < 0)
                {
                    newValue = 0;
                }

                monkeyController.PredatorsTable[listenSignal.LastHeardSignal, indexOfPredator] = newValue;

                Debug.Log(gameController.PredatorsTablesText());
            }

            DetectsPrey detectsPrey = predator.transform.GetChild(1).GetComponent<DetectsPrey>();

            detectsPrey.AlertState = false;

            predator.transform.position = new Vector3
                (
                    Random.Range(-gameController.boundary.x, gameController.boundary.x),
                    Random.Range(-gameController.boundary.y, gameController.boundary.y),
                    collider.transform.position.z
                );
        }
    }
}
