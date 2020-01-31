using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<TileScript> tileList = new List<TileScript>();

    public Vector3 offset;

    PlayerScript[] players = new PlayerScript[2];

    Vector3 dir;

    private void Awake()
    {
        instance = this;

        players = FindObjectsOfType<PlayerScript>();
    }

    public bool IsThereGround(Vector3 pos)
    {

        foreach (var tile in tileList)
        {
            if (tile.transform.position == (pos + offset))
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

    Vector3 InputController()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0) v = 0;

        return new Vector3(h, 0, v);
    }

    public TileScript.TileType CheckTile(Vector3 pos, out TileScript tileScript)
    {
        foreach (var tile in tileList)
        {
            if (tile.transform.position == Utility.NearestVector(pos + offset))
            {
                tileScript = tile;
                return tile.tileType;
            }
        }

        tileScript = null;
        return TileScript.TileType.Normal;
    }

    public TileScript.TileType CheckTile(Vector3 pos)
    {
        foreach (var tile in tileList)
        {
            if (tile.transform.position == Utility.NearestVector(pos + offset))
            {
                return tile.tileType;
            }
        }

        return TileScript.TileType.Normal;
    }

    private void Update()
    {
        if (!ArePlayersReady())
        {
            return;
        }

        dir = InputController();

        if (dir == Vector3.zero)
            return;

        foreach (var player in players)
        {
            if (player.isRightPlayer)
                dir.x *= -1;

            if (!IsThereGround(dir + player.transform.position))
            {
                continue; // normal ground


            }
            else
            {

                player.isMoving = true;
                player.dist = dir + player.transform.position;
                if (CheckTile(dir + player.transform.position) == TileScript.TileType.Pit)
                {
                    Debug.Log("GameOver");
                }

            }
        }
    }


}
