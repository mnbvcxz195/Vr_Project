using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TurotialGuide
{
    public string imgGuide;
    public string txtAbout;
    public string txtAbout2;
    public bool isCheck;

    public TurotialGuide(string imgGuide, string txtAbout, string txtAbout2, bool isCheck)
    {
        this.imgGuide = imgGuide;
        this.txtAbout = txtAbout;
        this.txtAbout2 = txtAbout2;
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

        guides[0] = new TurotialGuide("인벤 열기", "어서오세요. \nB키를 눌러 인벤토리 창을 활성화할 수 있습니다. \n\n인벤토리에 있는 아이템을 클릭하면 ", "해당 아이템을 사용(장착)할 수 있습니다.\n장착한 아이템을 내려놓기: G키", false); //o
        guides[1] = new TurotialGuide("아이템 장착하기", ".", "장착한 아이템을 내려놓기: G키", false); //o
        guides[2] = new TurotialGuide("재료 찾기", "게임 오브젝트와 상호작용해가며 방 안에 숨겨진 재료 아이템을 찾을 수 있습니다.", "아이템 줍기/내려놓기: G키", false); //o
        guides[3] = new TurotialGuide("열쇠 찾기", "문을 열기 위해선 특별한 아이템을 사용해야할 것 같습니다.", "", false); //o
        guides[4] = new TurotialGuide("아이템 획득", "획득한 아이템은 인벤토리 창에서 확인할 수 있습니다.", "", false); //o
        guides[5] = new TurotialGuide("아이템 조합", "아이템 조합 창에 재료 아이템을 올려 새로운 아이템을 제작할 수 있습니다.", "재료 아이템 올리기: 드래그 앤 드롭", false); //o
        guides[6] = new TurotialGuide("열쇠 사용", "제작된 열쇠 아이템을 사용해 문을 열 수 있습니다.", "", false);  //o
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
    public Text txtGuide2;
    public Button btnConfirm;
    GuideList guideList;
    Inventory _Inventory;

    public XRRayInteractor ryInteractor;
    public XRDirectInteractor drInteractor;

    [SerializeField] private Transform inventoryParent;
    [SerializeField] private Transform UIPos;
    [SerializeField] private Transform UIRot;

    void Start()
    {
        btnConfirm.onClick.AddListener(OnConfirm);
        guideList = GuideList.GetInstance();

        _Inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnConfirm()
    {
        objGuide.SetActive(false);
        if (!_Inventory.inventoryActivated)
        {
            ryInteractor.gameObject.SetActive(false);
            drInteractor.gameObject.SetActive(true);
        }
    }

    public void OnTrigger(int num)
    {
        if (guideList.Guides[num].isCheck != true)
        {
            inventoryParent.position = UIPos.position;
            inventoryParent.rotation = UIRot.rotation;

            objGuide.SetActive(true);
            txtGuide.text = $"{guideList.Guides[num].txtAbout}";
            txtGuide2.text = $"{guideList.Guides[num].txtAbout2}";
            guideList.SetCheck(num);
            drInteractor.gameObject.SetActive(false);
            ryInteractor.gameObject.SetActive(true);
        }
    }
}
