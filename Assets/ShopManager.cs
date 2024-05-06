using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : NetworkBehaviour
{
    GameObject UIShop;
    GameObject[] emotes = new GameObject[4];
    GameObject networkmanager;


    

    public override void OnNetworkSpawn()
    {
       

        base.OnNetworkSpawn();

        populatebutons();
        if (IsServer)
        {
            Debug.Log("Server has initialized.");
        }

        if (IsLocalPlayer)
        {
            Debug.Log("Local player has initialized.");
            AssignButtonActions();
        }
      


    }

    void AssignButtonActions()
    {
        Button btn1 = emotes[0].GetComponent<Button>();
        Button btn2 = emotes[1].GetComponent<Button>();
        Button btn3 = emotes[2].GetComponent<Button>();
        Button btn4 = emotes[3].GetComponent<Button>();


        btn1.onClick.AddListener(() => TryPurchaceEmote3(btn1));
        btn2.onClick.AddListener(() => TryPurchaceEmote4(btn2));
        btn3.onClick.AddListener(() => TryPurchaceEmote5(btn3));
        btn4.onClick.AddListener(() => TryPurchaceEmote6(btn4));
    }

    public void populatebutons()
    {
        
        UIShop = GameObject.Find("shopUI");
        emotes[0] = UIShop.transform.GetChild(0).GetChild(0).gameObject;
        emotes[1] = UIShop.transform.GetChild(0).GetChild(1).gameObject;
        emotes[2] = UIShop.transform.GetChild(0).GetChild(2).gameObject;
        emotes[3] = UIShop.transform.GetChild(0).GetChild(3).gameObject;


    }

    void updateButtons()
    {
        if (!IsLocalPlayer) return;
        int[] arrayEmotes = SaveSystem.LoadPlayerData();

        for (int i = 0; i < arrayEmotes.Length; i++)

            if (arrayEmotes[i] == 0)
            {

                emotes[i].GetComponent<Button>().interactable = false;
                i++;
            }

    }
    public void TryPurchaceEmote3(Button clickedButton)
    {
        if (!IsLocalPlayer) return;
        int goldtext = PlayerPrefs.GetInt("Currency");
        int[] arrayEmotes = SaveSystem.LoadPlayerData();
        if (goldtext > 1000)
        {
            arrayEmotes[0] = 0;
            SaveSystem.SavePlayerData(arrayEmotes);
            clickedButton.interactable = false;
        }

        updateButtons();



    }

    public void TryPurchaceEmote4(Button clickedButton)
    {
        if (!IsLocalPlayer) return;
        int goldtext = PlayerPrefs.GetInt("Currency");
        int[] arrayEmotes = SaveSystem.LoadPlayerData();
        if (goldtext > 1500)
        {
            arrayEmotes[1] = 0;
            SaveSystem.SavePlayerData(arrayEmotes);
            clickedButton.interactable = false;
        }

        updateButtons();
    }
    public void TryPurchaceEmote5(Button clickedButton)
    {
        if (!IsLocalPlayer) return;
        int goldtext = PlayerPrefs.GetInt("Currency");
        int[] arrayEmotes = SaveSystem.LoadPlayerData();
        if (goldtext > 1700)
        {
            arrayEmotes[2] = 0;
            SaveSystem.SavePlayerData(arrayEmotes);
            clickedButton.interactable = false;
        }

        updateButtons();
    }

    public void TryPurchaceEmote6(Button clickedButton)
    {
        if (!IsLocalPlayer) return;
        int goldtext = PlayerPrefs.GetInt("Currency");
        int[] arrayEmotes = SaveSystem.LoadPlayerData();
        if (goldtext > 2000)
        {
            arrayEmotes[3] = 0;
            SaveSystem.SavePlayerData(arrayEmotes);
            clickedButton.interactable = false;
        }

        updateButtons();
    }
}
