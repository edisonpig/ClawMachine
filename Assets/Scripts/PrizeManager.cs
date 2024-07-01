using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PrizeManager : MonoBehaviour
{
    [Header("VCam")]
    [SerializeField] CinemachineVirtualCamera vcamIntro;
    [SerializeField] CinemachineVirtualCamera vcamMain;
    [SerializeField] CinemachineVirtualCamera vcamPrize;

    [Header("Prize")]
    [SerializeField] public List<GameObject> ToBeShown;
    [SerializeField] public List<GameObject> Showed;
    public GameObject offset;
    public bool examine = false;

    public int coinGot = 0;
    [SerializeField] private Text coinsText;
    [SerializeField] private GameObject examiningObject;



    [Header("UI")]
    [SerializeField] private GameObject menu = null;
    [SerializeField] private GameObject backMenu = null;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;



    }

    // Update is called once per frame
    void Update()
    {

        if (ToBeShown.Count == 0 && examine && Input.GetMouseButtonDown(0))
        {
            //Showed.Add(examiningObject);
            vcamPrize.m_Priority = 9;
            examine = false;
            Debug.Log("count ==0 Destroying: " + examiningObject);
            Destroy(examiningObject);


        }
        if (ToBeShown.Count != 0 && examine && Input.GetMouseButtonDown(0))
        {
            if (ToBeShown[0] != null)
            {
                if (examiningObject != ToBeShown[0])
                {
                    Debug.Log("count!=0 -> tobe0 !=null -> exam!tobe: " + examiningObject);
                    Destroy(examiningObject);
                    ShowPrize(ToBeShown[0]);
                }
                else
                {
                    Destroy(examiningObject);
                    for (int i = 1; i < ToBeShown.Count; i++)
                    {
                        ToBeShown[i - 1] = ToBeShown[i];
                        if ((ToBeShown[i - 1] == ToBeShown[i]) && (ToBeShown[i - 1] == ToBeShown[ToBeShown.Count - 1]))
                        {
                            ToBeShown.RemoveAt(i);

                        }
                    }
                    Debug.Log("count!=0 -> tobe0 !=null -> exam=tobe: " + examiningObject);
                    if (ToBeShown[0] == null)
                    {
                        Debug.Log("count!=0 -> tobe0 !=null -> exam=tobe: Back");
                        ToBeShown.RemoveAt(0);
                        vcamPrize.m_Priority = 9;
                        examine = false;

                    }
                    else
                    {

                        ShowPrize(ToBeShown[0]);
                    }
                }

            }
            else
            {
                Debug.Log("Back");
                ToBeShown.RemoveAt(0);
                vcamPrize.m_Priority = 9;
                examine = false;

            }

            /*  if (!Showed.Contains(ToBeShown[0]))
              {

              }
              else
              {
                  Showed.Remove(ToBeShown[0]);
              }
  */
        }
        if (examine && examiningObject != null)
        {
            examiningObject.transform.Rotate(0, 5 * Time.deltaTime, 0);

        }

    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("prize");
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        foreach (Collider c in other.gameObject.GetComponents<Collider>())
        {
            c.enabled = false;
        }
        if (!examine)
        {

            if (!ToBeShown.Contains(other.gameObject))//&& !Showed.Contains(other.gameObject)
                ToBeShown.Add(other.gameObject);
            ShowPrize(ToBeShown[0]);
        }
        else
        {
            if (examiningObject == other.gameObject)
            {
                Debug.Log("Repeat examining");
            }

            else if (ToBeShown.Contains(other.gameObject))//|| Showed.Contains(other.gameObject)
            {
                Debug.Log("duplicate");

            }

            else
            {

                ToBeShown.Add(other.gameObject);

                Debug.Log("TobeShown Added");
            }


        }
    }

    public void ShowPrize(GameObject prizeShow)
    {

        vcamPrize.m_Priority = 12;
        examine = true;
        Debug.Log("now showing: " + prizeShow);
        examiningObject = prizeShow;
        prizeShow.transform.position = offset.transform.position;
        prizeShow.transform.rotation = offset.transform.rotation;
        for (int i = 1; i < ToBeShown.Count; i++)
        {
            ToBeShown[i - 1] = ToBeShown[i];
            if ((ToBeShown[i - 1] == ToBeShown[i]) && (ToBeShown[i - 1] == ToBeShown[ToBeShown.Count - 1]))
            {
                ToBeShown.RemoveAt(i);

            }
        }
        coinGot += 100;
        PlayerPrefs.SetInt("Coins", coinGot);
        coinsText.text = "Coins: " + PlayerPrefs.GetInt("Coins");




    }

    public void StartGame()
    {
        Time.timeScale = 1;
        vcamIntro.m_Priority = 1;
        menu.SetActive(false);
        backMenu.SetActive(true);
        coinsText.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        coinGot = PlayerPrefs.GetInt("Coins");


    }

    public void ShowMenu()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        backMenu.SetActive(false);

    }

    public void ShoppingMenu() => SceneManager.LoadScene("ShopScene");


    public void LeaveGame()
    {
        Debug.Log("put showed prize into data (and maybe currency?) ");
    }
}
