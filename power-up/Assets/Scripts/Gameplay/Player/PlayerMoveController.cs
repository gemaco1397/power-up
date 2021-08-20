using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    
    Vector2 playerInput;
    Vector3 move;
    [SerializeField] float speed;
    CharacterController cc;
    
    // Start is called before the first frame update
    void Start()
    {
        
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        cc.Move(CalculateMove());
        
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

}
