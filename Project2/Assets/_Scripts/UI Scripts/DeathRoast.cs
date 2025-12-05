using TMPro;
using UnityEngine;

namespace _Scripts.UI_Scripts
{
    public class DeathRoast : MonoBehaviour
    {
        [Header("UI Element to display the roast")]
        public TMP_Text roastText;

        private string[] _roasts =
        {
            "Wow… you died faster than my framerate.",
            "Skill issue detected.",
            "Even the zombies feel bad for you.",
            "Pro tip: surviving is usually helpful.",
            "Bro tripped over *nothing* and died.",
            "Sponsored by: Respawn™ — You’ll be here a lot.",
            "Your aim called… it wants a refund.",
            "Death speedrun any% incoming.",
            "Did you try… not getting hit?",
            "You just got folded like a lawn chair."
        };

        void OnEnable()
        {
            ShowRandomRoast();
        }

        private void ShowRandomRoast()
        {
            if (roastText == null) return;

            int index = Random.Range(0, _roasts.Length);
            roastText.text = _roasts[index];
        }
    }
}