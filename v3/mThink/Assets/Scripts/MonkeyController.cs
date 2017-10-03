using UnityEngine;

public class MonkeyController : MonoBehaviour {

    private GameController gameController;
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
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        DefineTables();
    }

    private void DefineTables()
    {
        predatorsTable = new float[gameController.symbols, gameController.Predators.Count];

        for (int i = 0; i < predatorsTable.GetLength(0); i++)
        {
            for (int j = 0; j < predatorsTable.GetLength(1); j++)
            {
                predatorsTable[i, j] = Mathf.Round(Random.Range(0.0f, 0.5f) * 100.0f) / 100.0f;
            }
        }
    }

    public string PredatorsTableText()
    {
        string tablePrint = "";

        tablePrint += "\t";

        for (int i = 0; i < predatorsTable.GetLength(1); i++)
        {
            tablePrint += "P" + i + "(" + gameController.Predators[i].name + ")\t";
        }

        tablePrint += "\n";

        for (int i = 0; i < predatorsTable.GetLength(0); i++)
        {
            tablePrint += "s" + i + "\t";

            for (int j = 0; j < predatorsTable.GetLength(1); j++)
            {
                tablePrint += predatorsTable[i, j] + "\t\t";
            }

            tablePrint += "\n";
        }

        return tablePrint;
    }
}