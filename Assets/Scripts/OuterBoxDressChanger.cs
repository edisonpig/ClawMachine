using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterBoxDressChanger : MonoBehaviour
{
    [SerializeField] private SkinManager skinManager;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = skinManager.GetSelectedSkin().material;


    }

    // Update is called once per frame
    void Update()
    {

    }
}
