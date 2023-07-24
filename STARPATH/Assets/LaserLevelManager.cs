using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelManager : MonoBehaviour
{

    IEnumerator phase0()
    {

        blackgrid.SetActive(true);
        for (int i = 0; i < 10; ++i)
        {
            blasts[i] = 0;
        }

        chargetime = 3f;
        blastime = 4f;
        settletime = 5f;

        for (int i = 0; i < 10; ++i)
        {
            thealth[i] = 60f;
        }
        print("starting shuffling");
        for (int i = 0; i < 10; ++i)
        {
            bool b = false;
            while (!b)
            {
                int k = Random.Range(0, 10);

                if (thealth[k] == 60f)
                {
                    thealth[k] = lhealth[i];
                    b = true;

                }

            }

        }

        for (int i = 0; i < 10; ++i)
        {
            lhealth[i] = thealth[i];
        }

        int lc = 0;
        int br = 0;

        for (int i = 0; i < 10; ++i)
        {
            int k = Random.Range(0, 11);


            if (lhealth[i] > 0)
            {
                


                if (k > thresh)
                {
                   

                    if ((i < 5 && lc < 4) || (i >= 5 && br < 4))
                    {
                        al += 1;
                        // print("k = " + k);
                        // print("laser " + i + "is active");
                        blasts[i] = 1;

                        if (i < 5)
                        {
                            ++lc;
                        }
                        if (i >= 5)
                        {
                            ++br;
                        }


                    }
                   

                }



            }


        }

        yield return new WaitForSeconds(3);
        print("phase 0 completed");
        print("starting phase 1");
        p = 1;


    }
   
    /*
    IEnumerator waiter(float x)
    {
        p = 0;
        setl = 0;
        fin = false;
        blackgrid.SetActive(false);
        thresh = 5.0;
        for (int i = 0; i < 10; ++i)
        {
            blasts[i] = 0;
        }
        for (int i = 0; i < 10; ++i)
        {
            lhealth[i] = 50f;
        }

        chargetime = 3f;
        blastime = 4f;
        settletime = 5f;
        blastart = false;
        al = 0;
        rs = true;
        sl = 0;


        while (dl < 10)
        {

            al = 0;
            print("another round");
            fl = 0;
            sl = 0;
            asl = 0;
            setl = 0;


            //phase 1 shuffle and assign healths

            for (int i = 0; i < 10; ++i)
            {

                blasts[i] = 0;

            }



            for (int i = 0; i < 10; ++i)
            {
                thealth[i] = 60f;
            }
            print("starting shuffling");
            for (int i = 0; i < 10; ++i)
            {
                bool b = false;
                while (!b)
                {
                    int k = Random.Range(0, 10);

                    if (thealth[k] == 60f)
                    {
                        thealth[k] = lhealth[i];
                        b = true;

                    }

                }

            }

            for (int i = 0; i < 10; ++i)
            {
                lhealth[i] = thealth[i];
            }
            print("phase 0 completed");

            for (int i = 0; i < 10; ++i)
            {
                int k = Random.Range(0, 11);

                if (lhealth[i] > 0)
                {
                    int lc = 0;
                    int br = 0;


                    if (k > thresh)
                    {
                        if (i < 5)
                        {
                            ++lc;
                        }
                        if (i >= 5)
                        {
                            ++br;
                        }

                        if ((i < 5 && lc < 4) || (i >= 5 && br < 4))
                        {
                            al += 1;
                            // print("k = " + k);
                            // print("laser " + i + "is active");
                            blasts[i] = 1;
                        }
                        else
                        {
                            if (i < 5)
                            {
                                --lc;
                            }
                            if (i >= 5)
                            {
                                --br;
                            }

                        }

                    }



                }


            }


            blackgrid.SetActive(true);
            yield return new WaitForSeconds(2f);
            blackgrid.SetActive(false);
            fin = true;
   
            p = 1;


            yield return new WaitForSeconds(.001f);

            p = 2;

            float t = chargetime + blastime + 1.25f;

            yield return new WaitForSeconds(t);


            p = 3;

            yield return new WaitForSeconds(settletime);

           

        }
        
        IEnumerator LaserCycle()
        {

            for (int i = 0; i < 10; ++i)
            {
                lhealth[i] = 50f;
            }

            while (dl > 0)
            {

                setl = 0;
                fin = false;
                blackgrid.SetActive(false);
                thresh = 5.0;
                for (int i = 0; i < 10; ++i) //reset which lasers will blast
                {
                    blasts[i] = 0;
                }

                //shuffle healths

                for (int i = 0; i < 10; ++i)
                {
                    thealth[i] = 60f;
                }
                print("starting shuffling");
                for (int i = 0; i < 10; ++i)
                {
                    bool b = false;
                    while (!b)
                    {
                        int k = Random.Range(0, 10);

                        if (thealth[k] == 60f)
                        {
                            thealth[k] = lhealth[i];
                            b = true;

                        }

                    }

                }


                for (int i = 0; i < 10; ++i)
                {
                    lhealth[i] = thealth[i];
                }
                print("phase 0 completed");

                //assign healths

                blackgrid.SetActive(true);
              
                l0.GetComponent<lasert>().health = lhealth[0];
                l1.GetComponent<lasert>().health = lhealth[1];
                l2.GetComponent<lasert>().health = lhealth[2];
                l3.GetComponent<lasert>().health = lhealth[3];
                l4.GetComponent<lasert>().health = lhealth[4];
                l5.GetComponent<lasert>().health = lhealth[5];
                l6.GetComponent<lasert>().health = lhealth[6];
                l7.GetComponent<lasert>().health = lhealth[7];
                l8.GetComponent<lasert>().health = lhealth[8];
                l9.GetComponent<lasert>().health = lhealth[9];

                yield return new WaitForSeconds(2f);
                blackgrid.SetActive(false);

                p = 2;













                yield return new WaitForSeconds(settletime);



                chargetime = 3f;
                blastime = 4f;
                settletime = 5f;



                







            }




        }



        
      
        
    }
    */

    bool p0, p1, p2, p3;
    public static bool blastart;
    public static int asl;
    public static float chargetime;
    public static float blastime;
    public static float settletime;
    public static float blasting;
    public static int[] blasts = new int[10];
    public static float[] lhealth = new float[10];
    public static float[] thealth = new float[10];
    public static GameObject l0, l1, l2, l3, l4, l5, l6, l7, l8, l9;
    public static int dl = 0;
    public static int al = 0;
    public static int fl = 0;
    public static int  p1d, p2d, p3d;
    public static int setl;
    public static int p;
    bool rs;
    bool fin;
   public static int sl;
    double thresh;
    public GameObject blackgrid, indicator, elevdoor, doortrig;
    public static GameObject[] Tiles = new GameObject[10];
    

    System.Random r = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        elevdoor.SetActive(true);
        p = 0;
        blackgrid.SetActive(false);
        thresh = 5.0;
        for (int i = 0; i < 10; ++i)
        {
            lhealth[i] = 50f;
        }
        p0 = false;
        p1 = false;
        p2 = false;
        p3 = false;


    }

    // Update is called once per frame
    void Update()
    {

        if (dl == 10)
        {
            p = 5;
        }

        if (p == 5)
        {
            elevdoor.SetActive(false);

            doortrig.SetActive(true);
        }

        if (p == 0 && p0==false)//phase 0
        {
            p0 = true;

            StartCoroutine(phase0());
  
        }


        if (p == 1 && p1 == false )//phase 1
        {

            //wait until healths have been applied then set phase to 2
            if (p1d == 10 && p1 == false)
            {
                p1 = true;
                p1d = 0;
                blackgrid.SetActive(false);
                p = 2;
                print("starting phase 2");
            }


        }


        if ( p == 2 && p2 == false)//blast phase
        {

            if (p2d == 10)
            {
                p2 = true;
                p2d = 0;
                p = 3;
                     print("starting phase 3"); ;
            }


        }

        if ( p == 3  && p3 == false)//phase 3
        {
            if (p3d == 10)
            {
                
                p3d = 0;
                p = 0;
                p0 = false;
                p1 = false;
                p2 = false;
                p3 = false;
                print("ending phase 3");
            }

        }








        //print("phase: " + p);
        // print("al: " + al + " | sl: " + sl + " | fl: " + fl);
        if (p == 3)
        {
            indicator.SetActive(false);
        }
        else
        {

            indicator.SetActive(true);
        }
      



    }
}
