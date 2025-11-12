using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Transform player1;
    public Transform player2;
    public float maxDistanceX = 50f;

    void Update()
    {
        float distanceX = player2.position.x - player1.position.x;

        if (distanceX > maxDistanceX)
        {
            float midX = (player1.position.x + player2.position.x) / 2f;
            player1.position = new Vector3(midX - maxDistanceX / 2f, player1.position.y, player1.position.z);
            player2.position = new Vector3(midX + maxDistanceX / 2f, player2.position.y, player2.position.z);
        }
        else if (distanceX < -maxDistanceX)
        {
            float midX = (player1.position.x + player2.position.x) / 2f;
            player1.position = new Vector3(midX + maxDistanceX / 2f, player1.position.y, player1.position.z);
            player2.position = new Vector3(midX - maxDistanceX / 2f, player2.position.y, player2.position.z);
        }
    }
}
