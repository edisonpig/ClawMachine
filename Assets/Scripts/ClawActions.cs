using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawActions : MonoBehaviour
{
    [Header("Claws Reference")]
    [SerializeField] private GameObject clawBox = null;  //for left right x +/-

    [SerializeField] private GameObject clawPipes = null; // for front back z +/-

    [SerializeField] private GameObject clawCore = null; // for up down y +/-

    [SerializeField] private GameObject clawA = null; //claw1 rotate x& y =ve
    [SerializeField] private GameObject clawB = null; //claw2 rotate x +ve
    [SerializeField] private GameObject clawC = null; //claw3 rotate x -ve & y +ve

    [Header("Movement")]

    [SerializeField] private PrizeManager prizeMan = null;

    float horizontalInput;
    float verticalInput;

    [Header("Clawing")]
    bool clawInProgress;

    [SerializeField] private Animator AnimA = null;
    [SerializeField] private Animator AnimB = null;
    [SerializeField] private Animator AnimC = null;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        MyInput();
        if (!clawInProgress && !prizeMan.examine)
        {
            MoveClaws();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !clawInProgress && !prizeMan.examine)
        {
            ClawStart();

        }

        if (clawCore.transform.position.y < 5f)
        {
            Debug.Log("Claw at 4.5");

            clawHands();
        }



        //clawBox boundaries
        if (clawBox.transform.position.x > 0.4f)//left
        {
            clawBox.transform.position = new Vector3(0.39f, clawBox.transform.position.y, clawBox.transform.position.z);
        }

        if (clawBox.transform.position.x < -3.2f) //right
        {
            clawBox.transform.position = new Vector3(-3.19f, clawBox.transform.position.y, clawBox.transform.position.z);
        }

        //clawPipes boundaries

        if (clawPipes.transform.position.z > 0.2f)//close
        {
            clawPipes.transform.position = new Vector3(clawPipes.transform.position.x, clawPipes.transform.position.y, 0.19f);
        }

        if (clawPipes.transform.position.z < -2.3f)///far
        {
            clawPipes.transform.position = new Vector3(clawPipes.transform.position.x, clawPipes.transform.position.y, -2.29f);
        }

        if (clawPipes.transform.position.z == 0.19f && clawBox.transform.position.x == 0.39f)
        {


            AnimA.SetInteger("StateChange", 0);
            AnimB.SetInteger("StateChange", 0);
            AnimC.SetInteger("StateChange", 0);
            AnimA.speed = 1;
            AnimB.speed = 1;
            AnimC.speed = 1;
            clawInProgress = false;

        }




    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MoveClaws()
    {
        clawBox.transform.position = Vector3.MoveTowards(clawBox.transform.position, new Vector3(clawBox.transform.position.x + horizontalInput * -2f, clawBox.transform.position.y, clawBox.transform.position.z), Time.deltaTime);
        clawPipes.transform.position = Vector3.MoveTowards(clawPipes.transform.position, new Vector3(clawPipes.transform.position.x, clawPipes.transform.position.y, clawPipes.transform.position.z + verticalInput * -2f), Time.deltaTime);

        /*
        clawBoxRB.AddForce(new Vector3(horizontalInput * 2f, 0, 0));
        clawPipesRB.AddForce(new Vector3(0, 0, verticalInput * 2f));
        */

    }

    public void RightButton()
    {
        horizontalInput = 1;
    }

    public void LeftButton()
    {
        verticalInput = 1;
    }


    public void ClawStart()
    {
        clawInProgress = true;




        // clawCore.transform.position = Vector3.Lerp(clawCore.transform.position, new Vector3(clawCore.transform.position.x, 4.7f, clawCore.transform.position.z), 5 * Time.deltaTime);
        StartCoroutine(MoveOverSeconds(clawCore, new Vector3(clawCore.transform.position.x, 4.7f, clawCore.transform.position.z), 5f));
        Debug.Log("claw down");


        Invoke("ClawReturn", 8.5f);


    }


    public void ClawReturn()
    {



        // clawCore.transform.position = Vector3.Lerp(clawCore.transform.position, new Vector3(clawCore.transform.position.x, 6.6f, clawCore.transform.position.z), 5 * Time.deltaTime);
        StartCoroutine(MoveBackOverSeconds(clawCore, new Vector3(clawCore.transform.position.x, 6.59f, clawCore.transform.position.z), 5f));






    }

    public void clawHands()
    {
        Debug.Log("clawhands");
        AnimA.SetInteger("StateChange", 1);
        AnimB.SetInteger("StateChange", 1);
        AnimC.SetInteger("StateChange", 1);
    }

    public IEnumerator MoveBackOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0.1f;

        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {

            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            //objectToMove.GetComponent<Rigidbody>().position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }



        objectToMove.transform.position = end;
        yield return MoveOverSeconds(clawBox, new Vector3(0.39f, clawBox.transform.position.y, clawBox.transform.position.z), 5f);
        yield return MoveOverSeconds(clawPipes, new Vector3(clawPipes.transform.position.x, clawPipes.transform.position.y, 0.19f), 5f);

    }





    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0.1f;

        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {

            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            //objectToMove.GetComponent<Rigidbody>().position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }



        objectToMove.transform.position = end;




    }




}
