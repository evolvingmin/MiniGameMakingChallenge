using System.Collections;
using System.Collections.Generic;
using ChallengeKit;
using ChallengeKit.Board;
using UnityEngine;

namespace Challenge.PayTax
{
    public class GameMaster : MonoBehaviour
    {
        [SerializeField]
        BoardManager boardManager;
        [SerializeField]
        ResourceManager resourceManager;
        [SerializeField]
        Banker banker;

        [SerializeField]
        Vector3 uniformCenter;

        [SerializeField]
        Vector3 uniformScale;
        
        // Start is called before the first frame update
        void Start()
        {
            // consider : 이렇게 넘겨주는 것도 템플릿 화 해야 할거 같은데...
            resourceManager.Initialize();
            boardManager.Initialize(resourceManager, uniformCenter, uniformScale);
            banker.Initialize(boardManager, resourceManager, uniformScale);

            banker.GenerateCoin(0, 0);
            banker.GenerateCoin(1, 1);
            banker.GenerateCoin(2, 2);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

