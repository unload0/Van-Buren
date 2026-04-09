using UnityEngine;

public class StarDrift : MonoBehaviour
{
    // Speed of the drifting stars
    public float speed = 30f;

    void Update()
    {
        // Move the container slowly to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Optional: Loop them back to the right if they go off-screen
        if (transform.localPosition.x < -1500)
        {
            transform.localPosition = new Vector3(1500, 0, 0);
        }
    }
}