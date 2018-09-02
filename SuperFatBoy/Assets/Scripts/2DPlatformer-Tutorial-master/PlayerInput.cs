using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour {

    public bool isMobile;
    public SoundSO runSound;
	Player player;

    void Awake()
    {
        PlayerSpawner.spawnEvent += GetPlayer;
    }

    void GetPlayer(Transform obj)
    {
        CrossPlatformInputManager.SetAxisZero("Horizontal");//если игрок умер нужно освободить кнопки от нажатия
        CrossPlatformInputManager.SetAxisZero("Vertical");
        player = obj.GetComponent<Player>();
    }

	void Update () {
        if (isMobile)
        {
            Vector2 directionalInput = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);

            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                player.OnJumpInputDown();
            }
            if (CrossPlatformInputManager.GetButtonUp("Jump"))
            {
                player.OnJumpInputUp();
            }
            if (directionalInput != Vector2.zero)//будет проверятся по нажатию клавиш
            {
                //AudioManager._instance.PlaySoundEffect(runSound);
                LevelManager.instance.StartTimer();
            }
        }
        else
        {
            Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.SetDirectionalInput(directionalInput);

            if (Input.GetButtonDown("Jump"))
            {
                player.OnJumpInputDown();
            }
            if (Input.GetButtonUp("Jump"))
            {
                player.OnJumpInputUp();
            }
            if (directionalInput != Vector2.zero)//будет проверятся по нажатию клавиш
            {
                //AudioManager._instance.PlaySoundEffect(runSound);
                LevelManager.instance.StartTimer();
            }
        }
	}
}
