using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {


    //-------------------------------------------Unity Functions-------------------------------------------

    private void Update()
    {
        var playerMovement = new Vector2(Input.GetAxis("Horizontal"),
                                         Input.GetAxis("Vertical"));
        if(playerMovement.magnitude > 0.0f) Move(playerMovement);
        ApplyGravity();
    }
}
