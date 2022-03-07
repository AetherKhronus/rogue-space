using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffMenu : MonoBehaviour {

    public GameObject canvasQuests;
    public GameObject canvasChoice;
    public GameObject buffImgs;
    public Sprite[] buffSprites;
    public GameObject[] buffObjects;
    public Button buff3Button;
    public Text[] buffTexts;
    public Text[] buffInfos;

    private bool[] buffsTaken;
    private string[] buffsTakenTexts;
    private string[] buffsTakenInfos;
    private int[] upgrades = new int[4];
    private int buff;

    // gold [fireRate moreBullet goldWorth dodgeChance every5Bullet] freeUpgrade
    void Awake() {
        
        upgrades[0] = PlayerPrefs.GetInt("DMG");
        upgrades[1] = PlayerPrefs.GetInt("HP");
        upgrades[2] = PlayerPrefs.GetInt("SP");
        upgrades[3] = PlayerPrefs.GetInt("MG");

        GetBuffs();
        SetBuff1();
        SetBuff2();
        SetBuff3();

    }

    private void GetBuffs() {

        string[] buffs = PlayerPrefs.GetString("Buffs").Split('_');

        buffsTaken = new bool[buffs.Length];
        buffsTakenTexts = new string[buffs.Length];
        buffsTakenInfos = new string[buffs.Length];

        for (int i = 0; i < buffs.Length; i++) {

            if (buffs[i] == "0") {

                buffsTaken[i] = false;

            } else {

                buffsTaken[i] = true;

            }
            
        }

        buffsTakenTexts[0] = "Fire Rate";
        buffsTakenInfos[0] = "-0.2s per Shot";
        buffsTakenTexts[1] = "Bullets";
        buffsTakenInfos[1] = "+1 Bullet per Shot";
        buffsTakenTexts[2] = "Gold Worth";
        buffsTakenInfos[2] = "+1 Gold per Drop";
        buffsTakenTexts[3] = "Dodge Chance";
        buffsTakenInfos[3] = "+35% Chance to Dodge Hits";
        buffsTakenTexts[4] = "5th Bullet";
        buffsTakenInfos[4] = "Every 5th Bullet deals 2x Damage";

        buff = 0;

    }

    private void SetBuff1() {

        buffTexts[0].text = "Gold";
        buffInfos[0].text = "+50 Gold";
        buffObjects[0].GetComponent<SpriteRenderer>().sprite = buffSprites[0];
        buffObjects[0].GetComponent<SpriteRenderer>().color = Color.yellow;

    }

    private void SetBuff2() {

        while (buffsTaken[buff] == true) {

            if (buff < 4) {
                
                buff++;
            
            } else if (buff == 4) {

                break;
                
            }

        }

        buffTexts[1].text = buffsTakenTexts[buff];
        buffInfos[1].text = buffsTakenInfos[buff];

        if (buff == 2) {    // Gold Worth

            buffObjects[1].GetComponent<SpriteRenderer>().sprite = buffSprites[buff + 1];
            buffObjects[1].GetComponent<SpriteRenderer>().color = Color.yellow;

        } else if (buff == 3) {    // Dodge

            int color = PlayerPrefs.GetInt("Color");
            buffObjects[1].GetComponent<SpriteRenderer>().color = Color.white;

            if (color == 1) {

                buffObjects[1].GetComponent<SpriteRenderer>().sprite = buffSprites[4];

            } else if (color == 2) {

                buffObjects[1].GetComponent<SpriteRenderer>().sprite = buffSprites[5];

            } else {

                buffObjects[1].GetComponent<SpriteRenderer>().sprite = buffSprites[6];

            }

        } else if (buff == 4) { // 5th Bullet

            buffObjects[1].GetComponent<SpriteRenderer>().sprite = buffSprites[buff + 3];
            buffObjects[1].GetComponent<SpriteRenderer>().color = Color.red;

        } else { // 0 1

            buffObjects[1].GetComponent<SpriteRenderer>().sprite = buffSprites[buff + 1];

        }

    }

    private void SetBuff3() {
        
        if (upgrades[0] == 10 && upgrades[1] == 10 && upgrades[2] == 10 && upgrades[3] == 10) {

            buffTexts[2].text = "Upgrade";
            buffTexts[2].color = Color.red;
            buffInfos[2].text = "Already Fully Upgraded";
            buffInfos[2].color = Color.red;
            buff3Button.interactable = false;

        } else {

            buffTexts[2].text = "Upgrade";
            buffInfos[2].text = "Random Free Upgrade";

        }

    }

    public void ChooseBuff1() {

        PlayerPrefs.SetInt("Coins" , PlayerPrefs.GetInt("Coins") + 50);
        PlayerPrefs.SetInt("Total Coins" , 50 + PlayerPrefs.GetInt("Total Coins"));
        PlayerPrefs.Save();

        buffImgs.SetActive(false);
        canvasChoice.SetActive(false);
        gameObject.SetActive(false);
        canvasQuests.SetActive(true);

    }

    public void ChooseBuff2() {
        
        switch (buff) {

            case 0:
                PlayerPrefs.SetString("Buffs" , "1_0_0_0_0");
            break;
            case 1:
                PlayerPrefs.SetString("Buffs" , "1_1_0_0_0");
            break;
            case 2:
                PlayerPrefs.SetString("Buffs" , "1_1_1_0_0");
            break;
            case 3: 
                PlayerPrefs.SetString("Buffs" , "1_1_1_1_0");
            break;
            default:
                PlayerPrefs.SetString("Buffs" , "1_1_1_1_1");
            break;
        }

        PlayerPrefs.Save();

        GetBuffs();
        SetBuff2();
        buffImgs.SetActive(false);
        canvasChoice.SetActive(false);
        gameObject.SetActive(false);
        canvasQuests.SetActive(true);

    }

    public void ChooseBuff3() {

        int rand = Random.Range(0 , 4);

        while (upgrades[rand] == 10) {

            rand = Random.Range(0 , 4);
        }

        if (rand == 0) {

            PlayerPrefs.SetInt("DMG" , upgrades[rand] + 1);

        } else if (rand == 1) {

            PlayerPrefs.SetInt("HP" , upgrades[rand] + 1);

        } else if (rand == 2) {

            PlayerPrefs.SetInt("SP" , upgrades[rand] + 1);

        } else {

            PlayerPrefs.SetInt("MG" , upgrades[rand] + 1);

        }
        
        PlayerPrefs.Save();

        buffImgs.SetActive(false);
        gameObject.SetActive(false);
        canvasChoice.SetActive(false);
        canvasQuests.SetActive(true);

    }

}
