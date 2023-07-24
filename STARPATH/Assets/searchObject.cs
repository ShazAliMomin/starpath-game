using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class searchObject : MonoBehaviour//, ISerializationCallbackReceiver
{
    public int id;
    public bool used = false;
    
    public ItemObject item;
    public ItemObject item2;
    public void OnAfterDeserialize() { }

   /* public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;

#if UNITY_EDITOR
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }*/

    [Header("Cue")]
    [SerializeField] private GameObject cue;

    private bool playerinRange;

    private void Start()
    {
        
        if(id >=10 && id <=15)
        {
            if (id == EGLevel1.chosen)
            {
                item = item2;
            }

            
        }

       

        playerinRange = false;
        cue.SetActive(false);
        if (hubmanager.sbarr[id])
        {
            
            Destroy(cue);
            Destroy(this.gameObject);
            Debug.Log(id + " is dead at awake");
        }
        else
        {

            Debug.Log(id + " is alive at awake");
        }
    }

    private void Update()
    {
        if (used)
        {
            hubmanager.sbarr[id] = true;
        }

        if (hubmanager.sbarr[id])
        {
            Destroy(cue);
            Destroy(this.gameObject);                  

        }
        else
        {
            if (playerinRange)
            {
                cue.SetActive(true);

            }
            else
            {
                cue.SetActive(false);
            }
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


