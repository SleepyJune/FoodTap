using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Game.CookingScene;

using UnityEngine;
public class BoardManager : MonoBehaviour
{
    public Dictionary<Vector3, Slot> uiSlots;

    public List<Slot> slotList;

    public Transform slotParent;
    public Slot uiSlotPrefab;

    float xMin = 9999, xMax = 0, yMin = 9999, yMax = 0;
    float slotSize = 100;
            
    void Start()
    {
        uiSlots = new Dictionary<Vector3, Slot>();
        
        MakeRandomBoard();
    }

    public void MakeRandomBoard()
    {
        //ClearBoard();

        slotParent.DeleteChildren();

        var gridColumns = 25;
        var gridRows = 25;

        var maxWidth = 9999;
        var maxHeight = 9999;

        //slotSize = uiSlotPrefab.GetComponent<RectTransform>().sizeDelta.x / 2f;


        int xHalf = (int)Math.Floor(gridColumns / 2f);

        for (int row = (int)Math.Round(-gridRows / 2f); row < gridRows; row++)
        {
            for (int column = (int)Math.Round(-gridColumns / 2f); column < gridColumns; column++)
            {
                var hex = new Hex(column, row);

                var worldPos = hex.GetWorldPos(slotSize);

                if (worldPos.x < 0 || worldPos.y < 0 || worldPos.x > maxWidth || worldPos.y > maxHeight)
                {
                    //continue;
                }

                if (worldPos.x * worldPos.x + worldPos.y * worldPos.y > Math.Pow(400,2))
                {
                    continue;
                }

                var newUISlot = Instantiate(uiSlotPrefab, slotParent);
                newUISlot.Initialize(hex, slotSize);

                uiSlots.Add(newUISlot.position, newUISlot);

                SetBoardDimension(newUISlot);
            }
        }
        
        SetNeighbours();
        AdjustBoard();

        slotList = uiSlots.Values.ToList();
    }

    public Slot GetEmptySlot()
    {
        int maxTries = slotList.Count;
        for (int i = 0; i < maxTries; i++)
        {
            int index = UnityEngine.Random.Range(0, slotList.Count);
            var randomSlot = slotList[index];

            if(randomSlot.foodItem == null)
            {
                return randomSlot;
            }            
        }

        for(int i = 0; i < slotList.Count; i++)
        {
            var randomSlot = slotList[i];

            if (randomSlot.foodItem == null)
            {
                return randomSlot;
            }
        }

        return null;
    }

    void SetNeighbours()
    {
        //Add neighbours
        foreach (var slot in uiSlots.Values)
        {
            slot.neighbours = new HashSet<Slot>();

            foreach (var direction in VectorExtensions.directions)
            {
                var pos = slot.position + direction;

                Slot neighbour;
                if (uiSlots.TryGetValue(pos, out neighbour))
                {
                    slot.AddNeighbour(neighbour); //will be checked in add neighbour
                }
            }
        }
    }


    void SetBoardDimension(Slot slot)
    {
        float buffer = slotSize + 40;

        xMin = Math.Min(xMin, slot.transform.localPosition.x - buffer);
        xMax = Math.Max(xMax, slot.transform.localPosition.x + buffer);
        yMin = Math.Min(yMin, slot.transform.localPosition.y - buffer);
        yMax = Math.Max(yMax, slot.transform.localPosition.y + buffer);
    }

    float GetBoardScaling()
    {
        float minScaling = .6f;
        float maxScaling = 99f;

        float maxColSize = xMax - xMin;
        float maxRowSize = yMax - yMin;

        float xScaling = 1080 / (maxColSize);
        float yScaling = 1920 / (maxRowSize);
                
        float boardScaling = Math.Min(xScaling, yScaling);
        boardScaling = Math.Max(minScaling, boardScaling);
        boardScaling = Math.Min(maxScaling, boardScaling);

        return boardScaling;
    }
    public void AdjustBoard()
    {
        //var slotParent = this.slotParent;

        var boardScaling = GetBoardScaling();

        slotParent.localScale = new Vector3(boardScaling, boardScaling, boardScaling);

        float xCenter = xMin + (xMax - xMin) / 2;
        float yCenter = yMin + (yMax - yMin) / 2;

        float xStart = -(xCenter) * boardScaling;
        float yStart = -(yCenter) * boardScaling;


        //450, 900 - 150, 324

        slotParent.localPosition = new Vector3(xStart, yStart + 1920 / 4f, 0);

        //Debug.Log()

    }

    public Slot GetUISlot(Vector3 pos)
    {
        return uiSlots[pos];
    }
}
