using UnityEngine;

public enum DeathState
{
    Dead,
    Alive,
    Animating
}

public class Player : MonoBehaviour
{
    [HideInInspector] public RG_Rigidbody RGRB;
    [HideInInspector] public RG_Collider MainCollider;
    private DeathState CurrentDeathState = DeathState.Alive;
    private Vector2Int TouchingGround = Vector2Int.zero;

    public GameObject DeathParticle;

    public Transform CurrentCannon = null;

    public static Player Instance;

    private const float MoveForce = 20;
    private const float MaxMoveSpeed = 6.5f;
    private const float JumpForce = 8.5f;
    private const float WalljumpForceX = 8.5f;
    private const float WalljumpForceY = 6;
    private const float DragForce = 8;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    void Start()
    {
        if (Stage_Player.Instance != null)
        {
            transform.position = Stage_Player.Instance.CheckPointPos;
        }

        RGRB = GetComponent<RG_Rigidbody>();
    }

    void Update()
    {
        RGRB.Gravity_Scale = 1;
        if (CurrentCannon != null)
        {
            transform.position = CurrentCannon.position;
            RGRB.enabled = false;
            MainCollider.enabled = false;
            return;
        }
        else
        {
            RGRB.enabled = true;
            MainCollider.enabled = true;

            Move();
            Jump();
            Drag();
            Collision();
            Death();
        }
    }

    public void KillPlayer()
    {
        if (CurrentDeathState == DeathState.Alive)
        {
            CurrentDeathState = DeathState.Dead;
        }
    }

    private void Death()
    {
        if (CurrentDeathState == DeathState.Dead)
        {
            Instantiate(DeathParticle, transform);
            RGRB.enabled = false;
            MainCollider.enabled = false;
            CurrentDeathState = DeathState.Animating;
        }
        else if (CurrentDeathState == DeathState.Animating)
        {
            RGRB.enabled = false;
            MainCollider.enabled = false;
            if (GameObject.FindGameObjectWithTag("DeathPartical") == null)
            {
                Stage_Player.Instance.Level_Failed();
            }
        }
        else
        {
            RGRB.enabled = true;
            MainCollider.enabled = true;
        }
    }

    private void Jump()
    {
        if (TouchingGround.y < 0 && Input_Manager.Jump_Down())
        {
            RGRB.Velocity.y = JumpForce;
        }
        else if (Input_Manager.Jump_Down() && TouchingGround.x < 0)
        {
            RGRB.Velocity = new Vector2(WalljumpForceX, WalljumpForceY);
        }
        else if (Input_Manager.Jump_Down() && TouchingGround.x > 0)
        {
            RGRB.Velocity = new Vector2(-WalljumpForceX, WalljumpForceY);
        }
    }

    private void Move()
    {
        if (Input_Manager.Move_Axis() != 0)
        {
            transform.localScale = new Vector3(Input_Manager.Move_Axis(), 1, 1);
        }

        if ((RGRB.Velocity.x < MaxMoveSpeed && Input_Manager.Move_Axis() == 1) ||
            (RGRB.Velocity.x > -MaxMoveSpeed && Input_Manager.Move_Axis() == -1))
        {
            RGRB.Velocity += new Vector2(MoveForce * Input_Manager.Move_Axis() * Time.deltaTime, 0);
        }
    }

    private void Drag()
    {
        float Sign = Mathf.Sign(RGRB.Velocity.x);
        RGRB.Velocity -= new Vector2(DragForce * Sign * Time.deltaTime, 0);
        if (Mathf.Sign(RGRB.Velocity.x) != Sign)
        {
            RGRB.Velocity = new Vector2(0, RGRB.Velocity.y);
        }
    }

    private void Collision()
    {
        TouchingGround = Vector2Int.zero;
        foreach (RG_Collision collision in MainCollider.Get_Collisions())
        {
            if (collision.Other_GameObject.tag == "Hazzard")
            {
                KillPlayer();
            }

            if (collision.Other_Collider.gameObject.tag == "Ground" && !collision.Other_Collider.Is_Trigger)
            {
                if (collision.Side.Bottom)
                {
                    TouchingGround.y = -1;
                }
                else if (collision.Side.Left)
                {
                    TouchingGround.x = -1;
                }
                else if (collision.Side.Right)
                {
                    TouchingGround.x = 1;
                }
            }

            if (collision.Side.Top && collision.Side.Bottom && collision.Side.Left &&
                collision.Side.Right && collision.Other_Collider.Is_Trigger == false)
            {
                KillPlayer();
            }

            foreach (RG_Trigger_Overlap overlap in MainCollider.Get_Trigger_Overlaps())
            {
                if (overlap.Other_GameObject.tag == "Hazzard")
                {
                    KillPlayer();
                }
            }
        }
    }
}