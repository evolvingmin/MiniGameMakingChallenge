using System;
using System.Collections;
using System.Collections.Generic;
using ChallengeKit;
using ChallengeKit.Board;
using UnityEngine;

namespace Challenge.PayTax
{
    public class Coin : MonoBehaviour, IBlockInteractable
    {
        //SpriteRenderer spriteRenderer;

        private void Awake()
        {
            //spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(Vector3 localPosition, Vector3 unitScale)
        {
            transform.localPosition = localPosition;
            transform.localScale = unitScale;
        }

        public void UpdateState(BlockState newState)
        {
            //throw new NotImplementedException();
        }
    }
}
