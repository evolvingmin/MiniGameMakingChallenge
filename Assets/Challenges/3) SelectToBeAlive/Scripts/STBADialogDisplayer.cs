using System.Collections;
using TMPro;
using UnityEngine;
using ChallengeKit.GamePlay;
using System.Collections.Generic;

namespace Challenge.SelectToBeAlive
{
    public class STBAData : DialogSystem.Data
    {
        public string speaker;
        public string headerAlignment;
    }

    public class STBADialogDisplayer : MonoBehaviour, IDialogDisplayable
    {
        [SerializeField]
        private STBACharacterDisplayer characterDisplayer;

        [SerializeField]
        private STBADialogSelectListDisplayer dialogSelectListDisplayer;

        [SerializeField]
        private TextMeshProUGUI headerDisplay;

        [SerializeField]
        private TextMeshProUGUI sentencesDisplay;

        [SerializeField]
        private DialogSystem dialog;

        private void Start()
        {
            //dialog.Init(this);
            //dialog.ParseCSVData<SelectToBeAliveData>("End");
            //dialog.StartDialog();
        }

        public void AppendLetterToDisplayText(char Letter)
        {
            sentencesDisplay.text += Letter;
        }

        public void SetDisplayDialogByData(DialogSystem.Data DialogData)
        {
            dialogSelectListDisplayer.gameObject.SetActive(false);

            STBAData selectToBeAliveData = (STBAData)DialogData;

            headerDisplay.text = selectToBeAliveData.speaker;

            if (selectToBeAliveData.headerAlignment == "left")
            {
                headerDisplay.alignment = TextAlignmentOptions.TopLeft;
            }
            else if (selectToBeAliveData.headerAlignment == "right")
            {
                headerDisplay.alignment = TextAlignmentOptions.TopRight;
            }
            else if (selectToBeAliveData.headerAlignment == "center")
            {
                headerDisplay.alignment = TextAlignmentOptions.Center;
            }
        }

        public void OnClear()
        {
            sentencesDisplay.text = "";
            headerDisplay.text = "";
        }

        public void OnFinished()
        {
            
        }

        public void SetSentenceToDisplayText(string Sentences)
        {
            sentencesDisplay.text = Sentences;
        }

        public void DisplaySelectList(List<string> selectList)
        {
            dialogSelectListDisplayer.gameObject.SetActive(true);

            dialogSelectListDisplayer.UpdateUIBySelectList(selectList);
        }
    }

}
