using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapBehaviour : MonoBehaviour
{
    public Tilemap interactable, interactableNonTrigger;
    private Tile firstClickTile, secondClickTile;
    private bool firstTileTrigger, secondTileTrigger;
    private Vector3Int firstClickPos, secondClickPos;
    private bool _first = false;
    private bool _stop = true;

    private void Start()
    {
        interactable.color = Color.red;
        interactableNonTrigger.color = Color.red;
        firstTileTrigger = true;
        secondTileTrigger = true;
    }

    public void activateChanges()
    {
        _stop = true;
        _first = false;
    }

    public void deactivateChanges()
    {
        _stop = false;
        interactable.GetComponent<TilemapCollider2D>().enabled = false;
        interactable.GetComponent<TilemapCollider2D>().enabled = true;
        interactableNonTrigger.GetComponent<TilemapCollider2D>().enabled = false;
        interactableNonTrigger.GetComponent<TilemapCollider2D>().enabled = true;
        interactable.color = Color.white;
        interactableNonTrigger.color = Color.white;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!_first && _stop)
            {
                Debug.Log("First Click!");
                _first = true;
                firstClickPos = interactable.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                firstClickTile = interactable.GetTile<Tile>(firstClickPos);
                firstTileTrigger = true;
                if(firstClickTile == null)
                {
                    firstClickPos = interactableNonTrigger.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    firstClickTile = interactableNonTrigger.GetTile<Tile>(firstClickPos);
                    firstTileTrigger = false;
                }
                if (firstClickTile == null)
                {
                    _first = false;
                    return;
                }
                if (firstTileTrigger)
                {
                    interactable.SetTileFlags(firstClickPos, TileFlags.None);
                    interactable.SetColor(firstClickPos, Color.green);
                }
                else
                {
                    interactableNonTrigger.SetTileFlags(firstClickPos, TileFlags.None);
                    interactableNonTrigger.SetColor(firstClickPos, Color.green);
                }
            }
            else if(_stop)
            {
                _first = false;
                secondClickPos = interactable.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                secondClickTile = interactable.GetTile<Tile>(secondClickPos);
                secondTileTrigger = true;
                if (secondClickTile == null)
                {
                    secondClickPos = interactableNonTrigger.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    secondClickTile = interactableNonTrigger.GetTile<Tile>(secondClickPos);
                    secondTileTrigger = false;
                }
                if (secondClickTile == null)
                {
                    interactable.SetColor(firstClickPos, Color.white);
                    interactableNonTrigger.SetColor(firstClickPos, Color.white);
                    _first = false;
                    return;
                }
                if (firstTileTrigger && secondTileTrigger)
                {
                    interactable.SetTile(secondClickPos, firstClickTile);
                    interactable.SetTile(firstClickPos, secondClickTile);
                }
                if (firstTileTrigger && !secondTileTrigger)
                {
                    interactableNonTrigger.SetTile(secondClickPos, null);
                    interactable.SetTile(firstClickPos, null);
                    interactable.SetTile(secondClickPos, firstClickTile);
                    interactableNonTrigger.SetTile(firstClickPos, secondClickTile);
                }
                if (!firstTileTrigger && secondTileTrigger)
                {
                    interactableNonTrigger.SetTile(firstClickPos, null);
                    interactable.SetTile(secondClickPos, null);
                    interactable.SetTile(firstClickPos, secondClickTile);
                    interactableNonTrigger.SetTile(secondClickPos, firstClickTile);
                }
                if (!firstTileTrigger && !secondTileTrigger)
                {
                    interactableNonTrigger.SetTile(secondClickPos, firstClickTile);
                    interactableNonTrigger.SetTile(firstClickPos, secondClickTile);
                }
                interactable.SetColor(firstClickPos, Color.white);
                interactable.SetColor(secondClickPos, Color.white);
                interactableNonTrigger.SetColor(firstClickPos, Color.white);
                interactableNonTrigger.SetColor(secondClickPos, Color.white);
            }
        }
    }
}
