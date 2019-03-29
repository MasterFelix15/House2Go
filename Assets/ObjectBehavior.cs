using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehavior : MonoBehaviour
{
    private bool isSelected;
    public float y_offset;
    private Transform position_marker;
    private Transform original_parent;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        position_marker = GameObject.Find("PositionMarker").transform;
        // set the object to be selectable
        gameObject.layer = 10;
        if (y_offset == 0) {
            y_offset = gameObject.transform.position.y;
        }
        original_parent = gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected) {
            if (Input.GetKey(KeyCode.A)) { // rotate clockwise 1 degree/frame
                gameObject.transform.Rotate(0, 1, 0, Space.Self);
            } else if (Input.GetKey(KeyCode.D)) { // rotate counter-clockwise 1 degree/frame
                gameObject.transform.Rotate(new Vector3(0,-1,0));
            } else if (Input.GetKey(KeyCode.W)) { // attach the object to position marker
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.transform.parent = position_marker;
                gameObject.transform.localPosition = new Vector3(0, y_offset, 0);
            } else if (Input.GetKey(KeyCode.S)) {
                gameObject.transform.parent = original_parent;
                gameObject.GetComponent<Collider>().enabled = true;
            }
        }
    }

    public void SetSelect(bool status) {
        isSelected = status;
    }
}
