import 'package:face_library/face_library.dart';

void main() {
  // [face_detection]
  FaceLibrary.loadModel(modelPath, FaceModel.faceDetector4B, ProcessingUnit.cpu);

  // Create a new instance of the FaceDetector class.
  var faceDetector = FaceDetector()
    ..confidenceThreshold = 50
    ..model = FaceModel.faceDetector4B
    ..nmsIouThreshold = 40
    ..threadCount = 4;

  // Load an image from a file.
  Image image = Image.fromFile("image1.jpg", PixelFormat.bgr24Bits);

  // Detect faces on the image.
  DetectedFaceList detectedFaceList = faceDetector.detectFaces(image);

  // Enumerate detected faces.
  for (DetectedFace face in detectedFaceList) {
    // ...
  }

  // Disposes all resources allocated to the FaceDetector.
  faceDetector.dispose();
  // [face_detection]
}

void checkLicense() {
  // [license_check]
  try {
      FaceLibrary.checkLicense("id3Face.lic");
  }
  catch (FaceException ex)
  {
      print(ex.message);
  }
  // [license_check]
}

void activateLicense() {
  // [license_activation]
  String hardwareCode = License.getHostHardwareCode(LicenseHardwareCodeType.windowsOs);

  try {
    License.activateSerialKey(hardwareCode, "XXXX-XXXX-XXXX-XXXX", "[Computer name]", "id3Face.lic");
  } catch (FaceException ex) {
    print(ex.message);
  }
  // [license_activation]
}

void FaceComparisonOnCard() {
  // [face_comparison_on_card]
  var faceDetector = FaceDetector();
  var faceEncoder = FaceEncoder();

  // Load an image from a file.
  var image = Image.fromFile("image1.jpg", PixelFormat.bgr24Bits);

  // Detect faces on the image.
  var detectedFaceList = faceDetector.detectFaces(image);

  // Create the template from the largest detected faces.
  var faceTemplate = faceEncoder.createTemplate(image, detectedFaceList.getLargestFace());

  // Export the face template object to a Biometric Data Template (BDT) buffer.
  var bdt = faceTemplate.toBdt();

  // Send BDT buffer to card
  // ...

  // Dispose all resources
  detectedFaceList.dispose();
  faceEncoder.dispose();
  faceDetector.dispose();
  // [face_comparison_on_card]
}
