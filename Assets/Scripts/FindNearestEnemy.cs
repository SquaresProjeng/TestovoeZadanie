using UnityEngine;

public class FindNearestEnemy : MonoBehaviour
{
    GameObject[] enemy;
    public GameObject nearest_enemy;
    public int Enemies;
    public float LastDist;


    public void Update_Enemy_List()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemie");
        Enemies = enemy.Length;
    }

    void Start()
    {
        Update_Enemy_List();
    }

    public GameObject FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in enemy)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            bool isLive = go.GetComponent<EnemyBrain>().enemie_is_dead;
            if (curDistance < distance & !isLive)
            {
                nearest_enemy = go;
                distance = curDistance;
                LastDist = distance;
            }
        }
        return nearest_enemy;
    }
}
