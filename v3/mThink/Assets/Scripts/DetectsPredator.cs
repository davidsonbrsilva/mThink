using System.Collections.Generic;
using UnityEngine;

public class DetectsPredator : MonoBehaviour {

    public GameObject signalRadius;
    public float signalRate;

    private GameController gameController;
    private float lastSignal = 0.0f;
    private MonkeyController monkeyController;
    private List<GameObject> predatorsSeen;
    private GameObject thisMonkey;

    public List<GameObject> PredatorsSeen
    {
        get
        {
            return predatorsSeen;
        }

        set
        {
            predatorsSeen = value;
        }
    }

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        monkeyController = gameObject.transform.parent.gameObject.GetComponent<MonkeyController>();
        predatorsSeen = new List<GameObject>();
        thisMonkey = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Eagle") || other.CompareTag("Tiger"))
        {
            GameObject predatorSeen = other.gameObject.transform.parent.gameObject;
            predatorsSeen.Add(predatorSeen);

            int indexOfPredatorSeen = gameController.Predators.IndexOf(predatorSeen);
            int indexOfThisMonkey = gameController.Monkeys.IndexOf(thisMonkey);

            if (other.CompareTag("Eagle"))
            {
                SendSignal(indexOfPredatorSeen, new Color(1, 1, 0.2890625f, 1));
            }
            else if (other.CompareTag("Tiger"))
            {
                SendSignal(indexOfPredatorSeen, new Color(1, 0.62890625f, 0.2890625f, 1));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Time.time > lastSignal + signalRate)
        {
            if (other.CompareTag("Eagle") || other.CompareTag("Tiger"))
            {
                GameObject predatorSeen = other.gameObject.transform.parent.gameObject;
                int indexOfPredatorSeen = gameController.Predators.IndexOf(predatorSeen);

                if (other.CompareTag("Eagle"))
                {
                    SendSignal(indexOfPredatorSeen, new Color(1, 1, 0.2890625f, 1));
                }
                else if (other.CompareTag("Tiger"))
                {
                    SendSignal(indexOfPredatorSeen, new Color(1, 0.62890625f, 0.2890625f, 1));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tiger") || other.CompareTag("Eagle"))
        {
            GameObject predatorSeen = other.gameObject.transform.parent.gameObject;

            predatorsSeen.Remove(predatorSeen);
        }
    }

    private void SendSignal(int indexOfPredatorSeen, Color color)
    {
        int indexSignal = GetIndexSignal(indexOfPredatorSeen);

        signalRadius.GetComponent<SpriteRenderer>().color = color;

        GameObject clone = Instantiate(signalRadius, transform.parent.position, Quaternion.identity);
        clone.GetComponent<SignalRadiusController>().Sender = thisMonkey;
        clone.GetComponent<SignalRadiusController>().Signal = indexSignal;

        lastSignal = Time.time;
    }

    public int GetIndexSignal(int indexPredator)
    {
        float higuest = 0.0f;
        int indexSignal = -1;

        for (int i = 0; i < monkeyController.PredatorsTable.GetLength(0); i++)
        {
            if (monkeyController.PredatorsTable[i, indexPredator] > higuest)
            {
                higuest = monkeyController.PredatorsTable[i, indexPredator];
                indexSignal = i;
            }
        }

        return indexSignal;
    }
}
