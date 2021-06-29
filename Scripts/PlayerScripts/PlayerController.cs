using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;


public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 15f;

    [SerializeField]
    private float runSpeed = 100f;

    private PlayerMotor motor;

    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private PostProcessingProfile postProcessing;

    public int characterType;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Material darkstorm;

    [SerializeField]
    private Color skyColor;

    [SerializeField]
    private SonarFx fx;

    [SerializeField]
    private MeshRenderer flashlight;

    [SerializeField]
    private Light flashSpotLight;

    [SerializeField]
    private GameObject flashlightObject;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private Slider staminaSlider;

    private float maxStamina = 5;

    private float currentStamina = 5;

    private float staminaBurnRate;

    private float staminaRegenRate;

    private bool canRun;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private TextMeshProUGUI counter;

    [SerializeField]
    private AudioSource walkingFootSteps;

    [SerializeField]
    private AudioSource runningFootSteps;

    private float savedAudio;




    void Start()
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        staminaBurnRate = 1;
        staminaRegenRate = 0.75f;
        canRun = true;

        motor = GetComponent<PlayerMotor>();
        if (!GameMaster.GetGameMaster().GetMultiplayerStatus())
        {
           characterType = GameMaster.GetGameMaster().players[0].GetSelectedCharacter();
        }
        else
        {
            MenuPlayerInformation playerInfo = new MenuPlayerInformation();
            playerInfo.SetName("SteamName");
            playerInfo.SetCharacterSelected(characterType);
            playerInfo.SetCharacterIndex(1);
            GameMaster.GetGameMaster().players.Add(playerInfo);
        }
        cam = GetComponentInChildren<Camera>();
        walkingFootSteps.loop = true;
        runningFootSteps.loop = true;

        audioMixer.GetFloat("volumne", out savedAudio);
        switch (characterType)
        {
            case 0:
                flashlightObject = cam.gameObject.transform.GetChild(0).gameObject;
                flashlightObject.SetActive(false);
                break;
            case 1:
                RenderSettings.skybox = darkstorm;
                RenderSettings.ambientSkyColor = skyColor;
                DynamicGI.UpdateEnvironment();
                audioMixer.SetFloat("volume", 0);
                break;
            case 2:
                RenderSettings.skybox = darkstorm;
                DynamicGI.UpdateEnvironment();
                break;
            default:
                RenderSettings.skybox = darkstorm;
                RenderSettings.ambientSkyColor = skyColor;
                RenderSettings.fog = enabled;
                DynamicGI.UpdateEnvironment();
                audioMixer.SetFloat("volume", 0);
                break;

        }

        
    }

    void Update()
    {
        if (fx != null && fx.enabled == true)
        {
            fx.enabled = false;
        }

        if (GameMaster.GetGameMaster().GetPaused())
        {
            canvas.enabled = false;
        }
        else if (!canvas.enabled)
        {
            canvas.enabled = true;
        }

        if (characterType == 1 || GameMaster.GetGameMaster().GetPaused())
        {
            audioMixer.SetFloat("volume", -80);
        }
        else
        {
            audioMixer.SetFloat("volume", savedAudio);
        }


        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMovement;
        Vector3 moveVertical = transform.forward * zMovement;

        Vector3 velocity;

        if (Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            velocity = (moveHorizontal + moveVertical).normalized * runSpeed;
            if (velocity != Vector3.zero)
            {
                staminaSlider.value -= Time.deltaTime / staminaBurnRate;
                if (!runningFootSteps.isPlaying)
                {
                    walkingFootSteps.Stop();
                    runningFootSteps.Play();
                }
                
            }
        }
        else
        {
            velocity = (moveHorizontal + moveVertical).normalized * speed;
            if (velocity != Vector3.zero)
            {
                if (!walkingFootSteps.isPlaying)
                {
                    walkingFootSteps.Play();
                    runningFootSteps.Stop();
                }
                
            }
            else
            {
                walkingFootSteps.Stop();
                runningFootSteps.Stop();
            }
            staminaSlider.value += Time.deltaTime / staminaRegenRate;
        }
        if (staminaSlider.value >= maxStamina)
        {
            staminaSlider.value = maxStamina;
            canRun = true;
        }
        else if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            canRun = false;
        }
        

        motor.Move(velocity);

        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        motor.Rotate(rotation);

        float xRot = Input.GetAxisRaw("Mouse Y");

        float cameraRot = xRot * lookSensitivity;

        motor.RotateCamera(cameraRot);



        int count = GameMaster.GetGameMaster().collectedCount;
        counter.SetText("Relics Collected: " + count + "/7");
    }

    public int getCharacterType()
    {
        return characterType;
    }




}
