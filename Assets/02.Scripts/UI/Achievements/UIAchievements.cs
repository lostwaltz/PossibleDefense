using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Achievement
{
    public class UIAchievements : UIBase, IPointerClickHandler
    {
        private AchievementManager _achievementManager;
        private Fader fader;

        private bool isClosed;
        
        [SerializeField] private UIAchievementsSlot uiAchievementsSlot;
        [SerializeField] private Transform content;

        private UIAchievementsSlot[] _uiAchievementsSlotArray;

        private void Awake()
        {
            fader = GetComponent<Fader>();
            _achievementManager = AchievementManager.Instance;
            InitUI();
        }


        public void InitUI()
        {
            _uiAchievementsSlotArray = new UIAchievementsSlot[_achievementManager.AchievementCount];
            
            var achievementDataArray = _achievementManager.GetAchievementData();

            for (var i = 0; i < achievementDataArray.Length; i++)
            {
                UIAchievementsSlot slot = Instantiate(uiAchievementsSlot, content);
                slot.gameObject.SetActive(true);
                slot.UpdateUI(achievementDataArray[i]);
                        
                _uiAchievementsSlotArray[i] = slot;
            }
        }

        private void UpdateUI()
        {
            var achievementDataArray = _achievementManager.GetAchievementData();
            
            for (var i = 0; i < achievementDataArray.Length; i++)
                _uiAchievementsSlotArray[i].UpdateUI(achievementDataArray[i]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
                UpdateUI();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CloseUI();
        }

        public void OpenUI()
        {
            gameObject.SetActive(true);
            fader.FadeTo(0f, 1f, 0.3f);
            UpdateUI();
            isClosed = false;
        }

        private void CloseUI()
        {
            if (true == isClosed) return;

            isClosed = true;
            
            fader.FadeTo(1f, 0f, 0.3f).
                OnComplete(() => SetActive(false));
        }

        private void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }   
}
