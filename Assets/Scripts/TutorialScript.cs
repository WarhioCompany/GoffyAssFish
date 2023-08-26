using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    /*
     
    The Tutorial:
        1. You see the cutscene in the MainMenu for Context
        2. Stop Time: 
            1. Oh no, they are after you and you are lighter than water
            2. You have to flee to the bottom of the ocean 
            3. click and hold to aim your spikes
            4. then release to shoot and grab onto stuff that can drag you down
            5. dont get hit by the humans
            6. good luck
     
     */

    public enum tutorialState
    {
        INTRODUCTION,
        GRAB,
        PUSH,
        EPILOG
    }

    public bool skip;
    public float maxTutorialHeight;
    public GameObject blackBox;
    public GameObject clickIndocator;

    public Transform grabClickPos;
    public Transform pushClickPos;

    public GameObject firstWood;
    public GameObject secondWood;

    public tutorialState state;

    public TMP_Text textBubble;
    public string[] introductionText;

    public string[] grabText;

    public string[] pushText;

    public string[] epilogText;

    private bool readyForAction;
    private int curTextIdx;
    private bool waitForMouseClick;
    private bool waitForAttach;
    private bool canClick;

    private void Start()
    {
        if (skip) {
            ResetChanges();
            return;
        } 

        GameObject.FindGameObjectWithTag("Player").GetComponent<SHITSpikeManager>().canShoot = false;
        ShowNextText();
    }

    private void Update()
    {
        switch (state)
        {
            case tutorialState.INTRODUCTION:
                Time.timeScale = 0f;

                if (curTextIdx >= introductionText.Length)
                {
                    curTextIdx = 0;
                    state = tutorialState.GRAB;
                }
                else
                {
                    // still text to show
                    if (Input.GetMouseButtonDown(0))
                    {
                        ShowNextText();
                    }
                }

                break;
            case tutorialState.GRAB:
                if (curTextIdx >= grabText.Length)
                {
                    readyForAction = true;
                }
                else
                {
                    // still text to show
                    if (Input.GetMouseButtonDown(0) && !readyForAction)
                    {
                        ShowNextText();
                    }
                }

                if (!readyForAction) return;


                if (!waitForMouseClick && !waitForAttach)
                {
                    // set time
                    Time.timeScale = 0.1f;
                    waitForMouseClick = true;

                    blackBox.SetActive(true);
                    clickIndocator.SetActive(true);
                }
                else if (waitForMouseClick)
                {
                    blackBox.transform.position = firstWood.transform.position;
                    clickIndocator.transform.position = grabClickPos.transform.position;
                }

                if (Input.GetMouseButtonUp(0) && waitForMouseClick && canClick)
                {
                    blackBox.SetActive(false);
                    clickIndocator.SetActive(false);

                    Time.timeScale = 1f;
                    waitForMouseClick = false;
                    waitForAttach = true;
                }
                else if (waitForAttach && firstWood.GetComponent<ObjectScript>().isPlayerAttached)
                {
                    readyForAction = false;
                    waitForAttach = false;
                    waitForMouseClick = false;
                    canClick = false;
                    curTextIdx = 0;
                    state = tutorialState.PUSH;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<SHITSpikeManager>().canShoot = false;
                    Time.timeScale = 0;
                }

                if (Input.GetMouseButtonUp(0) && readyForAction)
                {
                    canClick = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<SHITSpikeManager>().canShoot = true;
                }
                break;
            case tutorialState.PUSH:
                if (curTextIdx >= pushText.Length)
                {
                    readyForAction = true;
                }
                else
                {
                    // still text to show
                    if (Input.GetMouseButtonDown(0) && !readyForAction)
                    {
                        ShowNextText();
                    }
                }

                if (!readyForAction) return;

                if (!waitForMouseClick && !waitForAttach)
                {
                    // set time
                    Time.timeScale = 0.1f;
                    waitForMouseClick = true;

                    blackBox.SetActive(true);
                    clickIndocator.SetActive(true);
                }
                else if (waitForMouseClick)
                {
                    blackBox.transform.position = firstWood.transform.position;
                    clickIndocator.transform.position = grabClickPos.transform.position;
                }

                if (Input.GetMouseButtonUp(0) && waitForMouseClick && canClick)
                {
                    blackBox.SetActive(false);
                    clickIndocator.SetActive(false);

                    Time.timeScale = 1f;
                    waitForMouseClick = false;
                    waitForAttach = true;
                }

                if (waitForAttach && firstWood.GetComponent<ObjectScript>().isPlayerAttached)
                {
                    readyForAction = false;
                    state = tutorialState.EPILOG;
                    canClick = false;
                    curTextIdx = 0;
                }

                if (Input.GetMouseButtonUp(0) && readyForAction)
                {
                    canClick = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<SHITSpikeManager>().canShoot = true;
                }
                break;
            case tutorialState.EPILOG:
                if (curTextIdx >= epilogText.Length)
                {
                    // end tutorial
                    ResetChanges();
                }
                else
                {
                    // still text to show
                    if (Input.GetMouseButtonDown(0))
                    {
                        ShowNextText();
                    }
                }
                break;
            default:
                break;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.y < maxTutorialHeight)
        {
            ResetChanges();
        }
    }

    public void ResetChanges()
    {
        textBubble.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SHITSpikeManager>().canShoot = true;
        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<UIManager>().UIFadeIn();

        // reset time
        Time.timeScale = 1;

        // deactivate blockbox
        blackBox.SetActive(false);

        // deactivate click indicator
        clickIndocator.SetActive(false);

        // deactivate this script
        Destroy(this);
    }

    public void ShowNextText()
    {
        switch (state)
        {
            case tutorialState.INTRODUCTION:
                textBubble.text = introductionText[curTextIdx];
                break;
            case tutorialState.GRAB:
                textBubble.text = grabText[curTextIdx];
                break;
            case tutorialState.PUSH:
                textBubble.text = pushText[curTextIdx];
                break;
            case tutorialState.EPILOG:
                textBubble.text = epilogText[curTextIdx];
                break;
            default:
                break;
        }
        curTextIdx += 1;
    }
}
