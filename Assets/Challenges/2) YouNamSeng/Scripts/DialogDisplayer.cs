using System.Collections;
using TMPro;
using UnityEngine;
using ChallengeKit.GamePlay;
using System.Collections.Generic;

namespace Challenge.YouNamSeng
{
    public class DialogDisplayer : MonoBehaviour, IDialogDisplayable
    {
        [SerializeField]
        private CharacterDisplayer characterDisplayer;

        [SerializeField]
        private TextMeshProUGUI headerDisplay;

        [SerializeField]
        private TextMeshProUGUI sentencesDisplay;

        [SerializeField]
        private DialogSystem dialog;

        public class YouNamSengData : DialogSystem.Data
        {
            public string speaker;
            public string headerAlignment;
            public string emotion;
        }

        private void Start()
        {
            dialog.Init(new DialogParser(), this);
            dialog.ParseCSVData<YouNamSengData>("Sample");
            dialog.StartDialog();
        }

        public void AppendLetterToDisplayText(char Letter)
        {
            sentencesDisplay.text += Letter;
        }

        public void SetDisplayDialogByData(DialogSystem.Data DialogData)
        {
            YouNamSengData youNamSengData = (YouNamSengData)DialogData;

            headerDisplay.text = youNamSengData.speaker;

            if (youNamSengData.headerAlignment == "left")
            {
                headerDisplay.alignment = TextAlignmentOptions.TopLeft;
            }
            else if (youNamSengData.headerAlignment == "right")
            {
                headerDisplay.alignment = TextAlignmentOptions.TopRight;
            }
            else if (youNamSengData.headerAlignment == "center")
            {
                headerDisplay.alignment = TextAlignmentOptions.Center;
            }

            // 이렇게 되면, 사실 케릭터에 대한 별개 테이블 시트가 존재해서, 그 시트에서 가져오도록 구조 변경이 되어야 한다.
            // 그리고 이렇게만 작성하니, 특정 캐릭터에 대한 리엑션이 늦게 된다. 
            // 어떤 대사를 하게 되면, 다른 캐릭터도 즉시나 혹은 일정 시간후에 리엑션이 있어야 하는데
            // 이렇게 되면 대사를 치지 않으면 해당 케릭터가 바로 반응 할 수 없다. ( 물론 대사를 치기만 하면 된다 )
            if (youNamSengData.speaker == "나")
            {
                characterDisplayer.SetEmotionColor(0, youNamSengData.emotion);
            }
            else if (youNamSengData.speaker == "유남생")
            {
                characterDisplayer.SetEmotionColor(1, youNamSengData.emotion);
            }
        }

        public void OnClear()
        {
            sentencesDisplay.text = "";
            headerDisplay.text = "";
        }

        public void OnFinished()
        {
            characterDisplayer.ResetEmotions();
        }

        public void SetSentenceToDisplayText(string Sentences)
        {
            sentencesDisplay.text = Sentences;
        }

        public void DisplaySelectList(List<string> selections)
        {
           //
        }
    }

}
