using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.LucidEditor;

namespace Cainos.PixelArtPlatformer_VillageProps
{
    public class Chest : MonoBehaviour, ISaveManager
    {
        [FoldoutGroup("Reference")]
        public Animator animator;
        public GameObject key;
        private bool isPlayerNearby;
        private ItemDrop itemDrop;


        private void Start()
        {
            key.SetActive(false);
            itemDrop = GetComponent<ItemDrop>();

            Invoke("CheckOpenedChest", 0.1f);
         
        }

        private void CheckOpenedChest()
        {
            if (isOpened)
                Open();
        }

        [FoldoutGroup("Runtime"), ShowInInspector, DisableInEditMode]
        public bool IsOpened
        {
            get { return isOpened; }
            set
            {
                isOpened = value;
                animator.SetBool("IsOpened", isOpened);
            }
        }
        private bool isOpened;

        [FoldoutGroup("Runtime"),Button("Open"), HorizontalGroup("Runtime/Button")]
        public void Open()
        {
            IsOpened = true;
        }

        [FoldoutGroup("Runtime"), Button("Close"), HorizontalGroup("Runtime/Button")]
        public void Close()
        {
            IsOpened = false;
        }

        private void Update()
        {
            if (isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !IsOpened)
            {
                Open();
                key.SetActive(false);
                itemDrop.GenerateDrop();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                isPlayerNearby = true;

                if(!isOpened)
                    key.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<Player>() != null)
            {
                isPlayerNearby = false;
                key.SetActive(false);
            }
        }

        public void LoadData(GameData _data)
        {
            if (_data != null)
                this.isOpened = _data.isOpened;
            else
                this.isOpened = false;
        }

        public void SaveData(ref GameData _data)
        {
            _data.isOpened = this.isOpened;
        }
    }
}
