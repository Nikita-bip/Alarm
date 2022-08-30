using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyboardController : MonoBehaviour
{
    public Player Player;

    private void Update () 
    {
        if (Player != null)
        {

            if (Input.GetKey(KeyCode.D) | (Input.GetKey(KeyCode.A)))
            {
                Player.Move();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.Jump();
            }
        }
    }
}
