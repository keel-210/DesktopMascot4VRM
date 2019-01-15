/*******************************************************************************

INTEL CORPORATION PROPRIETARY INFORMATION
This software is supplied under the terms of a license agreement or nondisclosure
agreement with Intel Corporation and may not be copied or disclosed except in
accordance with the terms of that agreement
Copyright(c) 2014 Intel Corporation. All Rights Reserved.

*******************************************************************************/
using System;

#if RSSDK_IN_NAMESPACE
namespace intel.rssdk
{
#endif

    public static class PXCMTypeExtension
    {
        public static string ToString(this PXCMImage.PixelFormat format)
        {
            return PXCMImage.PixelFormatToString(format);
        }

        public static Int32 ToSize(this PXCMAudio.AudioFormat format)
        {
            return PXCMAudio.AudioFormatToSize(format);
        }

        public static String ToString(this PXCMAudio.AudioFormat format)
        {
            return PXCMAudio.AudioFormatToString(format);
        }

        public static String ToString(this PXCMCapture.StreamType stream)
        {
            return PXCMCapture.StreamTypeToString(stream);
        }

        public static Int32 ToIndex(this PXCMCapture.StreamType type)
        {
            return PXCMCapture.StreamTypeToIndex(type);
        }

        public static String ToString(this PXCM3DScan.FileFormat format)
        {
            return PXCM3DScan.FileFormatToString(format);
        }

        public static String ToString(this PXCMCapture.DeviceModel model)
        {
            return PXCMCapture.DeviceModelToString(model);
        }

        public static Boolean IsError(this pxcmStatus status)
        {
            return status < pxcmStatus.PXCM_STATUS_NO_ERROR;
        }

        public static Boolean IsWarning(this pxcmStatus status)
        {
            return status < pxcmStatus.PXCM_STATUS_NO_ERROR;
        }
        public static Boolean IsSuccessful(this pxcmStatus status)
        {
            return status >= pxcmStatus.PXCM_STATUS_NO_ERROR;
        }
    };

#if RSSDK_IN_NAMESPACE
}
#endif
