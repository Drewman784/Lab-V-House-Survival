using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System;
using Unity.VisualScripting;

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

    [SerializeField] GameObject invenPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
            if(Input.GetKeyDown(KeyCode.F)){
                defenseInteractableObject.TryToRepair(GetComponent<PlayerInventory>());
            } else if(Input.GetKeyDown(KeyCode.E)){
                if(GetComponent<PlayerInventory>().IsHolding()){
                    PlaceObject();
                }
                defenseInteractableObject.gameObject.transform.parent = invenPoint.transform;
                //defenseInteractableObject.gameObject.transform.localScale = new Vector3(f,0.2f,0.2f);
                GetComponent<PlayerInventory>().SetHolding(defenseInteractableObject.gameObject);
                defenseInteractableObject.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                defenseInteractable = false;
                defenseInteractableObject = null;
            } 
        }else if(Input.GetKeyDown(KeyCode.E)){
            PlaceObject();
        }

    }

    //REPAIR CODE
    public void EnterDefenseRange(DefenseBase defObj){
        defenseInteractableObject = defObj;
        defenseInteractable = true;
        Debug.Log("entered range");
    }

    public void ExitDefenseRange(DefenseBase defObj){
        if(defObj == defenseInteractableObject){
            defenseInteractableObject = null;
            defenseInteractable = false;
        }
    }

    //INVENTORY CODE
    private void PlaceObject(){
        if(GetComponent<PlayerInventory>().IsHolding()){
            GameObject toPlace = GetComponent<PlayerInventory>().GetHolding();
            toPlace.transform.localScale =  new Vector3(5f,5f,5f);
            invenPoint.transform.DetachChildren();
            toPlace.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

}
