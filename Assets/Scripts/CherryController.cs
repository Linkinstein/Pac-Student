using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject cherryPrefab;
    public float spawnInterval = 10.0f;
    private Camera mainCamera;

    private bool moving = false;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnCherry());
    }

    IEnumerator SpawnCherry()
    {
        while (true)
        {
            GameObject newCherry = Instantiate(cherryPrefab);

            Vector3 spawnPosition = GetRandomSpawnPosition();
            newCherry.transform.position = spawnPosition;

            Vector3 targetPosition = GetOppositeSidePosition(spawnPosition);

            StartCoroutine(MoveCherry(newCherry, targetPosition));

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        int side = Random.Range(0, 4);

        Camera mainCamera = Camera.main;
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0: //Top
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), 1.1f, mainCamera.nearClipPlane));
                break;
            case 1: //Bottom
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(0.0f, 1.0f), -0.1f, mainCamera.nearClipPlane));
                break;
            case 2: //Left
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(0.0f, 1.0f), mainCamera.nearClipPlane));
                break;
            case 3: //Right
                spawnPosition = mainCamera.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(0.0f, 1.0f), mainCamera.nearClipPlane));
                break;
        }

        return spawnPosition;
    }

    Vector3 GetOppositeSidePosition(Vector3 spawnPosition)
    {
        float x = spawnPosition.x;
        float y = spawnPosition.y;
        float halfScreenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float halfScreenHeight = mainCamera.orthographicSize;

        if (x < -halfScreenWidth)
        {
            return mainCamera.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(-1.0f, 1.0f), mainCamera.nearClipPlane));
        }
        else if (x > halfScreenWidth)
        {
            return mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, Random.Range(-1.0f, 1.0f), mainCamera.nearClipPlane));
        }
        else if (y < -halfScreenHeight)
        {
            return mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(-1.0f, 1.0f), 1.1f, mainCamera.nearClipPlane));
        }
        else
        {
            return mainCamera.ViewportToWorldPoint(new Vector3(Random.Range(-1.0f, 1.0f), -0.1f, mainCamera.nearClipPlane));
        }
    }

    IEnumerator MoveCherry(GameObject cherry, Vector3 targetPosition)
    {
        moving = true;
        float journeyLength = Vector3.Distance(cherry.transform.position, targetPosition);
        float startTime = Time.time;

        while (moving && cherry != null)
        {
            float journeyFraction = (Time.time - startTime) / (journeyLength*2);
            cherry.transform.position = Vector3.Lerp(cherry.transform.position, targetPosition, journeyFraction);
            Debug.Log(cherry.transform.position + " " + targetPosition + " " + (journeyFraction*20));
            if ((journeyFraction*20) >= 1.0f)
            {
                Debug.Log("DELETE");
                if (cherry != null)
                    Destroy(cherry);
                moving = false;
            }

            yield return null;
        }
    }
}
