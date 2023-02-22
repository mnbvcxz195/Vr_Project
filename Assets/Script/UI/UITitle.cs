using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UITitle : MonoBehaviour
{
    [SerializeField] Text gameStory;
    [SerializeField] Button bntGameStory;
    [SerializeField] AudioSource sfx;

    int storyCount;

    void Start()
    {
        gameStory.DOText("항상 탐험가를 동경해왔다..", 2f);
        bntGameStory.onClick.AddListener(gameStorynext);
        AudioManager.GetInstance().SfxPlay(sfx, 0);

    }

    void gameStorynext()
    {
        switch (storyCount)
        {
            case 0:
                TitleText("모험과 탐험을 꿈꾸는 어린아이였던 나는 \n항상 사막의 깊은 곳에 숨겨진 고대 파라오와 그들의 보물들의 이야기에 매료되어 있었다.");
                TitleCountUP();
                break;
            case 1:
                TitleText("나는 항상 위대한 탐험가들의 발자취를 따라 유적들의 비밀을 발견하기를 갈망했다.");
                TitleCountUP();
                break;
            case 2:
                TitleText("하지만 시간이흘러 꿈은 흐려지고 나도 그저 꿈을 잃어버린 평범한 사람이 되어가고있었다.");
                TitleCountUP();
                break;
            case 3:
                TitleText("그러던 어는날 고대 문헌들을 훑어보다가\n사하라 사막 깊숙한 곳에 있는 고대 무덤의 위치를 보여주는 지도를 우연히 발견했다.\n지도는 낡고 희미했지만 길을 알려줄 만큼 선명했다.");
                TitleCountUP();
                break;
            case 4:
                TitleText("며칠 동안 나는 작열하는 사막을 여행하며 굳은결심을 하고 지도를 따라갔다.\n 나는 찌는 듯한 더위와 위험한 모래언덕과 싸웠지만 \n남아있던 나의 모험에 대한 열정이 나를 계속 전진시켰다.");
                TitleCountUP();
                break;
            case 5:
                TitleText("마침내, 사막에서 여러 날을 방황한 후, 나는 유적의 입구를 보았다.\n나는 어둠 속으로 내려오면서 흥분과 두려움으로 가득 차 있었다. 공기는 두껍고 곰팡내가 났고\n벽에는 상형문자와 신들의 모습이 줄지어 있었다.");
                TitleCountUP();
                break;
            case 6:
                TitleText("나는 유적속을 더 깊이 탐험하면서. \n탐험가로써의 경외심마져 들고있었다.");
                TitleCountUP();
                break;
            case 7:
                TitleText("유적은 마치 날 손님과도 같이 대접해주는것처럼\n나를 최상층까지 인도하여 주었다.");
                TitleCountUP();
                break;
            case 8:
                TitleText("당연히 나는 이번유적의 탐사를 끝내고 내가 동경하던 탐험가들과같이 위대한 탐험가로 칭송받을생각이였다.");
                TitleCountUP();
                break;
            case 9:
                TitleText("그러나...");
                TitleCountUP();
                break;
            case 10:
                ScenesManager.GetInstance().ChangeScene(Scene.Tutorial);
                break;


        }
    }

    void TitleText(string story)
    {
        gameStory.DOText(story, 2f);
    }
    void TitleCountUP()
    {
        AudioManager.GetInstance().SfxPlay(sfx, 0);
        storyCount += 1;
    }
}
