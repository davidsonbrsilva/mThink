using UnityEngine;

public class SignalRadiusController : MonoBehaviour {

    public float timeToDestroy;
    public float growthFactor;
    public float timeForVisualEffect;

    private float t = 0;
    private int signal;
    private GameObject sender;

    public int Signal
    {
        get
        {
            return signal;
        }

        set
        {
            signal = value;
        }
    }
    public GameObject Sender
    {
        get
        {
            return sender;
        }
        set
        {
            sender = value;
        }
    }

    private void Update()
    {
        Color currentColor = gameObject.GetComponent<SpriteRenderer>().color;
        Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, 0);
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(currentColor, targetColor, t);

        if (t < 1.0f)
        {
            t += Time.deltaTime / (timeForVisualEffect);
        }

        transform.localScale += new Vector3(1, 1, 0) * Time.deltaTime * growthFactor;
        Destroy(gameObject, timeToDestroy);
    }
}
