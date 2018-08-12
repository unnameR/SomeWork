using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    public static event System.Action deathEvent;

    public SoundSO jumpSound;
    public SoundSO deathSound;
	public float maxJumpHeight = 3.2f;
	public float minJumpHeight = 0.2f;
    public float timeToJumpApex = .4f;
    public float moveSpeed = 9;
	[SerializeField]float accelerationTimeAirborne = .15f;
    [SerializeField]
    float accelerationTimeGrounded = .05f;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	//public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;
    Animator anim;
	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

	void Start() {
		controller = GetComponent<Controller2D> ();
        anim = GetComponent<Animator>();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	void Update() {
		CalculateVelocity ();
		HandleWallSliding ();

		controller.Move (velocity * Time.deltaTime, directionalInput);
        if (controller.collisions.hitExit)
        {
            LevelManager.instance.HitExit();
            Destroy(gameObject);
        }
        if (controller.collisions.hitEnemy)
        {
            Die();
        }
		if (controller.collisions.above || controller.collisions.below) 
        {
			velocity.y = 0;
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
        anim.SetFloat("run", input.x);
	}
    public void Trapmline(float power)
    {
        velocity.y = power;
    }
    public void Push(float power)
    {
        velocity.y += power;
    }
	public void OnJumpInputDown() {

        AudioManager._instance.PlaySoundEffect(jumpSound);
        anim.SetTrigger("jump");

		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) 
        {
			velocity.y = maxJumpVelocity;
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

    public void Die()
    {
        AudioManager._instance.PlaySoundEffect(deathSound);
        Debug.Log("Dead");
        anim.SetTrigger("dead");
        if (deathEvent != null)
            deathEvent();
        Destroy(gameObject);
    }
	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;//анимация скольжения по стене
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below /*&& velocity.y < 0*/) 
        {
			wallSliding = true;
            //anim.SetBool("wallSlide", true);

            /*if (velocity.y < -wallSlideSpeedMax)//Скорость реко уменьшается если после полёта прислонится к стене. Возможно стоит сделать мягкий переход от скорости полёта до скорости скольжения
            {
                velocity.y = -wallSlideSpeedMax;
            }*/

            if (velocity.y < 0 && timeToWallUnstick > 0)
            {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;//слишком быстро ускоряет игрока вниз. Возможно стоит немного уменьшить гравитацию или ограничить максимальную скорость падения.
	}
}
