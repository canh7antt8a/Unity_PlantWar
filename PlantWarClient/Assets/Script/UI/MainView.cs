using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView :MonoBehaviour {
    public static MainView GetInstance { get; private set; }

    void Awake() {
        //单例初始化
        GetInstance = this;
    }

    private void OnEnable() {
        InvokeRepeating("RandomStar", -1, 1);
    }

    private void OnDisable() {
        CancelInvoke("RandomStar");
    }

    // Use this for initialization
    void Start() {

    }

    void RandomStar() {
        for (int i = 0; i < 3; i++) {
            StartCoroutine(
               GameApplication.GetInstance.DelayedAction(() => {
                   int idx = Random.Range(0, 3);
                   ResManager.GetInstance.loadPrefab("Star" + idx, (star) => {
                       GameObject starItem = Instantiate((GameObject)star, transform);
                       var canvas = GameApplication.GetInstance.canvas;
                       starItem.transform.position = new Vector3(Random.Range(0f, canvas.sizeDelta.x), Random.Range(0f, canvas.sizeDelta.y), 0);
                       starItem.SetActive(true);
                       StartCoroutine(
                       GameApplication.GetInstance.DelayedAction(() => {
                           Destroy(starItem);
                       }, 1.5f)
                   );
                   });
               }, Random.Range(0, 1f))
            );
        }
    }

    //点击事件处理
    public void menuClick(int type) {
        SoundManager.GetInstance.PlaySound("btnClick");
        if (type == 0) {
            ViewManager.GetInstance.popView("RankView", true, null);
        } else if (type == 1) {
            GameApplication.GetInstance.onShareBtnClick((isOk) => {
            });
        } else if (type == 2) {
            ViewManager.GetInstance.showView("GameView", true, true);
        } else if (type == 3) {

        }
    }


    // Update is called once per frame
    void Update() {

    }
}
