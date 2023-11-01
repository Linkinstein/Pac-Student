using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public Tilemap wallTilemap;
    public Tilemap OrbTilemap;
    public ParticleSystem walkEffect;

    private Vector3 currentInput;
    private Vector3 lastInput;
    private bool moving;

    void Start()
    {
        currentInput = Vector3.zero;
        lastInput = Vector3.zero;
        moving = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) lastInput = Vector3.up;
        if (Input.GetKey(KeyCode.A)) lastInput = Vector3.left;
        if (Input.GetKey(KeyCode.S)) lastInput = Vector3.down;
        if (Input.GetKey(KeyCode.D)) lastInput = Vector3.right;

        if (!moving)
        {

            Vector3 newDir = transform.position + lastInput;
            Vector3 oldDir = transform.position + currentInput;

            if (IsWalkable(newDir))
            {
                currentInput = lastInput;
                StartCoroutine(LerpToPosition(newDir));
            }
            else if (IsWalkable(oldDir))
            {
                StartCoroutine(LerpToPosition(oldDir));
            }
        }
    }
    
    IEnumerator LerpToPosition(Vector3 target)
    {
        walkEffect.Play();
        moving = true;
        float journeyLength = Vector3.Distance(transform.position, target);
        float journeyTime = journeyLength / moveSpeed;
        float startTime = Time.time;

        while (Time.time < startTime + journeyTime)
        {
            float journeyFraction = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Lerp(transform.position, target, journeyFraction);
            yield return null;
        }

        transform.position = target;
        moving = false;
        walkEffect.Stop();
    }

    bool IsWalkable(Vector3 position)
    {
        // Convert world position to grid position
        Vector3Int gridPosition = wallTilemap.WorldToCell(position);

        // Check if there is a tile (wall) at the grid position
        TileBase tile = wallTilemap.GetTile(gridPosition);

        // Return false if there's a wall (tile), true if it's walkable (no tile)
        return tile == null;
    }
}
