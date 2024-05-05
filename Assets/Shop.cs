using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
     if(   other.CompareTag("player"))
            {
            Debug.Log("Player");

             }
    }
}
