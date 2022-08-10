using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MenuAsset
{
    /*
     * Code by Kristopher Kath
     */


    //Class that handles logic for rebinding a key

    public class RebindingDisplay : MonoBehaviour
    {
        [Tooltip("Player Input Component on player object.")]
        [SerializeField] private PlayerInput playerInput;
        [Tooltip("Name of Menu Action Map.")]
        [SerializeField] private string MenuActionMapName;
        [Tooltip("Name of Menu Action Map.")]
        [SerializeField] private string GameplayActionMapName;

        private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

        // Think about making it optional to switch Action Maps. We may not want to go into the gameplay map when still in menu

        public void StartRebinding(InputActionReference inputAction, GameObject rebindButton, GameObject bindingDisplayNameObject, GameObject waitingForInputObject, string rebindsKey)
        {
            if (rebindButton != null)
                rebindButton.SetActive(false);
            if (waitingForInputObject != null)
                waitingForInputObject.SetActive(true);

            playerInput.SwitchCurrentActionMap(MenuActionMapName); // changes action map to the menu action map to enable rebinding.

            // wait for input and execute rebind
            rebindingOperation = inputAction.action.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => RebindComplete(inputAction, rebindButton, bindingDisplayNameObject, waitingForInputObject, rebindsKey))
                .Start();
        }

        private void RebindComplete(InputActionReference inputAction, GameObject rebindButton, GameObject bindingDisplayNameObject, GameObject waitingForInputObject, string rebindsKey)
        {
            UpdateBindUIObject(inputAction, bindingDisplayNameObject); //update the UI text

            // free memory
            rebindingOperation.Dispose();

            if (rebindButton != null)
                rebindButton.SetActive(true);
            if (waitingForInputObject != null)
                waitingForInputObject.SetActive(false);

            SaveRebinds(rebindsKey); // save the rebind

            playerInput.SwitchCurrentActionMap(GameplayActionMapName); // switch back to gameplay action map
        }

        public void UpdateBindUIObject(InputActionReference inputAction, GameObject bindingDisplayNameObject)
        {
            Debug.Log("Changing UI");

            int bindingIndex = inputAction.action.GetBindingIndexForControl(inputAction.action.controls[0]);

            // set bound key text
            if (bindingDisplayNameObject != null)
            {
                bindingDisplayNameObject.GetComponent<Text>().text = InputControlPath.ToHumanReadableString(
                    inputAction.action.bindings[bindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice);
            }
        }

        public PlayerInput GetSelectedPlayerInput()
        {
            return playerInput;
        }


        // Saves rebind data
        public void SaveRebinds(string rebindsKey)
        {
            Debug.Log("Saving rebind data...");
            string rebinds = playerInput.actions.SaveBindingOverridesAsJson();

            PlayerPrefs.SetString(rebindsKey, rebinds);
        }
    }
}