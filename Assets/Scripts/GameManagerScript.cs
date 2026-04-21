using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find("AudioManager") is { } am)
        {
            if (am != this.gameObject)
                Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
