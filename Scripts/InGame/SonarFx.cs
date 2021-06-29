//
// Sonar FX
//
// Copyright (C) 2013, 2014 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class SonarFx : MonoBehaviour
{
    // Sonar mode (directional or spherical)
    public enum SonarMode { Directional, Spherical }
    [SerializeField] SonarMode _mode = SonarMode.Directional;
    public SonarMode mode { get { return _mode; } set { _mode = value; } }

    // Wave direction (used only in the directional mode)
    [SerializeField] Vector3 _direction = Vector3.forward;
    public Vector3 direction { get { return _direction; } set { _direction = value; } }

    // Wave origin (used only in the spherical mode)
    [SerializeField] Vector3 _origin = Vector3.zero;
    public Vector3 origin { get { return _origin; } set { _origin = value; } }

    // Base color (albedo)
    [SerializeField] Color _baseColor = new Color(0.2f, 0.2f, 0.2f, 0);
    public Color baseColor { get { return _baseColor; } set { _baseColor = value; } }

    // Wave color
    [SerializeField] Color _waveColor = new Color(1.0f, 0.2f, 0.2f, 0);
    public Color waveColor { get { return _waveColor; } set { _waveColor = value; } }

    // Wave color
    [SerializeField] Color _waveColor2 = new Color(1.0f, 0.2f, 0.2f, 0);
    public Color waveColor2 { get { return _waveColor2; } set { _waveColor2 = value; } }

    // Wave color
    [SerializeField] Color _waveColor3 = new Color(1.0f, 0.2f, 0.2f, 0);
    public Color waveColor3 { get { return _waveColor3; } set { _waveColor3 = value; } }
    // Wave color
    [SerializeField] Color _waveColor4 = new Color(1.0f, 0.2f, 0.2f, 0);
    public Color waveColor4 { get { return _waveColor4; } set { _waveColor4 = value; } }

    // Wave color amplitude
    [SerializeField] float _waveAmplitude = 2.0f;
    public float waveAmplitude { get { return _waveAmplitude; } set { _waveAmplitude = value; } }

    // Exponent for wave color
    [SerializeField] float _waveExponent = 22.0f;
    public float waveExponent { get { return _waveExponent; } set { _waveExponent = value; } }

    // Interval between waves
    [SerializeField] float _waveInterval = 20.0f;
    public float waveInterval { get { return _waveInterval; } set { _waveInterval = value; } }

    // Wave speed
    [SerializeField] float _waveSpeed = 10.0f;
    public float waveSpeed { get { return _waveSpeed; } set { _waveSpeed = value; } }

    // Additional color (emission)
    [SerializeField] Color _addColor = Color.black;
    public Color addColor { get { return _addColor; } set { _addColor = value; } }

    // Reference to the shader.
    [SerializeField] Shader shader;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private PlayerController controller;

    [SerializeField]
    private Slider visionSlider;

    [SerializeField]
    private GameObject visionBar;

    private bool isPulsating;

    private bool canPulsate;

    private float maxVision = 5;

    [SerializeField]
    private AudioSource pulsate;

    // Private shader variables
    int baseColorID;
    int waveColorID;
    int waveColorI2;
    int waveColorID3;
    int waveColorID4;
    int waveParamsID;
    int waveVectorID;
    int addColorID;

    void Awake()
    {
        baseColorID = Shader.PropertyToID("_SonarBaseColor");
        waveColorID = Shader.PropertyToID("_SonarWaveColor");
        waveColorI2 = Shader.PropertyToID("_SonarWaveColor2");
        waveColorID3 = Shader.PropertyToID("_SonarWaveColor3");
        waveColorID4 = Shader.PropertyToID("_SonarWaveColor4");
        waveParamsID = Shader.PropertyToID("_SonarWaveParams");
        waveVectorID = Shader.PropertyToID("_SonarWaveVector");
        addColorID = Shader.PropertyToID("_SonarAddColor");
    }

    void OnEnable()
    {
        StartCoroutine(setup());
    }

    IEnumerator setup()
    {
        yield return new WaitForSeconds(0.01f);
        if (controller.characterType == 0)
        {
            canPulsate = true;
            isPulsating = false;
            visionBar.SetActive(true);
            visionSlider.maxValue = maxVision;
            visionSlider.value = maxVision;
        }
        else
        {
            visionBar.SetActive(false);
        }
    }


    void OnDisable()
    {
    }

    IEnumerator Pulsate()
    {
        if (controller.characterType == 0)
        {
            isPulsating = true;
            canPulsate = false;
            pulsate.Play();
            GetComponent<Camera>().SetReplacementShader(shader, "MatTag");

            Shader.SetGlobalColor(baseColorID, _baseColor);
            Shader.SetGlobalColor(waveColorID, _waveColor);
            Shader.SetGlobalColor(waveColorI2, _waveColor2);
            Shader.SetGlobalColor(waveColorID3, _waveColor3);
            Shader.SetGlobalColor(waveColorID4, _waveColor4);
            Shader.SetGlobalColor(addColorID, _addColor);

            var param = new Vector4(_waveAmplitude, _waveExponent, _waveInterval, _waveSpeed);
            Shader.SetGlobalVector(waveParamsID, param);

            if (_mode == SonarMode.Directional)
            {
                Shader.DisableKeyword("SONAR_SPHERICAL");
                Shader.SetGlobalVector(waveVectorID, _direction.normalized);
            }
            else
            {
                _origin = playerTransform.localPosition;
                Shader.EnableKeyword("SONAR_SPHERICAL");
                Shader.SetGlobalVector(waveVectorID, _origin);
            }
            yield return new WaitForSeconds(5);
            GetComponent<Camera>().ResetReplacementShader();
            isPulsating = false;
            pulsate.Stop();

        }

    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && canPulsate)
        {
            StartCoroutine(Pulsate());
        }

        if (isPulsating)
        {
           visionSlider.value -= Time.deltaTime;
        }
        else
        {
            visionSlider.value += Time.deltaTime / 0.5f;

        }
        if (visionSlider.value >= maxVision)
        {
            visionSlider.value = maxVision;
            canPulsate = true;
        }

    }
}
