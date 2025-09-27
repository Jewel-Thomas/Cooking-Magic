using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footStepsTimer;
    private float footstepsTimerMax = 0.1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepsTimer -= Time.deltaTime;
        if (footStepsTimer < 0)
        {
            footStepsTimer = footstepsTimerMax;
            if (player.IsWalking())
            {
                float volume = 0.1f;
                SoundManager.Instance.PlayFootStepSound(player.transform.position, volume);
            }
        }
    }
}
