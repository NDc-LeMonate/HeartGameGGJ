using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<Transform> tileList = new List<Transform>();

    public Vector3 offset;

    PlayerScript[] players = new PlayerScript[2];

    private void Awake()
    {
        instance = this;
        players = FindObjectsOfType<PlayerScript>();
    }

    public bool IsThereGround(Vector3 pos)
    {

        foreach (var tile in tileList)
        {
            if(tile.transform.position == (pos + offset))
            {
                return true;
            }
        }
        return false;
    }

    public bool ArePlayersReady()
    {
        return !(players[0].isMoving || players[1].isMoving);
    }
}
