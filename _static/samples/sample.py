import face_library

def main():

    # [face_detection]
    face_library.load_model(model_path, face_model=face_library.FaceModel.FACE_DETECTOR_4B, processing_unit=face_library.ProcessingUnit.CPU)

    # Create a new instance of the FaceDetector class.
    face_detector = face_library.FaceDetector()
    face_detector.confidence_threshold = 50
    face_detector.model = face_library.FaceModel.FACE_DETECTOR_4B
    face_detector.nms_iou_threshold = 40
    face_detector.thread_count = 4

    # Load an image from a file.
    image = face_library.Image.from_file("image1.jpg", pixel_format=face_library.PixelFormat.BGR_24_BITS)

    # Detect faces on the image.
    detected_face_list = face_detector.detect_faces(image)

    # Enumerate detected faces.
    for face in detected_face_list:
        # ...

    # Dispose of all resources allocated to the FaceDetector.
    face_detector.dispose()
    # [face_detection]

    # [face_comparison_on_card]
    face_detector = FaceDetector()
    face_encoder = FaceEncoder()

    # Load an image from a file.
    image = Image.from_file("image1.jpg", PixelFormat.BGR_24_BITS)

    # Detect faces on the image.
    detected_face_list = face_detector.detect_faces(image)

    # Create the template from the largest detected face.
    face_template = face_encoder.create_template(image, detected_face_list.largest_face)

    # Export the face template object to a Biometric Data Template (BDT) buffer.
    bdt = face_template.to_bdt()

    # Send BDT buffer to card
    # ...

    # Dispose all resources
    detected_face_list.dispose()
    face_encoder.dispose()
    face_detector.dispose()
    # [face_comparison_on_card]

if __name__ == "__main__":
    main()

