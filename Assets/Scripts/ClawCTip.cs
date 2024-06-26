using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawCTip : MonoBehaviour
{

    [SerializeField] private Animator AnimC = null;
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
            //AnimC.speed = 0.1f;
            //other.transform.parent = gameObject.transform;
        }
    }

}
