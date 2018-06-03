using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public AudioClip shieldUp;
    public AudioClip shieldDown;
    public SpriteRenderer sr;

    public Sprite[] shieldsPowerSprite;
    [HideInInspector]
    public int shieldPower = 0;

    private bool isShieldDown = true;
    private AudioSource aSourse;

	void Awake () {
        aSourse = GetComponent<AudioSource>();
	}
    void Start()
    {
        if (shieldPower - 1 >= 0)
        {
            isShieldDown = false;
            sr.sprite = shieldsPowerSprite[shieldPower - 1];
        }
        else
        {
            sr.enabled = false;
            isShieldDown = true;
        }
    }
    public void ShieldUp()
    {
        if (isShieldDown)
        {
            isShieldDown = false;
            sr.enabled = true;
        }
        if (shieldPower < 3)
        {
            shieldPower++;
            GameManager.shieldCount--;
            aSourse.PlayOneShot(shieldUp);
        }
        sr.sprite = shieldsPowerSprite[shieldPower - 1];
    }
    public void DamageToShield()
    {
        shieldPower --;
        aSourse.PlayOneShot(shieldDown);
        if (shieldPower <= 0)
        {
            isShieldDown = true;
            sr.enabled = false;
        }
        else
            sr.sprite = shieldsPowerSprite[shieldPower - 1];
    }
    public bool IsShieldDown
    {
        get { return isShieldDown; }
        set { isShieldDown = value; }

    }
}
