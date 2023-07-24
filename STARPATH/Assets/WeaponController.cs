using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    private SpriteRenderer spriteRender;
    public Vector3 mousePosition;
    public Transform player;//this was added here
    public Transform gun;
    public Transform bulletTransform;
    // Start is called before the first frame update
    private void Start()
   {
        spriteRender = GetComponent<SpriteRenderer>();
   }

    // Update is called once per frame
    
    void Update()
    {/*
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        *//*Vector3 rotation = mousePos - transform.position;

        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz); */
        /* if (transform.eulerAngles.z < 91 || transform.eulerAngles.z > 239)
         {
             //Debug.Log(transform.eulerAngles.z);
             spriteRender.flipY = false;
         }
         else
         {
             //Debug.Log(transform.eulerAngles.z);
             spriteRender.flipY = true;
         }*/
        /*if (transform.localScale.x > -0.24)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        else if (transform.localScale.x < -0.24)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -rotZ);
        }*/
        
        
        flipPlayer();
    }
    

    private void flipPlayer()
    {
        //mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //bruh = Quaternion.Euler(0f, 0f, rotZ);
        //Debug.Log(bruh.eulerAngles.z);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (rotZ < 89 && rotZ > -89)
        {
            //Debug.Log(mousePosition.x);
            spriteRender.flipX = false;
            //transform.rotation = Quaternion.Euler(-1f, -1f, rotZ);
            //bulletTransform.position = new Vector3(player.position.x + 0.221f, player.position.y - 0.322f, 0f);
        }
        else 
        {
            // Debug.Log(mousePosition.x);
            spriteRender.flipX = true;
           // transform.rotation = Quaternion.Euler(0f,0f, rotZ);
            //bulletTransform.position = new Vector3(player.position.x - 0.221f, player.position.y - 0.322f, 0f);


        }
        
        
        /*
        if (difference.x < -0.24)
        {
            //spriteRender.flipX = true;
        }
        else if (difference.x > -0.24)
        {
            // Debug.Log(mousePosition.x);
            spriteRender.flipX = false;

        }
        */ 
       
    }

}
