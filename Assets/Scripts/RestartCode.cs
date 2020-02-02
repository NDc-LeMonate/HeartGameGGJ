using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartCode : MonoBehaviour
{
    private void Update()
    {
        if(GameController.instance.isGameOver)
        {
            GetComponent<UnityEngine.UI.Text>().enabled = true;
        }
    }
}
