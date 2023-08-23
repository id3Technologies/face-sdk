import id3Face as face_library

def main():
    check_license()

def check_license():
    # [license_check]
    try:
        face_library.check_license("id3Face.lic")
    except face_library.FaceException as ex:
        print(ex.message)
    # [license_check]

def activate_license():
    # [license_activation]
    hardware_code = face_library.License.get_host_hardware_code(face_library.LicenseHardwareCodeType.windowsOs)

    try:
        face_library.License.activate_serial_key(hardware_code, "XXXX-XXXX-XXXX-XXXX", "[Computer name]", "id3Face.lic")
    except face_library.FaceException as ex:
        print(ex.message)
    # [license_activation]

def face_detection():
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
    del face_detector
    # [face_detection]

def face_comparison_on_card():
    # [face_comparison_on_card]
    face_detector = face_library.FaceDetector()
    face_encoder = face_library.FaceEncoder()

    # Load an image from a file.
    image = face_library.Image.from_file("image1.jpg", face_library.PixelFormat.BGR_24_BITS)

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

