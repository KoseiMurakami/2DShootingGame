using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    //クラス型変数を用意して後で取得
    private BossScript bossScript;

    //Inspectorからの呼び出し
    public GameObject enemy;
    public Text scoreText;
    public Text bombLevelText;
    public Text replayText;
    public Text bossHpText;

    public double startTime;
    private int score;
    private int bombLevel;
    private bool isGameOver;
    private bool BossDeadFlg;

    //コルーチンで敵を生成
    IEnumerator SpawnEnemy()
    {
        //無限ループで敵を生成
        //Level1
        while (true)
        {
            if (BossDeadFlg == true)
            {
                break;
            }
            Instantiate(
                enemy,
                new Vector3(Random.Range(-8f, 8f), transform.position.y, 0f),
                transform.rotation
            );
            if (Time.time - startTime > 20)
            {
                break;
            }
            yield return new WaitForSeconds(1.0f);
        }

        //Level2
        while (true)
        {
            if (BossDeadFlg == true)
            {
                break;
            }
            Instantiate(
                enemy,
                new Vector3(Random.Range(-8f, 8f), transform.position.y, 0f),
                transform.rotation
            );
            if (Time.time - startTime > 80)
            {
                break;
            }
            //0.1秒だけ処理を待つ
            yield return new WaitForSeconds(0.1f);
        }

        //Level3
        while (true)
        {
            if (BossDeadFlg == true)
            {
                break;
            }
            Instantiate(
                enemy,
                new Vector3(Random.Range(-8f, 8f), transform.position.y, 0f),
                transform.rotation
            );
            //0.1秒だけ処理を待つ
            yield return new WaitForSeconds(0.01f);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        //GameContorollerオブジェクトを探す
        bossScript = GameObject
            .FindWithTag("BossScript")
            .GetComponent<BossScript>();
        BossDeadFlg = false;
        startTime = Time.time;
        StartCoroutine("SpawnEnemy");
        score = 0;
        bombLevel = 0;
        UpdateScoreText();
        replayText.text = "";
        //bossHpText.text = "";
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            return;
        }

        if (Input.GetMouseButton(0) && isGameOver == true)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void AddScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddBombLevel()
    {
        bombLevel += 1;
        UpdateBombLevelText();
    }

    void UpdateBombLevelText()
    {
        bombLevelText.text = "Bomb Level: " + bombLevel;
    }

    public void gameOver()
    {
        replayText.text = "魔法石を買いますか?";
        isGameOver = true;
    }

    public void UpdateBossHpText()
    {
        bossHpText.text = "BOSS HP: " + bossScript.BossHp;
    }

    public void DemolishTheBoss()
    {
        BossDeadFlg = true;
    }
}
