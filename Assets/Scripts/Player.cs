using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private float speedX;
    [SerializeField] private float forceJumpY;
    [SerializeField] private float health;
    [SerializeField] private float timeVulnerable;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("GROUND CHECK")]
    [SerializeField] private LayerMask maskGround;
    [SerializeField] private Transform positionCheck;
    [SerializeField] private bool isGround;
    private Animator anim;

    private float horizontal;
    private bool isVulnerable = false;
    private GameController _gameController;

    public bool IsVulnerable
    {
        get => isVulnerable;
        set => isVulnerable = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        AnimControl();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(horizontal * speedX * Time.fixedDeltaTime, rig.velocity.y);
        
        if(horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else if(horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void AnimControl()
    {
        if (horizontal == 0  && isGround)
        {
            anim.SetInteger("transition", 0);
        }
        else if ((horizontal > 0 || horizontal < 0) && isGround)
        {
            anim.SetInteger("transition", 1);
        }else if (!isGround)
        {
            anim.SetInteger("transition", 2);
        }
    }

    void Jump()
    {
        
        if(Input.GetButtonDown("Jump") && isGround)
        {
            rig.velocity = Vector2.zero;
            rig.AddForce(forceJumpY * Vector2.up, ForceMode2D.Impulse);
        }
    }


    public void Hit()
    {
        if (!isVulnerable)
        {
            health--;
            _gameController.LoseLife(health);
            isVulnerable = true;
            StartCoroutine(Vulnererable());
        }
    }

    float timeWaitFotSeconds = 0.05f;
    IEnumerator Vulnererable()
    {

        float timer = 0;

        while(timer < timeVulnerable)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(timeWaitFotSeconds);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(timeWaitFotSeconds);
            timer += 0.1f;
        }

        isVulnerable = false;
    }
    void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(positionCheck.position , 0.02f, maskGround);
    } 
}
