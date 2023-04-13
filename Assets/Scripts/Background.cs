using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public float backgroundSpeed;
    public Renderer backgroundRenderer;
    
    private Player player;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(!player.isDead)
            backgroundRenderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
    }
}
