using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject doorToNextRoom;
    public GameObject rewardChest;
    public Transform roomCenter;
    public LayerMask enemyLayer;
    public LayerMask playerLayer;
    public int radius = 10;

    private bool enemyOnRadios = true;
    private bool playerOnRadios = true;
    private Collider2D enemyHit;
    private Collider2D playerHit;
    // Start is called before the first frame update
    void Start()
    {
        doorToNextRoom.SetActive(!enemyOnRadios);
        rewardChest.SetActive(!enemyOnRadios);
    }

    void FixedUpdate()
    {
        playerHit = Physics2D.OverlapCircle(roomCenter.position, radius, playerLayer);

        playerOnRadios = playerHit != null;

        if (!playerOnRadios)
        {
            return;
        }

        enemyHit = Physics2D.OverlapCircle(roomCenter.position, radius, enemyLayer);

        enemyOnRadios = enemyHit != null;

        rewardChest.SetActive(!enemyOnRadios);

        if (!enemyOnRadios && rewardChest.transform.Find("Decoration").GetComponent<BauController>().isOpen)
        {
            doorToNextRoom.SetActive(!enemyOnRadios);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(roomCenter.position, radius);
    }
}
