using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed = 5.0f;
    public Animator animator;
    public static PlayerController Instance(){return instance;}
    private static PlayerController instance;
    public string AreaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private string PlayerName = "Tim";

    public bool canMove = true;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            ProcessMovementInput();
        }
        else
        {
            StopAllMovement();
        }
        
        animator.SetFloat("moveX", rigidBody.velocity.x);
        animator.SetFloat("moveY", rigidBody.velocity.y);
    }

    private void StopAllMovement()
    {
        rigidBody.velocity = Vector2.zero;
    }

    private void ProcessMovementInput()
    {
        rigidBody.velocity =
            new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        // keep player inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
        topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);;
    }

    public string GetPlayerName()
    {
        return PlayerName;
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }
}
