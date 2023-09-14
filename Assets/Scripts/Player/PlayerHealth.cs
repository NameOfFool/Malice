using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private TMP_Text healthCount;
    [SerializeField] private Slider bar;
    [Header("Player Health")]
    [SerializeField] private float maxHP;
    [CustomAttributes.ReadOnly] private float currentHP;
    public float MaxHP { get => maxHP; set => maxHP = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public bool IsAlive { get; set; }
    public bool IsInvincible { get; set; }

    void Start()
    {
        SetHealthCount();
    }

    void Update()
    {

    }

    private void SetHealthCount()
    {
        healthCount.text = CurrentHP + "";
        bar.value = CurrentHP / MaxHP;
    }
}
