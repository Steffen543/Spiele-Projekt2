using UnityEngine;

public class MouseScript : MonoBehaviour {
    // https://www.scirra.com/store/icons-for-game-dev/gui-icons-pack-436-671

    public Texture2D enemyCursorTexture;
    public Texture2D collectableCursorTexture;
    public Texture2D defaultCursorTexture;
   
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    Ray ray;
    RaycastHit hit;
    
    void Update()
    {
        
        Texture2D cursorTextureToSet = defaultCursorTexture;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int lm = LayerMask.GetMask("Default");
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, lm))
        {
            
            var name = hit.collider.tag;
            switch (name)
            {
                case "Enemy":
                    cursorTextureToSet = enemyCursorTexture;
                    break;
                case "CollectableItem":
                    cursorTextureToSet = collectableCursorTexture;
                    break;
            }
           
        }
        Cursor.SetCursor(cursorTextureToSet, hotSpot, cursorMode);
    }


  
}
