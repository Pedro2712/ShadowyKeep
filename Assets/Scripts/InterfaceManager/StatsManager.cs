using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI level;
    public TextMeshProUGUI xp;

    private void Start()
    {
        playerName.text = player.entity.name.ToString();
    }
    private void Update()
    {
        level.text = player.entity.level.ToString();
        xp.text = player.entity.experience.ToString() + "/" + player.entity.experienceToNextLevel.ToString() + " xp";
    }
}