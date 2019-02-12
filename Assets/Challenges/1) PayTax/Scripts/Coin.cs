using System;
using System.Collections;
using System.Collections.Generic;
using ChallengeKit;
using ChallengeKit.BoardSystem;
using UnityEngine;

namespace Challenge.PayTax
{
    public class Coin : MonoBehaviour, IBlockInteractable
    {
        Block block;
        CoinMaker coinMaker;

        [SerializeField]
        private int amount = 100;

        public int Amount
        {
            get
            {
                return amount;
            }
        }

        public void Initialize(CoinMaker coinMaker, Block block, Vector3 unitScale)
        {
            this.block = block;

            block.AttachInteratable(this);

            this.coinMaker = coinMaker;
            transform.localPosition = block.transform.localPosition;
            transform.localScale = unitScale;
        }

        public void UpdateState(BlockState newState)
        {
            if(newState == BlockState.Selected)
            {
                block.DettachInteratable();
                coinMaker.CollectCoin(this);
            }
        }

    }
}
