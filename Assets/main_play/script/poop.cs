using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class poop : MonoBehaviour
{
    Rigidbody2D rb;
    int power;
    int speed;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        speed = 140;
        power = 10;
        StartCoroutine(move());
    }
    IEnumerator move()
    {
        yield return new WaitForSeconds(0.1f);
        rb.AddForce(Vector3.right*speed);
        //if(Vector3.Distance(gull.transform.position,))
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.StartsWith("enemy"))
        {
            col.transform.GetComponent<devil>().hit(power);
            Destroy(gameObject);
        }
    }

}
