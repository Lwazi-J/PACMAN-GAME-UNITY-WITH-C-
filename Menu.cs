using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void Play()
	{
        SceneManager.LoadScene("GamePlay");
    }

	public void Instructions()
	{
        SceneManager.LoadScene("Instruction");
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Back()
	{
        SceneManager.LoadScene("Menu");
	}

    public void Next()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
