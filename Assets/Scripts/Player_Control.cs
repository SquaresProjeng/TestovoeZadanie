using UnityEngine;
using UnityEngine.UI;

public class Player_Control : MonoBehaviour
{
    [SerializeField] private CharProperty _pstats;  
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private GameObject _g_o_text;
    [SerializeField] private GameObject _player_skin;
    [SerializeField] private HPBarPlayer _healthBar;
    [SerializeField] private Character_Anim_Control _animController;
    [SerializeField] private SpriteRenderer _gun;
    [SerializeField] private Sprite[] _guns;
    [SerializeField] private bool _isScreenInput;
    [SerializeField] Text _sctext;
    [SerializeField] G_Manager _saver;

    public Inventory inventory;
    public int kiledEnemie;
    public bool player_is_dead= false;
    public bool player_can_Shoot = false;

    private Rigidbody2D rb;
    private Vector2 moveVector;
    private int anim_state;
    private int mirror = 1;
    private bool player_attack;

    public void Player_Dead()
    {
        _saver.Save_Game();
        _g_o_text.SetActive(true);
    }

    public void Player_Damage(float dmg)
    {
        if (_pstats.Health>0)
        {
            _pstats.Health -= dmg;
            _healthBar.Change_Health_Bar(_pstats.Health);
        } 
        else player_is_dead = true;
    }

    public void Change_Gun(int gid)
    {
        _pstats.GunId = gid;
        switch (_pstats.GunId)
        {
            case 0:
                _gun.sprite = _guns[0];
                break;
            case 1:
                _gun.sprite = _guns[1];
                break;
            case 2:
                _gun.sprite = _guns[2];
                break;
            default:
                _gun.sprite = _guns[0];
                break;
        }
    }

    public void Player_Panch() 
    {
        if (_pstats.Health > 0) {
            if ((_pstats.GunId>0) & inventory.CheckId(3))
                anim_state = 4;
            else
                anim_state = 3;
            player_attack = true;
        }
    }

    public void Player_Idle()
    {
        anim_state = 0;
        player_attack = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _pstats = GetComponent<CharProperty>();
        inventory = GetComponent<Inventory>();
        _healthBar.Change_Max_Health(_pstats.Health);
        _animController = gameObject.GetComponentInChildren<Character_Anim_Control>();
        _g_o_text.SetActive(false);
        kiledEnemie = _saver.shouldBeKiled;
        Player_Idle();
    }

    void Update()
    { 
 
        if (Input.GetKeyDown(KeyCode.F))
        {
            Player_Panch();
            moveVector = Vector2.zero;
        }
        _sctext.text = "Score = " + kiledEnemie.ToString();

        if (!player_attack)
        {
            anim_state = 0;
            moveVector = new Vector2(0.0f, 0.0f);
            if (_pstats.Health <= 0) {
                anim_state = 2;
                moveVector = Vector2.zero;
                _pstats.Health = 0;
            } else { 
 
                if (_isScreenInput) {
                    moveVector.x = _joystick.Horizontal;  
                    moveVector.y = _joystick.Vertical;
                } else {
                    moveVector.x = Input.GetAxis("Horizontal");
                    moveVector.y = Input.GetAxis("Vertical");
                }
                if (( moveVector.x != 0 | moveVector.y != 0)) {
                    anim_state = 1;
                    if (moveVector.x > 0) mirror = 1; else mirror = -1;
                }
                _player_skin.transform.localScale = new Vector3(mirror, 1.0f, 1.0f);    
            }
        }
        rb.velocity = moveVector;
        rb.MovePosition(rb.position + moveVector * _pstats.Speed * Time.deltaTime);

        _animController.Change_Animation(anim_state);
    }
}
