using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EGLevel2 : MonoBehaviour


{

    public static int e1, e2, e3, e4, e5, e6, e7;

    [SerializeField] private GameObject e1p, e1n, e2p, e2n, e3p, e3n, e4p, e4n, e5p, e5n, e6p, e6n, e7p, e7n, shield;
    public PlayerController1 p;
    private static GameObject shieldCamera;
    private static bool  shieldCameraActive;
    private static float cameraDisplayTime = 0.2f;
    private static float cameraTimer = 0.0f;
    public static bool esc = false;


    // Start is called before the first frame update
    void Start()
    {
        PlayerController1.fuel = 0;
        esc = false;
        p.inventory.RemoveItemID(6);
        shieldCameraActive = false;
        shieldCamera = GameObject.Find("Shield Camera");
        shieldCamera.SetActive(false);

        e1 = 1;
        e2 = -1;
        e3 = 1;
        e4 = 1;
        e5 = -1;
        e6 = 1;
        e7 = 1;

        hubmanager.pbarr[20] = false;
        hubmanager.pbarr[21] = false;
        hubmanager.pbarr[22] = false;
        hubmanager.pbarr[23] = false;
        hubmanager.pbarr[24] = false;
        hubmanager.pbarr[25] = false;
        hubmanager.pbarr[26] = false;
        hubmanager.pbarr[27] = false;
        hubmanager.pbarr[28] = false;
        hubmanager.pbarr[29] = false;

    }

    public static void ActivateShieldCamera()
    {
        shieldCameraActive = true;
        cameraTimer = cameraDisplayTime;
        shieldCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController1.fuel ==10)
        {

            esc = true;
        }
        if(shieldCameraActive && cameraTimer <= 0)
        {
            shieldCamera.SetActive(false);
            shieldCameraActive = false;
            cameraTimer = 0;
        }
        else if (shieldCameraActive)
        {
            cameraTimer -= Time.deltaTime;
        }

        if (e1 == -1 && e2 == -1 && e3 == -1 && e4 == -1 && e5 == -1 && e6 == -1)
        {
            if (shield != null)
            {
                SoundManager.SoundManagerInstance.PlayMiscSound(5);
            }
            Destroy(shield);
        }


        if (e1 == 1)
        {
            e1p.SetActive(true);
            e1n.SetActive(false);
        }
        else
        {
            e1p.SetActive(false);
            e1n.SetActive(true);
        }

        if (e2 == 1)
        {
            e2p.SetActive(true);
            e2n.SetActive(false);
        }
        else
        {
            e2p.SetActive(false);
            e2n.SetActive(true);
        }

        if (e3 == 1)
        {
            e3p.SetActive(true);
            e3n.SetActive(false);
        }
        else
        {
            e3p.SetActive(false);
            e3n.SetActive(true);
        }

        if (e4 == 1)
        {
            e4p.SetActive(true);
            e4n.SetActive(false);
        }
        else
        {
            e4p.SetActive(false);
            e4n.SetActive(true);
        }

        if (e5 == 1)
        {
            e5p.SetActive(true);
            e5n.SetActive(false);
        }
        else
        {
            e5p.SetActive(false);
            e5n.SetActive(true);
        }

        if (e6 == 1)
        {
            e6p.SetActive(true);
            e6n.SetActive(false);
        }
        else
        {
            e6p.SetActive(false);
            e6n.SetActive(true);
        }
        if (e7 == 1)
        {
            e7p.SetActive(true);
            e7n.SetActive(false);
        }
        else
        {
            e7p.SetActive(false);
            e7n.SetActive(true);
        }
    }

    //hub
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //SoundManager.SoundManagerInstance.ChangeMusicTrack(2);
            SceneManager.LoadScene("Town");
        }
    }
}
