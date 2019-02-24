using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChallengeKit.GamePlay;
using ChallengeKit.Pattern;
using System;
using ChallengeKit;
using TMPro;

namespace Challenge.MinAvgSliceIndex
{
    public class MASIUXSystem : SystemMono
    {
        [SerializeField]
        private Button addBlockButton;

        [SerializeField]
        private InputField numberInput;

        [SerializeField]
        private Transform panelRoot;

        [SerializeField]
        private TextMeshProUGUI roundField;

        [SerializeField]
        private TextMeshProUGUI taskIndexField;

        [SerializeField]
        private TextMeshProUGUI minAMValueField;


        private void Awake()
        {
            Init(new MASIUXSystemParser());
            numberInput.contentType = InputField.ContentType.DecimalNumber;
            numberInput.placeholder.GetComponent<Text>().text = "-10000~ 10000";
            //numberInput.onValueChanged.AddListener(delegate { OnInputValueChanged(); });

            addBlockButton.onClick.AddListener(delegate
            { OnAddBlockButtonClicked(); });
        }

        /*
        public void OnInputValueChanged()
        {
            
            int decimalValue = Convert.ToInt32(numberInput.text);
            numberInput.text = Convert.ToString(Mathf.Clamp(decimalValue, -1000, 1000));
        }
        */

        public void OnAddBlockButtonClicked()
        {
            int decimalValue = Convert.ToInt32(numberInput.text);
            MessageSystem.Instance.BroadcastSystems(this, "AddNumberBlock", Mathf.Clamp(decimalValue, -10000, 10000));
        }

        public void ToggleUIObject(string childName)
        {
            Transform childTransform = panelRoot.Find(childName);

            if(childTransform == null)
            {
                UnityEngine.Debug.LogWarning("Target UI Object was not found, Name : " + childName);
                return;
            }

            childTransform.gameObject.SetActive(!childTransform.gameObject.activeSelf);
        }

        public void DisableInputs()
        {
            GetComponent<GraphicRaycaster>().enabled = false;
        }

        public void UpdateRound(int round, int TaskIndex)
        {
            roundField.text = round.ToString();
            taskIndexField.text = TaskIndex.ToString();
        }

        public void UpdateMinAVValue(float minAMValue)
        {
            minAMValueField.text = minAMValue.ToString();
        }

        public void OnCodilityWordSelect(string word, int firstCharacterIndex, int length)
        {
           
            if (word != "CodilityMinAvgTwoSlice")
                return;

            if (Input.GetMouseButtonDown(0) == false)
                return;

            Application.OpenURL("https://app.codility.com/programmers/lessons/5-prefix_sums/min_avg_two_slice/");
        }

        
        public void OnCodilityLinkSelect(string LinkID, string LinkText, int LinkIndex)
        {
      
            if (LinkID != "ID_01")
                return;

            Application.OpenURL("https://app.codility.com/programmers/lessons/5-prefix_sums/min_avg_two_slice/");
        }

        public void OnCodilityButtonClick()
        {
            Application.OpenURL("https://app.codility.com/programmers/lessons/5-prefix_sums/min_avg_two_slice/");
        }
    }

    class MASIUXSystemParser : IParser
    {
        MASIUXSystem uxSystem;

        public Define.Result Init(SystemMono parentSystem)
        {
            uxSystem = (MASIUXSystem)parentSystem;

            if (uxSystem == null)
                return Define.Result.NOT_INITIALIZED;

            return Define.Result.OK;
        }

        public void ParseCommand(string Command, params object[] Objs)
        {
            if (uxSystem == null)
            {
                UnityEngine.Debug.LogWarning("gm is null, Command is : " + Command);
                return;
            }

            if (Command == "GameOver")
            {
                uxSystem.DisableInputs();
                uxSystem.ToggleUIObject(Command);
            }
            else if(Command == "Win")
            {
                uxSystem.DisableInputs();
                uxSystem.ToggleUIObject(Command);
            }
            else if(Command == "NextRound")
            {
                uxSystem.UpdateRound((int)Objs[0], (int)Objs[1]);
            }
            else if(Command == "UpdateMinAMValue")
            {
                uxSystem.UpdateMinAVValue((float)Objs[0]);
            }
        }
    }
}
