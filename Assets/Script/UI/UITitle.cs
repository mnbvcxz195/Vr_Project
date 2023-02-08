using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITitle : MonoBehaviour
{
    [SerializeField] Text gameStory;
    [SerializeField] Button bntGameStory;
    int storyCount;

    void Start()
    {
        gameStory.DOText("항상 탐험가를 동경해왔다..", 2f);
        bntGameStory.onClick.AddListener(gameStorynext);
    }

    void Update()
    {
        
    }

    void gameStorynext()
    {
        switch (storyCount)
        {
            case 0:
                TitleText("성장하며 꿈을 쫒다보니 어느세 세상은 나를 최고의 탐험가라 부르기시작했다.");
                storyCount += 1;
                break;
            case 1:
                TitleText("항상 새로운 유적지를 찾으면 제일먼저 답파하기시작했다.");
                storyCount += 1;
                break;
            case 2:
                TitleText("나는 항상 답파의 성공해왔고 이번 여정은 새롭게 발견된 고대유적...");
                storyCount += 1;
                break;
            case 3:
                TitleText("시작은 너무나도 간단하였다 항상 해오던일이였기에");
                storyCount += 1;
                break;
            case 4:
                TitleText("이번에도 쉽게 답파의 성공해 세상의 찬사를 들을예정이였다.");
                storyCount += 1;
                break;
            case 5:
                TitleText("하지만....");
                storyCount += 1;
                break;
            case 6:
                ScenesManager.GetInstance().ChangeSceneTutorial();

                break;

        }
    }
    
    void TitleText(string story)
    {
        gameStory.DOText(story, 2f);
    }
}
