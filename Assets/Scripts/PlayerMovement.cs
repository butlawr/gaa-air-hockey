﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Varaibles
    bool wasJustClicked = true;
    bool canMove;

    Rigidbody2D rb;

    // Boundary Holder to apply the game object boundary holder to this PlayerMovement Script:  
    public Transform BoundaryHolder;
    Boundary playerBoundary;

    Collider2D playerCollider;


    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        // Asigning Rigidbody to game object this script set on.
        rb = GetComponent<Rigidbody2D>();
        //Setting the boundary cords of the child game objects when the game starts.
        // Used to keep the player pusher object inside the boundary.
        playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                        BoundaryHolder.GetChild(1).position.y,
                                        BoundaryHolder.GetChild(2).position.x,
                                        BoundaryHolder.GetChild(3).position.x);

    }// End start

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (wasJustClicked)
            {
                wasJustClicked = false;

                // If the player asset was clicked from inside the transform axis the player can then move.
                if(playerCollider.OverlapPoint(mousePos))
                {
                    canMove = true;
                }
                else
                {
                    // If it is not within the assets transform axis then the player cannot move.
                    canMove = false;
                }// End if else for can move when clicked
            }// End if

            if (canMove)
            {
                Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                                                playerBoundary.Right),
                                                    Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                                                playerBoundary.Up));
                rb.MovePosition(clampedMousePos);
            }// End if
        }
        else
        {
            wasJustClicked = true;
        }// End else
    }// End Update Function
}// End class
