using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : BaseManager<GameManager>
{
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 80;
    }
    void Start()
    {
        Debug.Log("StartGame");
        if (UIManager.HasInstance())
        {
            //UIManager.Instance.ShowScreen<MainMenu>(null,true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
