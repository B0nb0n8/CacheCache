using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [SerializeField] GameObject[] hiddenDogs;
    [SerializeField] Transform[] spawnPoints;
      

    // Start is called before the first frame update
    void Start()
    {
        HideDogs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HideDogs()
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        foreach (GameObject dog in hiddenDogs)
        {
            int index = Random.Range(0, availablePoints.Count);
            Transform point = availablePoints[index];

            dog.transform.position = point.position;
            dog.transform.rotation = point.rotation;

            availablePoints.RemoveAt(index);
        }
    }
}
