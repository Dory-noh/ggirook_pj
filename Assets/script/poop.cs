using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{
    Rigidbody rb;
    int power;
    int speed;
    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        speed = 140;
        power = 5;
        StartCoroutine(move());
    }
    IEnumerator move()
    {
        yield return new WaitForSeconds(0.1f);
        rb.AddForce(Vector3.right*speed);
        //if(Vector3.Distance(gull.transform.position,))
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.StartsWith("enemy"))
        {
            other.transform.GetComponent<devil>().hit(power);
            Destroy(gameObject);
        }
    }
}
