using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Records
{
    public List<Record> listRecords = new List<Record>();
}

[System.Serializable]
public class Record
{
    public int valueScore;
    public float time;

    public Record(int valueScore, float time)
    {
        this.valueScore = valueScore;
        this.time = time;
    }
}

public class RecordsScreen : MonoBehaviour
{
    [SerializeField] private Button _backBtn;

    [SerializeField] private Records _records;

    [SerializeField] private RecordBlockUI _recordPrefab;
    [SerializeField] private Transform _container;

    private List<RecordBlockUI> listBlocks = new List<RecordBlockUI>();

    private void Awake()
    {
        _backBtn.onClick.AddListener(() =>
        {
            ScreensController.I.ShowPanel(ScreensController.ScreenName.Menu);
        });
    }

    private void Start()
    {
        string jsonRecord = PlayerPrefs.GetString("RecordsJson");
        if (jsonRecord != "")
        {
            _records = JsonUtility.FromJson<Records>(jsonRecord);
        }

        for (int  i = 0; i < _records.listRecords.Count; i++)
        {
            RecordBlockUI recordBlockUI = Instantiate(_recordPrefab, _container);
            recordBlockUI.gameObject.SetActive(true);
            listBlocks.Add(recordBlockUI);
        }
        UpdateDataBlocks();
    }

    public bool CheckRecord(int valueScore, float time)
    {
        Record result = new Record(valueScore, time);

        bool newRecord = false;

        _records.listRecords.Add(result);

        RecordBlockUI recordBlockUI = Instantiate(_recordPrefab, _container);
        recordBlockUI.gameObject.SetActive(true);
        listBlocks.Add(recordBlockUI);

        for (int i = 0; i < _records.listRecords.Count; i++)
        {
            for (int y = 0; y < _records.listRecords.Count; y++)
            {
                if (_records.listRecords[i].valueScore < _records.listRecords[y].valueScore)
                {
                    Record tempRecord = new Record(_records.listRecords[i].valueScore, _records.listRecords[i].time);
                    _records.listRecords[i] = new Record(_records.listRecords[y].valueScore, _records.listRecords[y].time);
                    _records.listRecords[y] = tempRecord;
                }
            }       
        }

        string jsonRecord = JsonUtility.ToJson(_records);
        PlayerPrefs.SetString("RecordsJson", jsonRecord);

        UpdateDataBlocks();

        return newRecord;
    }

    private void UpdateDataBlocks()
    {
        for(int i = 0; i < _records.listRecords.Count; i++)
        {
            listBlocks[i].Init(_records.listRecords.Count - i, _records.listRecords[i].time, _records.listRecords[i].valueScore);
        }
    }
}
