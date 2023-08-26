using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWinCondition : MonoBehaviour
{
    // Player WIN and LOSE
    // Win: get to the -maxHeight * meterHeightRatio y position --> Fade to blackscreen --> load Scene: Credits

    // Lose: get over the curEnemys Position --> Fade to Blackscreen with LOSE Msg and Retry Btn --> load scene: Main menu

    public GameObject LoseScreen;
    public TMP_Text heightDisp;

    private Nullable<float> heightOnLose = null;

    private void Update()
    {
        if (transform.position.y > 0)
        {
            // Lose
            Lose(GameValues.height);
        }
        else if (transform.position.y <= (-GameValues.MaxHeight * GameValues.heightMeterRatio))
        {
            // win
            Win();
        }
        else if (transform.position.y > GameObject.FindGameObjectWithTag("enemyManager").GetComponent<EnemyManager>().curEnemy.transform.position.y)
        {
            // lose
            if (heightOnLose == null)
                heightOnLose = GameValues.height;
            Lose((float)heightOnLose);
        }
    }

    public void Win()
    {
        StartCoroutine(WinInner());
        BlackFadeScript.instance.FadeIn();
    }
    public IEnumerator WinInner()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
    }
    public void Lose(float curHeight)
    {
        GetComponent<Animator>().SetBool("die", true);

        BlackFadeScript.instance.FadeIn();
        LoseScreen.GetComponent<Animator>().SetBool("fadeIn", true);
        float roundedHeight = Mathf.Round(curHeight);
        heightDisp.text = "You got: " + roundedHeight.ToString() + "m";
    }
}
