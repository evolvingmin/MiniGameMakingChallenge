using System;
using System.Collections;
using System.Collections.Generic;
using ChallengeKit;
using ChallengeKit.Board;
using UnityEngine;


namespace Challenge.PayTax
{
    public class RuleBook : MonoBehaviour
    {
        [SerializeField]
        private float coinSpawnTime = 1.0f;

        [SerializeField]
        private float payTaxTime = 10.0f;

        [SerializeField]
        private int coinValue = 10;

        [SerializeField]
        private int initialPayAmount = 100;

        [SerializeField]
        private float inflationValue = 1.05f;

        private int payAmout;

        private int turn = 0;

        public int PayAmout
        {
            get
            {
                return payAmout;
            }
        }

        public float PayTaxTime
        {
            get
            {
                return payTaxTime;
            }
        }

        public float CoinSpawnTime
        {
            get
            {
                return coinSpawnTime;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            payAmout = initialPayAmount;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DoInflation()
        {
            payAmout = (int)(payAmout * inflationValue);
        }
    }
}
