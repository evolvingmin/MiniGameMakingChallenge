using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ChallengeKit;
using ChallengeKit.BoardSystem;

namespace Challenge.PayTax
{
    public class GameInfoDisplayer : MonoBehaviour
    {
        private Text textField;



        [SerializeField]
        private GameMaster gameMaster;

        private void Awake()
        {
            textField = GetComponent<Text>();
        }

        private void Update()
        {
            textField.text = gameMaster.ToString();
        }
    }

}
