using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlInterface : MonoBehaviour
{   
    private ControlPlayer scriptControlPlayer;
    public Slider SliderPlayerHP;
    public GameObject GameOverScreen;
    public Text SurviveTime;
    public Text MaxSurvivalTime;
    public float MaxScore;

    void Start()
    {
        scriptControlPlayer = GameObject.FindWithTag("Player").GetComponent<ControlPlayer>();
        SliderPlayerHP.maxValue = scriptControlPlayer.playerStatus.CurrentHealth;
        UpdateHealthBar();
        Time.timeScale = 1;
        MaxScore = PlayerPrefs.GetFloat("MaxTimeScore");
    }

    public void UpdateHealthBar(){
        SliderPlayerHP.value = scriptControlPlayer.playerStatus.CurrentHealth;
    }

    public void GameOver(){
        GameOverScreen.SetActive(true);
        /*Let's see if our player is alive. If it isn't alive then we let him restart the game by clicking the mouse or Ctrl.*/
        Time.timeScale = 0;
        int minutes = (int)(Time.timeSinceLevelLoad / 60);
        int seconds = (int)(Time.timeSinceLevelLoad % 60);
        SurviveTime.text = "You survived for " + minutes + "min and " + seconds + "s.";

        MaxSurvivalScore(minutes, seconds);
    }   
    
    void MaxSurvivalScore(int min, int sec){
        if(Time.timeSinceLevelLoad > MaxScore){
            MaxScore = Time.timeSinceLevelLoad;
            MaxSurvivalTime.text = string.Format("Your better score is {0}min and {1}s", min, sec);
            PlayerPrefs.SetFloat("MaxTimeScore", MaxScore);

        }
        if(MaxSurvivalTime.text == ""){
            min = (int)MaxScore / 60;
            sec = (int)MaxScore % 60;
            MaxSurvivalTime.text = string.Format("Your better score is {0}min and {1}s", min, sec);
            PlayerPrefs.SetFloat("MaxTimeScore", MaxScore);
        }
    }

    public void Restart(){
        SceneManager.LoadScene("game");
    }
}
