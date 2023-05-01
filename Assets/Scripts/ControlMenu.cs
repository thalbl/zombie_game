using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{   
    public GameObject CloseButton;

    private void Start() {
    //know if the game is running in a pc app or web app
        #if UNITY_STANDALONE || UNITY_EDITOR
            CloseButton.SetActive(true);
        #endif
    }
    public void PlayGame(){
        SceneManager.LoadScene("game");
    }

    public void CloseGame(){
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
