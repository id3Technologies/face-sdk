using System;

namespace FaceSDK
{
    using id3.Face;

    class Program
    {
        const string ModelPath = "./models";

        static void Main(string[] args)
        {
            FaceLibrary.LoadModel(ModelPath, FaceModel.FaceDetector3B, ProcessingUnit.Cpu);
            FaceLibrary.LoadModel(ModelPath, FaceModel.FaceAgeEstimator1A, ProcessingUnit.Cpu);

            // Initialize the FaceAnalyser module
            FaceAnalyser analyser = new FaceAnalyser();

            // Initialize the FaceDetector module
            FaceDetector faceDetector = new FaceDetector();

            // load image from file
            Image image = Image.FromFile("image.jpg", PixelFormat.Bgr24Bits);

            // detect face
            DetectedFaceList detectedFaceList = faceDetector.DetectFaces(image);
            DetectedFace detectedFace = detectedFaceList.GetLargestFace();

            // compute age
            int age = faceAnalyser.ComputeAge(image, detectedFace);

            // Release all resources allocated to the FaceDetector module.
            faceDetector.Dispose();
 
             // Release all resources allocated to the FaceAnalyser module.
            analyser.Dispose();
       }
    }
}