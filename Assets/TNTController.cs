using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTController : MonoBehaviour
{
    private Player player;
    private UIController uiController;
    public Sprite explosion;
    private bool exploded;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        exploded = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !exploded){
            exploded = true;
            player.isDead = true;
            StartCoroutine("addPlayerVelocity");
            GetComponent<SpriteRenderer>().sprite = explosion;
        }
    }
    
    IEnumerator addPlayerVelocity()
    {
        player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-30, 80), ForceMode2D.Impulse);
        //Wait 1 second
        yield return new WaitForSeconds(1);
        //REmove rigidbody force
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
