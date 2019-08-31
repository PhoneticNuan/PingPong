using Eccentric.Utils;

using UnityEngine;
public class PlayerShooting : MonoBehaviour
{
    [Header("Setting")]
    public Transform shootPos;
    public string shootInput;
    [Header("Setting OtherClass")]
    [SerializeField] float shootCD;
    [SerializeField] float ballDuration;
    [SerializeField] float shootSpeed;
    [Header("Monitoring")]

    [SerializeField] PaintballValue ballValue;
    [SerializeField] int nextColor;
    Paintball ball;
    #region ROTATION
    [Header("Rotate")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float rotateStartValue;
    #endregion

    CountdownTimer timer;
    //---props
    public float BallDuration { get { return ballDuration; } set { ballDuration = value; } }
    public float ShootCD { get { return shootCD; } set { shootCD = value; } }
    public float ShootSpeed { get { return shootSpeed; } set { shootSpeed = value; } }
    public int NextColor { get { return nextColor; } }
    void Start()
    {
        timer = new CountdownTimer(shootCD);
        InitShoot();
    }
    void Update()
    {
        Rotate();
        if (timer.IsFinished && Input.GetButtonDown(shootInput))
        {
            Shoot();
        }
    }

    void Rotate()
    {
        float tmp = Mathf.PingPong(Time.time * rotateSpeed, 100f) + rotateStartValue;
        this.transform.rotation = Quaternion.Euler(0f, 0f, tmp);
    }

    void Shoot()
    {
        Vector2 direction = shootPos.position - this.transform.position;
        ballValue.force = direction.normalized * this.shootSpeed;
        ball.Shoot(ballValue);
        InitShoot();
    }

    //ask for a bullet
    void InitShoot()
    {
        timer.Reset();
        nextColor = Random.Range(0, 3);
        ballValue.tag = this.gameObject.tag + "Ball";
        ballValue.position = this.shootPos.position;
        ballValue.duration = this.ballDuration;
        ballValue.parent = this.transform;
        ball = PaintballPool.Instance.balls[nextColor].GetPooledObject(ballValue) as Paintball;
    }

}
