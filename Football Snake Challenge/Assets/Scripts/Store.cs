using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreData
{
    public List<ItemShop> itemShops;
}

[System.Serializable]
public class ItemShop
{
    public int num;
    public bool isBuyed;
    public int price;
}

public class Store : MonoBehaviour
{
    public static Store I;

    private void Awake()
    {
        if (I == null)
            I = this;
    }

    private int _numActive;

    [SerializeField] private StoreData storeData;
    [SerializeField] private ButtonStore[] buttonStores;

    [SerializeField] private Sprite[] _spriteBalls;
    public static Sprite _currentSpriteBall;

    private void Start()
    {
        string js = PlayerPrefs.GetString("jsStore");
        if (js != "")
        {
            storeData = JsonUtility.FromJson<StoreData>(js);
        }

        for (int i = 0; i < storeData.itemShops.Count; i++)
        {
            buttonStores[i].Init(storeData.itemShops[i].price, i, storeData.itemShops[i].isBuyed);
        }

        _currentSpriteBall = _spriteBalls[0];
        buttonStores[0].ChangeStateItemStore(ButtonStore.Status.Active);
    }

    public void ChoiceBall(int number)
    {
        if (storeData.itemShops[number].isBuyed)
        {
            buttonStores[_numActive].ChangeStateItemStore(ButtonStore.Status.Buyed);

            _numActive = number;
            //active

            buttonStores[_numActive].ChangeStateItemStore(ButtonStore.Status.Active);

            _currentSpriteBall = _spriteBalls[_numActive];
        }
        else
        {
            int valueBalance = Main._scorePlayer.GetBalance();

            if (valueBalance >= storeData.itemShops[number].price)
            {
                Main._scorePlayer.DiscreaseBalance(storeData.itemShops[number].price);

                buttonStores[_numActive].ChangeStateItemStore(ButtonStore.Status.Buyed);

                _numActive = number;
                //active

                buttonStores[_numActive].ChangeStateItemStore(ButtonStore.Status.Active);

                storeData.itemShops[_numActive].isBuyed = true;

                _currentSpriteBall = _spriteBalls[_numActive];

                Save();
            }
        }
    }

    private void Save()
    {
        string js = JsonUtility.ToJson(storeData);
        PlayerPrefs.SetString("jsStore", js);
    }
}
