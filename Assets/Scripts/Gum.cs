using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gum : MonoBehaviour
{
    public GameObject gum;
    public bool isBlowingUp = true;
    public bool GameOn = false;
    public float min = .03f;
    public float max = .1f;
    public float current = 0f;
    public Animator ani;
    private RectTransform rec;

    private void Start()
    {
        rec = gum.GetComponent<RectTransform>();
    }
    private void Update()
    {
/*
        if (rec.localScale < )
        {

        }*/
    }
}
