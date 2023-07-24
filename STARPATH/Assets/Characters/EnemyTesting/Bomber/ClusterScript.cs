using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    private float damage;
    public Vector3 vec3 = new Vector3(0, 0, 0);
    public Vector2 target;
    private Vector3 prev;
    private Transform player;
    private float distanceToTarget;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;

    // Start is called before the first frame update
    void Start()
    {
        //mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //rb = GetComponent<Rigidbody2D>();
       // mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        //vec3 = new Vector3(0, 0, 0);

        //Vector3 direction = mousePos - transform.position - vec3;
        //Vector3 rotation = transform.position - mousePos;
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //distanceToTarget = Vector3.Distance(mousePos, transform.position);
        //Debug.Log("Distance: " + distanceToTarget);
        //Debug.Log("Target: " + mousePos);

        //transform.position = Vector2.MoveTowards(this.transform.position, mousePos, force * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, target, force * Time.deltaTime);
        /*
        if (distanceToTarget < 1.5f)
        {
            Destroy(gameObject);
            Debug.Log("Target reached");
        }
        */

        /*
        if(transform.position == prev)
        {
            Debug.Log("Target reached");
            Detonate();
            
            //Create explosion object
            Destroy(gameObject);
        }

        prev = transform.position;
        */

        if(transform.position.x == target.x && transform.position.y == target.y){
            Detonate();
            Destroy(gameObject);
        }
            
        
    }

    void Detonate()
    {
        SoundManager.SoundManagerInstance.PlayExplosionSound((int)UnityEngine.Random.Range(0, 2));
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(p1, transform.position, transform.rotation);
        Instantiate(p2, transform.position, transform.rotation);
       
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //Should only explode prematurely if hitting terrain or destructable objects; should bypass enemies
        if (other.gameObject.tag != "Bullet" && other.gameObject.tag != "TurretBullet"
                && other.gameObject.tag != "TurretFriendly" && other.gameObject.tag != "BossBullet" && other.gameObject.tag != "Enemy"
                && other.gameObject.tag != "BlasterEnemy"
                && other.gameObject.tag != "Slime"
                && other.gameObject.tag != "Turret"
                && other.gameObject.tag != "Boss"
                && other.gameObject.tag != "TriggerBox")      //Remember to add new enemy tags to this list when they're implemented
        {
            Detonate();
            Destroy(gameObject);
        }

        //if other tag == terrain, then explode
    }
}
