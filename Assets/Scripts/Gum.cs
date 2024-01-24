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
    private GameObject opponent;
    [SerializeField]
    private GameObject Lose;
    [SerializeField]
    private Score MyScore;

    private bool isBlowingUp = true;
    public bool RoundOn = true; // I should make sure that after the start 3..2..1 it would change from false to true
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
        if (RoundOn)
        {
            if (canUpdate)
            {
                Delay();
            }
            // ex: rec.localScale.x = 1.020304
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
     
        StartCoroutine(DelayedHelfSec());
    }

    private IEnumerator DelayedHelfSec()
    {
        // Wait for 0.5 second
        yield return new WaitForSeconds(0.5f);
        canUpdate = true;
    }

    private void touched()
    {
        RoundOn = false;
        isTouched = true;
        GumAni.enabled = false; // stop the gum from moving
        Gum oponentGum = opponent.GetComponent<Gum>();
        bool oponentTouched = oponentGum.isTouched;

        // check if the opponent finished. if did - finish the round. else - do nothing
        if (oponentTouched)
        {
            Debug.Log("finish game");
            CompareVariables(oponentGum);
            // finish the round: if the player didn't get all of the points them restart the vars except the one who count the points. 
            StartCoroutine(Delayed3sec());
            // if he got all the points - finish the game
        }

    }

    private void CompareVariables(Gum otherObject)
    {
        if (canTouch)
        {
            canTouch = false;

            if (cmDouble > otherObject.cmDouble)
            {
                Debug.Log("This object has a higher variable value!");
                // Win round
                MyScore.GetPoint();
                // activate the Lose animation for the enemy
                enemyLose.SetActive(true);
                // unactive the gum object in order to see the Lose animation
                enemyGum.SetActive(false);
            }
            else if (cmDouble < otherObject.cmDouble)
            {
                Debug.Log("Other object has a higher variable value!");
                // lose round
                enemyScoreScript.GetPoint();
                // activate the Lose animation
                Lose.SetActive(true);
                // unactive the gum object in order to see the Lose animation
                gum.SetActive(false);
            }
            else
            {
                Debug.Log("Both objects have the same variable value!");
                // a tie. no points, update the text in both sides to "tie!"

            }
        }

    }

    private IEnumerator Delayed3sec()
    {
        // Wait for 0.5 second
        yield return new WaitForSeconds(3f);
        
        
        continueNextRound();
        enemyGumScript.continueNextRound(); // get the enemy ready for next round
    }

    public void continueNextRound()
    {
        // active player objs
        gum.SetActive(true);
        Lose.SetActive(false);
        GumAni.enabled = true; // make the gum moving again
        GumAni.SetTrigger("StartRound");
        canUpdate = true;
        RoundOn = true;
        isTouched = false;
        canTouch = true;
    }
}
