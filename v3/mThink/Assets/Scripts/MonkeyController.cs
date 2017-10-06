using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    public float maxDistance;
    public float turnSpeed;
    public float movementSpeed;
    public Range movementTime;
    public Range waitingTime;

    private Vector3 targetPosition;
    private Vector3 predatorPosition;
    private GameController gameController;
    private DetectsPredator detectsPredator;
    private float[,] predatorsTable;

    public float[,] PredatorsTable
    {
        get
        {
            return predatorsTable;
        }

        set
        {
            predatorsTable = value;
        }
    }

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        detectsPredator = transform.GetChild(1).GetComponent<DetectsPredator>();
        DefineTables();
    }

    private void Start()
    {
        StartCoroutine(SpawnPosition());
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x > gameController.boundary.x)
        {
            currentPosition.x -= 2 * gameController.boundary.x;
        }
        else if (currentPosition.x < -gameController.boundary.x)
        {
            currentPosition.x += 2 * gameController.boundary.x;
        }

        if (currentPosition.y > gameController.boundary.y)
        {
            currentPosition.y -= 2 * gameController.boundary.y;
        }
        else if (currentPosition.y < -gameController.boundary.y)
        {
            currentPosition.y += 2 * gameController.boundary.y;
        }

        Vector3 movementDirection = targetPosition - currentPosition;

        if (targetPosition != transform.position)
        {
            float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime * turnSpeed);
        }
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * movementSpeed);
    }

    private IEnumerator SpawnPosition()
    {
        //while(Time.time < gameController.runtime)
        while (true)
        {
            targetPosition = new Vector3(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance), 0.0f) + transform.position;
            yield return new WaitForSeconds(Random.Range(movementTime.Min, movementTime.Max));
            targetPosition = transform.position;
            yield return new WaitForSeconds(Random.Range(waitingTime.Min, waitingTime.Max));
        }
    }

    private void DefineTables()
    {
        predatorsTable = new float[gameController.symbols, gameController.Predators.Count];

        for (int i = 0; i < predatorsTable.GetLength(0); i++)
        {
            for (int j = 0; j < predatorsTable.GetLength(1); j++)
            {
                predatorsTable[i, j] = Mathf.Round(Random.Range(0.0f, 0.5f) * 10.0f) / 10.0f;
            }
        }
    }

    public string PredatorsTableText()
    {
        string tablePrint = "";

        tablePrint += "\t";

        for (int i = 0; i < predatorsTable.GetLength(1); i++)
        {
            tablePrint += "P" + i + "(" + gameController.Predators[i].name + ")\t\t";
        }

        tablePrint += "\n";

        for (int i = 0; i < predatorsTable.GetLength(0); i++)
        {
            tablePrint += "s" + i + "\t";

            for (int j = 0; j < predatorsTable.GetLength(1); j++)
            {
                tablePrint += predatorsTable[i, j] + "\t\t\t";
            }

            tablePrint += "\n";
        }

        return tablePrint;
    }
}