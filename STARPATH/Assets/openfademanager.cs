using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openfademanager : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
      
    }

    void updateoncomplete()
    {

        SceneManager.LoadScene("Town");
    }

    // Update is called once per frame
    void Update()
    {
        if(openmanager.fs == true)
        {

            animator.SetTrigger("f2trig");
        }

        

      


        


    }
}
