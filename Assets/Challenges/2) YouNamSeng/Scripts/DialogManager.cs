using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChallengeKit;
using ChallengeKit.Pattern;

namespace Challenge.YouNamSeng
{
    public class DialogManager : Singleton<DialogManager>
    {
        [SerializeField]
        private string dialogRootPath = "YNSScripts/";

        public class DialogData
        {
            public string speaker;
            public string sentences;
            public string headerAlignment;
            public string emotion;
            public float speed;
        }

        private List<DialogData> dialogDatas;

        public int DialogLenth { get { return dialogDatas.Count; } }

        public DialogData GetDialogDataAt(int Index)
        {
            return dialogDatas[Index];
        }

        private void Awake()
        {
            CsvTableHandler.ResourcePath = dialogRootPath;

            CsvTableHandler.Table SampleTable = CsvTableHandler.Get("Sample", CsvTableHandler.StreamMode.Resource);
            dialogDatas = new List<DialogData>(SampleTable.Length);
            for (int i = 0; i < SampleTable.Length; i++ )
            {
                DialogData Current = new DialogData
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

    }

}
