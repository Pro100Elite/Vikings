using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Image imageHp;
    public Image cdAttackImg;

    public float hp;
    public float maxHp = 20f;
    public float speed = 3.5f;
    public float enableAttack = 2f;
    public float cdAttack;

    private void Start()
    {
        hp = maxHp;
        cdAttack = enableAttack;
    }

    void Update()
    {
        IsLife();
        CdAttack();
    }

    void IsLife()
    {
        imageHp.fillAmount = hp / maxHp;
    }

    void CdAttack()
    {
        cdAttackImg.fillAmount = cdAttack / enableAttack;
    }
}
