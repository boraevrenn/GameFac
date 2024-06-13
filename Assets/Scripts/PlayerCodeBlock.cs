using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCodeBlock : MonoBehaviour
{
    Player player;


    [SerializeField] GameObject attackConfig;
    [SerializeField] GameObject MoveConfig;

    [SerializeField] Button moveButton;
    [SerializeField] Button attackButton;

    [SerializeField] TextMeshProUGUI moveInfo;
    [SerializeField] TextMeshProUGUI attackInfo;

    [SerializeField] Slider moveSlider;
    [SerializeField] Slider attackSlider;

    private void Update()
    {
        FindPlayer();
    }

    void FindPlayer()
    {
        if (player == null)
        {
            if (FindObjectsOfType<Player>().Length > 0)
            {
                player = FindObjectOfType<Player>();
            }
        }
    }


    public void MoveClicked()
    {
        MoveConfig.SetActive(true);
        moveButton.transform.gameObject.SetActive(false);
    }

    public void AttackedClicked()
    {
        attackConfig.SetActive(true);
        attackButton.transform.gameObject.SetActive(false);
    }


    public void MoveSlider()
    {
        if (player != null)
        {
            moveInfo.text = "Move Speed: " + moveSlider.value.ToString();
            player.moveSpeed = moveSlider.value;
        }

    }

    public void AttackSlider()
    {
        if (player != null)
        {
            attackInfo.text = "Attack Damage: " + attackSlider.value.ToString();
            player.attackDamage = attackSlider.value;
        }

    }



}
