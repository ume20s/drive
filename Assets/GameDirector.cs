using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // ゲームオブジェクト
    GameObject cat;
    GameObject pointer;

    // アニメーター
    Animator animator;

    // ネコスプライト
    public Sprite[] spriteCat = new Sprite[4];

    // メーター角度
    private int meterAngle;
    
    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトの取得
        cat = GameObject.Find("cat");
        pointer = GameObject.Find("pointerpivot");

        // アニメーターコンポーネントの取得
        animator = cat.GetComponent<Animator>();

        // メーター角度の初期化
        meterAngle = 0;
        setMeter(meterAngle);
    }

    // Update is called once per frame
    void Update()
    {
        int tapAngle;                           // タップ角度

        Vector2 pivot = new Vector2(3.004f, -0.314f);   // メーター軸
        Vector2 target;                                 // タップした位置

        // タップされていたらもろもろ処理
        if (Input.GetMouseButton(0))
        {
            // タップ位置からメーター角度を取得
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tapAngle = getAngle(pivot, target);

            // メーター範囲内がタップされていたらメーター移動
            if (tapAngle > 0 && tapAngle < 235)
            {
                if (tapAngle > meterAngle) meterAngle++;
                if (tapAngle < meterAngle) meterAngle--;
            }
            setMeter(meterAngle);

            // メーター角度によりアニメ遷移
            if (meterAngle <= 10)
            {
                animator.SetTrigger("to0");

            }
            else if (meterAngle <= 50)
            {
                animator.SetTrigger("to1");
            }
            else if (meterAngle <= 130)
            {
                animator.SetTrigger("to2");
            }
            else if (meterAngle <= 210)
            {
                animator.SetTrigger("to3");
            }
            else
            {
                animator.SetTrigger("to4");
            }
            // Debug.Log(tapAngle + "," + meterAngle);

        }
    }

    int getAngle(Vector2 pivot, Vector2 target)
    {
        Vector2 diff = target - pivot;
        float radian = Mathf.Atan2(diff.y, diff.x);
        int degree = (565 - (int)(radian * Mathf.Rad2Deg)) % 360;

        return degree;
    }

    void setMeter(int angle)
    {
        angle = (745 - angle) % 360;
        pointer.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
