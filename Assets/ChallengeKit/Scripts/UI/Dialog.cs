using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
            public string sentences;
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

        // 왜 이 고생을 하실까... 제이슨 파서나 xml 파서 system에서 제공하는 거 있고, 그거 어트리뷰트로 쓰면 될건데...
        // 기억이 안났음... -_-;;;;;;;;;;;;;;;;;;;;;;;;;

        public void ParseCSVData(string TableName, Type ParsingType) // 1) 파싱하고자 하는 타입 데이터를 받고
        { 
            CsvTableHandler.ResourcePath = dialogRootPath; // 이것도 폴더에 따른 계층 구분이 들어간다면... 리펙대상

            CsvTableHandler.Table CSVTable = CsvTableHandler.Get(TableName, CsvTableHandler.StreamMode.Resource);
            dialogDatas = new List<Data>(CSVTable.Length);


            for (int i = 0; i < CSVTable.Length; i++)
            {
                dialogDatas.Add((Data)CSVTable.GetAt(i).CovertToParsedRow(ParsingType));
            }
        }

        public Define.Result Init(IDialogDisplayable dialogDisplaybleObject)
        {
            this.dialogDisplaybleObject = dialogDisplaybleObject;

            if (dialogDisplaybleObject == null)
                return Define.Result.NOT_INITIALIZED;

            isSetDialogDisplayble = true;

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
