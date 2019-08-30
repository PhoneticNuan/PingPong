using Eccentric.Collections;
using Eccentric.Utils;

using UnityEngine;
public class Paintball : MonoBehaviour, IObjectPoolAble {
    bool bActive;
    CountdownTimer timer;
    PaintballValue value;
    Rigidbody2D rb = null;
    Transform tf;
    public ObjectPool Pool { get; set; }

    private bool bAddForce;

    void Awake ( ) {
        this.tf = this.gameObject.transform;
        this.rb = GetComponent<Rigidbody2D> ( );
        timer = new CountdownTimer (1f);
    }
    //store ref for item's gameObject
    //recycle item to pool
    public void Recycle ( ) {
        bActive = false;
        Pool.RecycleObject (this);
    }
    //action will do when item being push to pool
    public void Init<T> (T data) {
        bActive = true;
        this.value = data as PaintballValue;
        this.tf.position = value.position;
        this.gameObject.tag = value.tag;
        this.bAddForce = true;
        timer.Reset (value.duration);
        Debug.Log(value.force);
        //Debug.Log(rb.velocity);
    }
    void Update ( ) {
        if (bActive) {
            if (timer.IsFinished)
                Recycle ( );
        }

        if (bAddForce)
        {
            bAddForce = false;
            this.rb.AddForce(value.force);
        }
    }
    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Player1" && this.value.tag == "Player2Ball")
            ScoreManager.Instance.AddScore (1);
        else if (other.gameObject.tag == "Player2" && this.value.tag == "Player1Ball")
            ScoreManager.Instance.AddScore (0);
    }

}

[System.Serializable]
public class PaintballValue {
    public string tag;
    public float duration;
    public Vector2 force;
    public Vector2 position;
}
