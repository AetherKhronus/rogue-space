using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseMenu : MonoBehaviour {

    public void ChooseRed() {

        PlayerPrefs.SetInt("Color" , 1);
        PlayerPrefs.SetInt("DMG" , 2);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Main Menu");

    }

    public void ChooseGreen() {

        PlayerPrefs.SetInt("Color" , 2);
        PlayerPrefs.SetInt("HP" , 2);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Main Menu");

    }

    public void ChooseBlue() {

        PlayerPrefs.SetInt("Color" , 3);
        PlayerPrefs.SetInt("SP" , 2);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Main Menu");

    }

}
