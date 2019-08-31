using Eccentric.Collections;
using Eccentric.Utils;

using UnityEngine;
public class Paintball : MonoBehaviour, IObjectPoolAble
{
    [Header("Sprite")]
    [SerializeField] Sprite player1Sprite;
    [SerializeField] Sprite player2Sprite;
    bool bActive;
    CountdownTimer timer;
    PaintballValue value;
    Rigidbody2D rb = null;
    Transform tf;
    SpriteRenderer sr;
    Collider2D col;
    public ObjectPool Pool { get; set; }

    private bool bAddForce;
    bool bShoot;

    void Awake()
    {
        this.tf = this.gameObject.transform;
        this.rb = GetComponent<Rigidbody2D>();
        this.sr = GetComponent<SpriteRenderer>();
        this.col = GetComponent<Collider2D>();
        timer = new CountdownTimer(1f);
    }
    //store ref for item's gameObject
    //recycle item to pool
    public void Recycle()
    {
        bActive = false;
        bShoot = false;
        this.rb.velocity = Vector2.zero;
        Pool.RecycleObject(this);
    }
    //action will do when item being push to pool
    public void Init<T>(T data)
    {
        bActive = true;
        col.enabled = false;
        this.value = data as PaintballValue;
        this.tf.position = value.position;
        this.gameObject.tag = value.tag;
        this.tf.parent = value.parent;
        bShoot = false;
        SetSprite();
        Debug.Log("Init" + value.force + this.gameObject.GetHashCode());
    }
    public void Shoot(PaintballValue value)
    {
        col.enabled = true;
        bShoot = true;
        timer.Reset(value.duration);
        this.transform.parent = null;
        this.value = value;
        this.bAddForce = true;

    }
    void Update()
    {
        if (bActive && bShoot)
        {
            if (timer.IsFinished)
                Recycle();
        }

        if (bAddForce)
        {
            bAddForce = false;
            this.rb.AddForce(value.force);
            Debug.Log(value.force);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player1" && this.value.tag == "Player2Ball")
            ScoreManager.Instance.AddScore(1);
        else if (other.gameObject.tag == "Player2" && this.value.tag == "Player1Ball")
            ScoreManager.Instance.AddScore(0);
    }

    void SetSprite()
    {
        if (value.tag == "Player1Ball")
        {
            sr.sprite = player1Sprite;
        }
        else
        {
            sr.sprite = player2Sprite;
        }
    }

}

[System.Serializable]
public class PaintballValue
{
    public string tag;
    public float duration;
    public Vector2 force;
    public Vector2 position;
    public Transform parent;
}
