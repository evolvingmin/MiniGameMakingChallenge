using ChallengeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Challenge.MinAvgSliceIndex
{
    public class NumberBlock : MonoBehaviour
    {
        private NumberBlockSystem numberBlockSystem;

        private int number;

        private TextMeshPro proField;

        private void Awake()
        {
            proField = GetComponent<TextMeshPro>();
        }


        public void Initialize(NumberBlockSystem numberBlockSystem, int number, float OriginPosX, float OriginPosY, float OriginPosZ)
        {
            this.numberBlockSystem = numberBlockSystem;

            this.number = number;
            proField.text = number.ToString();

            float newLocalPosX = OriginPosX + LayoutUtility.GetPreferredWidth(GetComponent<RectTransform>()) / 2;

            transform.localPosition = new Vector3(newLocalPosX, OriginPosY, OriginPosZ);
        }
    }
}
