using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    Animator anim;

    AudioSource audio;
    public AudioClip collide;
    public AudioClip eat;
    public AudioClip step;
    public AudioClip death;

    public float moveSpeed = 10.0f;
    public Tilemap wallTilemap;
    public Tilemap orbTilemap;
    public ParticleSystem walkEffect;

    private Vector3 currentInput;
    private Vector3 lastInput;
    private bool moving = false;
    private bool start = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        currentInput = Vector3.zero;
        lastInput = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) lastInput = Vector3.up;
        if (Input.GetKey(KeyCode.A)) lastInput = Vector3.left;

        if (Input.GetKey(KeyCode.S))
        {
            lastInput = Vector3.down;
            if (!start)
            { 
                start = true;
                ChangeAudio(transform.position + lastInput);
            }
        }

        if (Input.GetKey(KeyCode.D)) 
        { 
            lastInput = Vector3.right;
            if (!start)
            {
                start = true;
                ChangeAudio(transform.position + lastInput);
            }
        }

        if (!moving)
        {

            Vector3 newDir = transform.position + lastInput;
            Vector3 oldDir = transform.position + currentInput;

            if (IsWalkable(newDir) && newDir != transform.position)
            {
                currentInput = lastInput;
                changeAnim();
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
        isOrb(position);
        Vector3Int gridPosition = wallTilemap.WorldToCell(position);
        TileBase tile = wallTilemap.GetTile(gridPosition);

        if (tile != null)
        {
            if (lastInput == currentInput)
            {
                anim.SetInteger("Direction", 5);
                ChangeAudio(position);
            }
            return false;
        } else return true;
    }

    bool isOrb(Vector3 position)
    {
        Vector3Int gridPosition = orbTilemap.WorldToCell(position);
        TileBase tile = orbTilemap.GetTile(gridPosition);
        Debug.Log(tile);
        return tile != null;
    }

    void changeAnim()
    {
        if (currentInput == Vector3.up)
        {
            anim.SetInteger("Direction", 8);
        }
        else if (currentInput == Vector3.left)
        {
            anim.SetInteger("Direction", 4);
        }
        else if (currentInput == Vector3.down)
        {
            anim.SetInteger("Direction", 2);
        }
        else if (currentInput == Vector3.right)
        {
            anim.SetInteger("Direction", 6);
        }
    }

    void ChangeAudio(Vector3 position)
    {
        if (!IsWalkable(position))
        {
            audio.loop = false;
            audio.clip = collide;
        }
        else if (isOrb(position))
        {
            audio.loop = true;
            audio.clip = eat;
        }
        else
        {
            audio.loop = true;
            audio.clip = step;
        }
        
        audio.Play();
    }
}
