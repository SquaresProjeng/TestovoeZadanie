using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    [SerializeField] private float camera_speed = 5.0f; //*----—корость движени€ камеры

    private Transform player; //*---------------------------—сылка на позицию персонажа

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player_head").transform;
    }

    void OnPreRender()
    {
        Vector3 temp = transform.position;
        Vector3 direction = (player.position - transform.position) * camera_speed * Time.deltaTime;
        temp.x = temp.x + direction.x;
        temp.y = temp.y + direction.y;

        transform.position = temp;
    }
}
