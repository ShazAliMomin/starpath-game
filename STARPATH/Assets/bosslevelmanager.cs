using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class bosslevelmanager : MonoBehaviour
{

    System.Random r = new System.Random();


    public GameObject boss;
    public GameObject player;
    public static int beacs;
    public static int nxt;
    public static int nxti;
    public static bool fact;
    public static bool rn;
    public static bool nxtrig = false;
    public GameObject TLB, MRB, TRB, BLB, BRB, MLB, TLG, MLG, MRG, TRG, BLG, BRG;
    public GameObject r1e1, r1e2, r1e3, r1e4, r2e1, r2e2, r2e3, r2e4, r3e1, r3e2, r3e3, r3e4,t1,t2;
    int k;
    static int[] oarr = new int[6];
    public static int mi = 0;

    IEnumerator blinks()
    {
        player.GetComponent<PlayerController1>().LockMovement();

        bool[] barr = new bool[6];
        barr[0] = false;
        barr[1] = false;
        barr[2] = false;
        barr[3] = false;
        barr[4] = false;
        barr[5] = false;
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 6; ++i)
        {
           
            int k = Random.Range(0, 6);
           // print("k = " + k);



            while (barr[k] == true)
            {
                k = Random.Range(0, 6);

            }
            oarr[i] = k;
            barr[k] = true;


            Pickme(k).SetActive(true);
            yield return new WaitForSeconds(.4f);
            Pickme(k).SetActive(false);
            fact = true;
            yield return new WaitForSeconds(.7f);


        } 

     

        nxti = 0;
        nxt = oarr[nxti];

        player.GetComponent<PlayerController1>().UnlockMovement();
    }

    IEnumerator wait(float f)
    {
        yield return new WaitForSeconds(f);
        mi += 1;
        nxti = 0;
        Restrt();
    }

    void Restrt()
    {

     
        rn = true;


    }


    void Nxtit()
    {

        int i = nxt;

        if (i == 0)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(12);
            TLG.SetActive(true);


        }
        else if (i == 1)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(13);
            TRG.SetActive(true);
        }
        else if (i == 2)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(10);
            MLG.SetActive(true);
        }
        else if (i == 3)
        {
            MRG.SetActive(true);
            SoundManager.SoundManagerInstance.PlayMiscSound(11);
        }
        else if (i == 4)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(8);
            BLG.SetActive(true);
        }
        else
        {
            BRG.SetActive(true);
            SoundManager.SoundManagerInstance.PlayMiscSound(9);
        }
        nxti += 1;

        if (nxti == 6)
        {
            StartCoroutine(wait(.5f));

            
        }
        else
        {
            nxt = oarr[nxti];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bool b = false;
        Ink.Runtime.Object obj = new Ink.Runtime.BoolValue(b);
        DialogueManager.GetInstance().SetVariableState("startboss", obj);
        GameObject.Find("TLG").SetActive(false);
        GameObject.Find("TRG").SetActive(false);
        GameObject.Find("MLG").SetActive(false);
        GameObject.Find("MRG").SetActive(false);
        GameObject.Find("BLG").SetActive(false);
        GameObject.Find("BRG").SetActive(false);

        r1e1.SetActive(false);
        r1e2.SetActive(false);
        r1e3.SetActive(false);
        r1e4.SetActive(false);
        r2e1.SetActive(false);
        r2e2.SetActive(false);
        r2e3.SetActive(false);
        r2e4.SetActive(false);
        r3e1.SetActive(false);
        r3e2.SetActive(false);
        r3e3.SetActive(false);
        r3e4.SetActive(false);
        t1.SetActive(false);
        t2.SetActive(false);



            beacs = 0;
        fact = true;
        rn = true;
        nxti = 0;
        nxt = oarr[nxti];
       // mi = 3;


        

    }

    // Update is called once per frame
    void Update()
    {
        if ( ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState("startboss")).value == true && !DialogueManager.GetInstance().dialogueIsPlaying)
        {


            if (nxtrig == true)
            {
                nxtrig = false;
                Nxtit();

            }

            // print("nxt = " + nxt);
            if (boss == null)
            {
                SceneManager.LoadScene("endreal");

            }

            if (rn)
            {
                rn = false;






                oarr[0] = -1;
                oarr[1] = -1;
                oarr[2] = -1;
                oarr[3] = -1;
                oarr[4] = -1;
                oarr[5] = -1;
                TLB.SetActive(false);
                TRB.SetActive(false);
                MLB.SetActive(false);
                MRB.SetActive(false);
                BLB.SetActive(false);
                BRB.SetActive(false);
                TLG.SetActive(false);
                TRG.SetActive(false);
                MLG.SetActive(false);
                MRG.SetActive(false);
                BLG.SetActive(false);
                BRG.SetActive(false);


                k = Random.Range(0, 6);

                if (mi < 3)
                {
                    StartCoroutine(blinks());
                }
                fact = false;

            }

            if (Boss.p == 2)
            {
                SoundManager.SoundManagerInstance.BossPhase(2);

                r1e1.SetActive(true);
                r1e2.SetActive(true);
                r1e3.SetActive(true);
                r1e4.SetActive(true);

            }


            if (Boss.p == 3)
            {
                SoundManager.SoundManagerInstance.BossPhase(3);
                t1.SetActive(true);
                t2.SetActive(true);
                r2e1.SetActive(true);
                r2e2.SetActive(true);
                r2e3.SetActive(true);
                r2e4.SetActive(true);

            }

            if (Boss.p == 4)
            {
                SoundManager.SoundManagerInstance.BossPhase(4);
                r3e1.SetActive(true);
                r3e2.SetActive(true);
                r3e3.SetActive(true);
                r3e4.SetActive(true);


            }


        }

    }

    





    GameObject Pickme(int i)
    {

        if (i == 0)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(12);
            return TLB;

        }
        else if (i == 1)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(13);
            return TRB;
        }
        else if (i == 2)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(10);
            return MLB;
        }
        else if (i == 3)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(11);
            return MRB;
        }
        else if (i == 4)
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(8);
            return BLB;
        }
        else
        {
            SoundManager.SoundManagerInstance.PlayMiscSound(9);
            return BRB;
        }
    }

     GameObject Pickmeg(int i)
    {

        if (i == 0)
        {
            return TLG;

        }
        else if (i == 1)
        {
            return TRG;
        }
        else if (i == 2)
        {
            return MLG;
        }
        else if (i == 3)
            return MRG;
        else if (i == 4)
        {
            return BLG;
        }
        else
            return BRG;

    }


}
