using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Dictionary<Vector3,Point> allPoints = new  Dictionary<Vector3, Point>();
	[SerializeField] Point StartPoint1;
	[SerializeField] Point StartPoint2;

	private void Awake()
	{
		allPoints.Clear();
		allPoints.Add(StartPoint1.gameObject.transform.position, StartPoint1);
		allPoints.Add(StartPoint2.gameObject.transform.position, StartPoint2);
		Time.timeScale = 0;
	}

}
