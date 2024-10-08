using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float normalmoveSpeed;
    public float sprintSpeed;
    //[SerializeField] private bool canSprint;

    private Rigidbody2D rb;
    private bool sprinting;

    //VARIABLES FOR REPAIR - MAYBE MOVE
    private DefenseBase defenseInteractableObject;
    private bool defenseInteractable;

    private bool placing;

    [SerializeField] GameObject invenPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprinting = false;

        //REPAIR VARS
        defenseInteractable = false;
        defenseInteractableObject = null;
    }

    void FixedUpdate()
    {
        //Movement Mechanics
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        var camera = Camera.main;
        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        var moveDirection = forward * verticalAxis + right * horizontalAxis;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void Update()
    {

        // Sprint Mechanics
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            if (!sprinting) 
            {
                moveSpeed += sprintSpeed;
                sprinting = true; 
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) 
        {
            moveSpeed = normalmoveSpeed;
            sprinting = false;
        }

        //REPAIR // INVENTORY CODE
        if(defenseInteractable){
            if(Input.GetKeyDown(KeyCode.F)){ //try to repair nearby defense

                defenseInteractableObject.TryToRepair(GetComponent<PlayerInventory>());

            } else if(Input.GetKeyDown(KeyCode.E)){ //pick up nearby defense

                if(!GetComponent<PlayerInventory>().InventoryIsFull()){ //check that there's room in the inventory
                     //defenseInteractableObject.gameObject.transform.parent = invenPoint.transform; //parent object to player 
                     GetComponent<PlayerInventory>().AddToInventory(defenseInteractableObject.gameObject); //add to inventory
                     defenseInteractableObject.gameObject.GetComponent<BoxCollider>().enabled = false;
                     defenseInteractableObject.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
                     defenseInteractableObject.gameObject.SetActive(false); // make object inactive
                     defenseInteractable = false; //clear object from current possible interactions
                     defenseInteractableObject = null;

                }
                
            } 
        }else if(Input.GetKeyDown(KeyCode.R)){ //finish placing defense
            if(!GetComponent<PlayerInventory>().InventoryIsEmpty()){
                if(PlaceObject()){
                 GetComponent<PlayerInventory>().SelectFromInventory().GetComponent<BoxCollider>().enabled = true;
                 GetComponent<PlayerInventory>().SelectFromInventory().GetComponentInChildren<BoxCollider>().enabled = true;
                    GetComponent<PlayerInventory>().RemoveFromInventory();
                }
            }
        }

        else if(Input.GetKey(KeyCode.C)){ //place defense
        if(!GetComponent<PlayerInventory>().InventoryIsEmpty()){
            Debug.Log("placing now");
           PlaceObject();
        }

        } 
	}
}
