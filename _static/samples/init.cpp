#include "id3Face.h"

void checkLicense()
{
    // [check_license]
    int err;
    std::string license_path = "C:\\ProgramData\\id3\\id3FaceSDKv9.lic";

    err = id3FaceLibrary_CheckLicense(license_path.c_str(), nullptr);

    if (err == id3FaceError_Success)
    {
    }
    // [check_license]
}

int main(const int argc, const char **argv)
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

    return 0;
}