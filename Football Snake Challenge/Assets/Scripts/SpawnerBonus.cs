using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBonus : MonoBehaviour
{
    [SerializeField] private Bonus _bonus;

    private void Start()
    {
        Main.OnStartGame += SpawnNextBonus;
        Main.OnTakeBonus += SpawnNextBonus;
    }

    private void OnDestroy()
    {
        Main.OnStartGame += SpawnNextBonus;
        Main.OnTakeBonus -= SpawnNextBonus;
    }

    public void SpawnNextBonus()
    {
        _bonus.transform.position = new Vector2(Random.Range(-2f, 2f), Random.Range(-2.5f, 3f));

        _bonus.gameObject.SetActive(true);
    }
}
