using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public bool isRightPlayer = false;
    float speed = 5f;

    public bool isMoving = false;
    
    Vector3 dist;
    Vector3 dir;
    private void Start()
    {
        dist = transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveDist(transform, dist);
        }        
        else 
        {           
            dir = InputController();
        }

        if (isRightPlayer)
        {
            dir.x *= -1f;
        }

        if (dir.magnitude >0 && GameController.instance.IsThereGround(Utility.NearestVector(transform.position + dir)))
        {
            isMoving = true;
        }               
        else 
        {            
            return;
        }

       

        dist = transform.position + dir;
        dir = Vector3.zero;

    }


    Vector3 InputController()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0) v = 0;

        return new Vector3(h, 0, v);
    }

    void MoveDist(Transform _transform, Vector3 _dist )
    {
        _transform.position = Vector3.Lerp(_transform.position, _dist, 5 * Time.deltaTime);

        if (Vector3.Distance(_transform.position, _dist) < 0.01f)
        {
            _transform.position = Utility.NearestVector(_transform.position);
            isMoving = false;
        }

    }

   
}
