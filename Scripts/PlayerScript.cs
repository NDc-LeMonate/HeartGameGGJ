using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool isRightPlayer = false;

    public bool isMoving = false;
    public bool IsFinished = false;


    public Vector3 dist;    

    private void Start()
    {
        dist = transform.position;
    }

    void Update()
    {
        //if (isMoving)
        //{
        //    MoveDist(transform, dist);
        //}        
        //else if(GameController.instance.ArePlayersReady())
        //{           
        //    dir = InputController();
        //}

        //if (isRightPlayer)
        //{
        //    dir.x *= -1f;
        //}

        //if (dir.magnitude >0 && GameController.instance.IsThereGround(Utility.NearestVector(transform.position + dir)))
        //{
        //    isMoving = true;
        //}               
        //else 
        //{            
        //    return;
        //}



        //dist = transform.position + dir;
        //dir = Vector3.zero;

        if(isMoving)
        {
            MoveDist(dist);
        }        

    }


  

    void MoveDist(Vector3 _dist )
    {
        transform.position = Vector3.Lerp(transform.position, _dist, 5 * Time.deltaTime);

        if (Vector3.Distance(transform.position, _dist) < 0.01f)
        {
            TileScript tileScript;

            if (GameController.instance.CheckTile(Utility.NearestVector(base.transform.position), out tileScript) == TileScript.TileType.Conveyor)
            {
                dist = Utility.NearestVector(base.transform.position + tileScript.ConveyorDir());
                return;
            }

            transform.position = Utility.NearestVector(transform.position);
            isMoving = false;
        }

    }

   
}
