using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {

        switch (gameObject.name)
        {
            case "Bttn_lv1":
                SceneManager.LoadScene("Level1");
                break;
            case "Bttn_lv2":
                //SceneManager.LoadScene("Level2");
                break;
        }

    }
}
