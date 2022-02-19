using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartQuit : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

   
    public void Quit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
