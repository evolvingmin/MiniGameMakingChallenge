using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Challenge.YouNamSeng
{
    public class CharacterDisplayer : MonoBehaviour
    {
        [SerializeField]
        private Image[] charcters;

        [SerializeField]
        private Color normalColor;

        [SerializeField]
        private Color angryColor;

        [SerializeField]
        private Color happyColor;

        public void SetEmotionColor(int index, string emotion)
        {
            
            switch (emotion)
            {
                case "angry":
                    charcters[index].color = angryColor;
                    break;
                case "happy":
                    charcters[index].color = happyColor;
                    break;
                case "normal":
                default:
                    charcters[index].color = normalColor;
                    break;
            }
        }

        public void ResetEmotions()
        {
            foreach (var character in charcters)
            {
                character.color = normalColor;
            }
        }
    }
}

