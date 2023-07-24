using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 10f;

    public void Fire() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }
}
