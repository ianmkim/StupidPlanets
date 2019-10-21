using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject helpMenu;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void loadLevel()
    {
        SceneManager.LoadScene("something");
    }

    public void loadHelp()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void loadMenu()
    {
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
}
