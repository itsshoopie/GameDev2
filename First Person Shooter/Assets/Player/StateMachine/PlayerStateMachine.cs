using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.AI;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState current_state;
    public PlayerGroundState ground_state = new PlayerGroundState();
    public PlayerAirState air_state = new PlayerAirState();
    
    
    //Debug
    public TMP_Text debug_text;
    [HideInInspector] public bool grounded;

    //Movement Variables
    [HideInInspector] public Vector2 move_input;
    [HideInInspector] public CharacterController Character_Controller;
    [HideInInspector] public Vector3 player_velocity;
    [HideInInspector] public Vector3 wish_dir = Vector3.zero;
    [HideInInspector] public bool jump_button_pressed = false;



    // Start is called before the first frame update
    void Start()
    {
        Character_Controller = GetComponent<CharacterController>();
        current_state = ground_state;
        current_state.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        current_state.UpdateState(this);
        DebugText();
    }

    void FixedUpdate()
    {
        FindWishDir();
        current_state.FixedUpdateState(this);
        MovePlayer();
        grounded = Character_Controller.isGrounded;
    }

    public void SwitchState(PlayerBaseState cur_state, PlayerBaseState new_State)
    {
        cur_state.ExitState(this);
        current_state = new_State;
        current_state.EnterState(this);
    }

    public void GetMoveInput(InputAction.CallbackContext context)
    {
        move_input = context.ReadValue<Vector2>();
    }
        public void GetJumpInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started) jump_button_pressed = true;
        if(context.phase == InputActionPhase.Canceled) jump_button_pressed = false;
    }

    public void DebugText()
    {
        debug_text.text = "Wish Dir " + wish_dir.ToString();
        debug_text.text += "\nPlayer Velocity: " + player_velocity.ToString();
        debug_text.text += "\nPlayer_Speed" + new Vector3(player_velocity.x, 0, player_velocity.z).magnitude.ToString();
        debug_text.text += "\n Grounded: " + grounded.ToString();
        debug_text.text += "\n State: " + current_state.ToString();
    
    }

    public void FindWishDir()
    {
        wish_dir = transform.right * move_input.x + transform.forward * move_input.y;
        wish_dir = wish_dir.normalized;
    }

    public void MovePlayer()
    {
  Character_Controller.Move(player_velocity * Time.deltaTime);
    }

}
