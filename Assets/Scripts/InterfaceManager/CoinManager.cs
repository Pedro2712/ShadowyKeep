using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI coinsCount;

    // Update is called once per frame
    void Update()
    {
        coinsCount.text = player.entity.coins.ToString();
    }
}