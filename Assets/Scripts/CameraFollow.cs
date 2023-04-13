using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraSpeed;
    private Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.isDead)
            transform.position += new Vector3(CameraSpeed * Time.deltaTime, 0, 0);
    }
}
