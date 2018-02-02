using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterWithBase : MonoBehaviour {

    public GameObject baseObject;
    public GameObject floaterObject;

    public float baseRotateSpeed;
    public float floaterRotationX;
    public float floaterRotationY;
    public float floaterRotationZ;
    public float floaterBobbingSpeed;
    public float floaterBobbingDistance;

    protected Vector3 originalFloaterPosition;
    protected int modifier = 1;
    protected Vector3 xAxis = new Vector3(1, 0, 0);
    protected Vector3 yAxis = new Vector3(0, 1, 0);
    protected Vector3 zAxis = new Vector3(0, 0, 1);
    
    void Start () {
        this.originalFloaterPosition = this.floaterObject.transform.position;
    }
	
	void Update () {
        baseObject.transform.Rotate(zAxis, baseRotateSpeed);

        this.floaterObject.transform.Rotate(this.xAxis, this.floaterRotationX);
        this.floaterObject.transform.Rotate(this.yAxis, this.floaterRotationY);
        this.floaterObject.transform.Rotate(this.zAxis, this.floaterRotationZ);

        if (Mathf.Abs(this.floaterObject.transform.position.y - this.originalFloaterPosition.y) >= this.floaterBobbingDistance)
        {
            this.modifier = -this.modifier;
        }

        this.floaterObject.transform.position = new Vector3(
            this.floaterObject.transform.position.x,
            this.floaterObject.transform.position.y + (this.floaterBobbingSpeed * this.modifier),
            this.floaterObject.transform.position.z);
    }
}
