using ChallengeKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Challenge.MinAvgSliceIndex
{
    // 이런거 만들때, 다음 설정 같은건 다 잡고 가야 한다
    // 한 화면에 보일 블록의 개수는 몇개가 될 것인지 
    // 블록 하나의 크기는 얼마가 되어야 하는지

    // 그런 제약상황 안잡히면 생각이 이리떠돌다 저리 떠돌다 흐지부지화 된다.

    // 흑흑
    public class NumberBlockSystem : MonoBehaviour
    {
        [SerializeField]
        private Vector3 BlockRoot;

        private ResourceManager resourceManager;


        private List<NumberBlock> numberBlocks;

        private float LastPosX, LastPosY;

        public Define.Result Initialize(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
            numberBlocks = new List<NumberBlock>();

            LastPosX = BlockRoot.x;
            LastPosY = BlockRoot.y;

            return Define.Result.OK;
        }

        public Define.Result AddNumberBlock(int Number)
        {
            GameObject created = resourceManager.GetObject<GameObject>("NumberBlock", "Base");
            NumberBlock currentBlock = created.GetComponent<NumberBlock>();
            currentBlock.Initialize(this, Number, LastPosX, LastPosY, BlockRoot.z);
            currentBlock.transform.SetParent(transform);
            numberBlocks.Add(currentBlock);

            LastPosX += LayoutUtility.GetPreferredWidth(currentBlock.GetComponent<RectTransform>());

            return Define.Result.OK;
        }

    }

}
