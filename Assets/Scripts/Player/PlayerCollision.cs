using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerCollision : MonoBehaviour
{
    #region Unity Methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Trigger(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Trigger(collision);
    }
    #endregion

    private void Trigger(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (PlayerInput.GetAxis(push) == 1)
            //    Debug.Log("push");
        }
    }
}
