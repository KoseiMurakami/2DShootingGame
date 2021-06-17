using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private GameControllerScript gameController;
    public GameObject explosion;
    public GameObject bombExplosion;
    public int BossHp;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject
            .FindWithTag("GameController")
            .GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - gameController.startTime < 150)
        {
            BossHp = 1000000;
            return;
        }
        transform.position += new Vector3(0f, -0.5f * Time.deltaTime - 5f * Mathf.Sin(100 * Time.time) * Time.deltaTime, 0f);
        if (BossHp < 0)
        {
            Instantiate(bombExplosion,
                new Vector3(
                    Random.Range(transform.position.x - 5f, transform.position.x + 5f),
                    Random.Range(transform.position.y - 5f, transform.position.y + 5f),
                    0f),
                transform.rotation);
            Destroy(gameObject, 3f);
            gameController.AddScore(1000);
            gameController.DemolishTheBoss();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.gameOver();
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("bullet"))
        {
            BossDamage(150);
            Instantiate(explosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("bomb"))
        {
            Instantiate(bombExplosion, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("blastWave"))
        {
            BossDamage(1250);
        }
    }

    void BossDamage(int power)
    {
        BossHp -= power;
        gameController.UpdateBossHpText();
    }
}
