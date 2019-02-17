using UnityEngine;
using UnityEngine.UI;

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
