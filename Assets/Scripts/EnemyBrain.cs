using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] CharProperty estats;
    [SerializeField] private GameObject _enemy_skin;
    [SerializeField] private GameObject P;
    [SerializeField] private Player_Control PlCon;
    [SerializeField] private HPBarPlayer HealthBar;
    [SerializeField] private Enemy_Anim_Control anim_comtroller;
    [SerializeField] private GameObject _bonusPrefab;
    [SerializeField] public bool _withPlayer = false;

    private Rigidbody2D rb;

    private Vector2 moveVector;
    private float dist; 
    private int animstate;
    private int mirror = 1;

    public bool enemie_is_dead = false;

    public void Clear_Space()
    {
        Instantiate(_bonusPrefab, transform.position, Quaternion.identity);
        PlCon.kiledEnemie += 1;
        Destroy(gameObject);
    }
    public void Set_Damage(float Dm)
    {
        if (estats.Health > 0)
        {
            estats.Health -= Dm;
            HealthBar.Change_Health_Bar(estats.Health);
        }
        else
        {
            enemie_is_dead = true;
            animstate = 3;
        }
    }
    public void Set_Idle_Anim()
    {
        animstate = 0;
    }
    public void Attack_Player()
    {
        PlCon.Player_Damage(estats.Damage);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        estats = GetComponent<CharProperty>();
        HealthBar.Change_Max_Health(estats.Health);
        P = GameObject.FindGameObjectWithTag("Player");
        PlCon = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
    }

    void Update()
    {
        if (!enemie_is_dead)
        {
            Vector2 direction = (P.transform.position - this.transform.position).normalized;
            dist = Vector2.Distance(this.transform.position, P.transform.position);

            if (dist < 4 & dist > 0.25 & animstate!=2 & !PlCon.player_is_dead & !enemie_is_dead) {
                moveVector = direction;
                animstate = 1;
            } else {
                moveVector = Vector2.zero;
                animstate = 0;
            }

            _withPlayer = (dist <= 0.5);

            if (_withPlayer & !PlCon.player_is_dead) animstate = 2;
            if ((direction.x != 0 | direction.y != 0))
            {
                if (direction.x > 0) mirror = 1; else mirror = -1;
            }
            _enemy_skin.transform.localScale = new Vector3(mirror, 1.0f, 1.0f);        
        } 
        else moveVector = Vector2.zero;

        rb.velocity = moveVector;
        rb.MovePosition(rb.position + moveVector * estats.Speed * Time.deltaTime);

        anim_comtroller.Change_Animation(animstate);
    }
}
