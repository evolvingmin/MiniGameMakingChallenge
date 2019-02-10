using System.Collections;
using TMPro;
using UnityEngine;

namespace Challenge.YouNamSeng
{
    public class DialogDisplayer : MonoBehaviour
    {
        [SerializeField]
        private CharacterDisplayer characterDisplayer;

        [SerializeField]
        private TextMeshProUGUI headerDisplay;

        [SerializeField]
        private TextMeshProUGUI sentencesDisplay;
        
        private int index = 0;
        private bool isDone = false;

        private Coroutine typingCoroutine;

        private void Start()
        {
            typingCoroutine = StartCoroutine(Typing());
        }

        IEnumerator Typing()
        {
            DialogManager.DialogData CurrentData = DialogManager.Instance.GetDialogDataAt(index);

            headerDisplay.text = CurrentData.speaker;

            if (CurrentData.headerAlignment == "left")
            {
                headerDisplay.alignment = TextAlignmentOptions.TopLeft;
            }
            else if (CurrentData.headerAlignment == "right")
            {
                headerDisplay.alignment = TextAlignmentOptions.TopRight;
            }
            else if (CurrentData.headerAlignment == "center")
            {
                headerDisplay.alignment = TextAlignmentOptions.Center;
            }

            // 이렇게 되면, 사실 케릭터에 대한 별개 테이블 시트가 존재해서, 그 시트에서 가져오도록 구조 변경이 되어야 한다.
            // 그리고 이렇게만 작성하니, 특정 캐릭터에 대한 리엑션이 늦게 된다. 
            // 어떤 대사를 하게 되면, 다른 캐릭터도 즉시나 혹은 일정 시간후에 리엑션이 있어야 하는데
            // 이렇게 되면 대사를 치지 않으면 해당 케릭터가 바로 반응 할 수 없다. ( 물론 대사를 치기만 하면 된다 )
            if (CurrentData.speaker == "나")
            {
                characterDisplayer.SetEmotionColor(0, CurrentData.emotion);
            }
            else if(CurrentData.speaker == "유남생")
            {
                characterDisplayer.SetEmotionColor(1, CurrentData.emotion);
            }

            foreach (char letter in CurrentData.sentences.ToCharArray())
            {
                sentencesDisplay.text += letter;
                yield return new WaitForSeconds(CurrentData.speed);
            }
            isDone = true;
        }

        public void OnContinuButtonClick()
        { 
            if (isDone)
            {
                bool isSentenceRemaining = index < DialogManager.Instance.DialogLenth - 1;

                isDone = false;
                index = isSentenceRemaining ? index + 1 : 0;
                sentencesDisplay.text = "";
                headerDisplay.text = "";
                typingCoroutine = StartCoroutine(Typing());

                if(isSentenceRemaining == false)
                {
                    characterDisplayer.ResetEmotions();
                }
            }
            else
            {
                isDone = true;
                StopCoroutine(typingCoroutine);
                // 이쯤되면 다이얼로그 매니져를 굳이 만들 필요가 없다는게 보인다. 규모나 사이즈생각하면 걍 몰아도 될듯.
                sentencesDisplay.text = DialogManager.Instance.GetDialogDataAt(index).sentences;
            }

        }

    }

}
