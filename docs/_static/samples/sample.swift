import FaceLibrary

func main() {

}

func checkLicense() {
    // [license_check]
    do {
        try FaceLibrary.checkLicense("id3Face.lic")
    } catch let error as FaceException {
        print(error.message)
    } catch {
        // Handle other exceptions
    }
    // [license_check]
}

func activateLicense() {
    // [license_activation]
    let hardwareCode = License.getHostHardwareCode(.windowsOs)

    do {
        try License.activateSerialKey(hardwareCode, "XXXX-XXXX-XXXX-XXXX", "[Computer name]", "id3Face.lic")
    } catch let error as FaceException {
        print(error.message)
    } catch {
        // Handle other exceptions
    }
    // [license_activation]
}

func faceDetection() {
    // [face_detection]
    FaceLibrary.loadModel(modelPath, model: .faceDetector4B, processingUnit: .cpu)

    // Create a new instance of the FaceDetector class.
    var faceDetector = FaceDetector()
    faceDetector.confidenceThreshold = 50
    faceDetector.model = .faceDetector4B
    faceDetector.nmsIouThreshold = 40
    faceDetector.threadCount = 4

    // Load an image from a file.
    let image = Image(fromFile: "image1.jpg", pixelFormat: .bgr24Bits)

    // Detect faces on the image.
    let detectedFaceList = faceDetector.detectFaces(image: image)

    // Enumerate detected faces.
    for face in detectedFaceList {
        // ...
    }

    // Dispose of all resources allocated to the FaceDetector.
    faceDetector.dispose()
    // [face_detection]
}

func faceComparisonOnCard() {
    // [face_comparison_on_card]
    
    // [face_comparison_on_card]
}