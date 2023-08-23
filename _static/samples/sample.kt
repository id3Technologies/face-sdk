import com.face.library.FaceLibrary
import com.face.library.FaceModel
import com.face.library.ProcessingUnit

fun main() {

    // [face_detection]
    FaceLibrary.loadModel(modelPath, FaceModel.FACE_DETECTOR_4B, ProcessingUnit.CPU)

    // Create a new instance of the FaceDetector class.
    val faceDetector = FaceDetector().apply {
        confidenceThreshold = 50
        model = FaceModel.FACE_DETECTOR_4B
        nmsIouThreshold = 40
        threadCount = 4
    }

    // Load an image from a file.
    val image = Image.fromFile("image1.jpg", PixelFormat.BGR_24_BITS)

    // Detect faces on the image.
    val detectedFaceList = faceDetector.detectFaces(image)

    // Enumerate detected faces.
    for (face in detectedFaceList) {
        // ...
    }

    // Dispose of all resources allocated to the FaceDetector.
    faceDetector.dispose()
    // [face_detection]

    // [face_comparison_on_card]
    val faceDetector = FaceDetector()
    val faceEncoder = FaceEncoder()

    // Load an image from a file.
    val image = Image.fromFile("image1.jpg", PixelFormat.BGR_24_BITS)

    // Detect faces on the image.
    val detectedFaceList = faceDetector.detectFaces(image)

    // Create the template from the largest detected face.
    val faceTemplate = faceEncoder.createTemplate(image, detectedFaceList.largestFace)

    // Export the face template object to a Biometric Data Template (BDT) buffer.
    val bdt = faceTemplate.toBdt()

    // Send BDT buffer to card
    // ...

    // Dispose all resources
    detectedFaceList.dispose()
    faceEncoder.dispose()
    faceDetector.dispose()
    // [face_comparison_on_card]
}

