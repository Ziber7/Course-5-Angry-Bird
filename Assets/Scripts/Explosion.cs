using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Bird
{
    [SerializeField]
    public float fieldofImpact;

    public float force;
    public int a = 1;

    public AudioSource audioSource;
    public AudioClip clip;

    public LayerMask LayerToHit;

    public GameObject explosionPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Enemy")
        {
            if (a == 1)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
                audioSource.PlayOneShot(clip, 0.5f);
                Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact,LayerToHit);
                foreach(Collider2D obj in objects)
                {
                    Vector2 direction = obj.transform.position - transform.position;;
                    obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
                }
                //Supaya hanya meledak sekali saja
                a -= 1;
            }
        } else {
            return;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofImpact);
    }
}
