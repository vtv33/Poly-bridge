using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public float maxLength = 1f;
    public Vector3 startPosition;
    [SerializeField] BoxCollider boxCollider;
    public HingeJoint startJoint;
    public HingeJoint endJoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCreatingBar(Vector3 toPosition)
    {
        transform.position = (toPosition + startPosition) / 2;

        Vector3 dir = toPosition - startPosition;
        float angle = Vector3.SignedAngle(Vector3.right, dir, Vector3.forward);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float length = dir.magnitude;
        transform.localScale = new Vector3(length, transform.localScale.y, transform.localScale.z);

    }
}
