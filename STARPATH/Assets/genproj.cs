using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genproj : MonoBehaviour
{

    public GameObject bullet;
    private bool playerinRange;
    [Header("Cue")]
    [SerializeField] private GameObject cue;

    // Start is called before the first frame update
    void Start()
    {
        playerinRange = false;
        
    }

    IEnumerator dropbs()
    {
        --level3manager.strikes;
        Destroy(cue);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(-2, 10, 0), Quaternion.identity);

        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(-1.5f, 10, 0), Quaternion.identity);
      
        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(-1,10,0) , Quaternion.identity);
      
        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(-.5f, 10, 0), Quaternion.identity);
       
        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
        
        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(.5f, 10, 0), Quaternion.identity);
        
        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(1, 10, 0), Quaternion.identity);
        
        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(1.5f, 10, 0), Quaternion.identity);

        yield return new WaitForSeconds(.2f);

        SoundManager.SoundManagerInstance.PlayFireSound(10);
        Instantiate(bullet, transform.position + new Vector3(2f, 10, 0), Quaternion.identity);



        Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

        if(playerinRange && level3manager.strikes > 0)
        {
            cue.SetActive(true);
            if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(dropbs());
            }
                    

        }
        else
        {

            cue.SetActive(false);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
            playerinRange = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {

        if (collider.gameObject.tag == "Player")
            playerinRange = false;

    }
}
