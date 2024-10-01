using RadioSilence.InventorySystem.GameplayComponents;
using RadioSilence.Services.InputServices;
using RadioSilence.UI;
using RadioSilence.UI.InventoryUI.Mediator;
using UnityEngine;

namespace RadioSilence.GameRoot
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        private const string INVENTORY_UI_PREFAB_PATH = "UI/PlayerInventoryUI";

        [SerializeField] private GameObject _sceneRootBinder;

        private InventoryUIMediator _inventoryUI;
        private UISwitcher _uiSwitcher;

        public void Run(Transform canvasTransform)
        {
            Debug.Log("Game scene loaded");

            InitializeGameplayUI(canvasTransform);

            PlayerBackpack inventory = Object.FindFirstObjectByType<PlayerBackpack>();
            UISwitcher switcher = Object.FindFirstObjectByType<UISwitcher>();
            var i = new KeyboardInputService();
            inventory.Inject(i);
            switcher.Inject(i);
        }

        private void InitializeGameplayUI(Transform parent)
        {
            InitializePlayerInventoryUI(parent);

            InitializeUISwitcher(parent, _inventoryUI);
        }

        private void InitializeUISwitcher(Transform parent, InventoryUIMediator inventoryUI)
        {
            _uiSwitcher = parent.gameObject.AddComponent<UISwitcher>();
            _uiSwitcher.Initialize(inventoryUI.gameObject);
        }

        private void InitializePlayerInventoryUI(Transform parent)
        {
            InventoryUIMediator inventoryPrefab = Resources.Load<InventoryUIMediator>(INVENTORY_UI_PREFAB_PATH);
            InventoryUIMediator inventoryUIObject = Instantiate(inventoryPrefab, parent);

            PlayerBackpack inventory = FindFirstObjectByType<PlayerBackpack>();

            inventoryUIObject.InitializeMediator(inventory);
            inventoryUIObject.gameObject.SetActive(false);
            _inventoryUI = inventoryUIObject;
        }
    }
}