using System;

namespace id3.Face.Samples
{
    using id3.Face;

    class Program
    {
        static void Main(string[] args)
        {
            string licensePath = @"id3FaceToolkit.lic";
            string modelPath = "../../../../../sdk/models";

            // [check_license]
            try
            {
                FaceLibrary.CheckLicense(licensePath);
            }
            catch (FaceException ex)
            {
                Console.WriteLine("Error during license check" + ex.Message);
                Environment.Exit(-1);
            }
            // [check_license]

            // [load_models]
            FaceLibrary.LoadModel(modelPath, FaceModel.FaceDetector3B, ProcessingUnit.Cpu);
            // [load_models]

            string imagePath1 = "../../../../../data/image1.jpg";
            string imagePath2 = "../../../../../data/image2.jpg";

            // [load_image]
            Image image1 = Image.FromFile(imagePath1, PixelFormat.Bgr24Bits);
            // [load_image]

            Image image2 = Image.FromFile(imagePath2, PixelFormat.Bgr24Bits);

            // [init_face_detector]
            FaceDetector faceDetector = new FaceDetector()
            {
                ConfidenceThreshold = 50,
                Model = FaceModel.FaceDetector3B,
                ThreadCount = 4
            };
            // [init_face_detector]

            DetectedFaceList detectedFaceList1 = faceDetector.DetectFaces(image1);
            DetectedFaceList detectedFaceList2 = faceDetector.DetectFaces(image2);

            // [init_face_encoder]
            FaceEncoder faceEncoder = new FaceEncoder()
            {
                Model = FaceModel.FaceEncoder9A,
                ThreadCount = 4
            };
            // [init_face_encoder]

            // [face_encoder_create_template]
            FaceTemplate faceTemplate1 = faceEncoder.CreateTemplate(image1, detectedFaceList1.GetLargestFace());
            // [face_encoder_create_template]

            FaceTemplate faceTemplate2 = faceEncoder.CreateTemplate(image2, detectedFaceList2.GetLargestFace());

            // [init_face_matcher]
            FaceMatcher faceMatcher = new FaceMatcher();
            // [init_face_matcher]

            int score = faceMatcher.CompareTemplates(faceTemplate2, faceTemplate1);

            if (score > (int)FaceMatcherThreshold.Fmr10000)
            {
                Console.WriteLine("Match: " + score);
            }
            else
            {
                Console.WriteLine("No match: " + score);
            }

            /**
	         * Face templates can be exported directly into a file or a buffer.
	         * When using the SDK face matcher the id3FaceTemplateBufferType_Normal must be used.
	        */
            Console.Write("Export template 1 as file...");
            faceTemplate1.ToFile(FaceTemplateBufferType.Normal, "../../../../../data/template1.bin");
            Console.Write("Export template 2 as buffer...");
            byte[] template2Buffer = faceTemplate2.ToBuffer(FaceTemplateBufferType.Normal);

            Console.WriteLine("Sample terminated successfully.");
            Console.ReadKey();
        }

