using Assets.Scripts;
using System.Collections;
using UnityEngine;

public class PredatorController : MonoBehaviour
{
    public float maxDistance;
    public float turnSpeed;
    public float movementSpeed;
    public Range movementTime;
    public Range waitingTime;

    private Vector3 targetPosition;
    private Vector3 monkeyPosition;
    private GameController gameController;
    private DetectsPrey detectsPrey;
    private float[,] predatorsTable;

    private void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        detectsPrey = transform.GetChild(1).GetComponent<DetectsPrey>();
    }

    private void Start()
    {
        StartCoroutine(SpawnPosition());
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        if(currentPosition.x > gameController.boundary.x)
        {
            currentPosition.x -= 2 * gameController.boundary.x;
        }
        else if(currentPosition.x < -gameController.boundary.x)
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

        if (detectsPrey.AlertState == true)
        {
            monkeyPosition = detectsPrey.LastMonkeySeen.transform.position;

            Vector3 movementDirection = monkeyPosition - currentPosition;

            if (monkeyPosition != transform.position)
            {
                float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime * turnSpeed);
            }
            transform.position = Vector3.MoveTowards(currentPosition, monkeyPosition, Time.deltaTime * movementSpeed);
        }
        else
        {
            Vector3 movementDirection = targetPosition - currentPosition;

            if (targetPosition != transform.position)
            {
                float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime * turnSpeed);
            }
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, Time.deltaTime * movementSpeed);
        }
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
}
