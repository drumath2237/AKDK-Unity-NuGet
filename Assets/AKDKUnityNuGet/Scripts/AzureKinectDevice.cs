using System;
using System.Threading.Tasks;
using Microsoft.Azure.Kinect.Sensor;
using UnityEngine;

namespace AKDKUnityNuGet
{
    public class AzureKinectDevice : MonoBehaviour
    {
        private Device _kinect;

        [SerializeField] private MeshRenderer meshRenderer;

        private Texture2D _colorTexture;

        private Memory<byte> _rawColorData;

        private bool _isRunning = false;
        private bool _needsUpdate = false;


        private void Start()
        {
            if (meshRenderer == null)
            {
                return;
            }

            try
            {
                _kinect = Device.Open();

                _kinect.StartCameras(new DeviceConfiguration
                {
                    ColorFormat = ImageFormat.ColorBGRA32,
                    ColorResolution = ColorResolution.R1080p,
                    DepthMode = DepthMode.NFOV_2x2Binned,
                    SynchronizedImagesOnly = true,
                    CameraFPS = FPS.FPS30
                });
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }

            _isRunning = true;

            var colorCalibration = _kinect.GetCalibration().ColorCameraCalibration;
            _colorTexture = new Texture2D(colorCalibration.ResolutionWidth, colorCalibration.ResolutionHeight,
                TextureFormat.BGRA32, false);

            meshRenderer.material.mainTexture = _colorTexture;

            _ = Capture();
        }

        private Task Capture()
        {
            return Task.Run(() =>
            {
                while (_isRunning)
                {
                    if (_needsUpdate)
                    {
                        continue;
                    }

                    using var capture = _kinect.GetCapture();

                    _rawColorData = capture.Color.Memory;

                    _needsUpdate = true;
                }
            });
        }

        private void Update()
        {
            if (_rawColorData.IsEmpty || !_needsUpdate)
            {
                return;
            }

            _colorTexture.LoadRawTextureData(_rawColorData.ToArray());
            _colorTexture.Apply();

            _needsUpdate = false;
        }

        private void OnApplicationQuit()
        {
            _isRunning = false;
            _kinect?.StopCameras();
            _kinect?.Dispose();
        }
    }
}