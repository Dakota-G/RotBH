using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Players
{
    namespace PMovement
    {
        public class MouseFollow : MonoBehaviour
        {
            public static Vector2 Movement;

            public static void Move(PC_Class PC)
            {
                float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? PC.Speed*2 : PC.Speed;
                Movement.x = Input.GetAxisRaw("Horizontal");
                Movement.y = Input.GetAxisRaw("Vertical");
                Vector2 MousePos = PC.Cam.ScreenToWorldPoint(Input.mousePosition);

                PC.RB.MovePosition(PC.RB.position + Movement * moveSpeed * Time.fixedDeltaTime);

                Vector2 lookDir = MousePos - PC.RB.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                PC.RB.rotation = angle;
            }
        }
    }
}
