using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EmotesSlection : NetworkBehaviour
{
    public GameObject EmoteWheel;
    public GameObject shopUI;
    private GameObject BlurScreen;
    public GameObject Gold;
    List<GameObject> EmoteButtonsGO = new List<GameObject>();
    Animator animator;
    int shopDetectionRadius=1;

    bool isInsideShop;






    private bool tabHeld = false;

    private void Awake()
    {
        BlurScreen = GameObject.Find("BlurScreen");
        EmoteWheel = GameObject.Find("EmoteWheel");
        shopUI = GameObject.Find("shopUI");
        animator = GetComponent<Animator>();
        Gold = GameObject.Find("Gold");
        int goldtext= PlayerPrefs.GetInt("Currency");
        Gold.GetComponent<TextMeshProUGUI>().text = goldtext.ToString();

    }
    void Update()
    {
        if (!IsLocalPlayer) return;
            if (BlurScreen != null && EmoteWheel != null && animator != null)
            {
                if (Input.GetKey(KeyCode.Tab))
                {
                    CanvasGroup cg = EmoteWheel.transform.GetChild(0).GetComponent<CanvasGroup>();
                    if (!tabHeld)
                    {
                        poulateButtonArray();
                        tabHeld = true;
                        cg.gameObject.SetActive(true);
                        BlurScreen.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (cg.alpha < 1) cg.alpha += 0.05f;
                }
                else
                {
                    if (tabHeld)
                    {
                        tabHeld = false;
                        GameObject go = EmoteWheel.transform.GetChild(0).gameObject;
                        go.GetComponent<CanvasGroup>().alpha = 0;
                        go.SetActive(false);
                        BlurScreen.transform.GetChild(0).gameObject.SetActive(false);
                    }

                }
            }
            else
            {
                Debug.Log("Not Expected Scene");
            }


        if (!IsLocalPlayer) return;
        Collider[] colliders = Physics.OverlapSphere(transform.position, shopDetectionRadius);
        bool foundShop = false;
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("EmoteShop"))
            {
                foundShop = true;
      
                break;
            }
        }
        if (foundShop && !isInsideShop)
        {
            shopUI.transform.GetChild(0).gameObject.SetActive(true);
            isInsideShop = true;
        }
        else if (!foundShop && isInsideShop)
        {
            shopUI.transform.GetChild(0).gameObject.SetActive(false);
            isInsideShop = false;
        }
    }

    

    public void poulateButtonArray()
    {
        if (!IsLocalPlayer) return;
        int[] lockedEmotes = SaveSystem.LoadPlayerData();
        for (int i = 1; i < EmoteWheel.transform.GetChild(0).childCount; i++)
        {
           
                GameObject childGameObject = EmoteWheel.transform.GetChild(0).GetChild(i).gameObject;
            if(i>2)
            {
              
                if (lockedEmotes[i - 3] == 0)
                {

                    childGameObject.GetComponent<Button>().interactable = true;
                }
                else
                    Debug.Log("locked");
            }
           
                string param = "Emote" + i.ToString();
                childGameObject.GetComponent<Button>().onClick.AddListener(() => SetEmote1Animation(param));
                EmoteButtonsGO.Add(childGameObject);
        }
    }

    void SetEmote1Animation(string triggername)
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(triggername);
        GetComponent<PlayerInput>().enabled = false;
        StartCoroutine(canAnimateAgain(triggername));
    }

    IEnumerator canAnimateAgain(string animationName)
    {
        while (true)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName(animationName))
            {
                GetComponent<PlayerInput>().enabled = false;
                yield return new WaitForSeconds(stateInfo.length);
                GetComponent<PlayerInput>().enabled = true;
            }
            else
            {
                yield return null; 
            }
        }
    }


   
}



