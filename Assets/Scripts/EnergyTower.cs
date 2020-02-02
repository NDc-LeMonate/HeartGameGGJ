using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyTower : MonoBehaviour
{

    [SerializeField] GameObject firstFX, secondFX,sparkFX;

    private void Update()
    {
        if(GameController.instance.isLevelFinished)
        {
            firstFX.SetActive(true);
            secondFX.SetActive(true);
            sparkFX.SetActive(false);
        }
    }
}
