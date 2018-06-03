using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static GameObject currentWeapon;

    public float speed = 5f;
    public bool faceingRight;

    public GameObject shop;
    public GameObject useCase;
    public Rigidbody2D rb;
    public AudioSource asourse;
    public Animator anim;
    public Transform gunHandler;
    public Transform groundCheck;
    public LayerMask ground;

    
    public Text goldUI;
    public Text ammoUI;

    public Sprite[] ammoSprites;

    public static int money=5000;

    private LootBox lb;
    private GunShooter currentGunShooter;
    private SpriteRenderer ammoSR;
    private float groundRadius;
    private bool graunded;
    private bool canUse;
    private bool isUsed;
    private bool shopClose=true;

    private int minMoneyDrop = 50;
    private int maxMoneyDrop = 200;
    private int currentDay;


	// Use this for initialization
	void Start () 
    {
        currentDay = GameManager.currentDay;
        currentGunShooter = currentWeapon.GetComponent<GunShooter>();
        ammoSR = ammoUI.transform.GetComponentInChildren<SpriteRenderer>();
	}

    public bool ShopClose 
    {
        get { return shopClose; } 
        set { shopClose = value;} 
    }
	// Update is called once per frame
	void Update () 
    {
        currentGunShooter = currentWeapon.GetComponent<GunShooter>();//гавнецо
        ChangeAmmoIcon(currentGunShooter.ammoType);
        goldUI.text = "Gold: " + money;
        ammoUI.text = "Ammo: " + currentGunShooter.currentAmmo + "/" + currentGunShooter.ammoLeft;
        if (canUse && Input.GetKey(KeyCode.F))
        {
            lb.IsPressed = true;
            lb.OpenAnim();
            Drop(lb.TakeLoot());
            canUse = false;
            anim.SetBool("useCase", false);
            useCase.SetActive(false);
        }
        if (graunded && Input.GetKey(KeyCode.W))
        {
            anim.SetBool("grounded", false);
            rb.AddForce(new Vector2(0, speed*10));
        }
        if (Input.GetKey(KeyCode.M))
        {
            shop.SetActive(true);
            shopClose = false;
            
        }
        if(shopClose)//гавнецо
            currentGunShooter.enabled = true;
        else currentGunShooter.enabled = false;
	}
    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");

        if (move != 0)
        {
            asourse.UnPause();
        }
        else asourse.Pause();

        graunded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);
        anim.SetBool("grounded", graunded);
        

        anim.SetFloat("velocityX",Mathf.Abs(move));
        rb.velocity=new Vector2(move*speed,rb.velocity.y);
        if (move < 0 && !faceingRight)
            Flip();
        if (move > 0 && faceingRight)
            Flip();
    }
    void Flip()
    {
        faceingRight = !faceingRight;
        Vector3 useCaseScale = useCase.transform.localScale;
        Vector3 theScale = transform.localScale;
        Vector3 gunScale = gunHandler.localScale;
        useCaseScale.x *= -1;
        theScale.x *= -1;
        gunScale.x *= -1;
        gunScale.y *= -1;
        transform.localScale = theScale;
        gunHandler.localScale = gunScale;
        useCase.transform.localScale = useCaseScale;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "LootBox")
        {
            lb = col.GetComponent<LootBox>();
            if (!lb.IsPressed)
            {
                useCase.SetActive(true);
                anim.SetBool("useCase", true);
                canUse = true;
            }
            else canUse = false;
        }
        if (col.tag == "ZombieDrop")
        {
            int id=col.GetComponent<ZombieDrop>().DropIndex;
            switch (id)
            {
                case 0:
                    int ammo = minMoneyDrop * currentDay;
                    if (currentGunShooter.ammoLeft + ammo <= currentGunShooter.maxAmmo)
                        currentGunShooter.ammoLeft += ammo;
                    else currentGunShooter.ammoLeft = currentGunShooter.maxAmmo;
                    break;

                case 1:
                    int gold = Random.Range(minMoneyDrop, maxMoneyDrop) * currentDay;
                    money += gold;
                    break;
                case 2:
                    GetComponent<Heals>().CurrentHeals+=10;
                    break;
            }
            Destroy(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        anim.SetBool("useCase", false);
        useCase.SetActive(false);
    }
    void Drop(string item)
    {
        switch (item)
        {
            case "ammo":
                //зависит от текущего оружия
                int ammo = Random.Range(minMoneyDrop, currentGunShooter.maxAmmo / 2) * currentDay;
                if (currentGunShooter.ammoLeft + ammo <= currentGunShooter.maxAmmo)
                    currentGunShooter.ammoLeft += ammo;
                else currentGunShooter.ammoLeft = currentGunShooter.maxAmmo;
                lb.DropAnim(4);
                //*день
                break;
            case "gold":
                int gold = Random.Range(minMoneyDrop, maxMoneyDrop) * currentDay;
                money += gold;
                lb.DropAnim(0);
                //*день
                break;
            case "Hmoney":
                int Hgold = Random.Range(minMoneyDrop, maxMoneyDrop) * currentDay;
                money += Hgold*10;
                //10х*день
                lb.DropAnim(5);
                break;
            case "fullammo":
                //на текущее оружие
                currentGunShooter.ammoLeft = currentGunShooter.maxAmmo;
                lb.DropAnim(1);
                break;
            case "fullHP":

                GetComponent<Heals>().CurrentHeals = GetComponent<Heals>().maxHeals;
                lb.DropAnim(2);
                break;
                
            case "weapon":
                //но не более +2 от открытых
                lb.DropAnim(3);
                break;
            case "turret":
                //но не более +2 от открытых
                lb.DropAnim(3);
                break;
        }
    }
    void ChangeAmmoIcon(State type)
    {
        switch (type)
        {
            case State.pistol:
                ammoSR.sprite = ammoSprites[0];
                break;
            case State.shotgun:
                ammoSR.sprite = ammoSprites[1];
                break;
            case State.semiauto:
                ammoSR.sprite = ammoSprites[2];
                break;
            case State.rifle:
                ammoSR.sprite = ammoSprites[3];
                break;
            case State.minigun:
                ammoSR.sprite = ammoSprites[4];
                break;
        }
    }
}
