using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class beac1 : MonoBehaviour
{
    public float health;
    public float shealth;
    public GameObject red;
    private bool isImmune;
    bool sw;
    public Image healthbar;
    public Image chargebar;
    float charge;


    // Start is called before the first frame update

    IEnumerator waiting()
    {
        charge = 0;
        isImmune = true;
        bosslevelmanager.beacs += 1;
        red.SetActive(true);

        
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        yield return new WaitForSeconds(.5f);
        charge += .5f;
        charge = 0;



        red.SetActive(false);
        bosslevelmanager.beacs -= 1;
        health = shealth;
        isImmune = false;




    }

    void Start()
    {
        red.SetActive(false);
        health = 50f;
        shealth = health;
        isImmune = false;
        sw = false;
        charge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.fillAmount = health / shealth;
        chargebar.fillAmount = charge / 8;

        if ( Boss.p == 2 && sw == false)
        {
            sw = true ;
            red.SetActive(false);
            isImmune = false;

        }
        if (Boss.p != 2)
        {
            health = 50f;
            isImmune = true;
            red.SetActive(true);

        }
        else
        {

            if (health <= 0 && isImmune == false)
            {
                StartCoroutine(waiting());
            }

        }
        

    }

    public void TakeDamage(float d)
    {
        if (isImmune == false)
        {
            health -= d;
        }
    }
   

}