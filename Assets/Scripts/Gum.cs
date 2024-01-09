using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gum : MonoBehaviour
{
    public GameObject gum;
    public GameObject text;
    public GameObject opponent;
    public bool isBlowingUp = true;
    public bool RoundOn = true; // I should make sure that after the start 3..2..1 it would change from false to true
    public bool isTouched = false;
    private RectTransform rec;
    private Animator GumAni;
    private float cmFloat;
    private double cmDouble;
    private bool canUpdate;

    private void Start()
    {
        rec = gum.GetComponent<RectTransform>();
        GumAni = gum.GetComponent<Animator>();
        canUpdate = true;
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

    public void touched()
    {
        RoundOn = false;
        isTouched = true;
        GumAni.enabled = false; // stop the gum from moving
        Gum oponentGum = opponent.GetComponent<Gum>();
        bool oponentTouched = oponentGum.isTouched;
        if (oponentTouched)
        {
            Debug.Log("finish game");
        }
        // check if the opponent finished. if did - finish the game. else - do nothing
    }
}
