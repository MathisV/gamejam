using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(CameraSpeed * Time.deltaTime, 0, 0);
    }
}
