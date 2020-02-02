using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<TileScript> tileList = new List<TileScript>();
    public List<Enemy> enemies = new List<Enemy>();
    public GameObject cameraControl;
    public GameObject canvasPrefab;
    public GameObject eventSystemPrefab;

    public Vector3 offset;

    PlayerScript[] players = new PlayerScript[2];

    Vector3 dir;

    public bool isGameOver = false;
    public bool gameOverActivated = false;

    public Material skyboxMat;

   public bool isPlayerTurn = true;

    float skyboxRotateRandomSeed = 0;

    public bool isLevelFinished = false;
    public bool levelFinishedActivated = false;

    public AudioClip winningClip,switchClip;

    public AudioSource auscFX, auscBG;

    private void Awake()
    {
        instance = this;
        RenderSettings.skybox = skyboxMat;

        players = FindObjectsOfType<PlayerScript>();
        skyboxRotateRandomSeed = Random.Range(0, 100);
        Instantiate(cameraControl, transform.position, Quaternion.identity);
        Instantiate(canvasPrefab);
        Instantiate(eventSystemPrefab);


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

    public bool ArePlayersStopMoving()
    {
        return !(players[0].isMoving || players[1].isMoving);
    }

    public bool AreEnemiesStopMoving()
    {
        foreach (var enemy in enemies)
        {
            if(enemy.isMoving)
            {
                return false;
            }
        }
        return true;
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
            if (tile.transform.position == Utility.NearestVector    (pos + offset))
            {
                return tile.tileType;
            }
        }

        return TileScript.TileType.Normal;
    }

    private void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time + skyboxRotateRandomSeed);

    }

    IEnumerator ProceedLevel()
    {
        yield return new WaitForSeconds(8);
        if (SceneManager.GetActiveScene().buildIndex == 17)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    private void Update()
    {


        if (isLevelFinished)
        {
            if(!levelFinishedActivated)
            {
                StartCoroutine(ProceedLevel());
            }
            levelFinishedActivated = true;
            return;
        }

        if(isGameOver)
        {

            if (!gameOverActivated)
            {
                players[0].anim.SetTrigger("Die");
                players[1].anim.SetTrigger("Die");
            }

            gameOverActivated = true;

            
           

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
            }
            return;
        }

        foreach (var player in players)
        {
            foreach (var enemy in enemies)
            {
                if(player.transform.position == enemy.transform.position)
                {
                    isGameOver = true;
                   // Debug.Log("GameOver");
                }
            }
        } 

        if ( players[0].IsFinished && players[1].IsFinished )
        {
            isLevelFinished = true;

            players[0].transform.forward= players[1].transform.position - players[0].transform.position;
            players[1].transform.forward = players[0].transform.position - players[1].transform.position;
            auscBG.clip = winningClip;
            
            auscBG.Play();
            players[0].anim.SetTrigger("Dance");
            players[1].anim.SetTrigger("Dance");
            //    Debug.Log("Game!!");
        }
        else if (!players[0].IsFinished && !players[1].IsFinished)
        {
            //nothing
        }
        else
        {
            isGameOver = true;
         //   Debug.Log("GameOver");
        }

        if (enemies.Count == 0)
        {
            isPlayerTurn = true;
        }


        if(ArePlayersStopMoving() && !isPlayerTurn && AreEnemiesStopMoving())
        {
            foreach (var enemy in enemies)
            {
                enemy.SetDestination();
            }
        }

       

        if (!ArePlayersStopMoving() || !isPlayerTurn)
        {
            return;
        }

       

        dir = InputController();

        if (dir == Vector3.zero)
            return;

        foreach (var player in players)
        {
            Vector3 curDir = dir;
            if (player.isRightPlayer)
                curDir.x *= -1;

            if (!IsThereGround(curDir + player.transform.position))
            {
                continue; // normal ground

            }
            else
            {
                if(CheckTile(curDir + player.transform.position) == TileScript.TileType.Barrier)
                {
                    continue;
                }                

                isPlayerTurn = false;
                player.isMoving = true;
                player.dist = curDir + player.transform.position;

                if (CheckTile(curDir + player.transform.position) == TileScript.TileType.Pit)
                {
                    isGameOver = true;
                   // Debug.Log("GameOver");
                }
                else if (CheckTile(curDir + player.transform.position) == TileScript.TileType.Goal)
                {
                    player.IsFinished = true;
                   // Debug.Log( player.name + " Reached");
                }
                else if(CheckTile((curDir + player.transform.position),out TileScript tileScript) == TileScript.TileType.Switch)
                {
                    //   Debug.Log("SwitchNinn");
                    
                    tileScript.ClearSwitch();

                    auscFX.clip = switchClip;
                    auscFX.pitch = 3;
                    auscFX.volume = 0.2f;
                    auscFX.Play();

                    tileScript.switchedTile.ClearBarrier();
                }

            }
        }
    }


}
