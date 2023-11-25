using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text text;
    public Slider hpSlider;
    
    public void SetPlayerHUD(Player player)
    {
        text.text = player.name;
        hpSlider.maxValue = player.MaxHealth;
        hpSlider.value = player.Health;
    }

    public void SetEnemyHUD(Enemy enemy)
    {
        text.text = enemy.name;
        hpSlider.maxValue = enemy.MaxHealth;
        hpSlider.value = enemy.Health;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
