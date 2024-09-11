using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bonus bonus))
        {
            bonus.gameObject.SetActive(false);
            SoundTrackHandler.sound.PlaySoundBonus();
            Main.OnTakeBonus?.Invoke();
        }

        if (collision.TryGetComponent(out BallItem ball))
        {
            Main.OnGameOver?.Invoke();
        }

        if (collision.TryGetComponent(out TriggerWall wall))
        {
            Main.OnGameOver?.Invoke();
        }
    }
}
