using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BarCreater : MonoBehaviour
{
	public GameObject RoadBar;
	public GameObject RopeBar;
	bool barCreationStarted = false;
	[SerializeField] Bar currentBar;
	public GameObject barToClone;
	[SerializeField] Transform barParent;
	[SerializeField] Point currentStartPoint;
	[SerializeField] Point currentEndPoint;
	[SerializeField] GameObject pointToClone;
	[SerializeField] Transform pointParent;

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 curPos = new Vector3(0,0,0);
		if(Physics.Raycast(ray, out RaycastHit raycastHit))
		{
			curPos = new Vector3(raycastHit.point.x, raycastHit.point.y, 0);
			if (barCreationStarted == true)
			{
				Vector3 EndPosition = Vector3Int.RoundToInt(curPos);
				Vector3 Dir = EndPosition - currentBar.startPosition;
				Vector3 ClampedPosition = currentBar.startPosition + Vector3.ClampMagnitude(Dir,currentBar.maxLength);

				currentEndPoint.transform.position = Vector3Int.FloorToInt(ClampedPosition);
				currentEndPoint.PointID = currentEndPoint.transform.position;
				currentBar.UpdateCreatingBar(currentEndPoint.transform.position);
			}
			if (Input.GetMouseButtonDown(0))
			{
				if (barCreationStarted == false)
				{
					barCreationStarted = true;
					StartBarCreation(Vector3Int.RoundToInt(curPos));
				}
				else FinishBarCreation();
			}
			else if (Input.GetMouseButtonDown(1))
			{
				barCreationStarted = false;
				DeleteCurrentBar();
			}
		}
		
	}

	void StartBarCreation(Vector3 StartPosition)
	{
		currentBar = Instantiate(barToClone, barParent).GetComponent<Bar>();
		currentBar.startPosition = StartPosition;

		if(GameManager.allPoints.ContainsKey(StartPosition))
		{
			currentStartPoint = GameManager.allPoints[StartPosition];
		}
		else
		{
			currentStartPoint = Instantiate(pointToClone, StartPosition, Quaternion.Euler(90, 0, 0), pointParent).GetComponent<Point>();
			GameManager.allPoints.Add(StartPosition,currentStartPoint);
		}
		currentEndPoint = Instantiate(pointToClone, StartPosition, Quaternion.Euler(90, 0, 0), pointParent).GetComponent<Point>();
	}

	void FinishBarCreation()
	{
		if (GameManager.allPoints.ContainsKey(currentEndPoint.transform.position))
		{
			Destroy(currentEndPoint.gameObject);
			currentEndPoint = GameManager.allPoints[currentEndPoint.transform.position];
		}
		else
		{
			GameManager.allPoints.Add(currentEndPoint.transform.position, currentEndPoint);
		}

		currentStartPoint.ConnectedBars.Add(currentBar);
		currentEndPoint.ConnectedBars.Add(currentBar);

		currentBar.startJoint.connectedBody = currentStartPoint.rbd;
		currentBar.startJoint.anchor = currentBar.transform.InverseTransformPoint(currentBar.startPosition);
		currentBar.endJoint.connectedBody = currentEndPoint.rbd;
		currentBar.endJoint.anchor = currentBar.transform.InverseTransformPoint(currentEndPoint.transform.position);

		StartBarCreation(currentEndPoint.transform.position);
	}

	void DeleteCurrentBar()
	{
		Destroy(currentBar.gameObject);
		if(currentStartPoint.ConnectedBars.Count == 0 && currentStartPoint.Runtime == true)
		{
			Destroy(currentStartPoint.gameObject);
		}
		if(currentEndPoint.ConnectedBars.Count == 0 && currentEndPoint.Runtime == true)
		{
			Destroy(currentEndPoint.gameObject);
		}
	}
}
