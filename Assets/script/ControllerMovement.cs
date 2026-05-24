using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static float move;
    public static bool jumpPressed;
    public static bool attackPressed;

    // =========================
    // MOVE
    // =========================

    public void MoveLeftDown()
    {
        move = -1f;
    }

    public void MoveRightDown()
    {
        move = 1f;
    }

    public void StopMove()
    {
        move = 0f;
    }

    // =========================
    // JUMP
    // =========================

    public void Jump()
    {
        jumpPressed = true;
    }

    public static bool GetJump()
    {
        bool pressed = jumpPressed;
        jumpPressed = false;
        return pressed;
    }

    // =========================
    // ATTACK
    // =========================

    public void Attack()
    {
        attackPressed = true;
    }

    public static bool GetAttack()
    {
        bool pressed = attackPressed;
        attackPressed = false;
        return pressed;
    }
}