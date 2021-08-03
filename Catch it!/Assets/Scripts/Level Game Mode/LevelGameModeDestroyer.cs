using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameModeDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}
