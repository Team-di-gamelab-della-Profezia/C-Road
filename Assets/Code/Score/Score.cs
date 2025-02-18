using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    bool isVisible = false;
    int score = 0;
    float playerSpeed = 0.5f;
    string playerName = "Manuel";

    public TextMeshProUGUI textScore;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            UpdateScore();
        }

        if (Input.GetKeyDown("up"))
        {
            UpdateScore();
        }

    }


    public void UpdateScore()
    {
        score = score + 1;
        // print(score);

        textScore.text = score.ToString();
    }


}
