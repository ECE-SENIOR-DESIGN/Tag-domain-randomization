using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class main : MonoBehaviour
{
    public Material QRtargetMaterial; // The material whose texture you want to change
    public Material BackgroundtargetMaterial;
    //private string folderPath = "C:/Users/pogoreli/Tag training/Assets/QR/textures/"; // The folder where your QR code textures are stored
    private string QRfolderPath;
    private string BackgroundfolderPath;
    private string CapturedImagesPath;
    private string CapturedCoordinatesPath;

    public GameObject tag;
    public GameObject camera;
    public GameObject light;

    public GameObject LT;
    public GameObject RB;

    private float tagX;
    private float tagY;
    private float tagZ;



    // Start is called before the first frame update
    void Start()
    {
        QRfolderPath = Path.Combine(Application.dataPath, "QR/textures/");
        BackgroundfolderPath = Path.Combine(Application.dataPath, "Background/textures/");
        CapturedImagesPath = Path.Combine(Application.dataPath, "captured/images/");
        CapturedCoordinatesPath = Path.Combine(Application.dataPath, "captured/labels/");

        if (!Directory.Exists(CapturedImagesPath))
        {
            Directory.CreateDirectory(CapturedImagesPath);
        }

        if (!Directory.Exists(CapturedCoordinatesPath))
        {
            Directory.CreateDirectory(CapturedCoordinatesPath);
        }
    }

    // Update is called once per frame
    int goal = 1001;
    int counter = 1;
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ChangeBackgroundTextureRandomly();
        //    randomizeTagPosition();
        //    ChangeQRTextureRandomly();
        //    randomizeCameraPosition();
        //    randomizeLightPosition();
        //    CaptureImage(1);
        //}
        //yield return new WaitForSeconds(2.0f);

        Debug.Log("Image: " + (counter - 1) + " out of: " + (goal - 1));


        ChangeBackgroundTextureRandomly();
        randomizeTagPosition();
        ChangeQRTextureRandomly();
        randomizeCameraPosition();
        randomizeLightPosition();
        //yield return new WaitForSeconds(0.2f);
        CaptureImage(counter++);
        //yield return new WaitForSeconds(0.2f);
        
        if (counter >= goal) {
            Application.Quit();
        }

        
    }

    void ChangeQRTextureRandomly()
    {
        // Generate a random number between 1 and 101 (inclusive)
        int randomIndex = Random.Range(1, 102);

        // Generate the file name based on the random number
        string fileName = "qr" + randomIndex.ToString("D4") + ".png";

        // Full path to the texture file
        string fullPath = QRfolderPath + fileName;

        // Load the texture
        byte[] fileData = System.IO.File.ReadAllBytes(fullPath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Apply the texture to the material
        QRtargetMaterial.mainTexture = texture;
    }

    void ChangeBackgroundTextureRandomly()
    {
        // Generate a random number between 1 and 101 (inclusive)
        int randomIndex = Random.Range(1, 16);

        // Generate the file name based on the random number
        string fileName = "background" + randomIndex.ToString("D4") + ".png";

        // Full path to the texture file
        string fullPath = BackgroundfolderPath + fileName;

        // Load the texture
        byte[] fileData = System.IO.File.ReadAllBytes(fullPath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Apply the texture to the material
        BackgroundtargetMaterial.mainTexture = texture;
    }

    void randomizeTagPosition()
    {
        float newX = Random.Range(-50f, 50f);
        float newY = Random.Range(10f, 180f);
        float newZ = Random.Range(-2f, 5f);

        float newRoationX = Random.Range(-20f, 20f);
        float newRoationY = Random.Range(-20f, 20f);
        float newRoationZ = Random.Range(-50f, 50f);

        tagX = newX;
        tagY = newY;
        tagZ = newZ;

        tag.transform.position = new Vector3(newX, newY, newZ);
        tag.transform.rotation = Quaternion.Euler(newRoationX, newRoationY, newRoationZ);


    }

    void randomizeCameraPosition()
    {
        float newX = tagX + Random.Range(-10f, 10f);
        float newY = tagY + Random.Range(-10f, 10f) + 10;
        float newZ = -255 + Random.Range(-50f, 200f);

        float newRoationX = Random.Range(0f, 10f);
        float newRoationY = Random.Range(0f, 10f);
        float newRoationZ = Random.Range(-2f, 5f);

        camera.transform.position = new Vector3(newX, newY, newZ);
        camera.transform.rotation = Quaternion.Euler(newRoationX, newRoationY, newRoationZ);


    }

    void randomizeLightPosition()
    {
        float newX = Random.Range(-300f, 200f);
        float newY = Random.Range(-300f, 200f);
        float newZ = Random.Range(20f, 200f);

        float newRoationX = Random.Range(-50f, 50f);
        float newRoationY = Random.Range(-50f, 50f);
        float newRoationZ = Random.Range(-50f, 50f);

        float newIntensity = Random.Range(0f, 2f);

        float newColorR = Random.Range(0f, 1f);
        float newColorG = Random.Range(0f, 1f);
        float newColorB = Random.Range(0f, 1f);
        float newAlpha = Random.Range(0f, 1f);

        light.transform.position = new Vector3(newX, newY, newZ);
        light.transform.rotation = Quaternion.Euler(newRoationX, newRoationY, newRoationZ);

        Light lightComponent = light.GetComponent<Light>();
        lightComponent.intensity = newIntensity;
        lightComponent.color = new Color(newColorR, newColorG, newColorB, newAlpha);
    }

    void CaptureImage(int index)
    {
        Camera captureCamera = camera.GetComponent<Camera>();

        int width = 1920;
        int height = 1080;

        // Create a RenderTexture
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        captureCamera.targetTexture = renderTexture;

        // Capture the image
        captureCamera.Render();

        // Read the image into a Texture2D
        RenderTexture.active = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        // Convert to PNG and save
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(CapturedImagesPath + "image" + index.ToString("D4") + ".png", bytes);

        // Get and save object coordinates
        Vector2 lt = RectTransformUtility.WorldToScreenPoint(captureCamera, LT.transform.position);
        Vector2 rb = RectTransformUtility.WorldToScreenPoint(captureCamera, RB.transform.position);

        string coordinates = "1 " + (float)lt.x/(float)width + " " + (float)lt.y/(float)height + " " + (float)rb.x/(float)width + " " + (float)rb.y/(float)height;
        File.WriteAllText(CapturedCoordinatesPath + "coordinates" + index.ToString("D4") + ".txt", coordinates);

        captureCamera.targetTexture = null;

        // Clean up
        Destroy(texture);
        RenderTexture.active = null;
        Destroy(renderTexture);
    }
}
