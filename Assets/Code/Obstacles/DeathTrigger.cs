using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathTrigger : MonoBehaviour
{





    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            other.GetComponent<GameOverManager>().EndGame();
        }
    }
}
