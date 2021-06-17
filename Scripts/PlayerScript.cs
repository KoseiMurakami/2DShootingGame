using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject rightWeapon;
    //public GameObject leftWeapon;
    public int weaponLevelForBomb;
    public int weaponLevelForMax;

    private GameControllerScript gameController;
    private Vector2 startPoint;
    private Vector2 endPoint;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        weaponLevelForBomb = 0;
        weaponLevelForMax = 10;
        StartCoroutine("Shoot");
        StartCoroutine("BombShoot");
        //GameControllerをScriptの中から探す
        gameController = GameObject
            .FindWithTag("GameController")
            .GetComponent<GameControllerScript>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * 8f;
        float dy = Input.GetAxis("Vertical") * Time.deltaTime * 8f;
        //ボタン押下
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Input.mousePosition;
        }
        //ボタンおしっぱ
        if (Input.GetMouseButton(0))
        {
            endPoint = Input.mousePosition;
            transform.position = new Vector3(
            //自機の動ける範囲を限定
            //Mathf.Clamp(座標, 最小値, 最大値);
            Mathf.Clamp(transform.position.x + (endPoint.x - startPoint.x) * Time.deltaTime * 0.04f, -8f, 8f),
            Mathf.Clamp(transform.position.y + (endPoint.y - startPoint.y) * Time.deltaTime * 0.04f, -4.5f, 4.5f),
            0f
            );
        }
    }

    //並列処理(コルーチン)
    IEnumerator Shoot()
    {
        while(true)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            //Instantiate(leftWeapon, transform.position, transform.rotation);
            //0.2秒ごとに弾を発射する
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator BombShoot()
    {
        while(true)
        {
            while (weaponLevelForBomb == 0)
            {
                yield return new WaitForSeconds(1.0f);
            }
            Instantiate(rightWeapon, transform.position, transform.rotation);
            yield return new WaitForSeconds(1.0f / (float)weaponLevelForBomb);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("itemBomb"))
        {
            gameController.AddBombLevel();
            Destroy(collision.gameObject);
            weaponLevelForBomb++;
        }
    }
}
