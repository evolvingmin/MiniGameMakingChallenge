using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChallengeKit;
using ChallengeKit.Pattern;
using ChallengeKit.GamePlay;
using TMPro;


namespace Challenge.SelectToBeAlive
{
    public class STBADialogSelectListDisplayer : MonoBehaviour
    {
        [SerializeField]
        private RectTransform content;

        [SerializeField]
        private GameObject buttonPrefab;

        private List<Button> buttons;

        private void Awake()
        {
            buttons = new List<Button>();
        }

        public void UpdateUIBySelectList(List<string> selectList)
        {
            int counter = 0;
            foreach (var select in selectList)
            {
                int index = counter++;
                // 이거 오브젝트 풀링으로 해야 하는데... 귀찮으니깐 일단 바로 하자.
                GameObject buttonObject = Instantiate(buttonPrefab);
                Button button = buttonObject.GetComponent<Button>();
                button.GetComponentInChildren<TextMeshProUGUI>().text = select;
                button.onClick.AddListener(delegate () { OnSelectListButtonClick(index); });
                button.transform.SetParent(content);
                button.transform.Reset();
            }
            buttonPrefab.SetActive(false);
        }

        public void OnSelectListButtonClick(int index)
        {
            MessageSystem.Instance.BroadcastSystems(null, "OnSelectionConfirm", index);
        }
      
    }
}

