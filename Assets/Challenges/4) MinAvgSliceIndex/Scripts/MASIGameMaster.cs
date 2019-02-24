using ChallengeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ChallengeKit.GamePlay;
using ChallengeKit.Pattern;


namespace Challenge.MinAvgSliceIndex
{
    class MASIGameMaster : SystemMono
    {
        [SerializeField]
        private NumberBlockSystem numberBlockSystem;

        [SerializeField]
        private ResourceManager resourceManger;

        private List<int> numbers;

        private int currentRound = 0;

        private int taskIndex = 0;

        [SerializeField]
        private List<int> taskIndice;

        private void Awake()
        {
            Init(new MASIGameMasterParser());
            resourceManger.Initialize();
            numberBlockSystem.Initialize(resourceManger);

            numbers = new List<int>();
 
            AddNumberBlock(UnityEngine.Random.Range(-10000, 10000));

            taskIndex = taskIndice[currentRound];

            MessageSystem.Instance.BroadcastSystems(this, "NextRound", currentRound + 1, taskIndex);
        }

        public void CheckRound(int number)
        {
            AddNumberBlock(number);

            int minAvgSliceIndex = GetMinAvgSliceIndex();

            if(minAvgSliceIndex == -1)
            {
                MessageSystem.Instance.BroadcastSystems(this, "GameOver"); // TO UI
                return;
            }

            if(taskIndex != minAvgSliceIndex)
            {
                MessageSystem.Instance.BroadcastSystems(this, "GameOver"); // TO UI
                return;
            }

            if(currentRound + 1 >= taskIndice.Count)
            {
                MessageSystem.Instance.BroadcastSystems(this, "Win"); // TO UI
                return;
            }

            currentRound++;
            taskIndex = taskIndice[currentRound];

            MessageSystem.Instance.BroadcastSystems(this, "NextRound", currentRound + 1, taskIndex); // TO UI

        }

        private void AddNumberBlock(int number)
        {
            numbers.Add(number);
            numberBlockSystem.AddNumberBlock(number);
        }

        private int GetMinAvgSliceIndex()
        {
            if (numbers.Count < 2)
                return -1;

            float minAMValue = ( numbers[0] + numbers[1] ) / 2;
            int minAvgSliceIndex = 0;

            for (int i = 0; i< numbers.Count; i++)
            {
                if (i + 1 < numbers.Count)
                {
                    float current2SliceMinValue = ( numbers[i] + numbers[i+1] ) / 2;
                    if(current2SliceMinValue < minAMValue)
                    {
                        minAvgSliceIndex = i;
                        minAMValue = current2SliceMinValue;
                    }
                }

                if (i + 2 < numbers.Count)
                {
                    float current3SliceMinValue = ( numbers[i] + numbers[i + 1] + numbers[i + 2] ) / 3;
                    if (current3SliceMinValue < minAMValue)
                    {
                        minAvgSliceIndex = i;
                        minAMValue = current3SliceMinValue;
                    }
                }
            }

            MessageSystem.Instance.BroadcastSystems(this, "UpdateMinAMValue", minAMValue); // TO UI

            return minAvgSliceIndex;
        }

    }

    class MASIGameMasterParser : IParser
    {
        MASIGameMaster gm;

        public Define.Result Init(SystemMono parentSystem)
        {
            gm = (MASIGameMaster)parentSystem;

            if (gm == null)
                return Define.Result.NOT_INITIALIZED;

            return Define.Result.OK;
        }

        public void ParseCommand(string Command, params object[] Objs)
        {
            if (Command == "AddNumberBlock")
            {

                if (gm == null)
                {
                    UnityEngine.Debug.LogWarning("gm is null, Command is : " + Command);
                    return;
                }

                gm.CheckRound((int)Objs[0]);
                
            }
        }
    }


}
