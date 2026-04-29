using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static float move;
    public static bool jump;
    public static bool attack;

    public void MoveLeft()
    {
        move = -1f;
    }

    public void MoveRight()
    {
        move = 1f;
    }

    public void StopMove()
    {
        move = 0f;
    }

    public void Jump()
    {
        jump = true;
    }

    public void Attack()
    {
        attack = true;
    }
}