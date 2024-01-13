#include "pch.h"
#include <GdiPlus.h>
#include "DetourFunctions.h"
#include "LibStHook.h"
#include <Windows.h>
#include <Shlobj.h>
#include <Gdiplus.h>
#include <iostream>
#include <string>
#include <cstdlib>
#include <ctime>
#include <random>

#pragma comment(lib, "gdiplus.lib")

using namespace Gdiplus;
using namespace std;
HBITMAP g_GlobalHBitmap = nullptr;

fnTDMasterInitHook TDMasterInitHook = NULL;
fnTDMasterInitHook fpTDMasterInitHook = NULL;

fnGetDC fpGetDC = NULL;
fnCreateDCW fpCreateDCW = NULL;
fnSetWindowPos fpSetWindowPos = NULL;
fnGetDIBits fpGetDIBits = NULL;
fnCreateWindowExW fpCreateWindowExW = NULL;
HDC WINAPI DetourGetDC(HWND hWnd)
{
	printf("[Info] allowed GetDC: hwnd:%d\n", (int)hWnd);
	return fpGetDC(hWnd);
}

HDC WINAPI DetourCreateDCW(LPCWSTR pwszDriver, LPCWSTR pwszDevice, LPWSTR pszPort, const DEVMODEW* pdm)
{
	return fpCreateDCW(pwszDriver, pwszDevice, pszPort, pdm);
}

BOOL WINAPI DetourSetWindowPos(HWND hWnd, HWND hWndInsertAfter, int X, int Y, int cx, int cy, UINT uFlags)
{
	if (g_noTopMostWindow && hWndInsertAfter == HWND_TOPMOST) {
		if ((GetWindowLong(hWnd, GWL_STYLE) & WS_BORDER) == 0) {
			SetWindowLong(hWnd, GWL_STYLE, WS_OVERLAPPEDWINDOW);
			printf("[Info] reset Window style.\n");
			fpSetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, SWP_FRAMECHANGED | SWP_NOSIZE | SWP_NOMOVE | SWP_NOZORDER);
		}
		printf("[Info] disallowed set zorder.\n");
		return FALSE;
	}
	printf("[Info] allowed SetWindowPos.\n");
	return fpSetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, uFlags);
}

int WINAPI DetourGetDIBits(HDC hdc, HBITMAP hbm, UINT start, UINT cLines, LPVOID lpvBits, LPBITMAPINFO lpbmi, UINT usage)
{
	if (g_useFakeImage && lpvBits != NULL && cLines > 32) {
		// cLines > 32: prevent cursor redraw
		HBITMAP hbm = NULL;
		int ret = NULL;
		if (wcslen(g_fakeImagePath)) {
			Gdiplus::Bitmap bmp(g_fakeImagePath);
			Gdiplus::Color bgColor;
			bmp.GetHBITMAP(bgColor, &hbm);
			ret = fpGetDIBits(hdc, hbm, start, cLines, lpvBits, lpbmi, usage);
			SaveHBITMAPToFile(hbm);
			DeleteObject(hbm);
			printf("[Info] fake screen shot: hbm:0x%x GetDIBits:%d cLines:%d\n", hbm, ret, cLines);
		}
		printf("[Info] disallowed GetDIBits.\n");
		return ret;
	}
	printf("[Info] allowed GetDIBits.\n");
	return fpGetDIBits(hdc, hbm, start, cLines, lpvBits, lpbmi, usage);
}


HWND WINAPI DetourCreateWindowExW(
	DWORD     dwExStyle,
	LPCWSTR   lpClassName,
	LPCWSTR   lpWindowName,
	DWORD     dwStyle,
	int       X,
	int       Y,
	int       nWidth,
	int       nHeight,
	HWND      hWndParent,
	HMENU     hMenu,
	HINSTANCE hInstance,
	LPVOID    lpParam
)
{
	if (g_noBlackScreen) {
		if (lpWindowName != NULL && wcscmp(lpWindowName, L"BlackScreen Window") == 0) {
			printf("[Info] disallowed CreateWindowExW for BlackScreen Window.\n");
			return 0;
		}
	}
	printf("[Info] allowed CreateWindowExW.\n");
	return fpCreateWindowExW(
		dwExStyle,
		lpClassName,
		lpWindowName,
		dwStyle,
		X,
		Y,
		nWidth,
		nHeight,
		hWndParent,
		hMenu,
		hInstance,
		lpParam
	);
}

