using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState : MonoBehaviour
{
    public Image imageHp;
    public float hp;
    public  float maxHp = 2f;

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
