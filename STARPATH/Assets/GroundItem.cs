using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemObject item;
    public int id;
    public bool used = false;
    public void OnAfterDeserialize() {}

    public void OnBeforeSerialize() 
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
#if UNITY_EDITOR
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
    }

    private void Start()
    {
        
        if (hubmanager.pbarr[id])
        {

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
            hubmanager.pbarr[id] = true;
        }

        if (hubmanager.pbarr[id])
        {
            Destroy(this.gameObject);
        }


    }
}
