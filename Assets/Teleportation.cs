using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Teleportation : MonoBehaviour
{
    private RaycastHit whereHitLast;
    public AudioClip audioClip;
    public GameObject my_camera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

    }

    private GameObject GetObjectPointedAt()
    {

        Vector3 origin = transform.position;
        Vector3 direction = my_camera.transform.forward;
        float range = 1000;
        if (Physics.Raycast(origin, direction, out whereHitLast, range))
            return whereHitLast.collider.gameObject;
        else
            return null;
    }

    private void Teleportations()
    {

        transform.position = whereHitLast.point + whereHitLast.normal * 2;
        Vector3 transformed = transform.position;
        transform.position = new Vector3(transformed.x, (float)1.7, transformed.z);
        print(transform.position);
        if (audioClip != null)
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (GetObjectPointedAt() != null)
                Teleportations();
    }
}
