using Eccentric.Utils;

using UnityEngine;
public class PlayerShooting : MonoBehaviour {
    public Transform shootPos;
    public float shootCD;
    public float ballDuration;
    [SerializeField]CountdownTimer timer;
    public string shootInput;
    [SerializeField]int nextColor;
    public int NextColor{get{return nextColor;}}
    public PaintballValue ballValue;
    void Start ( ) {
        timer = new CountdownTimer (shootCD);
    }
    void Update ( ) {
        if (timer.IsFinished && Input.GetButtonDown (shootInput)) {
            ballValue.position = this.shootPos.position;
            ballValue.duration = this.ballDuration;
            //計算射出去的力
            ballValue.force = new Vector2 (1000f, 0f);
            ballValue.tag = this.gameObject.tag + "Ball";
            PaintballPool.Instance.balls [nextColor].GetPooledObject (ballValue);
            timer.Reset ( );
            nextColor = Random.Range (0, 3);
        }
    }

}
