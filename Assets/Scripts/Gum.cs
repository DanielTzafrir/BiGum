using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gum : MonoBehaviour
{
    [SerializeField]
    private GameObject gum;
    [SerializeField]
    private GameObject text;
    [SerializeField]
    private GameObject Lose;
    [SerializeField]
    private Score MyScore;

    private bool isBlowingUp = true;
    public bool RoundOn = true; // I should make sure that after the start 3..2..1 it would change from false to true
    private bool gameIsOn = true;
    public bool isTouched = false; // public bcz the script need this oponents var 
    private RectTransform rec;
    private Animator GumAni;
    private float cmFloat;
    public double cmDouble; // public bcz the script need this oponents var 
    private bool canUpdate = true; // the gum to get bigger or smaller
    private bool canTouch = true; // if false - then cant get points

    // enemy objs
    [SerializeField]
    private GameObject enemyGum;
    [SerializeField]
    private GameObject enemyLose;
    [SerializeField]
    private GameObject enemyText;
    private Score enemyScoreScript; // not serialzied bcz I need the script which I get on runtime (start func)
    private Gum enemyGumScript;

    

    private void Start()
    {
        rec = gum.GetComponent<RectTransform>();
        GumAni = gum.GetComponent<Animator>();
        if (tag == "Down")
        {
            enemyScoreScript = GameObject.Find("BGup").GetComponent<Score>();
            enemyGumScript = GameObject.Find("BGup").GetComponent<Gum>();
        }
        else
        {
            enemyScoreScript = GameObject.Find("BGdown").GetComponent<Score>();
            enemyGumScript = GameObject.Find("BGdown").GetComponent<Gum>();
        }
    }
    private void Update()
    {
        gameIsOn = (MyScore.gameIsOn && enemyScoreScript.gameIsOn);
        if (RoundOn && gameIsOn)
        {
            if (canUpdate)
            {
                Delay();
            }
            cmFloat = rec.localScale.x * 18; // convert to the right scale
            cmDouble = System.Math.Round(cmFloat, 2); // get only two digits after the point
            text.GetComponent<TMPro.TextMeshProUGUI>().SetText(cmDouble + " cm"); // update the text based on the cm           
        }
    }
    private void Delay()
    {
        canUpdate = false;
        isBlowingUp = Random.Range(0, 2) == 1;
        GumAni.SetBool("Bigger", isBlowingUp);
     
        StartCoroutine(DelayTheUpdate());
    }

    private IEnumerator DelayTheUpdate()
    {
        // Wait for 0.5 second
        yield return new WaitForSeconds(0.5f);
        canUpdate = true;
    }

    private void touched()
    {
        if (gameIsOn)
        {
            RoundOn = false;
            isTouched = true;
            GumAni.enabled = false; // stop the gum from moving
            bool oponentTouched = enemyGumScript.isTouched;

            // check if the opponent finished. if did - finish the round. else - do nothing
            if (oponentTouched)
            {
                CompareVariables(enemyGumScript);
                // finish the round: if the player didn't get all of the points them restart the vars except the one who count the points. 
                StartCoroutine(Delayed3sec());
            }
        }
    }

    private void CompareVariables(Gum otherObject)
    {
        if (canTouch)
        {
            canTouch = false;

            if (cmDouble > otherObject.cmDouble)
            {
                // Win round
                MyScore.GetPoint();
                // activate the Lose animation for the enemy
                enemyLose.SetActive(true);
                // unactive the gum object in order to see the Lose animation
                enemyGum.SetActive(false);
                // update text
                text.GetComponent<TMPro.TextMeshProUGUI>().SetText("You won!");
                enemyText.GetComponent<TMPro.TextMeshProUGUI>().SetText("You lost!");
            }
            else if (cmDouble < otherObject.cmDouble)
            {
                // lose round
                enemyScoreScript.GetPoint();
                // activate the Lose animation
                Lose.SetActive(true);
                // unactive the gum object in order to see the Lose animation
                gum.SetActive(false);
                // update text
                text.GetComponent<TMPro.TextMeshProUGUI>().SetText("You lost");
                enemyText.GetComponent<TMPro.TextMeshProUGUI>().SetText("You won!");
            }
            else
            {
                // a tie. no points, update the text in both sides to "tie!"
                // update text
                text.GetComponent<TMPro.TextMeshProUGUI>().SetText("tie!");
                enemyText.GetComponent<TMPro.TextMeshProUGUI>().SetText("tie!");
            }
        }

    }

    private IEnumerator Delayed3sec()
    {
        // Wait for 3 second
        yield return new WaitForSeconds(3f);
        
        
        continueNextRound();
        enemyGumScript.continueNextRound(); // get the enemy ready for next round
    }

    public void continueNextRound()
    {
        Debug.Log("gameIsOn " + gameIsOn);
        if (gameIsOn)
        {
            // active player objs
            gum.SetActive(true);
            Lose.SetActive(false);
            GumAni.enabled = true; // make the gum moving again
            GumAni.SetTrigger("StartRound"); // for the animation to start from the beginning
            canUpdate = true;
            RoundOn = true;
            isTouched = false;
            canTouch = true;
        }
        else
        {
            // if gameIsOn bool is true then the enemy won
            if (MyScore.gameIsOn)
            {
                text.GetComponent<TMPro.TextMeshProUGUI>().SetText("Lost the game..");
                enemyText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Won the game!!!");
            }
            else
            {
                text.GetComponent<TMPro.TextMeshProUGUI>().SetText("Won the game!!!");
                enemyText.GetComponent<TMPro.TextMeshProUGUI>().SetText("Lost the game..");
            }
        }
    }
}