        void CheckLicense()
        {
            // [license_check]
            try
            {
                FaceLibrary.CheckLicense("id3Face.lic");
            }
            catch (FaceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            // [license_check]
        }
        void ActivateLicense()
        {
            // [license_activation]
            string hardwareCode = License.GetHostHardwareCode(LicenseHardwareCodeType.WindowsOs);
            
            try
            {
                License.ActivateSerialKey(hardwareCode, "XXXX-XXXX-XXXX-XXXX", "[Computer name]", "id3Face.lic");
            }
            catch (FaceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            // [license_activation]
        }

        void FaceDetection()
        {
            // [face_detection]
            // Load the face detection AI model.
            FaceLibrary.LoadModel(modelPath, FaceModel.FaceDetector4B, ProcessingUnit.Cpu);

            // Create a new instance of the FaceDetector class.
            var faceDetector = new FaceDetector()
            {
                ConfidenceThreshold = 50,
                Model = FaceModel.FaceDetector4B,
                NmsIouThreshold = 40,
                ThreadCount = 4
            };

            // Load an image from a file.
            Image image = Image.FromFile("image1.jpg", PixelFormat.Bgr24Bits);

            // Detect faces on the image.
            DetectedFaceList detectedFaceList = faceDetector.DetectFaces(image);

            // Enumerate detected faces.
            foreach (DetectedFace face in detectedFaceList)
            {
                // ...                
            }

            // Disposes all resources allocated to the FaceDetector.
            faceDetector.Dispose();
            // [face_detection]
        }

        void FaceTracker()
        {
            // [face_tracking]
            // Load the face detection AI model.
            FaceLibrary.LoadModel(modelPath, FaceModel.FaceDetector4B, ProcessingUnit.Cpu);

            // Create a new instance of the FaceTracker class.
            var faceTracker = new FaceTracker()
            {
                ConfidenceThreshold = 50,
                Model = FaceModel.FaceDetector4B,
                NmsIouThreshold = 40,
                ThreadCount = 4
            };

            // Load an image from a file.
            Image image = Image.FromFile("image1.jpg", PixelFormat.Bgr24Bits);

            // Detect faces on the image.
            TrackedFaceList trackedFaceList = faceTracker.TrackFaces(image);

            // Enumerate tracked faces.
            foreach (TrackedFace face in trackedFaceList)
            {
                // ...                
            }

            // Dispose all resources allocated to the FaceTracker.
            faceTracker.Dispose();
            // [face_tracking]
        }

        void FaceComparisonOnCard()
        {
            // [face_comparison_on_card]
            FaceDetector faceDetector = new FaceDetector();
            FaceEncoder faceEncoder = new FaceEncoder();

            // Load an image from a file.
            Image image = Image.FromFile("image1.jpg", PixelFormat.Bgr24Bits);
           
            // Detect faces on the image.
            DetectedFaceList detectedFaceList = faceDetector.DetectFaces(image);

            // Create the template from the largest detected faces.
            FaceTemplate faceTemplate = faceEncoder.CreateTemplate(image, detectedFaceList.GetLargestFace());

            // Export the face template object to a Biometric Data Template (BDT) buffer.
            byte[] bdt = faceTemplate.ToBdt();

            // Send BDT buffer to card
            // ...

            // Dispose all resources
            detectedFaceList.Dispose();
            faceEncoder.Dispose();
            faceDetector.Dispose();
            // [face_comparison_on_card]
        }

        void FaceAnalysing()
        {
            // [face_analysing]
            FaceLibrary.LoadModel(ModelPath, FaceModel.FaceDetector3B, ProcessingUnit.Cpu);
            FaceLibrary.LoadModel(ModelPath, FaceModel.FaceAgeEstimator1A, ProcessingUnit.Cpu);
            FaceLibrary.LoadModel(ModelPath, FaceModel.FaceLandmarksEstimator2A, ProcessingUnit.Cpu);
            FaceLibrary.LoadModel(ModelPath, FaceModel.EyeGazeEstimator2A, ProcessingUnit.Cpu);

            // Initialize the FaceAnalyser module
            FaceAnalyser analyser = new FaceAnalyser()
            {
                ExpressionSensibility = 60,
                OverExposureSensibility = 70,
                UnderExposureSensibility = 70
            };

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

            // compute landmarks
            PointList landmarks = faceAnalyser.ComputeLandmarks(image, detectedFace);

            // compute eye-gaze
            EyeGaze eyeGaze = faceAnalyser.ComputeEyeGaze(image, detectedFace, landmarks);

            // Release all resources allocated to the FaceDetector module.
            faceDetector.Dispose();
 
            // Release all resources allocated to the FaceAnalyser module.
            analyser.Dispose();
            // [face_analysing]
        }
    }
}

