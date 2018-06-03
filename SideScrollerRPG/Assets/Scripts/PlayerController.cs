using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static bool isInThePit=true;

    public PlayerSO playerParam;
    public Rigidbody2D rb2d;
    public SpriteRenderer sr;
    public Transform gunHandle;
    public GameObject bullet;
    public SimpleObjectPool pool;

    public Slider ammoBar;
    public string shootSoundName;

    Positions currentPos;
    float minAngle = -45+90;
    float maxAngle = 45+90;
    float nextShoot;
    float currAmmo;
    float nextReload;
    int bulletSplash;

    public const float maxSplashAngle=45; //максимальный угол на который разлетаются пули.
    const int MOVELEIGHT = 3;

	void Start () 
    {
        currentPos = Positions.Mid;
        currAmmo = playerParam.magazineSize;
        bulletSplash = playerParam.bulletSplash;
	}
	
	void Update () {

        if (Time.time >= nextReload && currAmmo < playerParam.magazineSize)
        {
            currAmmo++;
            nextReload = Time.time + playerParam.reloadTime;
        }

        if(CrossPlatformInputManager.GetButtonDown("Up"))
            Move(true);
        if (CrossPlatformInputManager.GetButtonDown("Down"))
            Move(false);

        if (CrossPlatformInputManager.GetButton("Shoot"))
        {
            Vector2 dir = LookAtTouch();//нет необходимости определять направление.
            if (Time.time >= nextShoot&&currAmmo>0)
            {
                nextShoot = Time.fixedTime + (1 / playerParam.fireRate);

                isInThePit = false;
                sr.color = Color.black;

                Shoot();
                currAmmo--;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            bulletSplash = playerParam.bulletSplash;
            isInThePit = true;
            sr.color = Color.grey;
        }
        ammoBar.value = currAmmo / playerParam.magazineSize;
	}
    public void Move(bool up)
    {
        if (up)
        {
            if (currentPos != Positions.Up)
            {
                //move up
                rb2d.MovePosition(new Vector2(transform.position.x, transform.position.y + MOVELEIGHT));
                currentPos--;
            }
        }
        else
        {
            if (currentPos != Positions.Down)
            {
                //move down
                rb2d.MovePosition(new Vector2(transform.position.x, transform.position.y - MOVELEIGHT));
                currentPos++;
            }
        }
    }
    public void Shoot()
    {
        AudioManager._instance.PlaySoundEffect(shootSoundName);

        float stepAngleSize = (1f / bulletSplash) + bulletSplash / 3;

        for (int i = 0; i < bulletSplash; i++)
        {
            float angle = (-gunHandle.localEulerAngles.z + 90 - stepAngleSize / 2 + (bulletSplash * stepAngleSize / 2)) - (stepAngleSize * (i));

            GameObject blet = pool.GetObject(bullet);// Instantiate(bullet, gunHandle.GetChild(0).position, gunHandle.rotation);
            blet.transform.position = gunHandle.GetChild(0).position;
            blet.transform.rotation = gunHandle.rotation;
            blet.SetActive(true);
            blet.GetComponent<PlayerBullet>().PlayerInit(playerParam.damage, playerParam.bulletSpeed, playerParam.pushbackPower, DirFromAngle(angle, true).normalized);
        }
        bulletSplash = (bulletSplash <= 2) ? 1 : --bulletSplash;
    }
    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees -=gunHandle.localEulerAngles.z-90;
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    Vector2 LookAtTouch()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gunHandle.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        gunHandle.localRotation = Quaternion.Euler(0, 0, 90 - angle);
        return dir;
    }
    /*void DrawSplashview()
    {
        float stepAngleSize = (1f / bulletSplash) + bulletSplash/3;

        for (int i = 0; i < bulletSplash; i++)
        {
            float angle = (-gunHandle.localEulerAngles.z + 90 - stepAngleSize/2+(bulletSplash * stepAngleSize / 2)) - (stepAngleSize * (i));
            Debug.DrawLine(gunHandle.GetChild(0).position,gunHandle.GetChild(0).position+(Vector3)DirFromAngle(angle,true)*10,Color.green);
        }
    }*/
}
public enum Positions {Up,Mid,Down }
