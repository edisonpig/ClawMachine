using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawATip : MonoBehaviour
{

    [SerializeField] private Animator AnimA = null;
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
            //AnimA.speed = 0.1f;
        }

    }

}
