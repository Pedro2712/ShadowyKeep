using System.Collections;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{
    private Touch t;
    private Vector2 startPos;
    public GameObject Background;
    public Transform control;
    private PlayerMovement Playerplayer;

    // Start is called before the first frame update
    void Start()
    {
        t = new Touch { fingerId = -1 };
        Background.gameObject.SetActive(false);

        Playerplayer = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            for (int a = 0; a < Input.touchCount; a++)
            {
                if (t.fingerId == -1)
                {
                    if (Input.GetTouch(a).position.x < Screen.width / 2 && Input.GetTouch(a).position.y < Screen.height / 2)
                    {
                        t = Input.GetTouch(a);
                        startPos = t.position;
                        Background.transform.position = startPos;
                    }
                }
                else
                {
                    if (Input.GetTouch(a).fingerId == t.fingerId)
                    {
                        t = Input.GetTouch(a);
                    }
                }
            }
        }

        if (t.fingerId != -1)
        {
            if (t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended)
            {
                t = new Touch { fingerId = -1 };

                // Verifica se o Background está ativo antes de desativá-lo
                if (Background.gameObject.activeSelf)
                {
                    Background.gameObject.SetActive(false);
                    Playerplayer.MovePlayer(new Vector2(0, 0));
                }
            }
            else
            {
                // Verifica se o Background não está ativo antes de ativá-lo
                if (!Background.gameObject.activeSelf)
                {
                    Background.gameObject.SetActive(true);
                }

                Vector2 dist = t.position - startPos;
                control.position = startPos + Vector2.ClampMagnitude(dist, 100);
                Playerplayer.MovePlayer(dist * 0.006f);
            }
        }
    }
}