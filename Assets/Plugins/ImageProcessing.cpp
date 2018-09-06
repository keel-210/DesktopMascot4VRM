#ifdef __cplusplus
#import <opencv2/opencv.hpp>
#endif

extern "C" {
void UpdateTexture(char *data, int width, int height);
}

void UpdateTexture(char *data, int width, int height)
{
	IplImage *p = cvCreateImageHeader(cvSize(width, height), IPL_DEPTH_8U, 4);
	cvSetData(p, data, p->widthStep);
	IplImage *g = cvCreateImage(cvSize(width, height), IPL_DEPTH_8U, 4);
	cvCvtColor(p, g);
	cvReleaseImageHeader(&p);
	cvReleaseImage(&g);
}