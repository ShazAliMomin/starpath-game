using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest2Manager : MonoBehaviour
{
    public PlayerController1 player;

    public generatorscript gen1;
    public generatorscript gen2;
    public generatorscript gen3;
    public generatorscript gen4;
    public Turret1 bossTurret;

    public GameObject[] spawners;
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;

    public GameObject gateTilemap;

    public BlasterEnemy specBlastTroop;
    public BlasterEnemy blastTroop;
    public MeleeEnemy meleeTroop;

    private bool gen1down;
    private bool gen2down;
    private bool gen3down;
    private bool gen4down;

    private bool leveledUp;
    // Start is called before the first frame update
    void Start()
    {
        gen1down = false;
        gen2down = false;
        gen3down = false;
        gen4down = false;

        leveledUp = false;

        //CallSpecReinforcements();
        //CallSquadReinforcements(target1);
    }

    // Update is called once per frame
    void Update()
    {
        if(gen1.health <= 0 && !gen1down)
        {
            bossTurret.DamageShield();
            gen1down = true;
            CallSquadReinforcements(target1);
        }
        if (gen2.health <= 0 && !gen2down)
        {
            bossTurret.DamageShield();
            gen2down = true;
            CallSquadReinforcements(target2);
        }
        if (gen3.health <= 0 && !gen3down)
        {
            bossTurret.DamageShield();
            gen3down = true;
            CallSquadReinforcements(target3);
        }
        if (gen4.health <= 0 && !gen4down)
        {
            bossTurret.DamageShield();
            gen4down = true;
            CallSquadReinforcements(target4);
        }

        if(bossTurret.health <= 0)
        {
            gateTilemap.SetActive(false);
            if (!leveledUp)
            {
                player.superLevelUP(10);
                leveledUp = true;
            }
        }

    }

    public void CallSquadReinforcements(GameObject marchTarget)
    {
        //for(int i=0; i<5; i++)
        for(int i=0; i<2; i++)
        {
            Transform rand1 = marchTarget.transform;
            rand1.position = marchTarget.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            BlasterEnemy rein = Instantiate(blastTroop, spawners[i].transform.position, Quaternion.identity);
            rein.March(rand1);
        }

        BlasterEnemy specRein = Instantiate(specBlastTroop, spawners[5].transform.position, Quaternion.identity);
        specRein.March(marchTarget.transform);
    }

    public void CallSpecReinforcements()
    {
        for(int i=0; i<5; i++)
        {
            Transform rand1 = target1.transform;
            rand1.position = target1.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            BlasterEnemy rein = Instantiate(specBlastTroop, spawners[i].transform.position, Quaternion.identity);
            rein.March(rand1);
        }
        for (int i = 0; i < 5; i++)
        {
            Transform rand2 = target2.transform;
            rand2.position = target2.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f);

            BlasterEnemy rein = Instantiate(specBlastTroop, spawners[5].transform.position, Quaternion.identity);
            rein.March(rand2);
        }
    }
}
