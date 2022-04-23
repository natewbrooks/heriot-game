using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Crop : MonoBehaviour
{

    public CropData cropData;
    public bool harvestable = false;
    public int health = 100;
    public int maxWater;
    public int water;


    private GameObject crop;
    private List<Sprite> stageSprites;
    private GameObject harvestCrop;
    private Sprite rottenSprite;

    private float matureTimeSeconds;
    private float matureTimeCountdown;

    private float waterDecreaseCounter = 15f;
    private float secondsPerStage;
    private float nextStageCountdown;
    private int spriteIndex = 0;

    private bool growthEnabled = true;
    private bool cropSetup = false;
    private bool inHarvestRange = false;

    [Header("Visuals")]
    [SerializeField] private GameObject particles;
    [SerializeField] private TextMeshProUGUI floatingText; 
    private bool showText;


    // Start is called before the first frame update
    private void Start()
    {
        crop = transform.GetChild(0).gameObject;

        if(cropData != null && cropSetup == false) {
            SetupCrop();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(cropData != null && cropSetup == false) {
            SetupCrop();
        }

        if(cropData == null) {
            cropSetup = false;
            harvestable = false;
            growthEnabled = false;
            crop.SetActive(false);
            floatingText.gameObject.SetActive(false);
        } else {
            crop.SetActive(true);
            floatingText.gameObject.SetActive(true);
            if(!harvestable) {
                Growth();
                DiminishWater();
            }
        }

        if(inHarvestRange) {
            if(harvestable && Input.GetKeyDown(KeyCode.E)) {
                Instantiate(harvestCrop, transform.position, Quaternion.identity);
                cropData = null;
            }
        }
    }

    private void DiminishWater() {
        if(waterDecreaseCounter > 0) {
            waterDecreaseCounter -= Time.deltaTime;
        } else {
            if(water == 0) {
                health--;
            } else {
                water--;
            }

            waterDecreaseCounter = 15f;
        }

        if(health == 0) {
            // show particles suggesting its rotten
            // stop maturing
            // set sprite to a rotten variant
            crop.GetComponent<SpriteRenderer>().sprite = rottenSprite;
            particles.SetActive(true);
            floatingText.text = "ROTTEN";
            growthEnabled = false;
        }
    }

    private void Growth() {
        if(growthEnabled) {
            // sets the seconds per stage of crop
            if(secondsPerStage == 0) {
                secondsPerStage = matureTimeSeconds / stageSprites.Count;
                nextStageCountdown = secondsPerStage;
            }
            
            // handles the maturity countdown
            if(matureTimeCountdown > 0) {
                matureTimeCountdown -= Time.deltaTime;
                floatingText.text = Mathf.Floor(matureTimeCountdown / 60) + "m " + Mathf.Floor(matureTimeCountdown % 60) + "s ";
            } else {
                harvestable = true;
                growthEnabled = false;
                floatingText.text = "HARVEST";
            }

            // handles the sprites
            if(nextStageCountdown > 0) {
                nextStageCountdown -= Time.deltaTime;
            } else if (crop.GetComponent<SpriteRenderer>().sprite == stageSprites[stageSprites.Count - 1]) {
                crop.GetComponent<SpriteRenderer>().sprite = stageSprites[spriteIndex];
                nextStageCountdown = 1;
            } else {
                spriteIndex++;
                crop.GetComponent<SpriteRenderer>().sprite = stageSprites[spriteIndex];
                nextStageCountdown = secondsPerStage;
            }
        }
    }

    private void SetupCrop() {
        gameObject.name = cropData.name;
        stageSprites = cropData.stageSprites;
        rottenSprite = cropData.rottenSprite;
        spriteIndex = 0;
        growthEnabled = true;

        maxWater = cropData.water;
        water = maxWater;
        matureTimeSeconds = cropData.matureTimeSeconds;
        matureTimeCountdown = matureTimeSeconds;

        crop.GetComponent<SpriteRenderer>().sprite = stageSprites[spriteIndex];
        harvestCrop = cropData.harvestCrop;

        cropSetup = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player") {
            inHarvestRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "Player") {
            inHarvestRange = false;
        }
    }
}
