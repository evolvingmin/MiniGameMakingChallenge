﻿using System.Collections.Generic;
using UnityEngine;

namespace ChallengeKit.Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField]
        private int width = 5;

        [SerializeField]
        private int height = 5;

        [SerializeField]
        private bool useLog = false;
        public bool UseLog { get { return useLog; } }

        private Vector3 blockCenter;
        private Vector3 blockScale;

        private List<List<Block>> blocks;

        private ResourceManager resourceManager;

        private Block selectedBlock;

        [SerializeField]
        private Color[] stateColors;
        public Color[] StateColors
        {
            get
            {
                return stateColors;
            }
        }

        public Define.Result Initialize(ResourceManager resourceManager, Vector3 uniformCenter, Vector3 uniformScale)
        {
            if (width <= 0 || height <= 0)
                return Define.Result.ERROR_DATA_NOT_IN_PROPER_RANGE;

            this.resourceManager = resourceManager;

            blockCenter = uniformCenter;
            blockScale = uniformScale;

            float startPosX = ( blockCenter.x - ( ( ( blockScale.x * width ) / 2.0f ) - ( blockScale.x ) / 2.0f ) );
            float startPosY = ( blockCenter.y + ( ( ( blockScale.y * height ) / 2.0f ) - ( blockScale.y ) / 2.0f ) );

            float currentPosX = startPosX;
            float currentPosY = startPosY;

            blocks = new List<List<Block>>();
            for (int i = 0; i < height; i++)
            {
                blocks.Add(new List<Block>());
                for (int j = 0; j < width; j++)
                {
                    GameObject created = this.resourceManager.GetObject<GameObject>("Block", "Base");
                    Block currentBlock = created.GetComponent<Block>();
                    currentBlock.Initialize(this, new Vector2(j, i), new Vector3(currentPosX, currentPosY, blockCenter.z), blockScale);
                    currentBlock.transform.SetParent(transform);
                    currentPosX += blockScale.x;

                    blocks[i].Add(currentBlock);
                }
                currentPosY -= blockScale.y;
                currentPosX = startPosX;
            }

            return Define.Result.OK;
        }

        public void SetSelected(Block block)
        {
            if(selectedBlock != block)
            {
                if(selectedBlock != null)
                {
                    selectedBlock.State = BlockState.Default;
                }
                selectedBlock = block;
                selectedBlock.State = BlockState.Selected;
            }
        }

        public Define.Result IsInRange(int x, int y)
        {
            bool condition = ( x < width && y < height ) && ( x >= 0 && y >= 0 );

            if (condition == false)
                return Define.Result.ERROR_DATA_NOT_IN_PROPER_RANGE;

            return Define.Result.OK;
        }

        public Block GetBlock(int x, int y)
        {
            if(IsInRange(x,y) == Define.Result.OK)
            {
                return blocks[y][x];
            }

            return null;
        }

        /*
        private void OnDrawGizmosSelected()
        {
            blockCenter = new Vector3(3, 3, 1);
            blockScale = Vector3.zero;

            float startPosX = ( blockCenter.x - (  ( blockScale.x * width ) / 2.0f ));
            float startPosY = ( blockCenter.y + (  ( blockScale.y * height ) / 2.0f ));

            float currentPosX = startPosX;
            float currentPosY = startPosY;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    DrawGizmos.DrawRect2D( new Vector3(currentPosX, currentPosY), blockScale.x, blockScale.y, Color.blue);
                    currentPosX += blockScale.x;
                }

                currentPosY -= blockScale.y;
                currentPosX = startPosX;
            }
        }
        */
    }

}
