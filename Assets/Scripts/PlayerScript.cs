using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool isRightPlayer = false;

    public bool isMoving = false;
    public bool IsFinished = false;


    public Vector3 dist;
    [HideInInspector] public Animator anim;
    private void Start()
    {
        dist = transform.position;
        anim = GetComponentInChildren<Animator>();
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
<<<<<<< HEAD
        if(GameController.instance.isLevelFinished)
        {
          //  anim.SetTrigger("Dance");
        }
        else
        {
            anim.SetBool("IsMoving", isMoving);
        }
        
=======

        GetComponentInChildren<Animator>().SetBool("IsMoving", isMoving);
>>>>>>> 23a1d3f014bf60c862f122e78b1e55a4d72b7d02
        if(isMoving)
        {
            MoveDist(dist);
        }        

    }


  

    void MoveDist(Vector3 _dist )
    {
        transform.position = Vector3.MoveTowards(transform.position, _dist, Time.deltaTime * 3);
        //transform.position = Vector3.Lerp(transform.position, _dist, 10 * Time.deltaTime);
        Vector3 dir = _dist - transform.position;
<<<<<<< HEAD
        if ( Vector3.SqrMagnitude (dir) != 0 && !GameController.instance.isLevelFinished)
=======
        if ( Vector3.SqrMagnitude (dir) != 0)
>>>>>>> 23a1d3f014bf60c862f122e78b1e55a4d72b7d02
            transform.forward = dir;

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
