using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class tavernfightmanager : MonoBehaviour
{

    [SerializeField] GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (g == null)
        {

            SceneManager.LoadScene("tavernnormal");
        }
       
    }

    
}
