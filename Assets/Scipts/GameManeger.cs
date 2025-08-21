using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hiddenDogs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private TextMeshProUGUI score;
    private int curentScore;

    private void Start()
    {
        HideDogs();
        score.text = $"{curentScore} / {hiddenDogs.Length}";
    }

    private void HideDogs()
    {
        List<Transform> availablePoints = new
            List<Transform>(spawnPoints);
        foreach (GameObject dog in hiddenDogs)
        {
            int index = Random.Range(0, availablePoints.Count);
            Transform point = availablePoints[index];

            dog.transform.position = point.position;
            dog.transform.rotation = point.rotation;

            availablePoints.RemoveAt(index);


        }
    }

    public void ChangeScore()
    {

        curentScore += 1; // curentScore++
        score.text = $"{curentScore} / {hiddenDogs.Length}";
    }
}