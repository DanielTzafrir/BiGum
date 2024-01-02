using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject[] down;
    public GameObject[] up;
    public int indxDown = 0;
    public int indxUp = 0;

    public GameObject panelDown;
    public GameObject redBubbleWhiteOutlineDown;
    public GameObject plus1Down;

    public void DownGetPoint()
    {
        if (indxDown < down.Length)
        {
            //activate the animation
            redBubbleWhiteOutlineDown.GetComponent<Animator>().SetTrigger("addPointDown");
            plus1Down.GetComponent<Animator>().SetTrigger("addPointDown");

            StartCoroutine(Delayed2sec());
            StartCoroutine(Delayed1sec());
        }
        else
        {
            Debug.Log("out of bounds down socre");
        }
        
    }

    private IEnumerator Delayed2sec()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Move the animation objects to the next bubble
        Vector2 currentPosition = panelDown.GetComponent<RectTransform>().anchoredPosition;
        currentPosition.y += 29f;
        panelDown.GetComponent<RectTransform>().anchoredPosition = currentPosition;

    }
    private IEnumerator Delayed1sec()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        //change the colour of the white bubbles to red (for the rest of the game)
        down[indxDown].GetComponent<Image>().color = new Color32(205, 57, 101, 255);
        indxDown++;

    }
}
