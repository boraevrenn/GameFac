using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] Slider playerHealthSlider;
    [SerializeField] Slider enemyHealthSlider;
    [SerializeField] Player player;
    [SerializeField] Enemy enemy;
    [SerializeField] Canvas playerCanvas;
    [SerializeField] Canvas enemyCanvas;

    private void Update()
    {
        UpdatePlayerHealth();
        UpdateEnemyHealth();

    }
    private void LateUpdate()
    {
        DontUpdateSliderRotation();
    }

    void UpdatePlayerHealth()
    {
        if (player != null)
            playerHealthSlider.value = player.health;
    }

    void UpdateEnemyHealth()
    {
        if (enemy != null)
            enemyHealthSlider.value = enemy.health;
    }

    void DontUpdateSliderRotation()
    {
        if (player != null)  
        
        if (enemy != null)
            enemyHealthSlider.transform.localRotation = Quaternion.identity;
    }



}
