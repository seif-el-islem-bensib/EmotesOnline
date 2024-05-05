using Unity.Netcode;
using UnityEngine;

public class SaveSystem : NetworkBehaviour
{

  
    private const string initializedKey = "PlayerDataInitialized";


    private void Start()
    {
        InitializePlayerData();
    }

    public static void SavePlayerData(int[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            PlayerPrefs.SetInt("PlayerData_" + i, data[i]);
        }
        PlayerPrefs.SetInt("PlayerData_Length", data.Length);
        PlayerPrefs.Save();
    }
    public static int[] LoadPlayerData()
    {
        int dataLength = PlayerPrefs.GetInt("PlayerData_Length", 0);
        int[] data = new int[dataLength];
        for (int i = 0; i < dataLength; i++)
        {
            data[i] = PlayerPrefs.GetInt("PlayerData_" + i);
        }
        return data;
    }
    public static void InitializePlayerData()
    {

        //int[] initialData = new int[] { 1, 1, 1, 1 };
        //SavePlayerData(initialData);
        //PlayerPrefs.SetInt(initializedKey, 1);
        //PlayerPrefs.SetInt("Currency", 7000);
        //Debug.Log("Player data initialized successfully.");

        if (!PlayerPrefs.HasKey(initializedKey))
        {
            int[] initialData = new int[] { 1, 1, 1, 1 };
            SavePlayerData(initialData);
            PlayerPrefs.SetInt(initializedKey, 1);
            PlayerPrefs.SetInt("Currency", 7000);
            Debug.Log("Player data initialized successfully.");
        }
        else
        {
            Debug.Log("Player data already initialized.");
        }
    }
}
