using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sekai
{
    public class ScreenManager : MonoBehaviour
    {
        [Serializable]
        private sealed class DialogPrefabEntry
        {
            [SerializeField] private DialogType dialogType;
            [SerializeField] private DialogBase prefab;

            public DialogType DialogType => dialogType;
            public DialogBase Prefab => prefab;
        }

        [Serializable]
        private sealed class LayerRootEntry
        {
            [SerializeField] private ScreenLayerType layerType;
            [SerializeField] private RectTransform root;

            public ScreenLayerType LayerType => layerType;
            public RectTransform Root => root;
        }

        private static ScreenManager instance;

        [SerializeField] private Camera mainUICamera;
        [SerializeField] private int uiCameraPriority = 100;
        [SerializeField] private RectTransform defaultLayerRoot;
        [SerializeField] private List<LayerRootEntry> layerRoots = new List<LayerRootEntry>();
        [SerializeField] private List<DialogPrefabEntry> dialogPrefabs = new List<DialogPrefabEntry>();

        private readonly List<DialogBase> activeDialogs = new List<DialogBase>();
        private Camera baseCamera;

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject rootObject = GameObject.Find("LiveOutUI") ?? GameObject.Find("ScreenManager");
                    if (rootObject != null)
                    {
                        instance = rootObject.GetComponent<ScreenManager>();
                    }
                }

                return instance;
            }
        }

        public DialogBase ActiveDialog => activeDialogs.Count > 0 ? activeDialogs[activeDialogs.Count - 1] : null;
        public Camera BaseCamera => baseCamera;
        public Camera UICamera
        {
            get
            {
                if (mainUICamera != null)
                {
                    return mainUICamera;
                }

                Transform cameraTransform = transform.Find("UICamera");
                if (cameraTransform != null)
                {
                    mainUICamera = cameraTransform.GetComponent<Camera>();
                }

                return mainUICamera;
            }
        }
        public int UICameraPriority => uiCameraPriority;

        private void Awake()
        {
            SetupInstance(this);
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public static void SetupInstance(ScreenManager target)
        {
            instance = target;
        }

        public void SetBaseCamera(Camera camera)
        {
            baseCamera = camera;
        }

        public T ShowMultiButtonDialog<T>(
            DialogType dialogType,
            Dictionary<string, Action> actionDic,
            DialogSize dialogSize,
            ScreenLayerType layerType,
            bool allowCloseExternal)
            where T : CommonMultiButtonDialog
        {
            T dialog = InstantiateDialog<T>(dialogType, layerType);
            if (dialog == null)
            {
                return null;
            }

            dialog.Initialize(actionDic, dialogSize, allowCloseExternal);
            TrackDialog(dialog);
            return dialog;
        }

        public T Show2ButtonDialog<T>(
            DialogType dialogType,
            Action onClickOK,
            Action onClickCancel,
            ScreenLayerType layerType,
            bool allowCloseExternal)
            where T : Common2ButtonDialog
        {
            return Show2ButtonDialog<T>(
                dialogType,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                onClickOK,
                onClickCancel,
                DialogSize.Manual,
                layerType,
                allowCloseExternal);
        }

        public T Show2ButtonDialog<T>(
            DialogType dialogType,
            string titleKey,
            string messageBodyKey,
            string okButtonLabelKey,
            string cancelButtonLabelKey,
            Action onClickOK,
            Action onClickCancel,
            DialogSize dialogSize,
            ScreenLayerType layerType,
            bool allowCloseExternal)
            where T : Common2ButtonDialog
        {
            T dialog = InstantiateDialog<T>(dialogType, layerType);
            if (dialog == null)
            {
                return null;
            }

            dialog.Initialize(
                titleKey,
                messageBodyKey,
                okButtonLabelKey,
                cancelButtonLabelKey,
                onClickOK,
                onClickCancel,
                dialogSize,
                allowCloseExternal);
            TrackDialog(dialog);
            return dialog;
        }

        public void CloseActiveDialog()
        {
            DialogBase dialog = ActiveDialog;
            if (dialog != null)
            {
                dialog.Close();
            }
        }

        public void DestroyAllDialogs()
        {
            for (int i = activeDialogs.Count - 1; i >= 0; i--)
            {
                DialogBase dialog = activeDialogs[i];
                if (dialog != null)
                {
                    Destroy(dialog.gameObject);
                }
            }

            activeDialogs.Clear();
        }

        private T InstantiateDialog<T>(DialogType dialogType, ScreenLayerType layerType) where T : DialogBase
        {
            DialogBase prefab = GetDialogPrefab(dialogType);
            if (prefab == null)
            {
                Debug.LogErrorFormat(this, "Dialog prefab is not registered. type:{0}", dialogType);
                return null;
            }

            T typedPrefab = prefab as T;
            if (typedPrefab == null)
            {
                Debug.LogErrorFormat(this, "Dialog prefab type mismatch. type:{0}, expected:{1}, actual:{2}", dialogType, typeof(T).Name, prefab.GetType().Name);
                return null;
            }

            Transform parent = GetLayerRoot(layerType);
            T dialog = Instantiate(typedPrefab, parent, false);
            FitToParent(dialog.transform as RectTransform);
            return dialog;
        }

        private DialogBase GetDialogPrefab(DialogType dialogType)
        {
            if (dialogPrefabs == null)
            {
                return null;
            }

            for (int i = 0; i < dialogPrefabs.Count; i++)
            {
                DialogPrefabEntry entry = dialogPrefabs[i];
                if (entry != null && entry.DialogType == dialogType)
                {
                    return entry.Prefab;
                }
            }

            return null;
        }

        private Transform GetLayerRoot(ScreenLayerType layerType)
        {
            if (layerRoots != null)
            {
                for (int i = 0; i < layerRoots.Count; i++)
                {
                    LayerRootEntry entry = layerRoots[i];
                    if (entry != null && entry.LayerType == layerType && entry.Root != null)
                    {
                        return entry.Root;
                    }
                }
            }

            if (defaultLayerRoot != null)
            {
                return defaultLayerRoot;
            }

            return transform;
        }

        private void TrackDialog(DialogBase dialog)
        {
            if (dialog == null)
            {
                return;
            }

            activeDialogs.Remove(dialog);
            activeDialogs.Add(dialog);
            dialog.OnClose += () => activeDialogs.Remove(dialog);
        }

        private static void FitToParent(RectTransform rectTransform)
        {
            if (rectTransform == null)
            {
                return;
            }

            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.localScale = Vector3.one;
        }
    }
}
