using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public string level1, level2, level3, level4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(level2);
    }
    public void Level3()
    {
        SceneManager.LoadScene(level3);
    }
    public void Level4()
    {
        SceneManager.LoadScene(level4);
    }
}
