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

    public GameObject tag;
    public GameObject camera;



    // Start is called before the first frame update
    void Start()
    {
        QRfolderPath = Path.Combine(Application.dataPath, "QR/textures/");
        BackgroundfolderPath = Path.Combine(Application.dataPath, "Background/textures/");
    }

    private int counter = 0;
    // Update is called once per frame
    void Update()
    {
        //if (counter % 10 == 0) 
        //{
        //    
        //    ChangeBackgroundTextureRandomly();
        //    randomizeTagPosition();
        //    ChangeQRTextureRandomly();
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeBackgroundTextureRandomly();
            randomizeTagPosition();
            ChangeQRTextureRandomly();
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
        float newX = Random.Range(0f, 10f);
        float newY = Random.Range(0f, 10f);
        float newZ = Random.Range(-2f, 5f);

        float newRoationX = Random.Range(-20f, 20f);
        float newRoationY = Random.Range(-20f, 20f);
        float newRoationZ = Random.Range(-50f, 50f);

        tag.transform.position = new Vector3(newX, newY, newZ);
        tag.transform.rotation = Quaternion.Euler(newRoationX, newRoationY, newRoationZ);


    }
}
