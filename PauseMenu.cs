using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool pause = false;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

        void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            pause = false;
        }

        void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            pause = true;
        }
    }
}
