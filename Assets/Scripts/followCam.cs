using System.Collections;
using UnityEngine;

public class followCam : MonoBehaviour
{
        public GameObject player;
    public Vector3 offset = new Vector3(0, 0, -1);


    void FixedUpdate()
    {
        if (player)
        {
            transform.position = new Vector3(
                                             player.transform.position.x + offset.x,
                                             player.transform.position.y + offset.y,
                                             player.transform.position.z + offset.z);
        }
    }
}
