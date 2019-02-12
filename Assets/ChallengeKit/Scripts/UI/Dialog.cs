using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChallengeKit.UI
{
    public interface IDialogDisplayable
    {
        // This function will be called before the displaying letters. So setting displayble objects.
        void SetDisplayDialogByData(Dialog.Data DialogData);

        // This function will be called multiple times until hand over all the letters to make complete sentence.
        // ex ) sentencesDisplay.text += letter;
        void AppendLetterToDisplayText(char Letter);

        // During the text displaying, if user call skip events,
        // This function will be called and hand over the complete sentence at once. 
        void SetSentenceToDisplayText(string Sentence);

        // Clear all your display objects for continuing next dialog step.
        void OnClear();

        // This will be called when play all of the current displayScripts.
        void OnFinished();
    }

    public class Dialog : MonoBehaviour
    {
        public class Data
        {
            public string speaker;
            public string sentences;
            public string headerAlignment;
            public string emotion;
            public float speed;
        }

        [SerializeField]
        private string dialogRootPath = "Root/";

        [SerializeField]
        private bool isLoop = false;

        private IDialogDisplayable dialogDisplaybleObject;

        private List<Data> dialogDatas;

        public int DialogLenth { get { return dialogDatas.Count; } }

        private int index = 0;
        private bool isSkip = false;
        private bool isFinished = false;
        private bool isSetDialogDisplayble = false;

        private Coroutine typingCoroutine;

        public Data GetDialogDataAt(int Index)
        {
            return dialogDatas[Index];
        }

        // 여기 데이터 세팅도 csv 세팅에 따라서 달라질 수 있기 때문에 이 부분도 인터페이스 화 해야할 것 같다.
        private void Awake()
        {
            CsvTableHandler.ResourcePath = dialogRootPath;

            CsvTableHandler.Table SampleTable = CsvTableHandler.Get("Sample", CsvTableHandler.StreamMode.Resource);
            dialogDatas = new List<Data>(SampleTable.Length);
            for (int i = 0; i < SampleTable.Length; i++ )
            {
                Data Current = new Data
                {
                    sentences = SampleTable.GetAt(i).Get<string>("sentences"),
                    speaker = SampleTable.GetAt(i).Get<string>("speaker"),
                    headerAlignment = SampleTable.GetAt(i).Get<string>("headerAlignment"),
                    emotion = SampleTable.GetAt(i).Get<string>("emotion"),
                    speed = SampleTable.GetAt(i).Get<float>("speed")
                };
                dialogDatas.Add(Current);
            }
        }

        public Define.Result Init(IDialogDisplayable dialogDisplaybleObject)
        {
            this.dialogDisplaybleObject = dialogDisplaybleObject;

            if (dialogDisplaybleObject == null)
                return Define.Result.NOT_INITIALIZED;

            isSetDialogDisplayble = true;

            // 요건 시점 따로 제어해야할듯.
            StartDialog();

            return Define.Result.OK;
        }

        public void StartDialog()
        {
            dialogDisplaybleObject.OnClear();
            typingCoroutine = StartCoroutine(Typing());
        }

        public IEnumerator Typing()
        {
            if (isSetDialogDisplayble == false)
            {
                Debug.LogWarning("DialogDisplayable was not set");
                yield return null;
            }
                
            Data CurrentData = GetDialogDataAt(index);
            dialogDisplaybleObject.SetDisplayDialogByData(CurrentData);

            foreach (char letter in CurrentData.sentences.ToCharArray())
            {
                dialogDisplaybleObject.AppendLetterToDisplayText(letter);
                yield return new WaitForSeconds(CurrentData.speed);
            }
            isSkip = true;
        }

        public void ContinueDialog()
        {
            if (isSetDialogDisplayble == false)
            {
                Debug.LogWarning("DialogDisplayable was not set");
                return;
            }

            if(isLoop == false && isFinished)
            {
                return;
            }

            if (isSkip)
            {
                isFinished = index >= DialogLenth - 1;
                isSkip = false;
                
                if(isFinished)
                {
                    dialogDisplaybleObject.OnFinished();

                    if (isLoop)
                    {
                        isFinished = false;
                        index = 0;
                        StartDialog();
                    }
                }
                else
                {
                    index++;
                    StartDialog();
                }
            }
            else
            {
                // force ready to skip.
                isSkip = true;
                StopCoroutine(typingCoroutine);
                dialogDisplaybleObject.SetSentenceToDisplayText(GetDialogDataAt(index).sentences);
            }
        }
    }
}
