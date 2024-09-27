using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float normalmoveSpeed;
    public float sprintSpeed;
    public float maxmoveSpeed;
    [SerializeField] private bool canSprint;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //movement mechanics
        float movementInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float movementInput2 = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0, 0, movementInput);
        transform.position += new Vector3(movementInput2, 0, 0);

        //Sprint Code that doesn't work yet
        if (Input.GetKeyDown(KeyCode.LeftShift) && CheckIfSprint() && canSprint)
        {
            moveSpeed += sprintSpeed;
        }
        else
        {
            moveSpeed = normalmoveSpeed;
        }

    }

    private bool CheckIfSprint()
    {
        
        return canSprint;
    }

}
