using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class scenetransscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(hubmanager.ns);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
