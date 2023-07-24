using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest2Camera : MonoBehaviour
{
    public Camera cam;      //boss map position: -1.29, 49.51, -10
    public GameObject vCam;

    public PlayerController1 player;
    public Turret1 bossTurret;
    private bool turretDead;

    // Start is called before the first frame update
    void Start()
    {
        turretDead = false;
        cam.transform.position = new Vector3(-1.79f, 28f, -10f);
        cam.orthographicSize = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossTurret.health <= 0)
        {
            turretDead = true;
            Invoke("transitionCameraToPlayer", 5f);
        }
    }

    private void transitionCameraToPlayer()
    {
        vCam.SetActive(true);
        cam.transform.position = new Vector3(-1.79f, 28f, -10f);
        cam.orthographicSize = 6;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !turretDead)
        {
            vCam.SetActive(false);
            cam.transform.position = new Vector3(-1.29f, 49.51f, -10f);
            cam.orthographicSize = 15;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !turretDead)
        {
            vCam.SetActive(true);
            cam.transform.position = new Vector3(-1.79f, 28f, -10f);
            cam.orthographicSize = 6;
        }
    }
}
