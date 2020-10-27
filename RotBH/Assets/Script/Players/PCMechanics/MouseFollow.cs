using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Players
{
    namespace PCMechanics
    {
        public class MouseFollow : MonoBehaviour
        {
            public static Vector2 Movement;

            public static void Move(Rigidbody2D rb, Camera cam, float speed)
            {
                float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? speed*2 : speed;
                Movement.x = Input.GetAxisRaw("Horizontal");
                Movement.y = Input.GetAxisRaw("Vertical");
                Vector2 MousePos = cam.ScreenToWorldPoint(Input.mousePosition);

                rb.MovePosition(rb.position + Movement * moveSpeed * Time.fixedDeltaTime);

                Vector2 lookDir = MousePos - rb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
            }
        }
    }
}
