using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float JumpCount = 0;
    bool isOnGround;
    float JumpForce = 10.0f;
    float gravityModifier = 2.0f;
    float speed = 40f;
    float zLimit = 19.8f;
    float xLimit = 19.8f;

    Rigidbody playerRb;
    Renderer playerRdr;

    public Material[] playerMtrs;

    // DECLARTION ONLY - END

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation = start
        isOnGround = true;
        Physics.gravity *= gravityModifier;

        playerRb = GetComponent <Rigidbody>();
        playerRdr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);
        if (transform.position.z < -zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLimit);
            playerRdr.material.color = playerMtrs[2].color;
        }
        else if (transform.position.z > zLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLimit);
            playerRdr.material.color = playerMtrs[3].color;
        }
        if (transform.position.x < -xLimit)
        {
            transform.position = new Vector3(-xLimit, transform.position.y,transform.position.z);
            playerRdr.material.color = playerMtrs[4].color;
        }
        else if (transform.position.x > xLimit)
        {
            transform.position = new Vector3(xLimit, transform.position.y, transform.position.z);
            playerRdr.material.color = playerMtrs[5].color;
        }
        PlayerJump();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Listen for collision with the GamePlane TAG
        if (collision.gameObject.CompareTag("GamePlane") && JumpCount == 0)
        {
            isOnGround = true;
            playerRdr.material.color = playerMtrs[1].color;
            JumpCount = 1;
        }
    }
    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& isOnGround)
        {
            playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isOnGround = false;
            JumpCount = 0;

            playerRdr.material.color = playerMtrs[0].color;
        }
    }
}
