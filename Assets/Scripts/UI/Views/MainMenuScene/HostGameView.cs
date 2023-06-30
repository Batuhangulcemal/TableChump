using System.Collections;
using System.Collections.Generic;
using AsepStudios.Mechanic.GameCore;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace AsepStudios.UI
{
    public class HostGameView : View
    {
        [SerializeField] private Button hostButton;
        [SerializeField] private Button backButton;
        
        [SerializeField] private Button increaseMaxPlayerButton;
        [SerializeField] private Button decreaseMaxPlayerButton;
        [SerializeField] private Button increaseStartingPointButton;
        [SerializeField] private Button decreaseStartingPointButton;
        
        [SerializeField] private TextMeshProUGUI maxPlayerCountText;
        [SerializeField] private TextMeshProUGUI startingPointText;

        private int maxPlayerCount = 6;
        private int startingPoint = 40;
        
        private const int MAX_PLAYER_COUNT_SELECT_INTERVAL = 1;
        private const int STARTING_POINT_SELECT_INTERVAL = 5;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            hostButton.onClick.AddListener(TryConnectAsHost);

            backButton.onClick.AddListener(() =>
            {
                ViewManager.ShowView<SelectJoinOrHostView>();
            });
            
            increaseMaxPlayerButton.onClick.AddListener(() =>
            {
                AddMaxPlayerCount(MAX_PLAYER_COUNT_SELECT_INTERVAL);
            });
            
            decreaseMaxPlayerButton.onClick.AddListener(() =>
            {
                AddMaxPlayerCount(-MAX_PLAYER_COUNT_SELECT_INTERVAL);
            });
            
            increaseStartingPointButton.onClick.AddListener(() =>
            {
                AddStartingPoint(STARTING_POINT_SELECT_INTERVAL);
            });
            
            decreaseStartingPointButton.onClick.AddListener(() =>
            {
                AddStartingPoint(-STARTING_POINT_SELECT_INTERVAL);
            });

            maxPlayerCountText.text = maxPlayerCount.ToString();
            startingPointText.text = startingPoint.ToString();

        }


        private void AddMaxPlayerCount(int value)
        {
            if (maxPlayerCount + value > GameArgsHolder.MaxPlayerCount)
            {
                maxPlayerCount = GameArgsHolder.MaxPlayerCount;
            }
            else if (maxPlayerCount + value < GameArgsHolder.MinPlayerCount)
            {
                maxPlayerCount = GameArgsHolder.MinPlayerCount;
            }
            else
            {
                maxPlayerCount += value;
            }

            ControlMaxPlayerButtons();

            maxPlayerCountText.text = maxPlayerCount.ToString();
        }
        
        private void AddStartingPoint(int value)
        {
            if (startingPoint + value > GameArgsHolder.MaxPlayerStartPoint)
            {
                startingPoint = GameArgsHolder.MaxPlayerStartPoint;
            }
            else if (startingPoint + value < GameArgsHolder.MinPlayerStartPoint)
            {
                startingPoint = GameArgsHolder.MinPlayerStartPoint;
            }
            else
            {
                startingPoint += value;
            }

            ControlStartingPointButtons();
            startingPointText.text = startingPoint.ToString();
        }
        
        private void TryConnectAsHost()
        {

            GameArgsHolder.SetGameArgs(new GameArgs()
            {
                IsArgsInitialized = true,
                MaxPlayerCount = maxPlayerCount,
                PlayerPointStartValue = startingPoint
            });
            
            if (!GameArgsHolder.IsGameArgsInitialized)
            {
                return;
            }
            ConnectionService.ConnectAsHostLocally();
        }

        private void ControlMaxPlayerButtons()
        {
            increaseMaxPlayerButton.gameObject.SetActive(maxPlayerCount != GameArgsHolder.MaxPlayerCount);
            decreaseMaxPlayerButton.gameObject.SetActive(maxPlayerCount != GameArgsHolder.MinPlayerCount);
        }
        
        private void ControlStartingPointButtons()
        {
            increaseStartingPointButton.gameObject.SetActive(startingPoint != GameArgsHolder.MaxPlayerStartPoint);
            decreaseStartingPointButton.gameObject.SetActive(startingPoint != GameArgsHolder.MinPlayerStartPoint);
        }
    }

}

