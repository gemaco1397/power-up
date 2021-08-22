using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    
    Vector2 playerInput;
    Vector3 move;
    [SerializeField] float speed;
    CharacterController cc;
    public bool alive;

    #region singleton
    public static PlayerMoveController i;
    private void Awake()
    {
        i = this;
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            cc.Move(CalculateMove());
        }
        
        
    }
    private Vector3 CalculateMove()
    {
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxisRaw("Vertical");
        playerInput = playerInput.normalized;

        move = transform.right * playerInput.x + transform.forward * playerInput.y;
        move.y = -2;
        move *= (speed * Time.deltaTime);
        return move;
    }

    public void DisableControlls()
    {
        alive = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void EnableControlls()
    {
        alive = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