int DetourTDMasterInitHook(HWND hWnd, UINT msg, int a3)
{
	if (g_unhookKeyboard) {
		printf("[Info] disallowed InitHook.\n");
		return FALSE;
	}
	printf("[Info] allowed InitHook.\n");
	return fpTDMasterInitHook(hWnd, msg, a3);
}

void SaveHBITMAPToFile(HBITMAP hBitmap) {
	// 初始化GDI+
	GdiplusStartupInput gdiplusStartupInput;
	ULONG_PTR gdiplusToken;
	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	// 创建GDI+位图对象
	Bitmap bitmap(hBitmap, NULL);

	// 获取桌面路径
	wchar_t desktopPath[MAX_PATH];
	if (SUCCEEDED(SHGetFolderPathW(NULL, CSIDL_DESKTOP, NULL, 0, desktopPath))) {
		// 创建存放图片的文件夹
		wstring folderPath = wstring(desktopPath) + L"\\images\\";
		CreateDirectoryW(folderPath.c_str(), NULL);

		// 获取当前时间作为文件名
		SYSTEMTIME systemTime;
		GetLocalTime(&systemTime);
		wstring fileName = folderPath + to_wstring(systemTime.wYear) +
			to_wstring(systemTime.wMonth) + to_wstring(systemTime.wDay) + L"_" +
			to_wstring(systemTime.wHour) + to_wstring(systemTime.wMinute) + to_wstring(systemTime.wSecond) +
			L".jpg";
		Gdiplus::Bitmap bitmap(hBitmap, NULL);

		const int blockWidth = 100;  // 每块的宽度
		const int blockHeight = 100; // 每块的高度

		int imageWidth = bitmap.GetWidth();
		int imageHeight = bitmap.GetHeight();

		// 计算需要多少块
		int numBlocksX = (imageWidth + blockWidth - 1) / blockWidth;
		int numBlocksY = (imageHeight + blockHeight - 1) / blockHeight;

		// 逐块保存
		for (int blockY = 0; blockY < numBlocksY; ++blockY) {
			for (int blockX = 0; blockX < numBlocksX; ++blockX) {
				// 计算当前块的位置和大小
				int startX = blockX * blockWidth;
				int startY = blockY * blockHeight;
				int blockWidthActual = std::min<int>(blockWidth, imageWidth - startX);

				int blockHeightActual = std::min<int>(blockHeight, imageHeight - startY);

				// 创建临时位图，只包含当前块的数据
				Gdiplus::Bitmap blockBitmap(blockWidthActual, blockHeightActual);
				Gdiplus::Graphics g(&blockBitmap);
				g.DrawImage(&bitmap, 0, 0, startX, startY, blockWidthActual, blockHeightActual, Gdiplus::UnitPixel);

				// 将临时位图保存到文件
				wchar_t blockFilePath[MAX_PATH];
				swprintf_s(blockFilePath, L"%s_block_%d_%d.jpg", fileName, blockX, blockY);

				blockBitmap.Save(blockFilePath, &Gdiplus::ImageFormatJPEG, NULL);
			}
		}

		wcout << L"File saved: " << fileName << endl;
	}
	else {
		wcerr << L"Failed to get desktop path." << endl;
	}

	// 关闭GDI+
	GdiplusShutdown(gdiplusToken);
}
// 获取指定格式的图像编码器的 CLSID
int GetEncoderClsid(const WCHAR* format, CLSID* pClsid) {
	UINT num = 0;          // 编码器的数量
	UINT size = 0;         // 编码器信息结构体的大小

	// 获取编码器的数量和大小
	GetImageEncodersSize(&num, &size);

	if (size == 0) {
		return -1;  // 获取失败
	}

	// 分配存储编码器信息的内存
	ImageCodecInfo* pImageCodecInfo = (ImageCodecInfo*)(malloc(size));
	if (pImageCodecInfo == NULL) {
		return -1;  // 内存分配失败
	}

	// 获取编码器信息
	GetImageEncoders(num, size, pImageCodecInfo);

	// 查找指定格式的编码器
	for (UINT i = 0; i < num; ++i) {
		if (wcscmp(pImageCodecInfo[i].MimeType, format) == 0) {
			*pClsid = pImageCodecInfo[i].Clsid;
			free(pImageCodecInfo);
			return 0;   // 成功找到
		}
	}

	// 没有找到指定格式的编码器
	free(pImageCodecInfo);
	return -1;
}
