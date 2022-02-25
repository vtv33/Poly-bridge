using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Point : MonoBehaviour
{
    public bool Runtime = true;
    public Rigidbody rbd;
    public Vector3 PointID;
    public List<Bar> ConnectedBars;
    // Start is called before the first frame update
    void Start()
    {
        if(Runtime == false)
		{
            rbd.isKinematic = true;
            PointID = transform.position;
            if(GameManager.allPoints.ContainsKey(PointID) == false);
			{
                GameManager.allPoints.Add(PointID, this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Runtime == false)
		{
            if(transform.hasChanged == true)
			{
                transform.hasChanged = false;
                transform.position = Vector3Int.RoundToInt(transform.position);
            }
		}
    }
}
