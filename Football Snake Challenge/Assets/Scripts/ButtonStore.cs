using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStore : MonoBehaviour
{
    [SerializeField] private Button _thisBtn;

    private void Awake()
    {
        _thisBtn.onClick.AddListener(() =>
        {
            Store.I.ChoiceBall(number);
        });
    }

    [SerializeField] private GameObject _activeObj;
    [SerializeField] private Text _priceText;

    private int number;

    public enum Status
    {
        idle,
        Active,
        Buyed,
        NoBuy
    }

    public void Init(int price, int number, bool isBuyed)
    {
        this.number = number;
        _priceText.text = price.ToString();

        if (isBuyed)
        {
            ChangeStateItemStore(Status.Buyed);
        }
    }

    public void ChangeStateItemStore(Status status)
    {
        switch (status)
        {
            case Status.idle:
                break;
            case Status.Active:
                _priceText.transform.parent.gameObject.SetActive(false);
                _activeObj.SetActive(true);
                break;
            case Status.Buyed:
                _priceText.transform.parent.gameObject.SetActive(false);
                _activeObj.SetActive(false);
                break;
            case Status.NoBuy:
                _priceText.transform.parent.gameObject.SetActive(true);
                _activeObj.SetActive(false);
                break;
        }
    }
}
