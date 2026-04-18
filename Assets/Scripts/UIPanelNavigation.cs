using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPanelNavigation : MonoBehaviour
{
    [SerializeField]
    public bool InitializeStart = true;

    public void Start()
    {
        if (!InitializeStart) return;

        //make sure only one panel is active, default is first
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == 0 ? true : false);
        }
    }

    public void goToMenu(GameObject panel)
    {
        //set any child panel thats active to inactive
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        panel.gameObject.SetActive(true);
    }

    public void goToSceneByName(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void goToSceneByID(int BuildNum)
    {
        SceneManager.LoadScene(BuildNum);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
