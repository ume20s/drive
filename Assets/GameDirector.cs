using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // ゲームオブジェクト
    GameObject cat;
    GameObject pointer;

    // ネコスプライト
    public Sprite[] spriteCat = new Sprite[4];

    private int meterAngle;                         // メーター角度
    private int tapAngle;                           // タップ角度

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトの取得
        cat = GameObject.Find("cat");
        pointer = GameObject.Find("pointerpivot");

        // メーター角度の初期化
        meterAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pivot = new Vector2(3.004f, -0.314f);
        Vector2 target;

        if (Input.GetMouseButton(0))
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tapAngle = getAngle(pivot, target);

            Debug.Log(tapAngle + "," + meterAngle);

            if(tapAngle > 0 && tapAngle <235)
            {
                if (tapAngle > meterAngle) meterAngle++;
                if (tapAngle < meterAngle) meterAngle--;
            }

            setMeter(meterAngle);
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
