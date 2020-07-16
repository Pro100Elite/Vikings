using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Text text;
    public int value;
    void Start()
    {
        value = 0;
    }
    void Update()
    {
        text.text = value.ToString();
    }
}
