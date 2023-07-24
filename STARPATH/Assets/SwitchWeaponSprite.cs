using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeaponSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] weapons;
    private PlayerController1 playerController;
    private int shootMode = 1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        shootMode = AbilityManager.AbilityManagerInstance.GetShootMode();
        if (shootMode == 1)
        {
            spriteRenderer.sprite = weapons[0];
        }
        if (shootMode == 2)
        {
            spriteRenderer.sprite = weapons[2];
        }
        if (shootMode == 3)
        {
            spriteRenderer.sprite = weapons[1];
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            spriteRenderer.sprite = weapons[0];
        }
        if (PlayerController1.freya)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                spriteRenderer.sprite = weapons[2];
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                spriteRenderer.sprite = weapons[1];
            }
        }

    }

    public void gunCheck(int  keycode)
    {
        if (keycode == 1)
        {
            spriteRenderer.sprite = weapons[0];
        }
        if (keycode == 2)
        {
            spriteRenderer.sprite = weapons[2];
        }
        if (keycode == 3)
        {
            spriteRenderer.sprite = weapons[1];
        }
    }
}
