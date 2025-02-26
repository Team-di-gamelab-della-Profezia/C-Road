using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [Header("Impostazioni Game Over")]
    public string sceneToLoad = "MainMenu";
    public LayerMask selectableLayer;
    public int punteggioCorrente;

    private void OnTriggerEnter(Collider other)
    {
        if (IsSelectable(other.gameObject))
        {
            EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsSelectable(collision.gameObject))
        {
            EndGame();
        }
    }

    bool IsSelectable(GameObject obj)
    {
        return ((1 << obj.layer) & selectableLayer) != 0;
    }

    void EndGame()
    {
        Debug.Log("Salvataggio punteggio finale: " + punteggioCorrente);
        ScoreManager.SaveScore(punteggioCorrente);
        SceneManager.LoadScene(sceneToLoad);
    }
}