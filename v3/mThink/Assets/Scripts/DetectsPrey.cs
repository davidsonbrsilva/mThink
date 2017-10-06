using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectsPrey : MonoBehaviour {

    private GameController gameController;
    private GameObject lastMonkeySeen;
    private PredatorController predatorController;
    private List<GameObject> monkeysSeen;
    private GameObject thisPredator;
    private bool alertState;

    public List<GameObject> PredatorsSeen
    {
        get
        {
            return monkeysSeen;
        }

        set
        {
            monkeysSeen = value;
        }
    }
    public GameObject LastMonkeySeen
    {
        get
        {
            return lastMonkeySeen;
        }
    }
    public bool AlertState
    {
        get
        {
            return alertState;
        }

        set
        {
            alertState = value;
        }
    }

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        monkeysSeen = new List<GameObject>();
        thisPredator = gameObject.transform.parent.gameObject;
        predatorController = thisPredator.GetComponent<PredatorController>();
        alertState = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monkey"))
        {
            GameObject monkeySeen = other.gameObject.transform.parent.gameObject;
            monkeysSeen.Add(monkeySeen);

            if (alertState == false)
            {
                lastMonkeySeen = monkeySeen;
                alertState = true;
                //Debug.Log("O estado de alerta agora está ATIVO");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Tiger") || other.CompareTag("Eagle"))
        {
            monkeysSeen.Remove(other.gameObject.transform.parent.gameObject);

            if (alertState == true)
            {
                Invoke("TurnOffAlertState", 5f);
            }
        }
    }

    private void TurnOffAlertState()
    {
        lastMonkeySeen = null;
        alertState = false;
        //Debug.Log("O estado de alerta agora está INATIVO");
    }
}
