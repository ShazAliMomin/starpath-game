using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ecscript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if(bosslevelmanager.mi == 1)
        {

            animator.SetTrigger("exp1");
        }
        if (bosslevelmanager.mi == 2)
        {


            animator.SetTrigger("exp2");
        }
        if(bosslevelmanager.mi == 3)
        {
            animator.SetTrigger("exp1");
            animator.SetTrigger("exp2");

            animator.SetTrigger("exp3");
        }

    }
}
