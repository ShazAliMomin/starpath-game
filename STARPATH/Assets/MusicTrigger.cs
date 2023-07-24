using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private int trackIndex;
    [SerializeField] private GameObject[] enemies;
    private bool triggered, combatTrigger, finished;

    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        if (enemies != null) combatTrigger = true;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(combatTrigger && triggered && enemies == null)
        {
            SoundManager.SoundManagerInstance.ChangeMusicTrack(0);
            Destroy(gameObject);
        }*/

        
        if(combatTrigger && triggered)
        {
            finished = true;
            for(int i=0; i<enemies.Length; i++)
            {
                if(enemies[i] != null)
                {
                    finished = false;
                    break;
                }
            }
            if (finished)
            {
                SoundManager.SoundManagerInstance.ChangeMusicTrack(0);
                Destroy(gameObject);
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !triggered)
        {
            SoundManager.SoundManagerInstance.ChangeMusicTrack(trackIndex);
            triggered = true;
        }
    }
}
