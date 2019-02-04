using System.Collections;
using System.Collections.Generic;
using ChallengeKit;
using ChallengeKit.Board;
using UnityEngine;

namespace Challenge.PayTax
{
    public class Banker : MonoBehaviour
    {
        private Dictionary<string, GameObject> loadedPrefabs;
        private Vector3 unitScale;

        private BoardManager boardController;
        private ResourceManager resourceManager;

        [SerializeField]
        private string resourceCategory = "Coin";

        [SerializeField]
        private string baseCoin = "base";

        public Define.Result Initialize(BoardManager boardController, ResourceManager resourceManager, Vector3 uniformScale)
        {
            unitScale = uniformScale;
            this.boardController = boardController;
            this.resourceManager = resourceManager;

            return Define.Result.OK;
        }

        public Define.Result GenerateCoin(int x, int y)
        {
            Block block = boardController.GetBlock(x, y);
            GameObject coinObject = resourceManager.GetObject<GameObject>(resourceCategory, baseCoin);
            coinObject.transform.SetParent(transform);
            Coin coin = coinObject.GetComponent<Coin>();
            coin.Initialize(block.transform.localPosition,unitScale);

            return Define.Result.OK;
        }
    }
}

