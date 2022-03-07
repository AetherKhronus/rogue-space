using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradesMenu : MonoBehaviour {

    public GameObject window;

    public Button upgradeDMGButton;
    public Button upgradeHPButton;
    public Button upgradeSPButton;
    public Button upgradeMGButton;

    public Text upgradeDMGText;
    public Text upgradeHPText;
    public Text upgradeSPText;
    public Text upgradeMGText;
    public Text coinsText;

    public GameObject[] damagePoints;
    public GameObject[] healthPoints;
    public GameObject[] speedPoints;
    public GameObject[] magnetPoints;

    private Color color;

    private int upDMG;
    private int upHP;
    private int upSP;
    private int upMG;

    private int dmgCost;
    private int hpCost;
    private int spCost;
    private int mgCost;

    private int coins;


    void Start() {

        BorderColor();
        CheckUpgrades();

    }

    private void BorderColor() {

        var prefColor = PlayerPrefs.GetInt("Color"); //red green blue

        if (prefColor == 1) {

            color = Color.red;

        } else if (prefColor == 2) {

            color = Color.green;

        } else {

            color = Color.blue;

        }

        window.GetComponent<SpriteRenderer>().color = color;

    }

    private void CheckUpgrades() { //1 or 0

        upDMG = PlayerPrefs.GetInt("DMG"); 
        upHP = PlayerPrefs.GetInt("HP");
        upSP = PlayerPrefs.GetInt("SP");
        upMG = PlayerPrefs.GetInt("MG");
        coins = PlayerPrefs.GetInt("Coins");

        dmgCost = (upDMG + 1) * 10; 
        hpCost = (upHP + 1) * 10; 
        spCost = (upSP + 1) * 10;
        mgCost = (upMG + 1) * 10; 
    
        UpdateCoins();
        SetUpgrades();
        SetButtons();

    }

    private void UpdateCoins() {

        coinsText.text = coins + " Gold";

    }

    private void SetUpgrades() {

        SetDMGUpgrades();
        SetHPUpgrades();
        SetSPUpgrades();
        SetMGUpgrades();

    }

    private void SetDMGUpgrades() {

        for (int i = 0; i < upDMG; i++) {

            damagePoints[i].GetComponent<SpriteRenderer>().color = color;
        }

    }

    private void SetHPUpgrades() {

        for (int i = 0; i < upHP; i++) {
            
            healthPoints[i].GetComponent<SpriteRenderer>().color = color;
        }

    }

    private void SetSPUpgrades() {
        
        for (int i = 0; i < upSP; i++) {
            
            speedPoints[i].GetComponent<SpriteRenderer>().color = color;
        }

    }

    private void SetMGUpgrades() {
        
        for (int i = 0; i < upMG; i++) {
            
            magnetPoints[i].GetComponent<SpriteRenderer>().color = color;
        }

    }

    private void SetButtons() {

        SetDMGButton();
        SetHPButton();
        SetSPButton();
        SetMGButton();

    }

    private void SetDMGButton() {

        if (upDMG == 10) {

            upgradeDMGButton.interactable = false;
            upgradeDMGText.text = "MAX";

        } else {

            upgradeDMGText.text = "Upgrade: " + dmgCost;

            if (coins < dmgCost) {

                upgradeDMGButton.interactable = false;
                upgradeDMGText.color = Color.red;

            }

        }
    }

    private void SetHPButton() {

        if (upHP == 10) {

            upgradeHPButton.interactable = false;
            upgradeHPText.text = "MAX";

        } else {

            upgradeHPText.text = "Upgrade: " + hpCost;

            if (coins < hpCost) {

                upgradeHPButton.interactable = false;
                upgradeHPText.color = Color.red;
            }

        }

    }

    private void SetSPButton() {

        if (upSP == 10) {

            upgradeSPButton.interactable = false;
            upgradeSPText.text = "MAX";

        } else {

            upgradeSPText.text = "Upgrade: " + spCost;
            
            if (coins < spCost) {

                upgradeSPButton.interactable = false;
                upgradeSPText.color = Color.red;

            }

        }

    }

    private void SetMGButton() {

        if (upMG == 10) {

            upgradeMGButton.interactable = false;
            upgradeMGText.text = "MAX";

        } else {

            upgradeMGText.text = "Upgrade: " + mgCost;

            if (coins < mgCost) {

                upgradeMGButton.interactable = false;
                upgradeMGText.color = Color.red;

            }

        }

    }

    public void UpgradeDMG() {

        coins = coins - dmgCost;
        upDMG++;

        PlayerPrefs.SetInt("Total Coins Spent" , dmgCost + PlayerPrefs.GetInt("Total Coins Spent"));

        dmgCost = (upDMG + 1) * 10; 

        UpdateCoins();
        SetButtons();
        SetDMGUpgrades();

        PlayerPrefs.SetInt("DMG" , upDMG);
        PlayerPrefs.SetInt("Coins" , coins);
        PlayerPrefs.Save();

    }

    public void UpgradeHP() {

        coins = coins - hpCost;
        upHP++;

        PlayerPrefs.SetInt("Total Coins Spent" , hpCost + PlayerPrefs.GetInt("Total Coins Spent"));

        hpCost = (upHP + 1) * 10; 

        UpdateCoins();
        SetButtons();
        SetHPUpgrades();

        PlayerPrefs.SetInt("HP" , upHP);
        PlayerPrefs.SetInt("Coins" , coins);
        PlayerPrefs.Save();

    }

    public void UpgradeSP() {

        coins = coins - spCost;
        upSP++;

        PlayerPrefs.SetInt("Total Coins Spent" , spCost + PlayerPrefs.GetInt("Total Coins Spent"));

        spCost = (upSP + 1) * 10;

        UpdateCoins();
        SetButtons();
        SetSPUpgrades();

        PlayerPrefs.SetInt("SP" , upSP);
        PlayerPrefs.SetInt("Coins" , coins);
        PlayerPrefs.Save();

    }

    public void UpgradeMG() {

        coins = coins - mgCost;
        upMG++;

        PlayerPrefs.SetInt("Total Coins Spent" , mgCost + PlayerPrefs.GetInt("Total Coins Spent"));

        mgCost = (upMG + 1) * 10;

        UpdateCoins();
        SetButtons();
        SetMGUpgrades();

        PlayerPrefs.SetInt("MG" , upHP);
        PlayerPrefs.SetInt("Coins" , coins);
        PlayerPrefs.Save();

    }

    public void GoBack() {

        SceneManager.LoadScene("Main Menu");

    }

}
