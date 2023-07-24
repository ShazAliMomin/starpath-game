using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public static bool InventoryIsOpen = false;
    public GameObject inventoryUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryIsOpen)
            {
                CloseInventoryUI();
            } else 
            {
                OpenInventoryUI();
            }
        }
    }

    void CloseInventoryUI()
    {
        inventoryUI.SetActive(false);
        InventoryIsOpen = false;
    }

    void OpenInventoryUI()
    {
        inventoryUI.SetActive(true);
        InventoryIsOpen = true;
    }
}
