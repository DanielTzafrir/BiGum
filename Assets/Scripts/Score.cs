using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private GameObject[] bubblesArr;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject redBubbleWhiteOutline;
    [SerializeField]
    private GameObject plus1;

    private int idx = 0;
    public bool gameIsOn = true;
    public void GetPoint()
    {
        if (idx < bubblesArr.Length -1)
        {
            //activate the animation
            redBubbleWhiteOutline.GetComponent<Animator>().SetTrigger("addPoint");
            plus1.GetComponent<Animator>().SetTrigger("addPoint");

            StartCoroutine(UpdatePositionOfScoreBubble());
            StartCoroutine(UpdateColorOfScoreBubble());
        }
        else
        {
            //activate the animation
            redBubbleWhiteOutline.GetComponent<Animator>().SetTrigger("addPoint");
            plus1.GetComponent<Animator>().SetTrigger("addPoint");

            StartCoroutine(UpdatePositionOfScoreBubble());
            StartCoroutine(UpdateColorOfScoreBubble());

            Debug.Log("out of bounds down socre"); // player won the GAME
            StartCoroutine(finishGame());
        }
        
    }

    private IEnumerator UpdatePositionOfScoreBubble()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Move the animation objects to the next bubble
        if (panel.tag == "Down")
        {
            Vector2 currentPosition = panel.GetComponent<RectTransform>().anchoredPosition;

            currentPosition.y += 29f;
            panel.GetComponent<RectTransform>().anchoredPosition = currentPosition;
        }
        else
        {
            Vector2 currentPosition = panel.GetComponent<RectTransform>().anchoredPosition;

            currentPosition.y -= 29f;
            panel.GetComponent<RectTransform>().anchoredPosition = currentPosition;
        }
        
        

    }
    private IEnumerator UpdateColorOfScoreBubble()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        //change the colour of the white bubbles to red (for the rest of the game)
        bubblesArr[idx].GetComponent<Image>().color = new Color32(205, 57, 101, 255);
        idx++;

    }

    private IEnumerator finishGame()
    {
        Debug.Log("1 " + gameIsOn);
        yield return new WaitForSeconds(2f);
        Debug.Log("2");
        gameIsOn = false;
    }


}
