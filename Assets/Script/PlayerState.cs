using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Image imageHp;

    public float hp;
    public float maxHp = 20f;
    public float speed = 3.5f;

    private void Start()
    {
        hp = maxHp;
    }

    void Update()
    {
        IsLife();
    }

    void IsLife()
    {
        imageHp.fillAmount = hp / maxHp;
    }
}
