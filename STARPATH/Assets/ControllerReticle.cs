using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerReticle : MonoBehaviour
{
    public Transform playerPos;
    [SerializeField] private SpriteRenderer reticleSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (AbilityManager.AbilityManagerInstance.GetController())
        {
            reticleSprite.color = new Color(1, 0, 0, 1);
        }
        else
        {
            reticleSprite.color = new Color(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(playerPos.position.x + (Input.GetAxis("RightStickX")*5), playerPos.position.y + (Input.GetAxis("RightStickY")*-3.5f));

        if (AbilityManager.AbilityManagerInstance.GetController())
        {
            reticleSprite.color = new Color(1, 0, 0, 1);
        }
        else
        {
            reticleSprite.color = new Color(0, 0, 0, 0);
        }
    }


}
