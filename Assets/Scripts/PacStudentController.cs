using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PacStudentController : MonoBehaviour
{
    private Animator anim;

    public Text scoreboard;
    private int score = 0;

    private AudioSource audio;
    public AudioClip collide;
    public AudioClip eat;
    public AudioClip step;
    public AudioClip death;

    public Tilemap wallTilemap;
    public Tilemap orbTilemap;
    public ParticleSystem walkEffect;

    public float moveSpeed = 10.0f;
    private Vector3 currentInput;
    private Vector3 lastInput;
    private bool moving = false;

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
        if (Input.GetKey(KeyCode.S)) lastInput = Vector3.down;
        if (Input.GetKey(KeyCode.D)) lastInput = Vector3.right;

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
        isOrb(target);
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
        Vector3Int gridPosition = wallTilemap.WorldToCell(position);
        TileBase tile = wallTilemap.GetTile(gridPosition);

        if (tile != null)
        {
            if (lastInput == currentInput && currentInput != Vector3.zero)
            {
                ChangeAudio(0);
                anim.SetInteger("Direction", 5);
            }
            return false;
        }
        else return true;
    }

    void isOrb(Vector3 position)
    {
        Vector3Int gridPosition = orbTilemap.WorldToCell(position);
        TileBase tile = orbTilemap.GetTile(gridPosition);
        if (tile != null && currentInput != Vector3.zero)
        {
            if (lastInput == currentInput)
            {
                ChangeAudio(1);
            }
        }
        else
        {
            if(currentInput != Vector3.zero && lastInput == currentInput) ChangeAudio(2);
        }
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

    void ChangeAudio(int x)
    {
        
        switch(x)
        { 
            case 0:
                    audio.loop = false;
                    audio.clip = collide;
                    StartCoroutine(DisableAudioAfterClip(audio.clip.length));
                break;

            case 1:
                audio.enabled = true;
                audio.loop = true;
                audio.clip = eat;
                break;

            case 2:
                audio.enabled = true;
                audio.loop = true;
                audio.clip = step;
                break;
        }
        audio.Play();
    }

    IEnumerator DisableAudioAfterClip(float clipLength)
    {
        audio.Play();
        yield return new WaitForSeconds(clipLength);
        audio.enabled = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Orbs") && orbTilemap != null)
        {
            Vector3 hitPosition = Vector3.zero;
            hitPosition = collision.GetContact(0).point;

            Vector3Int cellPosition = orbTilemap.WorldToCell(hitPosition);

            TileBase tile = orbTilemap.GetTile(cellPosition);

            if (tile != null)
            {
                score += 10;
                scoreboard.text = "High Score:\n" + score.ToString();
                orbTilemap.SetTile(cellPosition, null); 
            }
        }

        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            score += 100;
            scoreboard.text = "High Score:\n" + score.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Teleporter"))
        {
            transform.position = other.GetComponent<Telesupport>().exit.transform.position;
        }
    }

}
