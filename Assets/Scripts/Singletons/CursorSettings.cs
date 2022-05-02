using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CursorSettings : MonoBehaviour
{
    public bool cursorActive = true;
    public bool buildMode = false;

    public Tilemap groundMap;
    public TileBase groundTile;

    public GameObject tileHighlight;
    Vector2 mousePos;

    private void Start() {
    }

    private void Update() {
        if(cursorActive) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tileHighlight.SetActive(true);
            Cursor.visible = true;

            Vector3 snappedPos = new Vector3(Mathf.Floor(mousePos.x) + .5f, Mathf.Floor(mousePos.y) + .5f, 0);
            tileHighlight.transform.position = snappedPos;

            if (Input.GetMouseButtonDown(0)) {
                RaycastHit2D hit = Physics2D.CircleCast(snappedPos, .25f, Vector2.zero);
                // SetTile(groundMap, null);
                if(hit) {
                    hit.collider.gameObject.GetComponent<Interactable>().Interact();
                    Debug.Log(hit.collider.name);
                }
            }
        } else {
            Cursor.visible = false;
            tileHighlight.SetActive(false);
        }
    }


    public TileBase GetTile(Tilemap map) {
        Vector3Int location = map.WorldToCell(mousePos);
        if(map.GetTile(location)) {
            return map.GetTile(location);
        }
        return null;
    }

    public void SetTile(Tilemap map) {
        Vector3Int location = map.WorldToCell(mousePos);
        map.SetTile(location, groundTile);
    }

    public void SetTile(Tilemap map, TileBase tile) {
        Vector3Int location = map.WorldToCell(mousePos);
        map.SetTile(location, tile);
    }

    public GameObject GetObjectTile(Tilemap map) {
        Vector3Int location = map.WorldToCell(mousePos);
        if(map.GetInstantiatedObject(location)) {
            return map.GetInstantiatedObject(location);
        }
        return null;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(tileHighlight.transform.position, .25f);
    }
}
