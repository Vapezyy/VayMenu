using UnityEngine;
using System;
using System.IO;
using System.Reflection;

namespace StupidTemplate.UI
{
    public class ShowImage : MonoBehaviour
    {
        private Texture2D imageTexture;
        private float startTime;
        private bool isImageVisible = false;
        private float displayTime = 3.0f; // Czas wyświetlania obrazu w sekundach
        private bool isTextureLoaded = false; // Flaga wskazująca, czy tekstura została załadowana

        private void Start()
        {
            // Jeśli tekstura nie została załadowana wcześniej, załaduj ją
            if (!isTextureLoaded)
            {
                imageTexture = LoadTextureFromResource("StupidTemplate.Resources.logo.png");

                // Jeśli obrazek załadowany, wyświetl go
                if (imageTexture != null)
                {
                    isImageVisible = true;
                    startTime = Time.time; // Zapisz czas, kiedy obrazek został wyświetlony
                    isTextureLoaded = true; // Zmieniamy flagę na true, żeby nie ładować obrazu ponownie
                    Debug.Log("ShowImage: Start initialized.");
                }
                else
                {
                    Debug.LogWarning("ShowImage: Failed to load texture.");
                }
            }
        }

        private void Update()
        {
            // Jeśli obrazek jest widoczny, sprawdź, czy minęły 3 sekundy
            if (isImageVisible && Time.time - startTime > displayTime)
            {
                isImageVisible = false; // Ukryj obrazek
                Debug.Log("ShowImage: Time is up, hiding the image.");
            }
        }

        private void OnGUI()
        {
            if (isImageVisible && imageTexture != null)
            {
                // Rysowanie obrazka na całym ekranie
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), imageTexture, ScaleMode.StretchToFill);
            }
        }

        private Texture2D LoadTextureFromResource(string resourcePath)
        {
            Texture2D texture = new Texture2D(2, 2);
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
                if (stream != null)
                {
                    byte[] fileData = new byte[stream.Length];
                    stream.Read(fileData, 0, (int)stream.Length);
                    texture.LoadImage(fileData);
                    Debug.Log("ShowImage: Texture loaded successfully.");
                }
                else
                {
                    Debug.LogError("ShowImage: Failed to find resource stream: " + resourcePath);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("ShowImage: Error loading texture: " + ex.Message);
            }
            return texture;
        }
    }
}
