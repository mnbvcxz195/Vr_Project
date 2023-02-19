using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurotialGuide
{
    public string imgGuide;
    public string txtAbout;
    public bool isCheck;

    public TurotialGuide(string imgGuide, string txtAbout, bool isCheck)
    {
        this.imgGuide = imgGuide;
        this.txtAbout = txtAbout;
        this.isCheck = isCheck;
    }
}

public class GuideList : MonoBehaviour
{
    #region instance

    private static GuideList instance = null;

    public static GuideList GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@GuideList");
            instance = go.AddComponent<GuideList>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }

    #endregion

    private TurotialGuide[] guides;

    private void Start()
    {
        guides = new TurotialGuide[7];

        guides[0] = new TurotialGuide("인벤 열기", "Any Key를 눌러 인벤토리 창을 활성화할 수 있습니다.", false); //o
        guides[1] = new TurotialGuide("아이템 장착하기", "인벤토리에 있는 아이템을 클릭하면 해당 아이템을 사용(장착)할 수 있습니다", false); //o
        guides[2] = new TurotialGuide("재료 찾기", "게임 오브젝트와 상호작용해가며 방 안에 숨겨진 재료 아이템을 찾을 수 있습니다.", false); //o
        guides[3] = new TurotialGuide("열쇠 찾기", "문을 열기 위해선 특별한 아이템을 사용해야할 것 같습니다.", false); //o
        guides[4] = new TurotialGuide("아이템 획득", "획득한 아이템은 인벤토리 창에서 확인할 수 있습니다.", false); //o
        guides[5] = new TurotialGuide("아이템 조합", "아이템 조합 창에 재료 아이템을 올려 새로운 아이템을 제작할 수 있습니다.", false); //o
        guides[6] = new TurotialGuide("열쇠 사용", "제작된 열쇠 아이템을 사용해 문을 열 수 있습니다.", false); //o
    }

    public TurotialGuide[] Guides
    {
        get
        {
            return guides;
        }
    }

    public void SetCheck(int num)
    {
        guides[num].isCheck = true;
    }
}

public class UITurotialGuide : MonoBehaviour
{
    public GameObject objGuide;
    public Text txtGuide;
    public Button btnConfirm;
    GuideList guideList;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform UIPos;
    [SerializeField] private Transform UIRot;

    void Start()
    {
        btnConfirm.onClick.AddListener(OnConfirm);
        guideList = GuideList.GetInstance();
    }

    public void OnConfirm()
    {
        objGuide.SetActive(false);
    }

    public void OnTrigger(int num)
    {
        if (guideList.Guides[num].isCheck != true)
        {
            inventoryParent.position = UIPos.position;
            inventoryParent.rotation = UIRot.rotation;

            objGuide.SetActive(true);
            txtGuide.text = $"{guideList.Guides[num].txtAbout}";
            guideList.SetCheck(num);
        }
    }
}
