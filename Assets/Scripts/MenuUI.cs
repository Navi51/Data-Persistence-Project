using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUI : MonoBehaviour
{
    public static string playerName;
    public GameObject plName;
    public string textToLog;
    //private InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        //playerName = plName.GetComponent<Text>().text;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        playerName = plName.GetComponent<Text>().text;
        textToLog = "Name " + playerName;
        Debug.Log(textToLog);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        //MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
# endif
    }
}
