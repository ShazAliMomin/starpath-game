using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class searchObjectH1 : MonoBehaviour, ISerializationCallbackReceiver
{
    public bool used = false;

    public ItemObject item;
    public void OnAfterDeserialize() { }

    public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;

#if UNITY_EDITOR
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }

    [Header("Cue")]
    [SerializeField] private GameObject cue;

    private bool playerinRange;

    private void Awake()
    {
        playerinRange = false;
        cue.SetActive(false);
    }

    void OnDestroy()
    {

        cue.SetActive(false);

    }

    private void Update()
    {
        if(used)
        {
            hubmanager.sb1 = true;
        }

        if (hubmanager.sb1 == true)
        {
            cue.SetActive(false);

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

