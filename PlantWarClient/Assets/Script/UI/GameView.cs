using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJson;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GameView : MonoBehaviour {
    public static GameView GetInstance { get; private set; }

    float cardYSpace = 50;
    Color[] colors = {
        new Color(0, 160f/255, 233f/255, 1),
        new Color(234f/255, 104f/255, 162f/255, 1),
        new Color(1, 99f/255, 51f/255, 1),
        new Color(1, 71f/255, 71f/255, 1),
        new Color(126f/255, 107f/255, 90f/255, 1),
        new Color(246f/255, 179f/255, 127f/255, 1),
        new Color(0, 153f/255, 68f/255, 1),
    };
    int curColor;

    //卡槽UI列表
    List<Transform> cardSlotList = new List<Transform>();
    //卡片的数据列表（对应每个卡槽）
    List<List<int>> cardDataList = new List<List<int>>();
    //卡片的UI列表（对应每个卡槽）
    List<List<Transform>> cardUIList = new List<List<Transform>>();
    //卡牌池的UI列表
    List<Transform> cardPoolList = new List<Transform>();
    //卡牌池的卡牌
    List<Transform> cardPoolCards = new List<Transform>();

    //初始化ui
    void initUI() {
    }

    //初始化数据
    void initData() {
        var gameDataStr = PlayerPrefs.GetString("gameData", "0");
        if (gameDataStr != "0") {
            JsonObject gameData = (JsonObject)SimpleJson.SimpleJson.DeserializeObject(gameDataStr);
            JsonArray jarr = (JsonArray)gameData[ "cardData" ];
            //卡牌列表复原
            for (var i = 0; i < 4; i = i + 1) {
                JsonArray jarr1 = (JsonArray)jarr[ i ];
                //其中单个列表的复原
                for (var j = 0; j < jarr1.Count; j = j + 1) {
                   cardDataList[ i ].Add(int.Parse(jarr1[ j ].ToString()));
                }
            }
            //卡排池的卡牌复原
            var poolCard = (JsonArray)gameData[ "pool" ];
        }
        else {
            //初始化各类储存
            cardUIList.Clear();
            for (var i = 0; i < 4; i = i + 1) {
                if (cardUIList.Count > i) {
                    cardUIList[ i ].Clear();
                }
                else {
                    cardUIList.Add(new List<Transform>());
                }
            }
        }
       ResManager.GetInstance.loadPrefab("Plan",(prafab)=> {
           Instantiate(prafab, this.transform);
       });

    }

    //获取不相同的随机数
    int[] getRandom(int have, int count, int length) {
        List<int> arr = new List<int>();
        for (var i = 0; i < length; i = i + 1) {
            arr.Add(i);
        }
        if (have < length) {
            //去掉已存在的那个
            var center = arr[ have ];
            arr[ have ] = arr[ arr.Count - 1 ];
            arr[ arr.Count - 1 ] = center;
            arr.RemoveAt(arr.Count - 1);
        }
        //抽取所需要的数量
        List<int> need = new List<int>();
        var needIdx = 0;
        for (int i = 0; i < count; i = i + 1) {
            //随机抽取一个
            var ran = Random.Range(0, arr.Count);
            var center = arr[ ran ];
            arr.RemoveAt(ran);
            need.Add(center);
            needIdx = needIdx + 1;
        }
        return need.ToArray(); ;
    }



    void Awake() {
        //单例初始化
        GetInstance = this;
        initUI();
    }

    void OnEnable()
    {
        //SoundManager.GetInstance.LoadBg();
    }


    void OnDisable() {
    }

    // Use this for initialization
    void Start() {
        cardDataList = new List<List<int>>();
        gameStart();
    }

    //游戏开始
    public void gameStart() {
        initData();
    }
    

    //检测游戏结束
    void checkOver() {
    }
    
    

    public void CollisionEnter(GameObject other, GameObject self) {
        //检测是否有可以放置的位置
        if (other.tag == "CanCollision" && self.tag == "Normal") {
        }
    }
    public void CollisionStay(GameObject other, GameObject self) {

    }
    public void CollisionExit(GameObject other, GameObject self) {
    }


    //设置分数
    void setScore(int val) {
    }


    //点击事件处理
    public void menuClick(int type) {
        SoundManager.GetInstance.PlaySound("btnClick");
        if (type == 0) {
        }
        else if (type == 1) {
        }
        else if (type == 2) {
            
        }
        else if (type == 3) {
            ViewManager.GetInstance.popView("PauseView", true, null);
        }
        else if (type == 4) {
            var guideView = transform.Find("Guide").gameObject;
            guideView.SetActive(false);
        }
    }



    //连击特效
    void flyCombo(Transform card, int num) {
        num = num + 1;
        //if (this.scoreTime > 0) {
        //    num = num * 3;
        //}
        EffectManager.GetInstance.flyText("X " + num, card);
    }


    //储存数据
    void SetItem(string name, object val, int type) {
        switch (type) {
            case 0: {
                PlayerPrefs.SetInt(name, (int)val);
            }
            break;
            case 1: {
                PlayerPrefs.SetFloat(name, (float)val);
            }
            break;
            case 2: {
                PlayerPrefs.SetString(name, (string)val);
            }
            break;
        }
    }

    //获取数据
    object GetItem(string name, int type) {
        switch (type) {
            case 0: {
                return PlayerPrefs.GetInt(name, 0);
            }
            case 1: {
                return PlayerPrefs.GetFloat(name, 0);
            }
            case 2: {
                return PlayerPrefs.GetString(name, "0");
            }
        }
        return 0;
    }

    // Update is called once per frame
    void Update() {

    }

}




