using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : Damageable
{
    [Header("Player UI")]
    [SerializeField] private TMP_Text healthCount;
    [SerializeField] private Slider bar;

    protected override void Awake()
    {
        base.Awake();
        SetHealthCount();
    }

    private void SetHealthCount()
    {
        healthCount.text = CurrentHP + "";
        bar.value = CurrentHP / MaxHP;
    }
    public override void onHit(float damage, Vector2 knockback)
    {
        base.onHit(damage, knockback);
        SetHealthCount();
    }
}
