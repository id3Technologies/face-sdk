import com.face.library.FaceLibrary;
import com.face.library.FaceModel;
import com.face.library.ProcessingUnit;

public class Main {
    public static void main(String[] args) {

    }

    static void checkLicense() {
        // [license_check]
        try {
            FaceLibrary.checkLicense("id3Face.lic");
        } catch (FaceException ex) {
            System.out.println(ex.getMessage());
        }
        // [license_check]
    }

    static void activateLicense() {
        // [license_activation]
        String hardwareCode = License.getHostHardwareCode(LicenseHardwareCodeType.WindowsOs);
        
        try {
            License.activateSerialKey(hardwareCode, "XXXX-XXXX-XXXX-XXXX", "[Computer name]", "id3Face.lic");
        } catch (FaceException ex) {
            System.out.println(ex.getMessage());
        }
        // [license_activation]
    }
    
    static void FaceDetection() {
        // [face_detection]
        FaceLibrary.loadModel(modelPath, FaceModel.FACE_DETECTOR_4B, ProcessingUnit.CPU);

        // Create a new instance of the FaceDetector class.
        FaceDetector faceDetector = new FaceDetector();
        faceDetector.setConfidenceThreshold(50);
        faceDetector.setModel(FaceModel.FACE_DETECTOR_4B);
        faceDetector.setNmsIouThreshold(40);
        faceDetector.setThreadCount(4);

        // Load an image from a file.
        Image image = Image.fromFile("image1.jpg", PixelFormat.BGR_24_BITS);

        // Detect faces on the image.
        DetectedFaceList detectedFaceList = faceDetector.detectFaces(image);

        // Enumerate detected faces.
        for (DetectedFace face : detectedFaceList) {
            // ...
        }

        // Dispose of all resources allocated to the FaceDetector.
        faceDetector.dispose();
        // [face_detection]
    }

    static void FaceComparisonOnCard()
    {
        // [face_comparison_on_card]
        FaceDetector faceDetector = new FaceDetector();
        FaceEncoder faceEncoder = new FaceEncoder();

        // Load an image from a file.
        Image image = Image.fromFile("image1.jpg", PixelFormat.BGR_24_BITS);

        // Detect faces on the image.
        DetectedFaceList detectedFaceList = faceDetector.detectFaces(image);

        // Create the template from the largest detected face.
        FaceTemplate faceTemplate = faceEncoder.createTemplate(image, detectedFaceList.getLargestFace());

        // Export the face template object to a Biometric Data Template (BDT) buffer.
        byte[] bdt = faceTemplate.toBdt();

        // Send BDT buffer to card
        // ...

        // Dispose all resources
        detectedFaceList.dispose();
        faceEncoder.dispose();
        faceDetector.dispose();
        // [face_comparison_on_card]
    }

    static void FaceAnalysing()
    {
        // [face_analysing]
        FaceLibrary.loadModel(ModelPath, FaceModel.FACE_DETECTOR_3B, ProcessingUnit.CPU);
        FaceLibrary.loadModel(ModelPath, FaceModel.FACE_AGE_ESTIMATOR_1A, ProcessingUnit.CPU);
        FaceLibrary.LoadModel(ModelPath, FaceModel.FACE_LANDMARKS_ESTIMATOR_2A, ProcessingUnit.CPU);
        FaceLibrary.LoadModel(ModelPath, FaceModel.EYE_GAZE_ESTIMATOR_2A, ProcessingUnit.CPU);

        // Initialize the FaceAnalyser module
        FaceAnalyser analyser = new FaceAnalyser();
        analyser.setExpressionSensibility(60);
        analyser.setOverExposureSensibility(70);
        analyser.setUnderExposureSensibility(70);

        // Initialize the FaceDetector module
        FaceDetector faceDetector = new FaceDetector();

        // Load image from file
        Image image = Image.fromFile("image.jpg", PixelFormat.BGR_24_BITS);

        // Detect face
        DetectedFaceList detectedFaceList = faceDetector.detectFaces(image);
        DetectedFace detectedFace = detectedFaceList.getLargestFace();

        // Compute age
        int age = analyser.computeAge(image, detectedFace);

        // Compute landmarks
        PointList landmarks = analyser.computeLandmarks(image, detectedFace);

        // Compute eye-gaze
        EyeGaze eyeGaze = analyser.computeEyeGaze(image, detectedFace, landmarks);

        // Release all resources allocated to the FaceDetector module
        faceDetector.dispose();

        // Release all resources allocated to the FaceAnalyser module
        analyser.dispose();        
        // [face_analysing]
    }
}
