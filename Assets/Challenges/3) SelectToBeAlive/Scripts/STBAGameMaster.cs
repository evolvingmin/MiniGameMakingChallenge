using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChallengeKit.GamePlay;
using ChallengeKit.Pattern;


namespace Challenge.SelectToBeAlive
{
    public class STBAGameMaster : SystemMono
    {
        [SerializeField]
        private ScenarioSystem scenarioSystem;

        [SerializeField]
        private DialogSystem dialogSystem;

        [SerializeField]
        private STBADialogDisplayer dialogDisplayer;

        // 흠.. 시스템이 N개 이상 되면 이런식으로 활용하지말고, 게임 마스터가 Init을 호출해서 시스템들을 초기화를 하는 방향으로 가자.

        // Start is called before the first frame update
        void Start()
        {
            dialogSystem.Init(new DialogParser(), dialogDisplayer);
            // 여긴 커플링 해도 상관없다만... (하나밖에 안쓰고 프로젝트 활용단이라서)
            // 게임 마스터도 결국 나중엔 ChallengeKit으로 올라 갈 것이고, 그러면 나중에 뭔가 보이곘지만 지금은 그냥 쓰자.
            scenarioSystem.Init(new ScenarioParser());
            scenarioSystem.StartScenarioByRoot();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
