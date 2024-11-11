using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag.StartsWith("enemy"))
        {
            Debug.Log("ÆÜ");
            other.transform.GetComponent<devil>().hit();
            Destroy(gameObject);
        }
    }
}
