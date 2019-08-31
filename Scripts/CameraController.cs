using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject TrackingObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x,TrackingObject.transform.position.x,0.1f),Mathf.Lerp(this.transform.position.y,TrackingObject.transform.position.y,0.1f),-10);
    }
}
