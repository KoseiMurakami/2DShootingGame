using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject explosion;
    public GameObject bombExplosion;
    public GameObject itemBomb;
    //クラス型変数を用意して後で取得
    private GameControllerScript gameController;

    private float speed;
    private float phase;
    private float amplitude;
    // Start is called before the first frame update
    void Start()
    {
        phase = Random.Range(0f, Mathf.PI * 2f);
        amplitude = Random.Range(0f, 10f);
        speed = Random.Range(0.5f, 5f);
        //GameContorollerオブジェクトを探す
        gameController = GameObject
            .FindWithTag("GameController")
            .GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(
            (float)Mathf.Sin(Time.frameCount * 0.05f - phase) * Time.deltaTime * amplitude,
            -speed * Time.deltaTime,
            0f
        );
    }

    //このenemyの範囲に入ってきたら。。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            EnemyDied();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("bomb"))
        {
            Instantiate(bombExplosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("blastWave"))
        {
            EnemyDied();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.gameOver();
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void EnemyDied()
    {
        gameController.AddScore(10);
        Destroy(gameObject);
        Instantiate(explosion, transform.position, transform.rotation);
        if(Probability(90) == true)
        {
            Instantiate(itemBomb, transform.position, transform.rotation);
        }
    }

    //probability%の確率でtrueを出す。
    bool Probability(double probability)
    {
        if (Random.Range(0f, 1f) < probability / 100)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
