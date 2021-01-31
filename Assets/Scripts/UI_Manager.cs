using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Text VegetablesLabel;

    public GameObject keyboardUI;
    public GameObject controllerUI;
    public GameObject mouseUI;

    private void Start()
    {
        keyboardUI.SetActive(true);
        StartCoroutine(HideUI());
    }

    public void UpdateRemainingVegetables(int remaining)
    {
        VegetablesLabel.text = "Remaining Vegetables: " + remaining.ToString();
    }

    public void ShowKeyboardUI()
    {
        keyboardUI.SetActive(true);
        controllerUI.SetActive(false);
        mouseUI.SetActive(false);
        StartCoroutine(HideUI());
    }
    public void ShowControllerUI()
    {
        keyboardUI.SetActive(false);
        controllerUI.SetActive(true);
        mouseUI.SetActive(false);
        StartCoroutine(HideUI());
    }

    public void ShowMouseUI()
    {
        keyboardUI.SetActive(false);
        controllerUI.SetActive(false);
        mouseUI.SetActive(true);
        StartCoroutine(HideUI());
    }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(2f);
        keyboardUI.SetActive(false);
        controllerUI.SetActive(false);
        mouseUI.SetActive(false);
    }
}
