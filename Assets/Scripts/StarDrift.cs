using UnityEngine;

public class StarDrift : MonoBehaviour
{
    public float speed = 30f;

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.localPosition.x < -1150)
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}