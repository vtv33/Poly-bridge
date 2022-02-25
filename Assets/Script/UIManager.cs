using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button RoadBtn;
    [SerializeField] Button RopeBtn;
	[SerializeField] BarCreater barCreater;

	private void Start()
	{
		RoadBtn.onClick.Invoke();
	}

	public void Restart()
	{
		SceneManager.LoadScene("SampleScene");
	}

	public void Play()
	{
		Time.timeScale = 1;
	}

	public void ChangeBar(int myBarType)
	{
        if(myBarType == 0)
		{
			RopeBtn.GetComponent<Outline>().enabled = false;
			RoadBtn.GetComponent<Outline>().enabled = true;
			barCreater.barToClone = barCreater.RoadBar;
		}
		if(myBarType == 1)
		{
			RopeBtn.GetComponent<Outline>().enabled = true;
			RoadBtn.GetComponent<Outline>().enabled = false;
			barCreater.barToClone = barCreater.RopeBar;
		}
	}
}
