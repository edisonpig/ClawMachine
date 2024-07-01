using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Image selectedSkin;

    [SerializeField] private Text coinsText;

    [SerializeField] private SkinManager skinManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "Coins: " + PlayerPrefs.GetInt("Coins");
        selectedSkin.GetComponent<Image>().color = skinManager.GetSelectedSkin().material.color;

    }

    public void LoadMenu() => SceneManager.LoadScene("GameScene");
}
