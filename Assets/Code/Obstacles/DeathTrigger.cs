using UnityEngine;

public class DeathTrigger : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Application.Quit();
            Time.timeScale = 0;
        }
    }
}
