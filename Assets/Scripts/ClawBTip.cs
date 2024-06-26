using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawBTip : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator AnimB = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Toy")
        {
            //AnimB.speed = 0.1f;
        }
    }

}
