﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using ChallengeKit;
using ChallengeKit.Pattern;
using ChallengeKit.GamePlay.BoardSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Challenge.PayTax
{
    // 귀찮아서 IMessenger 여기 상속했지만, 분류상 여기하면 안됩니다.
    public class PTGameMaster : Singleton<PTGameMaster>, IMessenger
    {
        [SerializeField]
        private BoardManager boardManager;
        [SerializeField]
        private ResourceManager resourceManager;
        [SerializeField]
        private CoinMaker coinMaker;

        [SerializeField]
        private Vector3 uniformCenter;

        [SerializeField]
        private Vector3 uniformScale;

        private RuleBook ruleBook;

        [SerializeField]
        private Slave player;

        [SerializeField]
        private GameObject endNotifier;

        [SerializeField]
        private Canvas canvas;

        private AudioSource audioSource;

        private float currentTime;
        private float startTime;

        private float remainingPayTime;

        private bool isGameOver = false;
        
        private void Awake()
        {
            ruleBook = GetComponent<RuleBook>();
            audioSource = GetComponent<AudioSource>();
            isGameOver = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            // consider : 이렇게 넘겨주는 것도 템플릿 화 해야 할거 같은데...
            resourceManager.Initialize();
            boardManager.Initialize(resourceManager, uniformCenter, uniformScale);
            coinMaker.Initialize(boardManager, resourceManager, uniformScale);

            currentTime = 0;
            startTime = Time.realtimeSinceStartup;
            remainingPayTime = ruleBook.PayTaxTime;

            StartCoroutine(PayFlow());
            StartCoroutine(CoinFlow());
        }

        private void Update()
        {
            if (isGameOver)
                return;

            currentTime = Time.realtimeSinceStartup - startTime;
            remainingPayTime -= Time.deltaTime;
            remainingPayTime = Mathf.Max(remainingPayTime, 0);
        }

        public void ProcSend(string Command, params object[] Objs)
        {
            ProcReceive(Command, Objs);
        }

        public void ProcReceive(string Command, params object[] Objs)
        {
            if (Command == "CoinCollect")
            {
                //player.Deposit += ruleBook.CoinValue;
                // 위 코드가 맞지만 테스트 용도로.
                player.Deposit += (int)Objs[0];
                audioSource.Play();
            }
        }

        IEnumerator CoinFlow()
        {
            yield return new WaitForSeconds(ruleBook.CoinSpawnTime);

            while (isGameOver == false)
            {
                if(boardManager.IsFull == false)
                {
                    coinMaker.GenerateCoin();
                }

                yield return new WaitForSeconds(ruleBook.CoinSpawnTime);
            }
        }

        IEnumerator PayFlow()
        {
            yield return new WaitForSeconds(ruleBook.PayTaxTime);

            while (player.Deposit >= ruleBook.PayAmout)
            {
                remainingPayTime = ruleBook.PayTaxTime;
                player.Deposit -= ruleBook.PayAmout;
                ruleBook.DoInflation();
                yield return new WaitForSeconds(ruleBook.PayTaxTime);
            }

            GameOver();
        }

        public void GameOver()
        {
            isGameOver = true;
            StopAllCoroutines();
            endNotifier.SetActive(true);
            // 어이쿠 귀찮...
            canvas.GetComponent<GraphicRaycaster>().enabled = true;
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public override string ToString()
        {
            // 현재 시간
            // 다음에 세금을 내야 될 남은 시간.
            // 내 현재 소지 금액
            // 다음에 내가 내야 할 금액
            return string.Format("CurrentTime : [{0:0.00}] \nNext PayTime : [{1:0.00}] \nMy Deposit : [{2}] \nPayAmount : [{3}]", currentTime, remainingPayTime, player.Deposit, ruleBook.PayAmout);
        }

    }
}

