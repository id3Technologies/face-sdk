#include "id3Face.h"

void activateLicense()
{
    // [license_activation]
    int err;
    char hwCode[256];
    int hwCodeSize = 256;

    err = id3FaceLicense_GetHostHardwareCode(id3FaceLicenseHardwareCodeType_WindowsOs, hwCode, &hwCodeSize, NULL);

    if (err == id3FaceError_Success)
    {
        int licenseBufferSize = 1024*1024;
        unsigned char *licenseBuffer = (unsigned char*)malloc(size);

        err = id3FaceLicense_ActivateSerialKeyBuffer(hwCode, "XXXX-XXXX-XXXX-XXXX", "[Computer name]", licenseBuffer, &licenseBufferSize);
    }
    // [license_activation]
}

void checkLicense()
{
    // [license_check]
    int err;

    err = id3FaceLibrary_CheckLicense("id3Face.lic", NULL);

    if (err == id3FaceError_Success)
    {
    }
    // [license_check]
}

void checkLicenseBuffer()
{
    // [license_check_buffer]
    int err;

    err = id3FaceLibrary_CheckLicense(licenseBuffer, licenseBufferSize, NULL);

    if (err == id3FaceError_Success)
    {
    }
    // [license_check_buffer]
}

int FaceDetection()
{
    // [face_detection]
    ID3_FACE_IMAGE image = NULL;
    ID3_FACE_DETECTOR detector = NULL;
    ID3_DETECTED_FACE_LIST detected_face_list = NULL;
    ID3_DETECTED_FACE detected_face = NULL;
    int face_count = 0;
    int err;

    // Load the face detection AI model.
    err = id3FaceLibrary_LoadModel(models_dir.c_str(), id3FaceModel_FaceDetector3B, id3FaceProcessingUnit_Cpu);

    // Create a new instance of the FaceDetector class.
    if (err == id3FaceError_Success)
    {
        err = id3FaceDetector_Initialize(&detector);

        if (err == id3FaceError_Success)
            err = id3FaceDetector_SetConfidenceThreshold(detector, 50);
            
        if (err == id3FaceError_Success)
            err = id3FaceDetector_SetModel(detector, id3FaceModel_FaceDetector3B);

        if (err == id3FaceError_Success)
            err = id3FaceDetector_SetNmsIouThreshold(detector, 40);

        if (err == id3FaceError_Success)
            err = id3FaceDetector_SetThreadCount(detector, 4);
    }

    // load an image from a file
    if (err == id3FaceError_Success)
    {
        err = id3FaceImage_Initialize(&image);
    
        if (err == id3FaceError_Success)
            err = id3FaceImage_FromFile(image, "image1.jpg", id3FacePixelFormat_Bgr24Bits);
    }

    // Detect faces on the image.
    if (err == id3FaceError_Success)
    {
        err = id3DetectedFaceList_Initialize(&detected_face_list);

        if (err == id3FaceError_Success)
            err = id3DetectedFace_Initialize(&detected_face);

        if (err == id3FaceError_Success)
            err = id3FaceDetector_DetectFaces(detector, image, detected_face_list);
    }   

    // Enumerate detected faces.
    if (err == id3FaceError_Success)
    {
        err = id3DetectedFaceList_GetCount(detected_face_list, &face_count);

        if (err == id3FaceError_Success)
        {
            for (int i = 0; i < face_count; i++)
            {
                err = id3DetectedFaceList_Get(detected_face_list, i, detected_face); 

                if (err == id3FaceError_Success)
                {
                    id3FaceRectangle bounds;
                    err = id3DetectedFace_GetBounds(detected_face, &bounds);
                }
            }
        }
    }

    // Disposes all resources
    if (detected_face != NULL)    
        id3DetectedFace_Dispose(&detected_face);

    if (detected_face_list != NULL)
        id3FaceDetectedFaceList_Dispose(&detected_face_list);

    if (detector != NULL)
        id3FaceDetector_Dispose(&detector);
    //[face_detection]

    return err;
}

void FaceComparisonOnCard()
{
    //[face_comparison_on_card]
    
    //[face_comparison_on_card]
}

void FaceAnalysis()
{
    ID3FACE_ANALYSER hAnalyser;
    int err;

    // Initialize the FaceAnalyser module
    err = id3FaceAnalyser_Initialize(&hAnalyser);

    if (err == id3FaceError_Success)
    {
        id3FaceAnalyser_SetExpressionSensibility(hAnalyser, 60);
        id3FaceAnalyser_SetOverExposureSensibility(hAnalyser, 70);
        id3FaceAnalyser_SetUnderExposureSensibility(hAnalyser, 70);

        // ...

        // Release all resources allocated to the FaceAnalyser module.
        err = id3FaceAnalyser_Dispose(hAnalyser);
    }
}

int main(const int argc, const char **argv)
{

    return 0;
}