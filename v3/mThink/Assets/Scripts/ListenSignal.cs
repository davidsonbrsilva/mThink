using UnityEngine;

public class ListenSignal : MonoBehaviour {

    private GameController gameController;
    private SignalRadiusController src;
    private DetectsPredator dp;
    private MonkeyController mc;
    GameObject thisMonkey;

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        thisMonkey = gameObject.transform.parent.gameObject;
        dp = thisMonkey.transform.GetChild(1).GetComponent<DetectsPredator>();
        mc = thisMonkey.GetComponent<MonkeyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SignalRadius"))
        {
            SignalRadiusController src = collision.GetComponent<SignalRadiusController>();

            int indexOfThisMonkey = gameController.Monkeys.IndexOf(thisMonkey);
            int indexOfSender = gameController.Monkeys.IndexOf(src.Sender);

            if (thisMonkey != src.Sender)
            {
                if (dp.PredatorsSeen.Count > 0)
                {
                    int randomPredatorSeen = Random.Range(0, dp.PredatorsSeen.Count - 1);

                    int indexOfPredatorSeen = gameController.Predators.IndexOf(dp.PredatorsSeen[randomPredatorSeen]);

                    float newValue = mc.PredatorsTable[src.Signal, indexOfPredatorSeen] + 0.1f;

                    if (newValue > 1.0f)
                    {
                        mc.PredatorsTable[src.Signal, indexOfPredatorSeen] = 1.0f;
                    }
                    else
                    {
                        mc.PredatorsTable[src.Signal, indexOfPredatorSeen] = newValue;
                    }

                    Debug.Log("The monkey " + indexOfThisMonkey + " received the signal " + src.Signal + " and related it to the predator " + indexOfPredatorSeen + "\n" + gameController.PredatorsTablesText());
                }
            }
        }
    }
}
