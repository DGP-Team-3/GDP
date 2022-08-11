using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MenuAsset
{
    /*
     * Code by Kristopher Kath
     */


    // Class to be placed onto UI objects to initiate rebind of a key
    public class RebindKeyObject : MonoBehaviour
    {
        [Tooltip("The input action reference.")]
        [SerializeField] private InputActionReference inputAction;
        [Tooltip("The rebind button game object. (Optional)")]
        [SerializeField] private GameObject rebindButton;
        [Tooltip("Object with text for what action is bound to. (Optional)")]
        [SerializeField] private GameObject bindingDisplayNameObject;
        [Tooltip("Waiting for input text display object. (Optional)")]
        [SerializeField] private GameObject waitingForInputObject;

        private PlayerInput playerInput;
        private RebindingDisplay rebindingDisplay;
        private string rebindsKey = "rebinds";

        private void Start()
        {
            rebindingDisplay = FindObjectOfType<RebindingDisplay>();
            if (rebindingDisplay != null)
            {
                //load saved rebinds
                playerInput = rebindingDisplay.GetSelectedPlayerInput(); //set the player input object for loading data

                string rebinds = PlayerPrefs.GetString(rebindsKey, string.Empty);
                if (!string.IsNullOrEmpty(rebinds))
                {
                    playerInput.actions.LoadBindingOverridesFromJson(rebinds);
                }
            }
            else
            {
                Debug.LogError("Please set up a Rebinding Display object for rebinding key object to work!");
            }

            int bindingIndex = inputAction.action.GetBindingIndexForControl(inputAction.action.controls[0]);

            // set bound key text
            bindingDisplayNameObject.GetComponent<Text>().text = InputControlPath.ToHumanReadableString(
                inputAction.action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }

        //Sends info to rebinding display when button pressed
        public void SendRebindingInformation()
        {
            rebindingDisplay.StartRebinding(inputAction, rebindButton, bindingDisplayNameObject, waitingForInputObject, rebindsKey);
        }
    }
}