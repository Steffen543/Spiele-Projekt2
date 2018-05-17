using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour {

    public GameObject TextFollowObject;
    public Texture2D HealthBackgroundTexture;

    public Texture2D HealthTexture100;
    public Texture2D HealthTexture80;
    public Texture2D HealthTexture60;
    public Texture2D HealthTexture40;
    public Texture2D HealthTexture20;


    public Texture2D IsFollowedTexture;


    private EnemyScript enemyScript;

    // Use this for initialization
    void Start ()
    {
        enemyScript = GetComponent<EnemyScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        EnemyBar();
    }


    public void IsFollowed()
    {
        var adjustment = 0.2f;
        var myTransform = TextFollowObject.transform;
        var worldPosition = new Vector3(myTransform.position.x, myTransform.position.y + adjustment, myTransform.position.z);
        var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        GUI.DrawTexture(new Rect(screenPosition.x - 20 / 2, Screen.height - screenPosition.y - 30, 20, 20), IsFollowedTexture);
    }


    public void EnemyBar()
    {
        //var labelTop = 20;
        //var labelWidth = 110;
        //var labelHeight = 15;

        var healthBarHeight = 5f;
        var healthBarLength = 100f;
        var healthBarLeft = 100f;
        var healthbarTop = 0;

        var myStyle = new GUIStyle();
        myStyle.fontSize = 12;

        //var playerName = "Stinkendes Monster";
        var adjustment = 0.2f;

        var myTransform = TextFollowObject.transform;
        var worldPosition = new Vector3(myTransform.position.x, myTransform.position.y + adjustment, myTransform.position.z);
        var screenPosition = Camera.main.WorldToScreenPoint(worldPosition);


        var away = Vector3.Distance(Camera.main.transform.position, transform.position);

        healthBarLength = healthBarLength / away * 5;
        healthBarLeft = healthBarLeft / away * 5;


        var healthPercent = enemyScript.HP / enemyScript.MaxHP;
        var healthForegroundLength = healthPercent * healthBarLength;
        Texture healthForegroundTexture = null;

        if (healthPercent <= 0.2)
            healthForegroundTexture = HealthTexture20;
        else if (healthPercent <= 0.4)
            healthForegroundTexture = HealthTexture40;
        else if (healthPercent <= 0.6)
            healthForegroundTexture = HealthTexture60;
        else if (healthPercent <= 0.8)
            healthForegroundTexture = HealthTexture80;
        else
            healthForegroundTexture = HealthTexture100;
 


        GUI.Box(new Rect(screenPosition.x - healthBarLeft / 2 - 1, Screen.height - screenPosition.y - healthbarTop, healthBarLength + 2, healthBarHeight), "");
        GUI.DrawTexture(new Rect(screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - healthbarTop, healthBarLength, healthBarHeight), HealthBackgroundTexture);
        GUI.DrawTexture(new Rect(screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - healthbarTop, healthForegroundLength, healthBarHeight), healthForegroundTexture);
        //GUI.Label(new Rect(screenPosition.x - labelWidth / 2, Screen.height - screenPosition.y - labelTop, labelWidth, labelHeight), playerName, myStyle);
        return;
        /*
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);

        //GUI.Box(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), HP + "/" + MaxHP);
        GUI.backgroundColor = Color.red;
        //GUI.Button(new Rect(targetPos.x, Screen.height - targetPos.y, 60, 20), "Health");
        var width = 60;
        var height = 20;

        GUI.HorizontalScrollbar(new Rect(targetPos.x - width / 2, Screen.height - targetPos.y - 250, width, height), 0, HP, 0, MaxHP);*/
    }
}
