using UnityEngine;

public class Apple : MonoBehaviour
{
   

    public GameObject collected;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collider)  
    {
        if(collider.gameObject.tag == "Player")
        {
           

            Destroy(gameObject); 
        }
    }
}