using Eccentric.Utils;

using UnityEngine;
public class PlayerShooting : MonoBehaviour {
    [Header ("Setting")]
    public Transform shootPos;
    public string shootInput;
    [Header ("Setting OtherClass")]
    [SerializeField] float shootCD;
    [SerializeField] float ballDuration;
    [SerializeField] float shootSpeed;
    [Header ("Monitoring")]

    [SerializeField] PaintballValue ballValue;
    [SerializeField] int nextColor;
    Paintball ball;
    #region ROTATION
    [Header ("Rotate")]
    [SerializeField] float rotateSpeed;
    public float rotateStartValue;
    new AudioSource audio;
    #endregion

    CountdownTimer timer;
    //---props
    public float BallDuration { get { return ballDuration; } set { ballDuration = value; } }
    public float ShootCD { get { return shootCD; } set { shootCD = value; } }
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }
    public int NextColor { get { return nextColor; } }

    public bool canShoot = true;

    void Start ( ) {
        audio = GetComponent<AudioSource>();
        timer = new CountdownTimer (shootCD);
        nextColor = Random.Range (0, 3);
        InitShoot ( );
    }
    void Update ( ) {
        Rotate ( );
        if (timer.IsFinished && Input.GetButtonDown (shootInput)) {
            Shoot ( );
        }
    }

    void Rotate ( ) {
        float tmp = Mathf.PingPong (Time.time * rotateSpeed, 140f) + rotateStartValue;
        this.transform.rotation = Quaternion.Euler (0f, 0f, tmp);
    }

    void Shoot ( ) {
        if (canShoot)
        {
            Vector2 direction = shootPos.position - this.transform.position;
            ballValue.force = direction.normalized * this.shootSpeed;
            ball.Shoot(ballValue);
            InitShoot();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1Ball" || collision.gameObject.tag == "Player2Ball")
            audio.Play();
    }

    //ask for a bullet
    void InitShoot ( ) {
        timer.Reset ( );
        int currentColor = nextColor;
        nextColor = Random.Range (0, 4);
        if (nextColor == 3) nextColor = currentColor;
        ballValue.tag = this.gameObject.tag + "Ball";
        ballValue.position = this.shootPos.position;
        ballValue.duration = this.ballDuration;
        ballValue.parent = this.transform;
        ball = PaintballPool.Instance.balls [nextColor].GetPooledObject (ballValue) as Paintball;
    }

}
