using UnityEngine;
using System.Collections;

public class Controller2D : RaycastController {

	public CollisionInfo collisions;
	[HideInInspector]
	public Vector2 playerInput;

	public override void Start() {
		base.Start ();
		collisions.faceDir = 1;

	}

	public void Move(Vector2 moveAmount, bool standingOnPlatform) {
		Move (moveAmount, Vector2.zero, standingOnPlatform);
	}

	public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false) {
		UpdateRaycastOrigins ();

		collisions.Reset ();
		collisions.moveAmountOld = moveAmount;
		playerInput = input;
		
		if (moveAmount.x != 0) {
			collisions.faceDir = (int)Mathf.Sign(moveAmount.x);
		}

		HorizontalCollisions (ref moveAmount);
		if (moveAmount.y != 0) {
			VerticalCollisions (ref moveAmount);
		}

		transform.Translate (moveAmount);

		if (standingOnPlatform) {
			collisions.below = true;
		}
	}

	void HorizontalCollisions(ref Vector2 moveAmount) {
		float directionX = collisions.faceDir;
		float rayLength = Mathf.Abs (moveAmount.x) + skinWidth;

		if (Mathf.Abs(moveAmount.x) < skinWidth) {
			rayLength = 2*skinWidth;
		}

		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1)?raycastOrigins.bottomLeft:raycastOrigins.bottomRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);//убрать

			if (hit) {
                if (hit.collider.tag == "Enemy")
                {
                    collisions.hitEnemy = true;
                    return;
                }
                if (hit.collider.tag == "Exit")//возможно Exit будет иметь свой скрипт где будет обрабатывыатся столкновение с игроком.
                {
                    collisions.hitExit = true;
                    return;
                }
                if (hit.collider.tag == "MoveBox"&&hit.transform!=this.transform)
                {
                    float pushX = moveAmount.x - (hit.distance - skinWidth) * directionX; 
                    float pushY = -skinWidth;
                       
                    hit.collider.GetComponent<Controller2D>().Move(new Vector2(pushX, pushY), false);
                }
				if (hit.distance == 0) {
                    collisions.hitEnemy = true;//зажало меж текстур(придавило платформой)
                    Debug.Log("hit.distance = 0");
                    return;
                    //continue;
				}

				moveAmount.x = (hit.distance - skinWidth) * directionX;
				rayLength = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}
	}

	void VerticalCollisions(ref Vector2 moveAmount) {
		float directionY = Mathf.Sign (moveAmount.y);
		float rayLength = Mathf.Abs (moveAmount.y) + skinWidth;

		for (int i = 0; i < verticalRayCount; i ++) {

			Vector2 rayOrigin = (directionY == -1)?raycastOrigins.bottomLeft:raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY,Color.red);//убрать

			if (hit) {
                if (hit.collider.tag == "Enemy")
                {
                    collisions.hitEnemy = true;
                    return;
                }
                if (hit.collider.tag == "Exit")
                {
                    collisions.hitExit= true;
                    return;
                }
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
                
			}
		}
	}

	public struct CollisionInfo {
		public bool above, below;
		public bool left, right;
        public bool hitEnemy;
        public bool hitExit;


		public Vector2 moveAmountOld;
		public int faceDir;

		public void Reset() {
			above = below = false;
			left = right = false;
            hitEnemy = false;
		}
	}

}
