using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFloors : MonoBehaviour
{
    [SerializeField] private GameObject[] removeTiles, enemies, addTiles, objects, addObjects;
    [SerializeField] private Transform target;
    [SerializeField] private bool soundAlarm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.tag == "Player")
        {
            if (soundAlarm == true)
            {
                SoundManager.SoundManagerInstance.SoundAlarm();
            }

            
            SoundManager.SoundManagerInstance.PlayMiscSound(4);
            if (removeTiles != null)
            {
                for(int i=0; i<removeTiles.Length; i++)
                {
                    removeTiles[i].SetActive(false);
                }
            }
            if (enemies != null)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].SetActive(true);

                    MeleeEnemy mEnemy = enemies[i].GetComponent<MeleeEnemy>();
                    BlasterEnemy bEnemy = enemies[i].GetComponent<BlasterEnemy>();

                    if (target) {
                        if (mEnemy)
                        {
                            mEnemy.March(target);
                        }
                        else if (bEnemy)
                        {
                            bEnemy.March(target);
                        }
                    }
                }
            }
            if (addTiles != null)
            {
                for (int i = 0; i < addTiles.Length; i++)
                {
                    addTiles[i].SetActive(true);
                }
            }
            if(objects != null)
            {
                for(int i=0; i<objects.Length; i++)
                {
                    objects[i].SetActive(false);
                }
            }
            if (addObjects != null)
            {
                for (int i = 0; i < addObjects.Length; i++)
                {
                    addObjects[i].SetActive(true);
                }
            }
            Destroy(gameObject);
        }
        
        
    }
}
